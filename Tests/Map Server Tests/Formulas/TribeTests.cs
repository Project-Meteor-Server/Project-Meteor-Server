using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Meteor.Map.Tests.Formulas
{
    [Collection("engines")]
    public sealed class TribeTests
    {
        private const string Source = "CL-TRIBE";

        // ffxivgame.exe 1.23b: 15 rows × 15 int32. Row i is tribe id i + 1:
        // { i, STR, VIT, DEX, INT, MND, PIE, 90, Fire, Ice, Wind, Earth, Lightning, Water, 90 }.
        private const uint TableVa = 0x12be318;
        private const uint ImageBase = 0x400000;

        // The same table as read from the client, so the test runs without one.
        private static readonly int[][] Client =
        {
            new[] { 16, 15, 14, 16, 13, 16, 16, 13, 15, 15, 15, 16 },
            new[] { 16, 15, 14, 16, 13, 16, 16, 13, 15, 15, 15, 16 },
            new[] { 18, 17, 15, 13, 15, 12, 15, 16, 14, 14, 18, 13 },
            new[] { 14, 13, 18, 17, 12, 16, 12, 14, 18, 17, 14, 15 },
            new[] { 14, 13, 18, 17, 12, 16, 12, 14, 18, 17, 14, 15 },
            new[] { 15, 14, 15, 18, 15, 13, 14, 16, 12, 17, 15, 16 },
            new[] { 15, 14, 15, 18, 15, 13, 14, 16, 12, 17, 15, 16 },
            new[] { 13, 13, 17, 16, 15, 16, 14, 13, 15, 16, 17, 15 },
            new[] { 13, 13, 17, 16, 15, 16, 14, 13, 15, 16, 17, 15 },
            new[] { 12, 12, 15, 16, 17, 18, 17, 12, 16, 18, 15, 12 },
            new[] { 12, 12, 15, 16, 17, 18, 17, 12, 16, 18, 15, 12 },
            new[] { 16, 15, 17, 13, 14, 15, 18, 15, 13, 15, 12, 17 },
            new[] { 13, 12, 16, 14, 18, 17, 13, 18, 15, 14, 16, 14 },
            new[] { 17, 18, 13, 12, 16, 14, 13, 17, 17, 12, 13, 18 },
            new[] { 15, 16, 12, 15, 17, 15, 18, 14, 16, 13, 14, 15 },
        };

        private readonly FormulaScript lua;
        private readonly ITestOutputHelper output;

        public TribeTests(EngineFixture engines, ITestOutputHelper output)
        {
            lua = new FormulaScript(engines);
            this.output = output;
        }

        [Fact]
        public void ServerTableMatchesClient()
        {
            var wrong = new List<string>();
            for (int tribe = 1; tribe <= 15; tribe++)
                for (int col = 1; col <= 12; col++)
                {
                    int got = (int)lua.One("tribe", tribe, col);
                    if (got != Client[tribe - 1][col - 1])
                        wrong.Add($"tribe {tribe} column {col}: client {Client[tribe - 1][col - 1]}, server {got}");
                }
            Assert.True(wrong.Count == 0, Sources.Cite(Source, string.Join("\n", wrong)));
        }

        // Set METEOR_CLIENT_EXE to a 1.23b ffxivgame.exe to check the copy above against the client.
        [Fact]
        public void CopyMatchesClientExe()
        {
            var exe = Environment.GetEnvironmentVariable("METEOR_CLIENT_EXE");
            if (string.IsNullOrEmpty(exe) || !File.Exists(exe))
            {
                output.WriteLine("METEOR_CLIENT_EXE not set: not checked against the client.");
                return;
            }

            var image = File.ReadAllBytes(exe);
            long offset = FileOffset(image, TableVa - ImageBase);
            for (int row = 0; row < 15; row++)
            {
                var r = Enumerable.Range(0, 15).Select(k => BitConverter.ToInt32(image, (int)(offset + (row * 15 + k) * 4))).ToArray();
                Assert.Equal(row, r[0]);
                var fromExe = r.Skip(1).Take(6).Concat(r.Skip(8).Take(6)).ToArray();
                Assert.True(fromExe.SequenceEqual(Client[row]),
                    Sources.Cite(Source, $"row {row}: exe {string.Join(",", fromExe)}, copy {string.Join(",", Client[row])}"));
            }
        }

        private static long FileOffset(byte[] image, uint rva)
        {
            int pe = BitConverter.ToInt32(image, 0x3c);
            int sections = BitConverter.ToUInt16(image, pe + 6);
            int optionalSize = BitConverter.ToUInt16(image, pe + 20);
            int table = pe + 24 + optionalSize;
            for (int i = 0; i < sections; i++)
            {
                int s = table + i * 40;
                uint va = BitConverter.ToUInt32(image, s + 12), rawSize = BitConverter.ToUInt32(image, s + 16), raw = BitConverter.ToUInt32(image, s + 20);
                if (rva >= va && rva < va + rawSize)
                    return raw + (rva - va);
            }
            throw new InvalidDataException($"0x{rva:x} is in no section of the file");
        }
    }
}
