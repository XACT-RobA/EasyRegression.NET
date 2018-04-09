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
            _optimiser = optimiser ?? new BatchGradientDescentOptimiser();
        }

        public void Train(double[][] x, double[] y)
        {
            var preprocessedData = _preprocessor.Preprocess(x);
            _optimiser.Train(preprocessedData, y);
        }

        public void Train(double?[][] x, double[] y)
        {
            var preprocessedData = _preprocessor.Preprocess(x);
            _optimiser.Train(preprocessedData, y);
        }

        public double Predict(double[] input)
        {
            var preprocessedData = _preprocessor.Reprocess(input);
            return _optimiser.Predict(preprocessedData);
        }

        public double Predict(double?[] input)
        {
            var preprocessedData = _preprocessor.Reprocess(input);
            return _optimiser.Predict(preprocessedData);
        }
    }
}