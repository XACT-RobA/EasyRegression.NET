using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyRegression.Core
{
    public class LinearRegressionEngine : BaseRegressionEngine
    {
        private IPreprocessor _preprocessor;
        private IOptimiser _optimiser;

        public LinearRegressionEngine()
        {
            _preprocessor = new Preprocessor();
            _optimiser = new BatchGradientDescentOptimiser();
        }

        public IPreprocessor GetPreprocessor()
        {
            return _preprocessor;
        }

        public IOptimiser GetOptimiser()
        {
            return _optimiser;
        }

        public void SetPreprocessor(IPreprocessor preprocessor)
        {
            _preprocessor = preprocessor ?? _preprocessor;
        }

        public void SetOptimiser(IOptimiser optimiser)
        {
            _optimiser = optimiser ?? _optimiser;
        }

        public void Train(TrainingModel<double> trainingModel)
        {
            var preprocessedData = _preprocessor.Preprocess(trainingModel);
            _optimiser.Train(preprocessedData.X, preprocessedData.Y);
        }

        public void Train(TrainingModel<double?> trainingModel)
        {
            var preprocessedData = _preprocessor.Preprocess(trainingModel);
            _optimiser.Train(preprocessedData.X, preprocessedData.Y);
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

        public override string Serialise()
        {
            var data = new
            {
                regressionType = GetRegressionType(),
                preprocessor = _preprocessor.Serialise(),
                optimiser = _optimiser.Serialise(),
            };
            return JsonConvert.SerializeObject(data);
        }

        public static LinearRegressionEngine Deserialise(string data)
        {
            var json = JObject.Parse(data);

            var type = json["regressionType"].ToObject<string>();

            if (type != nameof(LinearRegressionEngine)) { return null; }

            var preprocessorData = json["preprocessor"].ToString();
            var optimiserData = json["optimiser"].ToString();

            var preprocessor = Preprocessor.Deserialise(preprocessorData);
            var optimiser = BaseOptimiser.Deserialise(optimiserData);

            var regression = new LinearRegressionEngine();
            regression.SetPreprocessor(preprocessor);
            regression.SetOptimiser(optimiser);
            return regression;
        }
    }
}