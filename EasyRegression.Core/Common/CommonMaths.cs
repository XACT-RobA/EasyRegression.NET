using System.Linq;

namespace EasyRegression.Core.Common
{
    public class CommonMaths
    {
        // Numeric validation

        public static bool ValidDouble(double input)
        {
            if (double.IsNaN(input) ||
                double.IsInfinity(input))
            {
                return false;
            }

            return true;
        }

        public static bool ValidDouble(double? input)
        {
            if (input == null) return false;

            return input.Value.IsValidDouble();
        }

        // Statistics
        // Mean

        public static double Mean(double[] input)
        {
            double sum = 0.0;
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].IsValidDouble())
                {
                    sum += input[i];
                    count++;
                }
            }

            if (count < 1) return 1.0;

            return sum / count;
        }

        public static double Mean(double?[] input)
        {
            double sum = 0.0;
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].IsValidDouble())
                {
                    sum += input[i].Value;
                    count++;
                }
            }

            if (count < 1) return 1.0;

            return sum / count;
        }

        public static double ColumnMean(double[][] input, int column)
        {
            double sum = 0.0;
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][column].IsValidDouble())
                {
                    sum += input[i][column];
                    count++;
                }
            }

            if (count < 1) return 1.0;

            return sum / count;
        }

        public static double ColumnMean(double?[][] input, int column)
        {
            double sum = 0.0;
            int count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i][column].IsValidDouble())
                {
                    sum += input[i][column].Value;
                    count++;
                }
            }

            if (count < 1) return 1.0;

            return sum / count;
        }

        // Median

        public static double Median(double[] input)
        {
            var validInputs = input.Where(x => x.IsValidDouble())
                                   .OrderBy(x => x)
                                   .ToArray();

            int validLength = validInputs.Length;
            int halfLength = validLength / 2;

            if (validLength % 2 == 0)
            {
                return validInputs[halfLength];
            }

            var first = validInputs[halfLength - 1];
            var second = validInputs[halfLength];

            return (first + second) / 2.0;
        }

        public static double Median(double?[] input)
        {
            var validInputs = input.Where(x => x.IsValidDouble())
                                   .Select(x => x.Value)
                                   .OrderBy(x => x)
                                   .ToArray();

            int validLength = validInputs.Length;
            int halfLength = validLength / 2;

            if (validLength % 2 == 0)
            {
                return validInputs[halfLength];
            }

            var first = validInputs[halfLength - 1];
            var second = validInputs[halfLength];

            return (first + second) / 2.0;
        }

        public static double ColumnMedian(double[][] input, int column)
        {
            throw new System.NotImplementedException();
        }

        public static double ColumnMedian(double?[][] input, int column)
        {
            throw new System.NotImplementedException();
        }
    }
}