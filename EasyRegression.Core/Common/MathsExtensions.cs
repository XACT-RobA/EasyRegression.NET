namespace EasyRegression.Core.Common
{
    public static class MathsExtensions
    {
        public static double Mean(this double[] input) => CommonMaths.Mean(input);

        public static double Mean(this double?[] input) => CommonMaths.Mean(input);

        public static bool IsValidDouble(this double input) => CommonMaths.ValidDouble(input);

        public static bool IsValidDouble(this double? input) => CommonMaths.ValidDouble(input);

        public static double ColumnMean(this double[][] input, int column) => CommonMaths.ColumnMean(input, column);

        public static double ColumnMean(this double?[][] input, int column) => CommonMaths.ColumnMean(input, column);
    }
}