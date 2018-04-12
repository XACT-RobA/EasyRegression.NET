using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public abstract class BaseDataExpander : IDataExpander
    {
        public virtual Matrix<double> Expand(Matrix<double> input)
        {
            return input;
        }

        public virtual double[] ReExpand(double[] data)
        {
            return data;
        }

        public virtual void StoreParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }

        public virtual void LoadParameters(string filepath)
        {
            throw new System.NotImplementedException();
        }
    }
}