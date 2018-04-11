using System;
using EasyRegression.Core.Common;
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
            var input = new Matrix<double>(TestData());
            var expected = new Matrix<double>(ExpectedDefaultTestData());

            var preprocessor = new Preprocessor();
            var output = preprocessor.Preprocess(input);

            Assert.Equal(expected.Length, output.Length);
            Assert.Equal(expected.Width, output.Width);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected.Data[0].Length; j++)
                {
                    Assert.Equal(expected.Data[i][j], output.Data[i][j], _places);
                }
            }
        }
    }
}