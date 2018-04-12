using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;

namespace EasyRegression.Core
{
    public class LinearRegressionEngine
    {
        private IPreprocessor _preprocessor;
        private IOptimiser _optimiser;

        public LinearRegressionEngine()
        {
            _preprocessor = new Preprocessor();
            _optimiser = new BatchGradientDescentOptimiser();
        }

        public void SetPreprocessor(IPreprocessor preprocessor)
        {
            _preprocessor = preprocessor ?? _preprocessor;
        }

        public void SetOptimiser(IOptimiser optimiser)
        {
            _optimiser = optimiser ?? _optimiser;
        }

        public void Train(Matrix<double> x, double[] y)
        {
            var preprocessedData = _preprocessor.Preprocess(x);
            _optimiser.Train(preprocessedData, y);
        }

        public void Train(Matrix<double?> x, double[] y)
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