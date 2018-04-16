using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Xunit;

namespace EasyRegression.Test.Preprocessing.DataExpansion
{
    public static class PolynomialProductDataExpanderTests
    {
        // More tests of Polynomial product expansion in PolynomialTreeTests.cs

        [Fact]
        public static void TestExpand()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 2.0, 3.0 },
                new[] { 3.0, 1.0 },
            };

            var expected = new[]
            {
                new[] { 1.0, 2.0, 1.0, 2.0 },
                new[] { 1.0, 3.0, 2.0, 6.0 },
                new[] { 1.0, 1.0, 3.0, 3.0 },
            };

            // [x0, x1] => [1.0, x1, x0, x0x1]
            var expander = new PolynomialProductDataExpander(order: 1);
            var expanded = expander.Expand(new Matrix<double>(data));

            Assert.Equal(expected.Length, expanded.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Length, expanded[i].Length);

                for (int j = 0; j < expected[i].Length; j++)
                {
                    Assert.Equal(expected[i][j], expanded[i][j]);
                }
            }
        }

        [Fact]
        public static void TestReExpand()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 2.0, 3.0 },
                new[] { 3.0, 1.0 },
            };

            // [x0, x1] => [1.0, x1, x0, x0x1]
            var expander = new PolynomialProductDataExpander(order: 1);
            expander.Expand(new Matrix<double>(data));

            var testing = new[] { 4.0, 5.0 };
            var expected = new[] { 1.0, 5.0, 4.0, 20.0 };
            var actual = expander.ReExpand(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public static void TestInterceptColumnNotSmoothed()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 2.0, 3.0 },
                new[] { 3.0, 1.0 },
            };

            // [x0, x1] => [1.0, x1, x0, x0x1]
            var expander = new PolynomialProductDataExpander(order: 1);
            var expanded = expander.Expand(new Matrix<double>(data));

            var smoother = new DataStandardiser();
            smoother.SetHasIntercept(true);
            var smoothed = smoother.Smooth(expanded);

            Assert.Equal(expanded.Length, smoothed.Length);
            Assert.Equal(expanded.Width, smoothed.Width);

            for (int i = 0; i < expanded.Length; i++)
            {
                Assert.Equal(expanded[i][0], smoothed[i][0]);

                for (int j = 1; j < expanded.Width; j++)
                {
                    Assert.NotEqual(expanded[i][j], smoothed[i][j]);
                }
            }
        }
    }
}