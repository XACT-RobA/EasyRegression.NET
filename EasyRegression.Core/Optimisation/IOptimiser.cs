using EasyRegression.Core.Common;

namespace EasyRegression.Core.Optimisation
{
    public interface IOptimiser
    {
        void Train(Matrix<double> x, double[] y);

        double Predict(double[] x);
    }
}