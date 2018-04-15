using EasyRegression.Core.Common.Models;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core.Optimisation
{
    public class BaseOptimiser : IOptimiser
    {
        public string GetOptimiserType()
        {
            return this.GetType().Name;
        }

        public virtual void Train(Matrix<double> x, double[] y)
        {
            throw new System.NotImplementedException();
        }

        public virtual double Predict(double[] x)
        {
            throw new System.NotImplementedException();
        }

        public virtual string Serialise()
        {
            throw new System.NotImplementedException();
        }

        public static IOptimiser Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var type = json["optimiserType"].ToObject<string>();

            switch (type)
            {
                case nameof(BatchGradientDescentOptimiser):
                    var parameters = ((JArray)json["parameters"]).ToObject<double[]>();
                    return new BatchGradientDescentOptimiser(parameters);
                default:
                    return null;
            }
        }
    }
}