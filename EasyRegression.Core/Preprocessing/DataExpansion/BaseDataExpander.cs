using System;
using EasyRegression.Core.Common.Models;
using Newtonsoft.Json.Linq;

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
            var json = JObject.Parse(data);

            var type = json["expanderType"].ToObject<string>();

            switch (type)
            {
                case nameof(BlankDataExpander):
                    return new BlankDataExpander();
                case nameof(InterceptDataExpander):
                    return new InterceptDataExpander();
                case nameof(PolynomialProductDataExpander):
                    return new PolynomialProductDataExpander(json["order"].ToObject<int>());
                case nameof(PolynomialDataExpander):
                    return new PolynomialDataExpander(json["order"].ToObject<int>());
                case nameof(FunctionDataExpander):
                    return new FunctionDataExpander(((JArray)json["functions"]).ToObject<string[]>());
                default:
                    return null;
            }
        }
    }
}