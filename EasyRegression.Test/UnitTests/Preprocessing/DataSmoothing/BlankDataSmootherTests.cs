using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Xunit;

namespace EasyRegression.Test.Preprocessing.DataSmoothing
{
    public static class BlankDataSmootherTests
    {
        private const int _places = 6;

        private static double[][] trainingData
        {
            get
            {
                return new[]
                {
                    new[] { 1.0,  2.0, 5.0 },
                    new[] { 1.0,  3.0, 4.0 },
                    new[] { -1.0, 4.0, 1.0 },
                };
            }
        }

        [Fact]
        public static void TestSmooth()
        {
            var blankSmoother = new BlankDataSmoother();

            var matrix = new Matrix<double>(trainingData);
            var expected = new Matrix<double>(trainingData);
            var actual = blankSmoother.Smooth(matrix);

            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.Width, actual.Width);

            for (int i = 0; i < expected.Length; i++)
            {
                for (int j = 0; j < expected.Width; j++)
                {
                    Assert.Equal(expected[i][j], actual[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestReSmooth()
        {
            var matrix = new Matrix<double>(trainingData);
            var blankSmoother = new BlankDataSmoother();
            blankSmoother.Smooth(matrix);

            var testing = new[] { 0.5, 2.5, 2.0 };
            var expected = new[] { 0.5, 2.5, 2.0 };
            var actual = blankSmoother.ReSmooth(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestSerialise()
        {
            var blankSmoother = new BlankDataSmoother();
            blankSmoother.Smooth(new Matrix<double>(trainingData));

            var serialised = blankSmoother.Serialise();
            var deserialised = BaseDataSmoother.Deserialise(serialised);

            var testing = new[] { 0.5, 2.5, 2.0 };
            var expected = new[] { 0.5, 2.5, 2.0 };
            var actual = deserialised.ReSmooth(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}