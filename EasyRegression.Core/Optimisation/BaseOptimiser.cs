using System;
using EasyRegression.Core.Common.Maths;
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
                    return new BatchGradientDescentOptimiser(((JArray)json["parameters"]).ToObject<double[]>());
                case nameof(MiniBatchGradientDescentOptimiser):
                    return new MiniBatchGradientDescentOptimiser(((JArray)json["parameters"]).ToObject<double[]>());
                default:
                    return null;
            }
        }

        protected void ValidateLearningRate(double learningRate)
        {
            if (learningRate <= 0 ||
                !learningRate.IsValidDouble())
            {
                throw new ArgumentException("Learning rate must be greater than 0");
            }
        }

        protected void ValidateMaxIterations(int maxIterations)
        {
            if (maxIterations <= 0)
            {
                throw new ArgumentException("Maximum iterations must be greater than 0");
            };
        }

        protected void ValidateConvergenceLimit(double convergenceLimit)
        {
            if (convergenceLimit <= 0 ||
                !convergenceLimit.IsValidDouble())
            {
                throw new ArgumentException("Convergence limit must be greater than 0");
            };
        }
    }
}