namespace EasyRegression.Core.Common
{
    public class FastMaths
    {
        public static double Mean(double[] input)
        {
            int length = input.Length;
            if (length == 0)
            {
                return 0.0;
            }

            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
                sum += input[i];
            }

            return sum / length;
        }

        public static double ColumnMean(double[][] input, int column)
        {
            int length = input.Length;
            if (length == 0)
            {
                return 0.0;
            }

            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
               sum += input[i][column];
            }

            return sum / length;
        }

        public static double[] ColumnMeans(double[][] input)
        {
            int length = input.Length;
            if (length == 0)
            {
                return new[] { 0.0 };
            }

            int width = input[0].Length;

            double[] means = new double[width];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    means[j] += input[i][j];
                }
            }

            for (int j = 0; j < width; j++)
            {
                means[j] /= length;
            }

            return means;
        }
    }
}