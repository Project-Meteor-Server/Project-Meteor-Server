# Lua engine benchmarks: MoonSharp and xLua (LuaJIT)

Measurements behind the switch from MoonSharp to xLua on LuaJIT 2.1. "Speedup" is MoonSharp time divided by xLua time: above 1× xLua is faster, below 1× it is slower.

## Both engines in CI

`EngineBenchmarks` (`Tests/Map Server Tests`, Debug build) on GitHub Actions, run 39385167192, commit 86c8e7ee, before MoonSharp was removed. Each cell is the speedup on that platform; the median is across the five. A dash is a MoonSharp time that read as zero, below the timer's resolution. "Lua function called from C#" takes about a microsecond on both engines, near that resolution, so its row is noisy; the BenchmarkDotNet section below measures it properly.

| Workload | Median | macOS Apple silicon | macOS Intel | Linux x64 | Linux ARM | Windows x64 |
|---|--:|--:|--:|--:|--:|--:|
| Load a quest script | **155×** | 155× | 75× | 141× | 179× | 340× |
| Load a GM command | **61×** | 61× | 41× | 54× | 65× | 120× |
| Load battle helpers | **133×** | 133× | 79× | 99× | 147× | 202× |
| Lua loop, 1M steps | **179×** | 179× | 347× | 254× | 157× | 168× |
| C# methods called from Lua, 100k | **2.0×** | 2.5× | 2.0× | 1.3× | 1.0× | 2.1× |
| Table build and scan, 10k rows | **347×** | 347× | 568× | 283× | 497× | 338× |
| Lua function called from C# | **0.21×** | 4.0× | 0.07× | 0.08× | 0.33× | — |
| Coroutine start, pause and finish | **66×** | 66× | 48× | 48× | 180× | 74× |

Milliseconds per run, MoonSharp / xLua:

| Workload | macOS Apple silicon | macOS Intel | Linux x64 | Linux ARM | Windows x64 |
|---|--:|--:|--:|--:|--:|
| Load a quest script | 4.65 / 0.030 | 7.57 / 0.101 | 4.08 / 0.029 | 5.37 / 0.030 | 6.80 / 0.020 |
| Load a GM command | 3.81 / 0.063 | 13.54 / 0.332 | 3.14 / 0.058 | 5.15 / 0.079 | 5.30 / 0.044 |
| Load battle helpers | 3.20 / 0.024 | 12.36 / 0.156 | 2.27 / 0.023 | 4.25 / 0.029 | 3.03 / 0.015 |
| Lua loop, 1M steps | 505 / 2.83 | 1,608 / 4.63 | 748 / 2.94 | 572 / 3.64 | 422 / 2.51 |
| C# methods called from Lua, 100k | 95.81 / 37.88 | 182 / 88.93 | 93.89 / 72.03 | 66.65 / 63.61 | 63.70 / 30.22 |
| Table build and scan, 10k rows | 403 / 1.16 | 1,464 / 2.58 | 444 / 1.57 | 664 / 1.34 | 365 / 1.08 |
| Lua function called from C# | 0.004 / 0.001 | 0.001 / 0.015 | 0.001 / 0.012 | 0.001 / 0.003 | 0.000 / 0.002 |
| Coroutine start, pause and finish | 0.132 / 0.002 | 0.713 / 0.015 | 0.239 / 0.005 | 0.902 / 0.005 | 0.223 / 0.003 |

## xLua on the final commit

Run 39395904590, commit dada4239: MoonSharp removed, every script call under the world's zone lock. Milliseconds per run. The table workload here builds 100k rows. This run predates the single-pass call from C# into Lua (below), so "Lua function called from C#" is the old path.

| Workload | macOS Apple silicon | macOS Intel | Linux x64 | Linux ARM | Windows x64 |
|---|--:|--:|--:|--:|--:|
| Load a quest script | 0.032 | 0.207 | 0.024 | 0.016 | 0.031 |
| Load a GM command | 0.065 | 0.378 | 0.043 | 0.027 | 0.058 |
| Load battle helpers | 0.027 | 0.116 | 0.019 | 0.011 | 0.020 |
| Lua loop, 1M steps | 2.54 | 12.05 | 2.54 | 3.69 | 2.89 |
| C# methods called from Lua, 100k | 34.29 | 196 | 43.66 | 45.73 | 62.09 |
| Table build and scan, 100k rows | 9.58 | 501 | 11.29 | 6.92 | 9.37 |
| Lua function called from C# | 0.002 | 0.015 | 0.002 | 0.002 | 0.003 |
| Coroutine start, pause and finish | 0.005 | 0.029 | 0.004 | 0.004 | 0.008 |

## Allocations, the C#/Lua boundary and collections

BenchmarkDotNet 0.14 (ShortRun, `MemoryDiagnoser`) on an Apple M1 Max, Release build, .NET 10, both engines in the same process (commit 0b565f66 plus a benchmark project that is not part of this PR). Times are means; bytes are what the .NET heap allocated. Memory LuaJIT allocates for itself is not counted in these bytes; see the footprint table.

### Summary

Median of each group's cases. The C# calling Lua row uses the single-pass time for xLua.

| Question | Per | MoonSharp time | xLua time | Speedup | MoonSharp .NET bytes | xLua .NET bytes | Note |
|---|---|--:|--:|--:|--:|--:|---|
| Lua calling C# | per call | 195 ns | 92 ns | **2.1×** | 633 B | 0 B | Nothing allocated on xLua (64 B when a Lua string becomes a C# string). |
| C# calling a script function | per call | 81 ns | 186 ns | **0.44×** | 256 B | 216 B | xLua's one slow spot. Over half was a separate check that the function exists; the Lua call helper now does that check (in this PR), so a call is a single pass into Lua. |
| Lua looping over a C# list or array by index | per element | 233 ns | 152 ns | **1.5×** | 679 B | 4 B | The fastest way for a script to read a collection, and the way scripts already do it. |
| Lua using a C# enumerator | per element | 242 ns | 281 ns | **0.86×** | 831 B | 52 B | Slower on xLua, which reaches enumerator types by reflection and boxes each `Current`; a dictionary is the worst case at 1.7×. Allocates a tenth as much. No script uses enumerators today. |
| Lua handing a collection to C# | per call | 333 ns | 148 ns | **2.3×** | 809 B | 0 B | A C# list passed back costs nothing extra; a Lua table converted to `int[]` allocates 424 B against 6 KB. |
| C# reading a table a script returns | per entry | 34 ns | 135 ns | **0.25×** | 95 B | 158 B | Each entry is copied out of LuaJIT. Scripts return small tables; a C# list a script returns is not copied. |
| Loading a quest script | per load | 1.34 ms | 49 µs | **28×** | 2.9 MB | 2,648 B | A 100k-step loop allocates under 400 B against 94 MB. |

**A C# method written with `yield return` can't be walked from a script on either engine.** The compiler's iterator class only implements `GetEnumerator` as an interface method, which neither engine sees. LINQ results work. Methods meant for scripts should return a List or an array.

### Every case

"Single pass" is xLua with the function-exists check folded into the Lua call helper, as in this PR.

**Boundary: Lua to C#**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes |
|---|---|--:|--:|--:|--:|--:|
| C# method, no arguments | per call | 165 ns | 67 ns | 2.5× | 585 B | 0 B |
| C# method, int argument | per call | 207 ns | 73 ns | 2.8× | 665 B | 0 B |
| C# method, double argument | per call | 227 ns | 78 ns | 2.9× | 713 B | 0 B |
| C# method, string argument | per call | 195 ns | 94 ns | 2.1× | 641 B | 64 B |
| C# method, object argument | per call | 195 ns | 85 ns | 2.3× | 641 B | 0 B |
| C# method returning an object | per call | 215 ns | 99 ns | 2.2× | 682 B | 0 B |
| C# method returning a string | per call | 181 ns | 92 ns | 2.0× | 633 B | 0 B |
| Read a C# property | per call | 135 ns | 94 ns | 1.4× | 313 B | 0 B |
| Write a C# property | per call | 113 ns | 80 ns | 1.4× | 193 B | 0 B |
| Read a C# field | per call | 138 ns | 95 ns | 1.5× | 313 B | 0 B |
| Read list[0] | per call | 222 ns | 179 ns | 1.2× | 633 B | 0 B |

**Boundary: C# to Lua**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes | xLua single pass |
|---|---|--:|--:|--:|--:|--:|--:|
| Call a script function, no arguments | per call | 81 ns | 483 ns | 0.17× | 256 B | 216 B | 186 ns |
| Call a script function with two numbers | per call | 254 ns | 611 ns | 0.42× | 920 B | 416 B | 288 ns |
| Check a script function exists | per call | 8.8 ns | 271 ns | 0.03× | 0 B | 32 B |  |

**Collections: Lua walks C#**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes |
|---|---|--:|--:|--:|--:|--:|
| `List<int>`, for i = 0, Count - 1 | per element | 249 ns | 209 ns | 1.2× | 691 B | 4 B |
| `int[]`, for i = 0, Length - 1 | per element | 217 ns | 94 ns | 2.3× | 667 B | 4 B |
| `List<int>`, GetEnumerator / MoveNext | per element | 242 ns | 280 ns | 0.86× | 805 B | 52 B |
| `IEnumerable<int>`, GetEnumerator / MoveNext | per element | 241 ns | 281 ns | 0.86× | 805 B | 52 B |
| `int[]`, GetEnumerator / MoveNext | per element | 274 ns | 266 ns | 1.0× | 892 B | 52 B |
| `Dictionary<string,int>`, enumerator, .Value | per element | 319 ns | 553 ns | 0.58× | 982 B | 92 B |
| List of objects, enumerator, .field | per element | 300 ns | 418 ns | 0.72× | 949 B | 28 B |
| yield method via .ToList(), enumerator | per element | 237 ns | 291 ns | 0.81× | 831 B | 65 B |
| LINQ Where/Select result, enumerator | per source element | 148 ns | 170 ns | 0.87× | 468 B | 29 B |

**Collections: Lua passes to C#**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes |
|---|---|--:|--:|--:|--:|--:|
| Pass a C# List to a `List<int>` parameter | per call | 331 ns | 138 ns | 2.4× | 809 B | 0 B |
| Pass a C# List to an `IEnumerable<int>` parameter | per call | 333 ns | 148 ns | 2.3× | 809 B | 0 B |
| Pass a 100-item Lua table to an `int[]` parameter | per call | 3,988 ns | 2,155 ns | 1.8× | 6,153 B | 424 B |

**Collections: C# walks Lua**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes |
|---|---|--:|--:|--:|--:|--:|
| C# foreach over a returned 100-item Lua table | per element | 34 ns | 135 ns | 0.25× | 95 B | 158 B |
| C# foreach over a returned C# List | per element | 3.6 ns | 9.1 ns | 0.40× | 8 B | 3 B |

**Speed**

| Workload | Per | MoonSharp | xLua | Speedup | MoonSharp bytes | xLua bytes |
|---|---|--:|--:|--:|--:|--:|
| Load a quest script (man0l0) | per run | 1.34 ms | 49 µs | 28× | 2.9 MB | 2,648 B |
| Load a GM command (warp) | per run | 1.50 ms | 119 µs | 13× | 2.9 MB | 6,537 B |
| Lua loop, 100k steps | per run | 38.36 ms | 282 µs | 136× | 89.3 MB | 378 B |
| Build and scan 1,000 tables | per run | 3.49 ms | 95 µs | 37× | 2.0 MB | 377 B |
| Build and join 1,000 strings | per run | 3.01 ms | 29 µs | 103× | 914 KB | 376 B |
| Coroutine start, pause, finish | per run | 409 µs | 1,634 ns | 250× | 2.0 MB | 1,104 B |

### Memory per loaded script

`man0l0.lua` loaded 200 times and kept, .NET heap measured after a full collection, LuaJIT's heap with `collectgarbage("count")`.

| Engine | .NET KB kept | .NET KB allocated per load | LuaJIT KB kept |
|---|--:|--:|--:|
| MoonSharp | 2,789.0 | 2,923.0 | — |
| xLua | 0.0 | 2.6 | 8.2 |

A loaded script costs MoonSharp about 2.7 MB of .NET heap and xLua about 8 KB of LuaJIT heap, because xLua scripts share one Lua state and one set of server bindings. xLua's .NET figure measured slightly below zero (noise) and is shown as 0.

## What the workloads are

- **Load a quest script**: Load `quests/man/man0l0.lua` with everything it requires, as every quest event does.
- **Load a GM command**: Load `commands/gm/warp.lua`, as each `!warp` does.
- **Load battle helpers**: Load `battleutils.lua`, which battle scripts require.
- **Lua loop, 1M steps**: Arithmetic in plain Lua, no calls into the server; where LuaJIT's compiler counts most.
- **C# methods called from Lua**: A script calling a server method 100k times, as scripts do on players and NPCs.
- **Table build and scan**: Build a Lua table of rows, then search it.
- **Lua function called from C#**: The server calling one function in an already loaded script.
- **Coroutine start, pause and finish**: What one `wait()` or NPC dialogue step costs.

CI runs on shared GitHub machines, so the same workload varies between runs: compare engines within one table, not across tables. To rerun the CI set: `dotnet test "Tests/Map Server Tests" --filter "Category=Benchmark" --logger "console;verbosity=detailed"`.
