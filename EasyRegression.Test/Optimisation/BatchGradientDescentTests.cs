using EasyRegression.Core.Optimisation;
using EasyRegression.Core.Preprocessing;
using Xunit;

namespace EasyRegression.Test.Optimisation
{
    public static class BatchGradientDescentTests
    {
        [Fact]
        public static void TestDefault()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var preprocessor = new Preprocessor();
            var preprocessedData = preprocessor.Preprocess(trainingData);
            var reprocessed = preprocessor.Reprocess(OptimisationTestData.TestingXData);

            var optimiser = new BatchGradientDescentOptimiser();
            optimiser.Train(preprocessedData.X, preprocessedData.Y);

            var result = optimiser.Predict(reprocessed);
            var expected = OptimisationTestData.TestingYData;

            Assert.True(optimiser.HasConverged);
            Assert.False(optimiser.HasDiverged);
            Assert.InRange<double>(result, expected * 0.99, expected * 1.01);
        }

        [Fact]
        public static void TestHigherLearningRate()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var preprocessor = new Preprocessor();
            var preprocessedData = preprocessor.Preprocess(trainingData);
            var reprocessed = preprocessor.Reprocess(OptimisationTestData.TestingXData);

            var optimiser = new BatchGradientDescentOptimiser();
            optimiser.SetLearningRate(1.0);
            optimiser.Train(preprocessedData.X, preprocessedData.Y);

            var result = optimiser.Predict(reprocessed);
            var expected = OptimisationTestData.TestingYData;

            Assert.True(optimiser.HasConverged);
            Assert.False(optimiser.HasDiverged);
            Assert.InRange<double>(result, expected * 0.99, expected * 1.01);
        }

        [Fact]
        public static void TestTooHighLearningRate()
        {
            var trainingData = OptimisationTestData.GetTrainingModel();

            var preprocessor = new Preprocessor();
            var preprocessedData = preprocessor.Preprocess(trainingData);
            var reprocessed = preprocessor.Reprocess(OptimisationTestData.TestingXData);

            var optimiser = new BatchGradientDescentOptimiser();
            optimiser.SetLearningRate(2.0);
            optimiser.Train(preprocessedData.X, preprocessedData.Y);

            Assert.False(optimiser.HasConverged);
            Assert.True(optimiser.HasDiverged);
        }
    }
}