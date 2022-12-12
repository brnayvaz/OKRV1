``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19044.2251/21H2/November2021Update)
11th Gen Intel Core i7-11850H 2.50GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2  [AttachedDebugger]
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|            Method |    Mean |    Error |   StdDev |
|------------------ |--------:|---------:|---------:|
| ForEachUnParalled | 6.021 s | 0.0107 s | 0.0100 s |
|     ForEachParell | 3.009 s | 0.0047 s | 0.0044 s |
