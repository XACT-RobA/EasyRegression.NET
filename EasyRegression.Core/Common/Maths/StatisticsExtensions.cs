using System;
using System.Collections.Generic;
using System.Linq;
using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Common.Maths
{
    public static class StatisticsExtensions
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
            if (!input.Any()) { return double.NaN; }

            var array = input.ToArray();
            int length = array.Length;
            int half = length / 2;

            if (length % 2 != 0) { return array[half]; }

            return (array[half] + array[half - 1]) / 2.0;
        }

        // Statistics
        // Mean
        public static double Mean(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble());

            return valids.Any() ? valids.Average() : double.NaN;
        }

        public static double Mean(this IEnumerable<double?> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .Select(x => x.Value);

            return valids.Any() ? valids.Average() : double.NaN;
        }

        public static double ColumnMean(this IEnumerable<double[]> input, int column)
        {
            return input.Select(arr => arr[column])
                        .Mean();
        }

        public static double ColumnMean(this IEnumerable<double?[]> input, int column)
        {
            return input.Select(arr => arr[column])
                        .Mean();
        }

        public static double[] ColumnMeans(this IEnumerable<double[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMean(i))
                        .ToArray();
        }

        public static double[] ColumnMeans(this IEnumerable<double?[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMean(i))
                        .ToArray();
        }

        // Median
        public static double Median(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .OrderBy(x => x);

            return valids.Any() ? valids.Middle() : double.NaN;
        }

        public static double Median(this IEnumerable<double?> input)
        {
            var valids = input.Where(x => x.IsValidDouble())
                              .Select(x => x.Value)
                              .OrderBy(x => x);

            return valids.Any() ? valids.Middle() : double.NaN;
        }

        public static double ColumnMedian(this IEnumerable<double[]> input, int column)
        {
            return input.Select(arr => arr[column])
                        .Median();
        }

        public static double ColumnMedian(this IEnumerable<double?[]> input, int column)
        {
            return input.Select(arr => arr[column])
                        .Median();
        }

        public static double[] ColumnMedians(this IEnumerable<double[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMedian(i))
                        .ToArray();
        }

        public static double[] ColumnMedians(this IEnumerable<double?[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMedian(i))
                        .ToArray();
        }

        // Minimum

        public static double ColumnMinimum(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) { return double.NaN; }

            return input.Select(arr => arr[column])
                        .Where(x => x.IsValidDouble())
                        .Min();
        }

        public static double[] ColumnMinimums(this IEnumerable<double[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMinimum(i))
                        .ToArray();
        }

        // Maximum

        public static double ColumnMaximum(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) { return double.NaN; }

            var valids = input.Select(arr => arr[column])
                              .Where(x => x.IsValidDouble());

            return valids.Any() ? valids.Max() : double.NaN;
        }

        public static double[] ColumnMaximums(this IEnumerable<double[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }

            return input.First()
                        .Select((x, i) => input.ColumnMaximum(i))
                        .ToArray();
        }

        // Variance

        public static double Variance(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble());

            if (!valids.Any()) { return double.NaN; }

            var mean = valids.Mean();
            return valids.Select(x => x - mean)
                         .Select(x => x * x)
                         .Sum() / valids.Count();
        }

        public static double ColumnVariance(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any()) { return double.NaN; }
            
            return input.Select(arr => arr[column])
                        .Variance();
        }

        public static double[] ColumnVariances(this IEnumerable<double[]> input)
        {
            if (!input.Any()) { return new[] { double.NaN }; }
            
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
            return Math.Sqrt(input.ColumnVariance(column));
        }

        public static double[] ColumnStandardDeviations(this IEnumerable<double[]> input)
        {
            return input.ColumnVariances()
                        .Select(x => Math.Sqrt(x))
                        .ToArray();
        }

        // Quartiles

        public static Range<double> Quartile(this IEnumerable<double> input)
        {
            var valids = input.Where(x => x.IsValidDouble());
            if (!valids.Any())
            {
                return new Range<double>(double.NaN, double.NaN);
            }

            var sorted = valids.OrderBy(x => x);

            var length = sorted.Count();
            var half = length / 2;

            IEnumerable<double> first;
            IEnumerable<double> second;

            if (length % 2 == 0)
            {
                first = sorted.Take(half);
                second = sorted.Skip(half).Take(half);
            }
            else
            {
                first = sorted.Take(half + 1);
                second = sorted.Skip(half).Take(half + 1);
            }

            return new Range<double>(second.Median(), first.Median());
        }

        public static Range<double> ColumnQuartile(this IEnumerable<double[]> input, int column)
        {
            if (!input.Any())
            {
                return new Range<double>(double.NaN, double.NaN);
            }
            
            return input.Select(arr => arr[column])
                        .Quartile();
        }

        public static Range<double>[] ColumnQuartiles(this IEnumerable<double[]> input)
        {
            if (!input.Any())
            {
                return new[] { new Range<double>(double.NaN, double.NaN) };
            }
            
            return input.First()
                        .Select((x, i) => input.ColumnQuartile(i))
                        .ToArray();
        }
    }
}