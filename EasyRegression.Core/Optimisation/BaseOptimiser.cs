using EasyRegression.Core.Common;

namespace EasyRegression.Core.Optimisation
{
    public class BaseOptimiser : IOptimiser
    {
        public virtual void Train(Matrix<double> x, double[] y)
        {
            throw new System.NotImplementedException();
        }

        public virtual double Predict(double[] x)
        {
            throw new System.NotImplementedException();
        }
    }
}