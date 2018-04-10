using System;
using EasyRegression.Core.Preprocessing;
using Xunit;

namespace EasyRegression.Test.Integration
{
    public class PreprocessingIntegrationTests
    {
        private readonly int _places = 6;

        private static double[][] TestData()
        {
            return new double[][]
            {
                new[] { 1.0, double.NaN },
                new[] { 2.0, 3.0 },
                new[] { 3.0, 1.0 },
                new[] { double.NaN, 2.0 },
            };
        }

        private static double[][] ExpectedDefaultTestData()
        {
            return new double[][]
            {
                new[] { -1.41421356, 0.0         },
                new[] { 0.0        , 1.41421356  },
                new[] { 1.41421356 , -1.41421356 },
                new[] { 0.0        , 0.0         },
            };
        }

        [Fact]
        public void TestDefault()
        {
            var input = TestData();
            var expected = ExpectedDefaultTestData();

            var preprocessor = new Preprocessor();
            var output = preprocessor.Preprocess(input);

            Assert.Equal(expected.Length, output.Length);
            Assert.Equal(expected[0].Length, output[0].Length);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected[0].Length; j++)
                {
                    Assert.Equal(expected[i][j], output[i][j], _places);
                }
            }
        }
    }
}