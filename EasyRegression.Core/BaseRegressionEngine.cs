using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core
{
    public class BaseRegressionEngine : IRegressionEngine
    {
        public virtual string GetRegressionType()
        {
            return this.GetType().Name;
        }

        public virtual string Serialise()
        {
            throw new System.NotImplementedException();
        }
    }
}