using System;
using Xunit;
using EasyRegression.Core;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Optimisation;

namespace EasyRegression.Test.Integration
{
    public static class LinearRegressionEngineIntegrationTests
    {
        // data from:
        // http://openclassroom.stanford.edu/MainFolder/DocumentPage.php?course=MachineLearning&doc=exercises/ex3/ex3.html
        private static double[][] trainingXData
        {
            get
            {
                return new[]
                {
                    new[] { 2.1040000e+03, 3.0 },
                    new[] { 1.6000000e+03, 3.0 },
                    new[] { 2.4000000e+03, 3.0 },
                    new[] { 1.4160000e+03, 2.0 },
                    new[] { 3.0000000e+03, 4.0 },
                    new[] { 1.9850000e+03, 4.0 },
                    new[] { 1.5340000e+03, 3.0 },
                    new[] { 1.4270000e+03, 3.0 },
                    new[] { 1.3800000e+03, 3.0 },
                    new[] { 1.4940000e+03, 3.0 },
                    new[] { 1.9400000e+03, 4.0 },
                    new[] { 2.0000000e+03, 3.0 },
                    new[] { 1.8900000e+03, 3.0 },
                    new[] { 4.4780000e+03, 5.0 },
                    new[] { 1.2680000e+03, 3.0 },
                    new[] { 2.3000000e+03, 4.0 },
                    new[] { 1.3200000e+03, 2.0 },
                    new[] { 1.2360000e+03, 3.0 },
                    new[] { 2.6090000e+03, 4.0 },
                    new[] { 3.0310000e+03, 4.0 },
                    new[] { 1.7670000e+03, 3.0 },
                    new[] { 1.8880000e+03, 2.0 },
                    new[] { 1.6040000e+03, 3.0 },
                    new[] { 1.9620000e+03, 4.0 },
                    new[] { 3.8900000e+03, 3.0 },
                    new[] { 1.1000000e+03, 3.0 },
                    new[] { 1.4580000e+03, 3.0 },
                    new[] { 2.5260000e+03, 3.0 },
                    new[] { 2.2000000e+03, 3.0 },
                    new[] { 2.6370000e+03, 3.0 },
                    new[] { 1.8390000e+03, 2.0 },
                    new[] { 1.0000000e+03, 1.0 },
                    new[] { 2.0400000e+03, 4.0 },
                    new[] { 3.1370000e+03, 3.0 },
                    new[] { 1.8110000e+03, 4.0 },
                    new[] { 1.4370000e+03, 3.0 },
                    new[] { 1.2390000e+03, 3.0 },
                    new[] { 2.1320000e+03, 4.0 },
                    new[] { 4.2150000e+03, 4.0 },
                    new[] { 2.1620000e+03, 4.0 },
                    new[] { 1.6640000e+03, 2.0 },
                    new[] { 2.2380000e+03, 3.0 },
                    new[] { 2.5670000e+03, 4.0 },
                    new[] { 1.2000000e+03, 3.0 },
                    new[] { 8.5200000e+02, 2.0 },
                    new[] { 1.8520000e+03, 4.0 },
                    new[] { 1.2030000e+03, 3.0 },
                };
            }
        }

        private static double[] trainingYData
        {
            get
            {
                return new[]
                { 
                    3.9990000e+05, 3.2990000e+05, 3.6900000e+05, 2.3200000e+05,
                    5.3990000e+05, 2.9990000e+05, 3.1490000e+05, 1.9899900e+05,
                    2.1200000e+05, 2.4250000e+05, 2.3999900e+05, 3.4700000e+05,
                    3.2999900e+05, 6.9990000e+05, 2.5990000e+05, 4.4990000e+05,
                    2.9990000e+05, 1.9990000e+05, 4.9999800e+05, 5.9900000e+05,
                    2.5290000e+05, 2.5500000e+05, 2.4290000e+05, 2.5990000e+05,
                    5.7390000e+05, 2.4990000e+05, 4.6450000e+05, 4.6900000e+05,
                    4.7500000e+05, 2.9990000e+05, 3.4990000e+05, 1.6990000e+05,
                    3.1490000e+05, 5.7990000e+05, 2.8590000e+05, 2.4990000e+05,
                    2.2990000e+05, 3.4500000e+05, 5.4900000e+05, 2.8700000e+05,
                    3.6850000e+05, 3.2990000e+05, 3.1400000e+05, 2.9900000e+05,
                    1.7990000e+05, 2.9990000e+05, 2.3950000e+05,
                };
            }
        }

        [Fact]
        public static void TestDefault()
        {
            var xData = new Matrix<double>(trainingXData);

            var engine = new LinearRegressionEngine();
            engine.Train(xData, trainingYData);

            var testing = new[] { 1650.0, 3.0 };
            var expected = 293081.0;
            var actual = engine.Predict(testing);

            Assert.InRange(actual, expected, expected + 1.0);
        }

        [Fact]
        public static void TestHigherLearning()
        {
            var xData = new Matrix<double>(trainingXData);

            var optimiser = new BatchGradientDescentOptimiser();
            optimiser.SetLearningRate(1.0);

            var engine = new LinearRegressionEngine();
            engine.SetOptimiser(optimiser);
            
            engine.Train(xData, trainingYData);

            var testing = new[] { 1650.0, 3.0 };
            var expected = 293081.0;
            var actual = engine.Predict(testing);

            Assert.InRange(actual, expected, expected + 1.0);
        }

        [Fact]
        public static void TestSerialise()
        {
            var xData = new Matrix<double>(trainingXData);

            var engine = new LinearRegressionEngine();
            engine.Train(xData, trainingYData);

            var serialised = engine.Serialise();
            var deserialised = LinearRegressionEngine.Deserialise(serialised);

            var testing = new[] { 1650.0, 3.0 };
            var expected = 293081.0;
            var actual = deserialised.Predict(testing);

            Assert.InRange(actual, expected, expected + 1.0);
        }
    }
}