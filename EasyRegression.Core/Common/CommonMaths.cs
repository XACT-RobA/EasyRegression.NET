namespace EasyRegression.Core.Common
{
    public class CommonMaths
    {
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
    }
}