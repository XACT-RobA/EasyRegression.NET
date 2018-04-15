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

        public static IRegressionEngine Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var type = json["regressionType"].ToObject<string>();
            
            switch (type)
            {
                case nameof(LinearRegressionEngine):
                    var preprocessorData = json["preprocessor"].ToString();
                    var optimiserData = json["optimiser"].ToString();
                    var preprocessor = Preprocessor.Deserialise(preprocessorData);
                    var optimiser = BaseOptimiser.Deserialise(optimiserData);
                    var regression = new LinearRegressionEngine();
                    regression.SetPreprocessor(preprocessor);
                    regression.SetOptimiser(optimiser);
                    return regression;
                default:
                    return null;
            }
        }
    }
}