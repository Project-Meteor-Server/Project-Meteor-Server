require ("global")
require ("modifiers")
require ("quests/man/man0l1")

TEXT_BOUND_BY_DUTY     = 50011;
TEXT_PROTECT_FROM_HARM = 51005;

POOL_SISIPU      = 9290007;
POOL_ANKLE_BITER = 9205603;
ESCORT_LEVEL     = 5;

ESCORT_TIME_LIMIT  = 1800;
ESCORT_HOLD_RADIUS = 28;
ARRIVAL_RADIUS     = 20;
PLAYER_LEASH       = 15;
DIALOGUE_SECONDS   = 20;
HURT_HP_RATIO      = 0.5;
CLOSE_HP_RATIO     = 0.85;
TORCH_X, TORCH_Z   = 137.44, 1322.0;

SISIPU_DISPLAY_ID      = 1500024;
ANKLE_BITER_DISPLAY_ID = 3205603;
TEXT_MOB_ENGAGED       = 30120;
TEXT_MOB_DEFEATED      = 30121;

TEXT_DUTY_START  = 105;
TEXT_GUIDANCE    = 283;
TEXT_DAWDLE      = 285;
TEXT_WAVE_CLEAN  = 286;
TEXT_WAVE_CLOSE  = 287;
TEXT_WAVE_THANKS = 288;
TEXT_SISIPU_HURT = 289;
TEXT_ARRIVAL     = 290;

SISIPU_START = { -60.25, 33.15, 162.51, 0.8 };
ANKLE_BITERS = {
	{ 112.30, 47.20, 148.17 }, { 129.15, 44.25, 291.87 }, { 69.96, 41.22, 449.76 },  { 134.69, 52.41, 597.19 },
	{ 74.46, 61.65, 741.17 },  { -2.35, 53.43, 878.18 },  { 48.45, 43.43, 1038.45 }, { 124.43, 46.40, 1205.66 },
};

local function dist2d(ax, az, bx, bz)
	return math.sqrt((ax - bx) ^ 2 + (az - bz) ^ 2);
end

local function say(player, quest, textId)
	player:SendGameMessage(quest, textId, 0x20);
end

function init()
	return "/Director/Quest/QuestDirectorMan0l101";
end

function onEventStarted(player, director, triggerName)
	player:EndEvent();
	local quest = player:GetQuest("Man0l1");
	if (quest == nil) then
		return;
	end

	local area = waitForContentArea(player, "SimpleContentMan0l101");
	if (area == nil) then
		return;
	end

	local duty = director:CreateDuty(area);
	duty:AddPlayer(player);
	duty:SetWinOnClear(false);
	duty:SetTimeLimit(ESCORT_TIME_LIMIT);
	local s = SISIPU_START;
	local sisipu = duty:SpawnEscort(POOL_SISIPU, ESCORT_LEVEL, s[1], s[2], s[3], s[4]);
	if (sisipu ~= nil) then
		duty:SetEscortFollow(sisipu, player, 5);
	end
	for _, b in ipairs(ANKLE_BITERS) do
		duty:SpawnEnemy(POOL_ANKLE_BITER, math.random(5, 7), b[1], b[2], b[3], 0);
	end
	player:SetMod(modifiersGlobal.MinimumHpLock, 1);

	player:SendGameMessage(GetWorldMaster(), TEXT_PROTECT_FROM_HARM, 0x20, SISIPU_DISPLAY_ID);
	player:SendGameMessage(GetWorldMaster(), TEXT_BOUND_BY_DUTY, 0x20);
	player:SendGameMessage(GetWorldMaster(), 25018, 0x20, ESCORT_TIME_LIMIT / 60);
	say(player, quest, TEXT_DUTY_START);
	duty:Start();

	local engagedSeen = {};
	local liveSeen = {};
	local lastNear = 0;
	local lastDialogue = 0;
	local escortFought = false;
	local tick = 0;
	while (duty:IsRunning() and player.CurrentArea == area) do
		wait(1);
		tick = tick + 1;
		if (sisipu == nil or sisipu:IsDead()) then
			break;
		end

		local nearLive, anyEngaged = 0, false;
		local liveNow = {};
		local enemies = duty:GetEnemies();
		for i = 0, enemies.Count - 1 do
			local mob = enemies[i];
			liveNow[mob.Id] = true;
			local d = math.min(dist2d(mob.positionX, mob.positionZ, player.positionX, player.positionZ),
				dist2d(mob.positionX, mob.positionZ, sisipu.positionX, sisipu.positionZ));
			if (d <= ESCORT_HOLD_RADIUS) then
				nearLive = nearLive + 1;
			end
			if (mob:IsEngaged()) then
				anyEngaged = true;
				if (not engagedSeen[mob.Id]) then
					engagedSeen[mob.Id] = true;
					player:SendGameMessageLocalizedDisplayName(GetWorldMaster(), TEXT_MOB_ENGAGED, 0x20, ANKLE_BITER_DISPLAY_ID);
				end
			end
		end
		for id, _ in pairs(liveSeen) do
			if (not liveNow[id]) then
				player:SendGameMessageLocalizedDisplayName(GetWorldMaster(), TEXT_MOB_DEFEATED, 0x20, ANKLE_BITER_DISPLAY_ID);
			end
		end
		liveSeen = liveNow;
		if (sisipu:IsEngaged()) then
			escortFought = true;
		end

		-- A wave just ended: her line follows how she came through it.
		if (nearLive == 0 and lastNear > 0) then
			local ratio = sisipu:GetHP() / math.max(1, sisipu:GetMaxHP());
			if (ratio <= HURT_HP_RATIO) then
				say(player, quest, TEXT_SISIPU_HURT);
			elseif (ratio <= CLOSE_HP_RATIO) then
				say(player, quest, TEXT_WAVE_CLOSE);
			elseif (escortFought) then
				say(player, quest, TEXT_WAVE_THANKS);
			else
				say(player, quest, TEXT_WAVE_CLEAN);
			end
			escortFought = false;
			lastDialogue = tick;
		end
		lastNear = nearLive;

		if (nearLive == 0 and not anyEngaged) then
			if (dist2d(player.positionX, player.positionZ, TORCH_X, TORCH_Z) <= ARRIVAL_RADIUS) then
				duty:Succeed();
			elseif (tick - lastDialogue >= DIALOGUE_SECONDS) then
				lastDialogue = tick;
				local lagging = dist2d(sisipu.positionX, sisipu.positionZ, player.positionX, player.positionZ) > PLAYER_LEASH;
				say(player, quest, lagging and TEXT_DAWDLE or TEXT_GUIDANCE);
			end
		end
	end

	if (player.CurrentArea ~= area) then
		player:SetMod(modifiersGlobal.MinimumHpLock, 0);
		duty:End();
		return;
	end

	quest = player:GetQuest("Man0l1");
	if (quest == nil or quest:GetSequence() ~= SEQ_050) then
		duty:End();
		return;
	end

	if (duty:IsSuccess()) then
		say(player, quest, TEXT_ARRIVAL);
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
