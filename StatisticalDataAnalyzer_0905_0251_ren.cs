// 代码生成时间: 2025-09-05 02:51:06
// StatisticalDataAnalyzer.cs
// This class provides functionality for analyzing statistical data.

using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticalAnalysis
{
    /// <summary>
    /// Analyzes statistical data and provides various metrics.
    /// </summary>
    public class StatisticalDataAnalyzer
    {
        private List<double> data;

        /// <summary>
        /// Initializes a new instance of the StatisticalDataAnalyzer class.
        /// </summary>
        /// <param name="data">A list of double values representing the statistical data.</param>
        public StatisticalDataAnalyzer(List<double> data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data), "Data cannot be null.");
        }

        /// <summary>
        /// Calculates the mean of the data.
        /// </summary>
        /// <returns>The mean of the data.</returns>
        public double CalculateMean()
        {
            if (data.Count == 0)
            {
                throw new InvalidOperationException("Cannot calculate mean of an empty dataset.");
            }

            return data.Average();
        }

        /// <summary>
        /// Calculates the median of the data.
        /// </summary>
        /// <returns>The median of the data.</returns>
        public double CalculateMedian()
        {
            if (data.Count == 0)
            {
                throw new InvalidOperationException("Cannot calculate median of an empty dataset.");
            }

            var sortedData = data.OrderBy(x => x).ToList();
            int middle = sortedData.Count / 2;
            if (sortedData.Count % 2 == 0)
            {
                return (sortedData[middle - 1] + sortedData[middle]) / 2.0;
            }
            else
            {
                return sortedData[middle];
            }
        }

        /// <summary>
        /// Calculates the mode of the data.
        /// </summary>
        /// <returns>The mode of the data.</returns>
        public List<double> CalculateMode()
        {
            if (data.Count == 0)
            {
                throw new InvalidOperationException("Cannot calculate mode of an empty dataset.");
            }

            return data.GroupBy(x => x)
                        .Select(group => new { Value = group.Key, Count = group.Count() })
                        .OrderByDescending(x => x.Count)
                        .ThenBy(x => x.Value)
                        .Select(x => x.Value)
                        .ToList();
        }

        /// <summary>
        /// Calculates the standard deviation of the data.
        /// </summary>
        /// <returns>The standard deviation of the data.</returns>
        public double CalculateStandardDeviation()
        {
            if (data.Count == 0)
            {
                throw new InvalidOperationException("Cannot calculate standard deviation of an empty dataset.");
            }

            double mean = data.Average();
            double variance = data.Average(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(variance);
        }

        // Additional statistical calculations can be added here following a similar pattern.
    }
}
