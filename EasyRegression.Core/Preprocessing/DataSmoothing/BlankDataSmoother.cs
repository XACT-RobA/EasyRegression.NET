using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class BlankDataSmoother : BaseDataSmoother
    {
        public BlankDataSmoother()
        {
            _subtractors = new double[0];
            _divisors = new double[0];
        }

        public override Matrix<double> Smooth(Matrix<double> input)
        {
            return input;
        }

        public override double[] ReSmooth(double[] data)
        {
            return data;
        }
    }
}