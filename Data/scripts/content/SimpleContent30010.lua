require ("global")
require ("modifiers")

POOL_WOLF     = 2;
POOL_PAPALYMO = 3;
POOL_YDA      = 4;
JOB_PUGILIST    = 2;
JOB_THAUMATURGE = 22;

WOLF_MIN_LEVEL = 1;
WOLF_MAX_LEVEL = 13;
ALLY_DAMAGE_PERCENT = -50;
POTENCIAL_LEVEL_HIDDEN = -1.0;
ALLY_LEVEL_LEAD = 10;
TANK_PULL_SECONDS = 3;

ALLIES = {
    {POOL_PAPALYMO, JOB_THAUMATURGE, 365.89, 4.0943, -706.72, -0.718},
    {POOL_YDA, JOB_PUGILIST, 365.266, 4.122, -700.73, 1.5659},
};

WOLVES = {
    {374.427, 4.4, -698.711, -1.942},
    {375.377, 4.4, -700.247, -1.992},
    {375.125, 4.4, -703.591, -1.54},
};

function onCreate(starterPlayer, contentArea, director)
    contentArea:SpawnActor(1090384, "openingstoper", 356.09, 3.74, -701.62, -1.41);

    director:AddMember(starterPlayer);
    director:AddMember(director);
    director:StartContentGroup();

    local level = math.max(1, starterPlayer:GetLevel());
    local wolfLevel = math.max(WOLF_MIN_LEVEL, math.min(WOLF_MAX_LEVEL, level));

    local duty = director:CreateDuty(contentArea);
    duty:SetAllyAggressive(false);

    for _, a in ipairs(ALLIES) do
        local ally = duty:SpawnAlly(a[1], wolfLevel + ALLY_LEVEL_LEAD, a[3], a[4], a[5], a[6]);
        if (ally ~= nil) then
            ally.charaWork.parameterSave.state_mainSkill[0] = a[2];
            ally:SetMod(modifiersGlobal.Damage, ALLY_DAMAGE_PERCENT);
            ally:SetMod(modifiersGlobal.MinimumHpLock, 1);
            if (a[1] == POOL_YDA) then
                duty:SetTank(ally, TANK_PULL_SECONDS);
            end
        end
    end

    for _, w in ipairs(WOLVES) do
        local mob = duty:SpawnEnemy(POOL_WOLF, wolfLevel, w[1], w[2], w[3], w[4]);
        if (mob ~= nil) then
            mob.neutral = true;
            mob.charaWork.battleSave.potencial = POTENCIAL_LEVEL_HIDDEN;
            mob:SetMobMod(mobModifiersGlobal.Roams, 0);
        end
    end

    starterPlayer:SetMod(modifiersGlobal.MinimumHpLock, 1);
    duty:Start();
end

function onDestroy()

end
