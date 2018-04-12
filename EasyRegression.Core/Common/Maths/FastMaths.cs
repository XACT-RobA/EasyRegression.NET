namespace EasyRegression.Core.Common.Maths
{
    public static class FastMaths
    {
        public static double Mean(double[] input)
        {
            int length = input.Length;
            if (length == 0)
            {
                return double.NaN;
            }

            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
                sum += input[i];
            }

            return sum / length;
        }

        public static double ColumnMean(Matrix<double> input, int column)
        {
            int length = input.Length;
            if (length == 0)
            {
                return double.NaN;
            }

            double sum = 0.0;
            for (int i = 0; i < length; i++)
            {
               sum += input[i][column];
            }

            return sum / length;
        }

        public static double[] ColumnMeans(Matrix<double> input)
        {
            int length = input.Length;
            if (length == 0)
            {
                return new[] { double.NaN };
            }

            int width = input.Width;

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
