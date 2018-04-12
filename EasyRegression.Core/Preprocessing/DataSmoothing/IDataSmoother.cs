using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public interface IDataSmoother : IPreprocessingPlugin
    {
        // Smooth training dataset so values are all closer to 0
        Matrix<double> Smooth(Matrix<double> input);

        // Smooth testing/prediction data using calculated values
        double[] ReSmooth(double[] data);
    }
}