using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Preprocessing;
using Xunit;

namespace EasyRegression.Test.Integration
{
    public class PreprocessorIntegrationTests
    {
        private const int _places = 6;
        private const double _nan = double.NaN;

        private static double[][] TestData()
        {
            return new[]
            {
                new[] { 1.0, _nan },
                new[] { 2.0, 3.0 },
                new[] { 3.0, 1.0 },
                new[] { _nan, 2.0 },
            };
        }

        private static double[][] ExpectedDefaultTestData()
        {
            return new[]
            {
                new[] { -1.41421356, 0.0         },
                new[] { 0.0        , 1.41421356  },
                new[] { 1.41421356 , -1.41421356 },
                new[] { 0.0        , 0.0         },
            };
        }

        [Fact]
        public static void TestDefault()
        {
            var input = new Matrix<double>(TestData());
            var expected = new Matrix<double>(ExpectedDefaultTestData());

            var preprocessor = new Preprocessor();
            var output = preprocessor.Preprocess(input);

            Assert.Equal(expected.Length, output.Length);
            Assert.Equal(expected.Width, output.Width);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected.Width; j++)
                {
                    Assert.Equal(expected[i][j], output[i][j], _places);
                }
            }
        }
    }
}