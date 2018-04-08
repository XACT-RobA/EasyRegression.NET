using System;
using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;

namespace EasyRegression.Core
{
    public class LinearRegressionEngine
    {
        private IPreprocessor _preprocessor;
        private IOptimiser _optimiser;

        public LinearRegressionEngine(IPreprocessor preprocessor = null,
            IOptimiser optimiser = null)
        {
            _preprocessor = preprocessor ?? new Preprocessor();
            _optimiser = optimiser ?? new Optimiser();
        }

        public void Train(double[][] x, double[] y)
        {
            throw new NotImplementedException();
        }

        public void Train(double?[][] x, double[] y)
        {
            throw new NotImplementedException();
        }

        public double Predict(double[] input)
        {
            throw new NotImplementedException();
        }

        public double Predict(double?[] input)
        {
            throw new NotImplementedException();
        }
    }
}