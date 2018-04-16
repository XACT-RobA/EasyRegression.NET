using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Xunit;

namespace EasyRegression.Test.Preprocessing.DataExpansion
{
    public static class BlankDataExpanderTests
    {
        [Fact]
        public static void TestExpand()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0, 3.0 },
                new[] { 2.0, 3.0, 1.0 },
                new[] { 3.0, 1.0, 2.0 },
            };

            // [x0, x1] => [x0, x1]
            var expander = new BlankDataExpander();
            var expanded = expander.Expand(new Matrix<double>(data));

            Assert.Equal(data.Length, expanded.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(data[i].Length, expanded[i].Length);

                for (int j = 0; j < data[i].Length; j++)
                {
                    Assert.Equal(data[i][j], expanded[i][j]);
                }
            }
        }

        [Fact]
        public static void TestReExpand()
        {
            var data = new[]
            {
                new[] { 1.0, 2.0, 3.0 },
                new[] { 2.0, 3.0, 1.0 },
                new[] { 3.0, 1.0, 2.0 },
            };

            // [x0, x1] => [x0, x1]
            var expander = new BlankDataExpander();
            expander.Expand(new Matrix<double>(data));

            var testing = new[] { 3.0, 2.0, 1.0 };
            var expected = new[] { 3.0, 2.0, 1.0 };
            var actual = expander.ReExpand(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}