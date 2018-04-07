namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public interface IDataFilter : IPreprocessingPlugin
    {
        void CalculateOutliers(double[][] data);

        double[][] Filter();
    }
}