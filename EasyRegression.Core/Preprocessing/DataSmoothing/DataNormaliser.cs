namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class DataNormaliser : BaseDataSmoother
    {
        protected override void CalculateParameters(double[][] data)
        {
            int length = data.Length;
            int width = data[0].Length;

            var mins = new double[width];
            var maxes = new double[width];

            for (int iw = 0; iw < width; iw++)
            {
                mins[iw] = double.MaxValue;
                maxes[iw] = double.MinValue;
            }

            for (int il = 0; il < length; il++)
            {
                for (int iw = 0; iw < width; iw++)
                {
                    var value = data[il][iw];
                    if (value < mins[iw]) mins[iw] = value;
                    if (value > maxes[iw]) maxes[iw] = value;
                }
            }

            _subtractors = new double[width];
            _divisors = new double[width];

            for (int iw = 0; iw < width; iw++)
            {
                _subtractors[iw] = mins[iw];
                _divisors[iw] = maxes[iw] - mins[iw];
            }
        }
    }
}