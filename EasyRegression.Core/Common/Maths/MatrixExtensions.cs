namespace EasyRegression.Core.Common.Maths
{
    public static class MatrixExtensions
    {
        public static double DotProduct(this double[] a, double[] b)
        {
            double output = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                output += a[i] * b[i];
            }
            return output;
        }

        public static double[] DotProduct(this Matrix<double> matrix, double[] vector, double[] output)
        {
            int length = matrix.Length;
            int width = matrix.Width;

            for (int row = 0; row < length; row++)
            {
                double sum = 0.0;
                for (int column = 0; column < width; column++)
                {
                    sum += matrix[row][column] * vector[column];
                }
                output[row] = sum;
            }

            return output;
        }

        public static double[] DotProduct(this Matrix<double> matrix, double[] vector)
        {
            var output = new double[matrix.Width];
            return DotProduct(matrix, vector, output);
        }
    }
}
