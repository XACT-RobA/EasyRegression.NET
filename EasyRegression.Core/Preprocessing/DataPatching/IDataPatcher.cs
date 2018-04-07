namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public interface IDataPatcher : IPreprocessingPlugin
    {
        // Patch null or invalid data for training
        double[][] Patch(double?[][] data);
        double[][] Patch(double[][] data);

        // Patch null or invalid data for predictions/testing
        double[] RePatch(double?[] data);
        double[] RePatch(double[] data);
    }
}