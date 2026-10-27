using System;
using System.Linq;
using Meteor.Map.Actors;
using Meteor.Map.actors.chara.ai.utils;
using Xunit;

namespace Meteor.Map.Tests.Formulas
{
    public sealed class ExpTests
    {
        [Fact]
        public void ExpToNextLevelMatchesClientTable()
        {
            // The client's EXP table sums to this over levels 1 to 49.
            Assert.True(Player.MAXEXP.Take(49).Sum() == 1_781_150,
                Sources.Cite("CL-EXP", $"Lv1-49 EXP: expected 1,781,150, got {Player.MAXEXP.Take(49).Sum():N0}"));
        }

        // Party EXP over solo EXP, from max-EXP-per-kill tables by party size. Five or more is ours.
        [Theory]
        [InlineData(1, 1.0)]
        [InlineData(2, 0.75)]
        [InlineData(3, 2.0 / 3.0)]
        [InlineData(4, 7.0 / 12.0)]
        public void PartyShare(int members, double expected)
        {
            Assert.True(Math.Abs(BattleUtils.GetPartyExpModifier(members) - expected) < 1e-9,
                Sources.Cite("EL-EXP", $"{members} members: expected {expected}, got {BattleUtils.GetPartyExpModifier(members)}"));
        }

        [Fact]
        public void PartyShareRange()
        {
            Assert.Equal(10, BattleUtils.EXP_LEVEL_WINDOW);
        }

        // Linked enemies → EXP bonus %, the 1.19 notes' table.
        [Theory]
        [InlineData(1, 25)]
        [InlineData(2, 50)]
        [InlineData(3, 75)]
        [InlineData(4, 100)]
        [InlineData(6, 100)]
        public void LinkBonus(int linked, int expected)
        {
            Assert.True(expected == BattleUtils.GetLinkBonus((ushort)linked),
                Sources.Cite("PN119-B", $"{linked} linked: expected +{expected}%, got +{BattleUtils.GetLinkBonus((ushort)linked)}%"));
        }

        [Fact]
        public void NoLinkNoBonus()
        {
            Assert.Equal(0, BattleUtils.GetLinkBonus(0));
        }

        // Chain number → seconds allowed to reach it and its EXP bonus %, the 1.19 notes' table.
        // The server keeps the chain status at the chain number reached, so reaching chain N runs
        // on the time limit set at chain N - 1.
        [Theory]
        [InlineData(1, 100, 20)]
        [InlineData(2, 80, 25)]
        [InlineData(3, 60, 30)]
        [InlineData(4, 20, 40)]
        [InlineData(5, 10, 50)]
        [InlineData(8, 10, 50)]
        public void ChainBonus(int chain, int timeLimit, int bonus)
        {
            int gotTime = BattleUtils.GetChainTimeLimit((ushort)(chain - 1));
            int gotBonus = BattleUtils.GetChainBonus((ushort)chain);
            Assert.True(timeLimit == gotTime && bonus == gotBonus,
                Sources.Cite("PN119-B", $"chain {chain}: expected {timeLimit} s, +{bonus}%; got {gotTime} s, +{gotBonus}%"));
        }
    }
}
