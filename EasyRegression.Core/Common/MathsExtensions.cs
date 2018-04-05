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
    }
}