using System;
using Xunit;
using EasyRegression.Core;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Optimisation;
using EasyRegression.Test.Optimisation;

namespace EasyRegression.Test.Integration
{
    public static class LinearRegressionEngineIntegrationTests
    {
        [Fact]
        public static void TestDefault()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var engine = new LinearRegressionEngine();
            engine.Train(trainingData);

            var actual = engine.Predict(OptimisationTestData.TestingXData);

            Assert.InRange(actual, OptimisationTestData.TestingYData - 1.0, OptimisationTestData.TestingYData + 1.0);
        }

        [Fact]
        public static void TestHigherLearning()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var optimiser = new BatchGradientDescentOptimiser();
            optimiser.SetLearningRate(1.0);

            var engine = new LinearRegressionEngine();
            engine.SetOptimiser(optimiser);            
            engine.Train(trainingData);

            var actual = engine.Predict(OptimisationTestData.TestingXData);

            Assert.InRange(actual, OptimisationTestData.TestingYData - 1.0, OptimisationTestData.TestingYData + 1.0);
        }

        [Fact]
        public static void TestSerialise()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var engine = new LinearRegressionEngine();
            engine.Train(trainingData);

            var serialised = engine.Serialise();
            var deserialised = LinearRegressionEngine.Deserialise(serialised);

            var actual = deserialised.Predict(OptimisationTestData.TestingXData);

            Assert.InRange(actual, OptimisationTestData.TestingYData - 1.0, OptimisationTestData.TestingYData + 1.0);
        }
    }
}