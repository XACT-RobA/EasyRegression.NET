namespace EasyRegression.Core.Common
{
    public static class MathsExtensions
    {
        // Numeric validation
        public static bool IsValidDouble(this double input)
            => CommonMaths.ValidDouble(input);

        public static bool IsValidDouble(this double? input)
            => CommonMaths.ValidDouble(input);

        // Statistics
        // Mean
        public static double Mean(this double[] input)
            => CommonMaths.Mean(input);

        public static double Mean(this double?[] input)
            => CommonMaths.Mean(input);

        public static double ColumnMean(this double[][] input, int column)
            => CommonMaths.ColumnMean(input, column);

        public static double ColumnMean(this double?[][] input, int column)
            => CommonMaths.ColumnMean(input, column);

        // Median
        public static double Median(this double[] input)
            => CommonMaths.Median(input);

        public static double Median(this double?[] input)
            => CommonMaths.Median(input);

        public static double ColumnMedian(this double[][] input, int column)
            => CommonMaths.ColumnMedian(input, column);

        public static double ColumnMedian(this double?[][] input, int column)
            => CommonMaths.ColumnMedian(input, column);
    }
}