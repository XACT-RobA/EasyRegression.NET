namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public interface IDataSmoother : IPreprocessingPlugin
    {
        // Smooth training dataset so values are all closer to 0
        double[][] Smooth(double[][] data);

        // Smooth testing/prediction data using calculated values
        double[] ReSmooth(double[] data);
    }
}