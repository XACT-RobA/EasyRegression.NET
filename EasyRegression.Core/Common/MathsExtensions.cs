using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyRegression.Core.Common
{
    public static class MathsExtensions
    {
        // Numeric validation
        public static bool IsValidDouble(this double input)
        {
            return !double.IsNaN(input) && !double.IsInfinity(input);
        }

        public static bool IsValidDouble(this double? input)
        {
            return input != null && IsValidDouble(input.Value);
        }

        // Middle
        public static double Middle(this IEnumerable<double> input)
        {
            if (!input.Any()) return 0.0;

            var array = input.ToArray();
            int length = array.Length;
            int half = length / 2;

            if (length % 2 != 0) return array[half];
            return (array[half] + array[half - 1]) / 2.0;
        }

        // Statistics
        // Mean
        public static double Mean(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble());
            return valids.Any() ? valids.Average() : 0.0;
        }

        public static double Mean(this IEnumerable<double?> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .Select(x => x.Value);
            return valids.Any() ? valids.Average() : 0.0;
        }

        public static double ColumnMean(this IEnumerable<double[]> input, int column)
        {
            var valids = input.Select(arr => arr[column])
                              .Where(x => x.IsValidDouble());
            return valids.Any() ? valids.Average() : 0.0;
        }

        public static double ColumnMean(this IEnumerable<double?[]> input, int column)
        {
            var valids = input.Select(arr => arr[column])
                              .Where(x => x.IsValidDouble())
                              .Select(x => x.Value);
            return valids.Any() ? valids.Average() : 0.0;
        }

        public static double[] ColumnMeans(this IEnumerable<double[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMean(i))
                        .ToArray();
        }

        public static double[] ColumnMeans(this IEnumerable<double?[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMean(i))
                        .ToArray();
        }

        // Median
        public static double Median(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .OrderBy(x => x);
            return valids.Any() ? valids.Middle() : 0.0;
        }

        public static double Median(this IEnumerable<double?> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .Select(x => x.Value)
                              .OrderBy(x => x);
            return valids.Any() ? valids.Middle() : 0.0;
        }

        public static double ColumnMedian(this IEnumerable<double[]> input, int column)
        {
            var valids = input.Select(arr => arr[column])
                              .Where(x => x.IsValidDouble())
                              .OrderBy(x => x);
            return valids.Any() ? valids.Middle() : 0.0;
        }

        public static double ColumnMedian(this IEnumerable<double?[]> input, int column)
        {
            var valids = input.Select(arr => arr[column])
                              .Where(x => x.IsValidDouble())
                              .Select(x => x.Value)
                              .OrderBy(x => x);
            return valids.Any() ? valids.Middle() : 0.0;
        }

        public static double[] ColumnMedians(this IEnumerable<double[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMedian(i))
                        .ToArray();
        }

        public static double[] ColumnMedians(this IEnumerable<double?[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMedian(i))
                        .ToArray();
        }

        // Minimum

        public static double ColumnMinimum(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) return 0.0;
            return input.Select(arr => arr[column])
                        .Min();
        }

        public static double[] ColumnMinimums(this IEnumerable<double[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMinimum(i))
                        .ToArray();
        }

        // Maximum

        public static double ColumnMaximum(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) return 0.0;
            return input.Select(arr => arr[column])
                        .Max();
        }

        public static double[] ColumnMaximums(this IEnumerable<double[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnMaximum(i))
                        .ToArray();
        }

        // Variance

        public static double Variance(this IEnumerable<double> input)
        {
            if (!input.Any()) return 0.0;
            var mean = input.Mean();
            return input.Select(x => x - mean)
                        .Select(x => x * x)
                        .Sum() / input.Count();
        }

        public static double ColumnVariance(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) return 0.0;
            var mean = input.ColumnMean(column);
            return input.Select(arr => arr[column])
                        .Variance();
        }

        public static double[] ColumnVariances(this IEnumerable<double[]> input)
        {
            if (!input.Any()) return new[] { 0.0 };
            return input.First()
                        .Select((x, i) => input.ColumnVariance(i))
                        .ToArray();
        }

        // Standard deviation

        public static double StandardDeviation(this IEnumerable<double> input)
        {
            return Math.Sqrt(input.Variance());
        }

        public static double ColumnStandardDeviation(this IEnumerable<double[]> input, int column)
        {
            return Math.Sqrt(input.ColumnStandardDeviation(column));
        }

        public static double[] ColumnStandardDeviations(this IEnumerable<double[]> input)
        {
            return input.ColumnVariances()
                        .Select(x => Math.Sqrt(x))
                        .ToArray();
        }
    }
}