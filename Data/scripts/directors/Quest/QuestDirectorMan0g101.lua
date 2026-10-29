require ("global")
require ("modifiers")
require ("quests/man/man0g1")

TEXT_PROTECT_FROM_HARM = 51005;

POOL_POWLE       = 9290009;
POOL_SANSA       = 9290010;
POOL_ANKLE_BITER = 9205603;
ESCORT_LEVEL     = 5;

ESCORT_TIME_LIMIT     = 1800;
ESCORT_HOLD_RADIUS    = 28;
ARRIVAL_RADIUS        = 20;
NEAR_GOAL_RADIUS      = 40;
MARCH_LINE_SECONDS    = 20;
HURT_LINE_SECONDS     = 5;
STUMP_X, STUMP_Z      = -756.77, -1092.33;

POWLE_DISPLAY_ID       = 1000029;
ANKLE_BITER_DISPLAY_ID = 3205603;
TEXT_MOB_ENGAGED       = 30120;
TEXT_MOB_DEFEATED      = 30121;

TEXT_DUTY_START     = 362;
TEXT_AMBUSH         = 363;
TEXT_WAVE_CLEAR     = 364;
TEXT_MARCH_RESUME   = 365;
TEXT_TRIP_FUN       = 366;
TEXT_WAVE_THANKS    = 367;
TEXT_KID_HURT_YELP  = 370;
TEXT_KID_HURT_GRIPE = 371;
TEXT_NEAR_GOAL      = 373;
TEXT_ARRIVAL_THANKS = 374;
TEXT_STUMP_IN_SIGHT = 177;

POWLE_START = { -197.7, 3.54, -1023.3, -1.642 };
SANSA_START = { -191.7, 3.54, -1023.3, -1.642 };
ANKLE_BITERS = {
	{ -183.06, 3.25, -884.72 }, { -255.57, 4.12, -832.77 }, { -376.15, 4.41, -886.16 },
	{ -507.36, 6.92, -902.53 }, { -619.96, 3.60, -954.60 }, { -634.94, 21.94, -1070.57 },
};

local function dist2d(ax, az, bx, bz)
	return math.sqrt((ax - bx) ^ 2 + (az - bz) ^ 2);
end

local function say(player, quest, textId)
	player:SendGameMessage(quest, textId, 0x20);
end

local function alive(npc)
	return npc ~= nil and not npc:IsDead();
end

function init()
	return "/Director/Quest/QuestDirectorMan0g101";
end

function onEventStarted(player, director, triggerName)
	player:EndEvent();
	local quest = player:GetQuest("Man0g1");
	if (quest == nil) then
		return;
	end

	local area = waitForContentArea(player, "SimpleContentMan0g101");
	if (area == nil) then
		return;
	end

	local duty = director:CreateDuty(area);
	duty:AddPlayer(player);
	duty:SetWinOnClear(false);
	duty:SetTimeLimit(ESCORT_TIME_LIMIT);
	local p, s = POWLE_START, SANSA_START;
	local powle = duty:SpawnEscort(POOL_POWLE, ESCORT_LEVEL, p[1], p[2], p[3], p[4]);
	local sansa = duty:SpawnEscort(POOL_SANSA, ESCORT_LEVEL, s[1], s[2], s[3], s[4]);
	if (powle ~= nil) then
		duty:SetEscortFollow(powle, player, 5);
	end
	if (sansa ~= nil) then
		duty:SetEscortFollow(sansa, powle or player, 6);
	end
	for _, b in ipairs(ANKLE_BITERS) do
		duty:SpawnEnemy(POOL_ANKLE_BITER, math.random(5, 7), b[1], b[2], b[3], 0);
	end
	player:SetMod(modifiersGlobal.MinimumHpLock, 1);

	player:SendGameMessage(GetWorldMaster(), TEXT_PROTECT_FROM_HARM, 0x20, POWLE_DISPLAY_ID);
	player:SendGameMessage(GetWorldMaster(), 25018, 0x20, ESCORT_TIME_LIMIT / 60);
	say(player, quest, TEXT_DUTY_START);
	duty:Start();

	local engagedSeen = {};
	local liveSeen = {};
	local lastNear = 0;
	local lastHurt = -HURT_LINE_SECONDS;
	local lastMarch = 0;
	local kidHp = {};
	local saidNearGoal = false;
	local tick = 0;
	while (duty:IsRunning() and player.CurrentArea == area) do
		wait(1);
		tick = tick + 1;
		if (not alive(powle) or not alive(sansa)) then
			break;
		end

		local hurt = false;
		for _, kid in ipairs({ powle, sansa }) do
			local hp = kid:GetHP();
			if (kidHp[kid.Id] ~= nil and hp < kidHp[kid.Id]) then
				hurt = true;
			end
			kidHp[kid.Id] = hp;
		end
		if (hurt and tick - lastHurt >= HURT_LINE_SECONDS) then
			lastHurt = tick;
			say(player, quest, (tick % 2 == 0) and TEXT_KID_HURT_YELP or TEXT_KID_HURT_GRIPE);
		end

		local nearLive, anyEngaged = 0, false;
		local liveNow = {};
		local enemies = duty:GetEnemies();
		for i = 0, enemies.Count - 1 do
			local mob = enemies[i];
			liveNow[mob.Id] = true;
			local d = math.min(dist2d(mob.positionX, mob.positionZ, player.positionX, player.positionZ),
				dist2d(mob.positionX, mob.positionZ, powle.positionX, powle.positionZ));
			if (d <= ESCORT_HOLD_RADIUS) then
				nearLive = nearLive + 1;
			end
			if (mob:IsEngaged()) then
				anyEngaged = true;
				if (not engagedSeen[mob.Id]) then
					engagedSeen[mob.Id] = true;
					player:SendGameMessageLocalizedDisplayName(GetWorldMaster(), TEXT_MOB_ENGAGED, 0x20, ANKLE_BITER_DISPLAY_ID);
					say(player, quest, TEXT_AMBUSH);
				end
			end
		end
		for id, _ in pairs(liveSeen) do
			if (not liveNow[id]) then
				player:SendGameMessageLocalizedDisplayName(GetWorldMaster(), TEXT_MOB_DEFEATED, 0x20, ANKLE_BITER_DISPLAY_ID);
			end
		end
		liveSeen = liveNow;

		if (nearLive == 0 and lastNear > 0) then
			say(player, quest, TEXT_WAVE_CLEAR);
			say(player, quest, TEXT_MARCH_RESUME);
			lastMarch = tick;
		end
		lastNear = nearLive;

		if (nearLive == 0 and not anyEngaged) then
			local toStump = dist2d(player.positionX, player.positionZ, STUMP_X, STUMP_Z);
			if (toStump <= ARRIVAL_RADIUS) then
				say(player, quest, TEXT_ARRIVAL_THANKS);
				say(player, quest, TEXT_STUMP_IN_SIGHT);
				duty:Succeed();
			else
				if (not saidNearGoal and toStump <= NEAR_GOAL_RADIUS) then
					saidNearGoal = true;
					say(player, quest, TEXT_NEAR_GOAL);
				end
				if (tick - lastMarch >= MARCH_LINE_SECONDS) then
					lastMarch = tick;
					say(player, quest, (tick % 3 == 0) and TEXT_WAVE_THANKS or TEXT_TRIP_FUN);
				end
			end
		end
	end

	if (player.CurrentArea ~= area) then
		player:SetMod(modifiersGlobal.MinimumHpLock, 0);
		duty:End();
		return;
	end

	quest = player:GetQuest("Man0g1");
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
