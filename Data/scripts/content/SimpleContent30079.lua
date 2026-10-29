require ("global")
require ("modifiers")

POOL_GOOBBUE      = 8203301;
POOL_NIELLEFRESNE = 8290003;
POOL_THANCRED     = 8290004;
JOB_PUGILIST = 2;
JOB_CONJURER = 23;

GOOBBUE_MIN_LEVEL = 1;
GOOBBUE_MAX_LEVEL = 13;
ALLY_DAMAGE_PERCENT = -50;
POTENCIAL_LEVEL_HIDDEN = -1.0;
ALLY_LEVEL_LEAD = 10;
TANK_PULL_SECONDS = 3;

ALLIES = {
    {POOL_NIELLEFRESNE, JOB_CONJURER, -11.86, 192, 35.06, -0.8},
    {POOL_THANCRED, JOB_PUGILIST, -26.41, 192, 39.52, 1.2},
};

GOOBBUE = {-6.193, 192, 47.658, -2.224};

function onCreate(starterPlayer, contentArea, director)
    contentArea:SpawnActor(1090385, "openingstoper", -24.34, 192, 34.22, 0);

    director:AddMember(starterPlayer);
    director:AddMember(director);
    director:StartContentGroup();

    local level = math.max(1, starterPlayer:GetLevel());
    local goobbueLevel = math.max(GOOBBUE_MIN_LEVEL, math.min(GOOBBUE_MAX_LEVEL, level));

    local duty = director:CreateDuty(contentArea);
    duty:SetAllyAggressive(false);

    for _, a in ipairs(ALLIES) do
        local ally = duty:SpawnAlly(a[1], goobbueLevel + ALLY_LEVEL_LEAD, a[3], a[4], a[5], a[6]);
        if (ally ~= nil) then
            ally.charaWork.parameterSave.state_mainSkill[0] = a[2];
            ally:SetMod(modifiersGlobal.Damage, ALLY_DAMAGE_PERCENT);
            ally:SetMod(modifiersGlobal.MinimumHpLock, 1);
            if (a[1] == POOL_THANCRED) then
                duty:SetTank(ally, TANK_PULL_SECONDS);
            end
        end
    end

    local g = GOOBBUE;
    local mob = duty:SpawnEnemy(POOL_GOOBBUE, goobbueLevel, g[1], g[2], g[3], g[4]);
    if (mob ~= nil) then
        mob.neutral = true;
        mob.charaWork.battleSave.potencial = POTENCIAL_LEVEL_HIDDEN;
        mob:SetMobMod(mobModifiersGlobal.Roams, 0);
    end

    starterPlayer:SetMod(modifiersGlobal.MinimumHpLock, 1);
    duty:Start();
end

function onDestroy()

end
