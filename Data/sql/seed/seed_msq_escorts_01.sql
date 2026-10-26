-- Man0g1 escort: the Powle, Sansa and ankle biter classes and pools QuestDirectorMan0g101 spawns.
-- Restart the map server after applying.

-- ---- class paths ---------------------------------------------------------------------------------
UPDATE gamedata_actor_class t JOIN gamedata_actor_class s ON s.id = 2290001
  SET t.classPath = '/Chara/Npc/Monster/Fighter/FighterAllyOpeningAttacker',
      t.propertyFlags = IF(t.propertyFlags = 0, s.propertyFlags, t.propertyFlags),
      t.eventConditions = IFNULL(t.eventConditions, s.eventConditions)
  WHERE t.id IN (2290009, 2290010) AND t.classPath = '';

UPDATE gamedata_actor_class t JOIN gamedata_actor_class s ON s.id = 2105612
  SET t.classPath = s.classPath,
      t.propertyFlags = IF(t.propertyFlags = 0, s.propertyFlags, t.propertyFlags),
      t.eventConditions = IFNULL(t.eventConditions, s.eventConditions)
  WHERE t.id = 2205603 AND t.classPath = '' AND s.classPath <> '';

-- ---- pools ---------------------------------------------------------------------------------------
-- (poolId, actorClassId, name, genusId, currentJob, combatSkill, combatDelay, combatDmgMult, aggroType, immunity, linkType, spellListId, skillListId)
DELETE FROM server_battlenpc_pools WHERE poolId IN (9290009, 9290010, 9205603);
INSERT INTO server_battlenpc_pools (poolId, actorClassId, name, genusId, currentJob, combatSkill, combatDelay, combatDmgMult, aggroType, immunity, linkType, spellListId, skillListId) VALUES
  (9290009, 2290009, 'escort_powle',       29, 0, 1, 4200, 1, 0, 0, 0, 0, 0),
  (9290010, 2290010, 'escort_sansa',       29, 0, 1, 4200, 1, 0, 0, 0, 0, 0),
  (9205603, 2205603, 'escort_ankle_biter', 36, 0, 1, 4200, 1, 1, 0, 0, 0, 0);
