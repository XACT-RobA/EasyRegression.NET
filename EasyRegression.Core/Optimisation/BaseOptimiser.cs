namespace EasyRegression.Core.Optimisation
{
    public class BaseOptimiser : IOptimiser
    {
        public virtual void Train(double[][] x, double[] y)
        {
            throw new System.NotImplementedException();
        }

        public virtual double Predict(double[] x)
        {
            throw new System.NotImplementedException();
        }
    }
}