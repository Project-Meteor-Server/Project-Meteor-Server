/*
===========================================================================
Copyright (C) 2015-2019 Project Meteor Dev Team

This file is part of Project Meteor Server.

Project Meteor Server is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Project Meteor Server is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Project Meteor Server. If not, see <https:www.gnu.org/licenses/>.
===========================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Meteor.Common;
using Meteor.Map.Actors;
using Meteor.Map.actors.chara.npc;

namespace Meteor.Map.actors.chara.ai.controllers
{
    // todo: this is probably not needed, can do everything in their script
    class AllyController : BattleNpcController
    {
        private const float FOLLOW_DISTANCE = 3.0f;

        protected new Ally owner;
        public AllyController(Ally owner) :
            base(owner)
        {
            this.owner = owner;
        }
        
        protected List<Character> GetContentGroupCharas()
        {
            List<Character> contentGroupCharas = null;

            if (owner.currentContentGroup != null && owner.CurrentArea != null)
            {
                contentGroupCharas = new List<Character>(owner.currentContentGroup.GetMemberCount());
                foreach (var charaId in owner.currentContentGroup.GetMembers())
                {
                    var chara = owner.CurrentArea.FindActorInArea<Character>(charaId);

                    if (chara != null)
                        contentGroupCharas.Add(chara);
                }
            }

            return contentGroupCharas;
        }

        public Player GetLeader()
        {
            var charas = GetContentGroupCharas();
            if (charas != null)
            {
                Player p = charas.OfType<Player>().FirstOrDefault(c => c.playerSession != null) ?? charas.OfType<Player>().FirstOrDefault();
                if (p != null)
                    return p;
            }
            if (owner.duty != null)
                return owner.duty.GetPlayers().FirstOrDefault(p => p.CurrentArea == owner.CurrentArea);
            return null;
        }

        //Iterate over players in the group and if they are fighting, assist them
        protected override void TryAggro(DateTime tick)
        {
            if (owner.passive)
                return;
            //lua.LuaEngine.CallLuaBattleFunction(owner, "tryAggro", owner, GetContentGroupCharas());

            var charas = GetContentGroupCharas();
            if (charas == null)
                return;
            foreach(Character chara in charas)
            {
                if(chara.IsPlayer())
                {
                    bool playerFights = chara.IsEngaged() || (chara.target is BattleNpc t && t.hateContainer.HasHateForTarget(chara));
                    if(playerFights && chara.target is BattleNpc foe && !(foe is Ally) && foe.IsAlive())
                    {
                        owner.hateContainer.AddBaseHate(foe);
                        owner.Engage(foe);
                        break;
                    }
                }
            }
            //base.TryAggro(tick);
        }

        public override void Disengage()
        {
            base.Disengage();
            owner.isMovingToSpawn = false;
            owner.aiContainer.mover.Stop();
            owner.spawnX = owner.positionX;
            owner.spawnY = owner.positionY;
            owner.spawnZ = owner.positionZ;
        }

        // server really likes to hang whenever scripts iterate area's actorlist
        protected override void DoCombatTick(DateTime tick, List<Character> contentGroupCharas = null)
        {
            if (contentGroupCharas == null)
            {
                contentGroupCharas = GetContentGroupCharas();
            }

            base.DoCombatTick(tick, contentGroupCharas);
        }

        public override void Update(DateTime tick)
        {
            base.Update(tick);
            if ((owner.followLeader || owner.escortFollow != null) && !owner.IsDead() && !owner.aiContainer.IsEngaged())
                DoRoamTick(tick);
        }

        protected override void DoRoamTick(DateTime tick, List<Character> contentGroupCharas = null)
        {
            if (contentGroupCharas == null)
                contentGroupCharas = GetContentGroupCharas();

            Character leader = owner.escortFollow;
            float gap = owner.escortFollow != null ? owner.escortFollowGap : FOLLOW_DISTANCE;
            if (leader == null && owner.followLeader)
                leader = contentGroupCharas?.Find(c => c is Player && c.IsAlive());
            var mover = owner.aiContainer.mover;
            if (leader != null && leader.IsAlive() && Utils.Distance(owner.GetPosAsVector3(), leader.GetPosAsVector3()) > gap
                && !mover.IsMoving && owner.aiContainer.CanFollowPath())
                mover.MoveTo(leader.GetPosAsVector3(), Mover.MOVE_STATE_RUN, Math.Max(1f, gap - 1f));
        }
    }
}
