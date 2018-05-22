using System;
using Xunit;
using EasyRegression.Core;
using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;
using System.Linq;

namespace EasyRegression.Test.Optimisation
{
    public static class MiniBatchGradientDescentTests
    {
        [Fact]
        public static void TestDefault()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var preprocessor = new Preprocessor();
            var preprocessedData = preprocessor.Preprocess(trainingData);
            var reprocessed = preprocessor.Reprocess(OptimisationTestData.TestingXData);

            var optimiser = new MiniBatchGradientDescentOptimiser();
            optimiser.Train(preprocessedData.X, preprocessedData.Y);

            var actual = optimiser.Predict(reprocessed);
            var expected = OptimisationTestData.TestingYData;

            Assert.True(optimiser.HasConverged);
            Assert.False(optimiser.HasDiverged);
            Assert.InRange<double>(actual, expected * 0.99, expected * 1.01);
        }

        [Fact]
        public static void TestSmallBatch()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var preprocessor = new Preprocessor();
            var preprocessedData = preprocessor.Preprocess(trainingData);
            var reprocessed = preprocessor.Reprocess(OptimisationTestData.TestingXData);

            var optimiser = new MiniBatchGradientDescentOptimiser();
            optimiser.SetRandom(new Random(0));
            optimiser.SetBatchSize(10);
            optimiser.SetLearningRate(0.01);
            optimiser.SetMaxIterations(1000);
            optimiser.Train(preprocessedData.X, preprocessedData.Y);

            var actual = optimiser.Predict(reprocessed);
            var expected = OptimisationTestData.TestingYData;
            
            Assert.InRange<double>(actual, expected * 0.99, expected * 1.01);
        }
    }
}