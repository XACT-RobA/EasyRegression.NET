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

        public static void DotProduct(this double[][] matrix, double[] vector, ref double[] output)
        {
            int length = matrix.Length;
            int width = vector.Length;

            for (int row = 0; row < length; row++)
            {
                double sum = 0.0;
                for (int column = 0; column < width; column++)
                {
                    sum += matrix[row][column] * vector[column];
                }
                output[row] = sum;
            }
        }

        public static double[] DotProduct(this double[][] matrix, double[] vector)
        {
            var output = new double[vector.Length];
            DotProduct(matrix, vector, ref output);
            return output;
        }
    }
}