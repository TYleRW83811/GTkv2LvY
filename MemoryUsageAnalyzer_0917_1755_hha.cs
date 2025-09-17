// 代码生成时间: 2025-09-17 17:55:55
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiApp
{
    /// <summary>
    /// A class to analyze memory usage of the application.
    /// </summary>
    public class MemoryUsageAnalyzer
    {
        private const int MaxStringLength = 256;

        private readonly PerformanceCounter memoryPerformanceCounter;

        public MemoryUsageAnalyzer()
        {
            // Create a new PerformanceCounter to track memory usage.
            // CategoryName should be set to 'Process'
            // CounterName should be set to 'Working Set'
            // InstanceName should be set to the process name
            memoryPerformanceCounter = new PerformanceCounter(
                "Process",
                "Working Set",
                Process.GetCurrentProcess().ProcessName
            );
        }

        /// <summary>
        /// Gets the current memory usage of the application.
        /// </summary>
        /// <returns>The current memory usage in bytes.</returns>
        public long GetCurrentMemoryUsage()
        {
            try
            {
                return (long)memoryPerformanceCounter.RawValue;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur when accessing the performance counter.
                // Log the exception and return a default value.
                Console.WriteLine($"Error retrieving memory usage: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Formats the memory usage into a human-readable string.
        /// </summary>
        /// <param name="memoryUsage">The memory usage in bytes.</param>
        /// <returns>A string representing the memory usage in a human-readable format.</returns>
        public string FormatMemoryUsage(long memoryUsage)
        {
            // Define a list of string representations for different units of measurement.
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            double value = (double)memoryUsage;
            int i = 0;

            // Calculate the appropriate suffix for the memory usage.
            while (value >= 1024 && i < sizeSuffixes.Length - 1)
            {
                value /= 1024;
                i++;
            }

            // Return the formatted memory usage string.
            return $