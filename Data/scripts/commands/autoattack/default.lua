require("global");
require("battle/actions");

function onSkillPrepare(caster, target, skill)
    return 0;
end;

function onSkillStart(caster, target, skill)
    return 0;
end;

function onSkillFinish(caster, target, skill, action, actionContainer)
    actions.autoAttack(caster, target, skill, action, actionContainer);
end;
