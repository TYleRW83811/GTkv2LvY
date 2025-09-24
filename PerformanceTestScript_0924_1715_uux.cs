// 代码生成时间: 2025-09-24 17:15:41
// PerformanceTestScript.cs
// This script performs performance testing for a MAUI application.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace PerformanceTesting
{
    public class PerformanceTestScript
    {
        private const int TestDurationSeconds = 10;
        private const int WarmupIterations = 5;
        private const int TestIterations = 100;

        public static async Task Main(string[] args)
        {
            try
            {
                // Warm up the application
                Console.WriteLine("Warm-up phase...");
                await WarmUp();

                // Start the performance test
                Console.WriteLine("Starting performance test...");
                var results = await RunPerformanceTest();

                // Output the results
                Console.WriteLine("Performance test results:");
awaiting foreach (var result in results)
                {
                    Console.WriteLine($"Test {result.TestNumber}: {result.ElapsedMilliseconds} ms");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static async Task WarmUp()
        {
            // Perform a series of warm-up runs to ensure the application is ready for testing
            for (int i = 0; i < WarmupIterations; i++)
            {
                await RunApplication();
            }
        }

        private static async Task<TimeSpan> RunApplication()
        {
            // Start the MAUI application and time its execution
            var startTime = Stopwatch.StartNew();

            var mauiApp = MauiApplication.CreateBuilder(args: null)
                .Build();
            await mauiApp.Run();

            var elapsed = startTime.Elapsed;
            return elapsed;
        }

        private static async Task<Result[]> RunPerformanceTest()
        {
            // Run the performance test with a specified number of iterations
            var results = new Result[TestIterations];
            for (int i = 0; i < TestIterations; i++)
            {
                var result = new Result
                {
                    TestNumber = i + 1,
                    ElapsedMilliseconds = await RunApplication().TotalMilliseconds
                };
                results[i] = result;
            }

            return results;
        }

        private class Result
        {
            public int TestNumber { get; set; }
            public double ElapsedMilliseconds { get; set; }
        }
    }
}