using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class BlankDataSmoother : BaseDataSmoother
    {
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