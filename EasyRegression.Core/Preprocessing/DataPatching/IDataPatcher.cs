using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public interface IDataPatcher : IPreprocessingPlugin
    {
        // Patch null or invalid data for training
        Matrix<double> Patch(Matrix<double?> input);
        Matrix<double> Patch(Matrix<double> input);

        // Patch null or invalid data for predictions/testing
        double[] RePatch(double?[] data);
        double[] RePatch(double[] data);
    }
}