using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Meteor.Map.Tests
{
    /// <summary>
    /// Checks values passed between scripts and the server, and plain Lua arithmetic, text and
    /// library functions, against fixed answers. The script side is scripts/__tests/probe.lua: each
    /// check there produces "check = result".
    /// </summary>
    [Collection("engines")]
    public sealed class ScriptValueTests
    {
        private readonly EngineFixture engines;

        public ScriptValueTests(EngineFixture engines)
        {
            this.engines = engines;
        }

        private Dictionary<string, string> Run(string function, params object[] args)
        {
            var script = engines.Engine.Load("./scripts/__tests/probe.lua");
            Assert.Null(script.LoadError);
            var text = (string)script.Call(function, args)[0];
            return text.Split('\n').Select(l => l.Split(" = ", 2)).ToDictionary(p => p[0], p => p.Length > 1 ? p[1] : "");
        }

        private static void Check(Dictionary<string, string> expected, Dictionary<string, string> actual)
        {
            Assert.Equal(expected.Keys.OrderBy(k => k), actual.Keys.OrderBy(k => k));
            var wrong = expected.Where(p => actual[p.Key] != p.Value).Select(p => $"{p.Key}: expected {p.Value}, got {actual[p.Key]}").ToList();
            Assert.True(wrong.Count == 0, string.Join("\n", wrong));
        }

        [Fact]
        public void ValuesBetweenScriptsAndServer()
        {
            Check(new Dictionary<string, string>
            {
                ["I(2.5)"] = "int 2",
                ["I(-2.7)"] = "int -2",
                // too big for an int: becomes the largest int
                ["I(3e9)"] = "int 2147483647",
                ["I(-3e9)"] = "int -2147483648",
                // below 0 for a uint: becomes 0
                ["U(-1)"] = "uint 0",
                ["U(4294967296)"] = "uint 4294967295",
                ["U(4294967295)"] = "uint 4294967295",
                ["U(2.5)"] = "uint 2",
                // byte and short wrap around
                ["B(300)"] = "byte 44",
                ["B(-1)"] = "byte 255",
                ["S(40000)"] = "short -25536",
                ["US(-1)"] = "ushort 65535",
                ["F(0.1)"] = "float 0.1",
                ["D(0.1)"] = "double 0.1",
                ["E(3)"] = "enum Three",
                // a number passed as "any type" is always a double
                ["O(1)"] = "Double",
                ["O(1.5)"] = "Double",
                ["O('x')"] = "String",
                ["O(true)"] = "Boolean",
                ["O(nil)"] = "null",
                ["Over(1)"] = "Over(int)",
                ["Over('a')"] = "Over(string)",
                // a number never matches a text parameter
                ["Named(5)"] = "by id 5",
                ["Named('x')"] = "by name x",
                ["type(L())"] = "userdata",
                ["L().Count"] = "3",
                // C# lists and arrays are counted from 0
                ["L()[0]"] = "10",
                ["L()[2]"] = "30",
                ["L() sum"] = "60",
                ["type(A())"] = "userdata",
                ["A().Length"] = "2",
                ["A()[1]"] = "2",
                ["type(bytes)"] = "userdata",
                ["bytes.Length"] = "4",
                ["bytes[0]"] = "7",
                ["bytes[3]"] = "9",
                ["type(D())"] = "userdata",
                ["D().Count"] = "1",
                ["Objects().Count"] = "1",
                ["Objects()[0]:I(4)"] = "int 4",
                ["N() == nil"] = "true",
                ["RetU()"] = "4000000000",
                ["RetL()"] = "1099511627776",
                ["RetB()"] = "200",
                ["type(RetE())"] = "userdata",
                ["RetE() == RetE()"] = "true",
                ["Params(1, 2, 3)"] = "3",
                ["Params()"] = "0",
                ["Opt(1)"] = "5",
                ["Opt(1, 9)"] = "10",
                ["field"] = "3",
                ["set field 7"] = "7",
                ["Prop"] = "1.25",
                ["set Prop 2.9"] = "2.9",
                ["Self() == p"] = "true",
                ["string arg"] = "héllo|5",
                // text from C# arrives as UTF-8 bytes: "naïve" is six
                ["string ret len"] = "6",
                ["Global()"] = "true",
            }, Run("interop", new Probe()));
        }

        [Fact]
        public void PlainLua()
        {
            Check(new Dictionary<string, string>
            {
                // Lua's % rounds down, so the answer takes the sign of the right-hand number
                ["7 % -3"] = "-2",
                ["-7 % 3"] = "2",
                ["-7.5 % 2"] = "0.5",
                ["math.fmod(-7, 3)"] = "-1",
                // numbers turn into text with 14 significant digits
                ["10 / 3"] = "3.3333333333333",
                ["2^53"] = "9.007199254741e+15",
                ["1e15"] = "1e+15",
                ["1e16"] = "1e+16",
                ["123456789012"] = "123456789012",
                ["0.1 + 0.2"] = "0.3",
                ["1/0"] = "inf",
                ["-1/0"] = "-inf",
                ["-0.0"] = "-0",
                ["0/0 ~= 0/0"] = "true",
                ["math.floor(-3.5)"] = "-4",
                ["math.ceil(-3.5)"] = "-3",
                ["math.abs(-2)"] = "2",
                ["math.max(1, 5, 3)"] = "5",
                ["math.sqrt(2)"] = "1.4142135623731",
                ["math.pi"] = "3.1415926535898",
                ["math.atan2(1, -1)"] = "2.3561944901923",
                ["math.pow(2, 10)"] = "1024",
                ["math.log(8, 2)"] = "3",
                ["math.log10(1000)"] = "3",
                ["tonumber('0x10')"] = "16",
                ["tonumber(' 12 ')"] = "12",
                ["tonumber('1e2')"] = "100",
                ["tonumber('z', 36)"] = "35",
                ["'10' + 5"] = "15",
                ["10 .. ''"] = "10",
                ["1.5 .. ''"] = "1.5",
                ["%d 3.7"] = "3",
                ["%d -3.7"] = "-3",
                ["%.2f 2.675"] = "2.67",
                ["%5.1f"] = "  3.1",
                ["%x 255"] = "ff",
                ["%X 0x80000000"] = "80000000",
                ["%s nil"] = "nil",
                ["%q"] = "\"a\\\"b\"",
                ["%g 0.1"] = "0.1",
                ["#'abc'"] = "3",
                // # counts bytes; é is two bytes
                ["#'é'"] = "2",
                // upper only changes A-Z
                ["upper('é')"] = "Aé",
                ["sub(2, -2)"] = "ell",
                ["find plain"] = "2",
                ["gsub count"] = "3",
                ["match %d+"] = "12",
                ["rep"] = "ababab",
                ["byte"] = "65",
                ["char"] = "Hi",
                ["select('#')"] = "2",
                ["unpack"] = "4",
                ["#{1,2,3}"] = "3",
                ["concat"] = "1,2,3",
                ["insert/remove"] = "0,1",
                ["sort"] = "1,2,3",
                ["sort desc"] = "3,2,1",
                ["tostring(true)"] = "true",
                ["tostring(nil)"] = "nil",
                ["type(print)"] = "function",
                ["type(os.time())"] = "number",
                ["type(os.clock())"] = "number",
                ["bit.band"] = "240",
                ["bit.bor"] = "184582272",
                ["bit.lshift(1, 4)"] = "16",
                // bit can return negatives; % 0x100000000 makes them positive
                ["bit.lshift(1, 31) % 2^32"] = "2147483648",
                ["bit.band(0xFFFFFFFF, 0xA0F00000) % 2^32"] = "2700083200",
                ["goto"] = "3",
                ["varargs"] = "3",
                ["closure"] = "2",
                ["pcall error value"] = "5",
                ["string methods on literal"] = "5",
            }, Run("language"));
        }
    }
}
