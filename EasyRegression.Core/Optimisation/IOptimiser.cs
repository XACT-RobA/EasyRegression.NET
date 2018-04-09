namespace EasyRegression.Core.Optimisation
{
    public interface IOptimiser
    {
        void Train(double[][] x, double[] y);

        double Predict(double[] x);
    }
}