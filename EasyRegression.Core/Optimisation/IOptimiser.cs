using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Optimisation
{
    public interface IOptimiser
    {
        void Train(Matrix<double> x, double[] y);

        double Predict(double[] x);

        string Serialise();

        string GetOptimiserType();
    }
}