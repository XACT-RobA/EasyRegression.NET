namespace EasyRegression.Core.Preprocessing.DataSmoothing
{
    public class BlankDataSmoother : BaseDataSmoother
    {
        public override double[][] Smooth(double[][] data)
        {
            return data;
        }

        public override double[] ReSmooth(double[] data)
        {
            return data;
        }
    }
}