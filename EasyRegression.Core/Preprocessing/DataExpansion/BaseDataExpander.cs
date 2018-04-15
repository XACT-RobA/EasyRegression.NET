using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public abstract class BaseDataExpander : BasePreprocessingPlugin, IDataExpander
    {
        public virtual Matrix<double> Expand(Matrix<double> input)
        {
            return input;
        }

        public virtual double[] ReExpand(double[] data)
        {
            return data;
        }
        
        public virtual bool HasIntercept()
        {
            return false;
        }

        public override string Serialise()
        {
            throw new System.NotImplementedException();
        }

        public static IDataExpander Deserialise(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}