require ("global")
require ("modifiers")
require ("quests/man/man0u1")

TEXT_BOUND_BY_DUTY     = 50011;
TEXT_PROTECT_FROM_HARM = 51005;

POOL_FLHAMINN   = 8290008;
POOL_CHINCHILLA = 8204010;
ESCORT_LEVEL    = 5;

ESCORT_TIME_LIMIT   = 1800;
ESCORT_HOLD_RADIUS  = 28;
ARRIVAL_RADIUS      = 20;
NEAR_GOAL_RADIUS    = 60;
-- Seconds at the camp before stragglers that can't reach it stop holding the arrival.
ARRIVAL_GRACE_TICKS = 10;
CAMP_X, CAMP_Z      = 34.8, -480.5;
FLHAMINN_DISPLAY_ID = 1900054;

FLHAMINN_START = { -31.0, 183.0, -66.0, 2.0 };
CHINCHILLAS = {
	{ -76.90, 181.36, -236.10 }, { -75.79, 181.29, -242.36 },
	{ -14.78, 183.96, -319.97 }, { -19.53, 184.61, -322.32 }, { -16.32, 184.29, -325.45 },
	{ -57.08, 184.50, -447.63 }, { -56.54, 184.14, -442.77 }, { -52.41, 184.50, -444.50 },
};

local function dist2d(ax, az, bx, bz)
	return math.sqrt((ax - bx) ^ 2 + (az - bz) ^ 2);
end

local function say(player, quest, textId)
	player:SendGameMessage(quest, textId, 0x20);
end

function init()
	return "/Director/Quest/QuestDirectorMan0u102";
end

function onEventStarted(player, director, triggerName)
	player:EndEvent();
	local quest = player:GetQuest("Man0u1");
	if (quest == nil) then
		return;
	end

	local area = waitForContentArea(player, "SimpleContentMan0u102");
	if (area == nil) then
		return;
	end

	local duty = director:CreateDuty(area);
	duty:AddPlayer(player);
	duty:SetWinOnClear(false);
	duty:SetTimeLimit(ESCORT_TIME_LIMIT);
	local s = FLHAMINN_START;
	local flhaminn = duty:SpawnEscort(POOL_FLHAMINN, ESCORT_LEVEL, s[1], s[2], s[3], s[4]);
	if (flhaminn ~= nil) then
		duty:SetEscortFollow(flhaminn, player, 4);
	end
	for _, c in ipairs(CHINCHILLAS) do
		duty:SpawnEnemy(POOL_CHINCHILLA, math.random(5, 7), c[1], c[2], c[3], 0);
	end
	player:SetMod(modifiersGlobal.MinimumHpLock, 1);

	player:SendGameMessage(GetWorldMaster(), TEXT_PROTECT_FROM_HARM, 0x20, FLHAMINN_DISPLAY_ID);
	player:SendGameMessage(GetWorldMaster(), TEXT_BOUND_BY_DUTY, 0x20);
	player:SendGameMessage(GetWorldMaster(), 25018, 0x20, ESCORT_TIME_LIMIT / 60);
	say(player, quest, TEXT_ESCORT_DANGERS_AHEAD);
	duty:Start();

	local engagedSeen = {};
	local lastNear = 0;
	local saidSoClose = false;
	local holdTicks = 0;
	local tick = 0;
	while (duty:IsRunning() and player.CurrentArea == area) do
		wait(1);
		tick = tick + 1;
		if (flhaminn == nil or flhaminn:IsDead()) then
			break;
		end

		local nearLive, anyEngaged = 0, false;
		local enemies = duty:GetEnemies();
		for i = 0, enemies.Count - 1 do
			local mob = enemies[i];
			local d = math.min(dist2d(mob.positionX, mob.positionZ, player.positionX, player.positionZ),
				dist2d(mob.positionX, mob.positionZ, flhaminn.positionX, flhaminn.positionZ));
			if (d <= ESCORT_HOLD_RADIUS) then
				nearLive = nearLive + 1;
			end
			if (mob:IsEngaged()) then
				anyEngaged = true;
				if (not engagedSeen[mob.Id]) then
					engagedSeen[mob.Id] = true;
					say(player, quest, TEXT_ESCORT_NOT_TOO_STRONG);
				end
			end
		end
		if (nearLive == 0 and lastNear > 0) then
			say(player, quest, (tick % 2 == 0) and TEXT_ESCORT_WELL_DONE or TEXT_ESCORT_ARE_YOU_HURT);
			say(player, quest, TEXT_ESCORT_AMAZING);
		end
		lastNear = nearLive;

		local toCamp = dist2d(player.positionX, player.positionZ, CAMP_X, CAMP_Z);
		if (not saidSoClose and toCamp <= NEAR_GOAL_RADIUS) then
			saidSoClose = true;
			say(player, quest, TEXT_ESCORT_SO_CLOSE);
		end
		holdTicks = (toCamp <= ARRIVAL_RADIUS) and (holdTicks + 1) or 0;
		if (toCamp <= ARRIVAL_RADIUS and ((nearLive == 0 and not anyEngaged) or holdTicks >= ARRIVAL_GRACE_TICKS)) then
			say(player, quest, TEXT_ESCORT_JUST_AHEAD);
			duty:Succeed();
		end
	end

	if (player.CurrentArea ~= area) then
		player:SetMod(modifiersGlobal.MinimumHpLock, 0);
		duty:End();
		return;
	end

	quest = player:GetQuest("Man0u1");
	if (quest == nil or quest:GetSequence() ~= SEQ_065) then
		duty:End();
		return;
	end

	if (duty:IsSuccess()) then
		wait(2);
		kickEventContinue(player, director, "noticeEvent", "noticeEvent");
		finishEscort(player, quest, duty, area);
	else
		if (duty:IsRunning()) then
			duty:Fail("guarded");
		end
		wait(3);
		failEscort(player, quest, duty, area);
	end
end

function main()
end

function waitForContentArea(player, contentName)
	for i = 1, 20 do
		local area = player.CurrentArea;
		if (area ~= nil and area:GetPrivateAreaName() == contentName) then
			return area;
		end
		wait(0.5);
	end
	return nil;
end
