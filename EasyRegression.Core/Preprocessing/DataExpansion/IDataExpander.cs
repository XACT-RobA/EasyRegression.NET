namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public interface IDataExpander : IPreprocessingPlugin
    {
        // Expand data for training
        double[][] Expand(double[][] data);

        // Expand testing/prediction data
        double[] ReExpand(double[] data);
    }
}