require ("global")
require ("modifiers")

POOL_AURELIA   = 8205403;
POOL_YSHTOLA   = 8290001;
POOL_STHALMANN = 8290002;
JOB_PUGILIST  = 2;
JOB_CONJURER  = 23;

AURELIA_MIN_LEVEL = 1;
AURELIA_MAX_LEVEL = 13;
ALLY_DAMAGE_PERCENT = -50;
POTENCIAL_LEVEL_HIDDEN = -1.0;
ALLY_LEVEL_LEAD = 10;
TANK_PULL_SECONDS = 3;

ALLIES = {
    {POOL_YSHTOLA, JOB_CONJURER, -8, 16.35, 6, 0.5},
    {POOL_STHALMANN, JOB_PUGILIST, 0, 16.35, 22, 3},
};

AURELIAS = {
    {-0.02, 16.39, 14.24, -2.81},
    {-3.02, 16.39, 14.24, -2.81},
    {-6.02, 16.39, 14.24, -2.81},
};

function onCreate(starterPlayer, contentArea, director)
    director:AddMember(starterPlayer);
    director:AddMember(director);
    director:StartContentGroup();

    local level = math.max(1, starterPlayer:GetLevel());
    local aureliaLevel = math.max(AURELIA_MIN_LEVEL, math.min(AURELIA_MAX_LEVEL, level));

    local duty = director:CreateDuty(contentArea);
    duty:SetAllyAggressive(false);

    for _, a in ipairs(ALLIES) do
        local ally = duty:SpawnAlly(a[1], aureliaLevel + ALLY_LEVEL_LEAD, a[3], a[4], a[5], a[6]);
        if (ally ~= nil) then
            ally.charaWork.parameterSave.state_mainSkill[0] = a[2];
            ally:SetMod(modifiersGlobal.Damage, ALLY_DAMAGE_PERCENT);
            ally:SetMod(modifiersGlobal.MinimumHpLock, 1);
            if (a[1] == POOL_STHALMANN) then
                duty:SetTank(ally, TANK_PULL_SECONDS);
            end
        end
    end

    for _, p in ipairs(AURELIAS) do
        local mob = duty:SpawnEnemy(POOL_AURELIA, aureliaLevel, p[1], p[2], p[3], p[4]);
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
