using System;
using EasyRegression.Core.Common.Models;
using EasyRegression.Core.Definitions;
using EasyRegression.Core.Preprocessing.DataExpansion;
using EasyRegression.Core.Preprocessing.DataSmoothing;
using Xunit;

namespace EasyRegression.Test.Preprocessing.DataExpansion
{
    public static class FunctionDataExpanderTests
    {
        private const int _places = 6;

        private static double[][] data =
        {
            new[] { 1.0, 2.0 },
            new[] { 2.0, 3.0 },
        };

        [Fact]
        public static void TestExpandTrig()
        {
            var expected = new[]
            {
                new[] { 1.0, 1.0, Math.Sin(1.0), Math.Cos(1.0), Math.Tan(1.0),
                    2.0, Math.Sin(2.0), Math.Cos(2.0), Math.Tan(2.0), },
                new[] { 1.0, 2.0, Math.Sin(2.0), Math.Cos(2.0), Math.Tan(2.0),
                    3.0, Math.Sin(3.0), Math.Cos(3.0), Math.Tan(3.0), },
            };

            var expander = new FunctionDataExpander(new[] { "sin", "cos", "tan" });
            var expanded = expander.Expand(new Matrix<double>(data));

            Assert.Equal(expected.Length, expanded.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(expected[i].Length, expanded[i].Length);

                for (int j = 0; j < expected[i].Length; j++)
                {
                    Assert.Equal(expected[i][j], expanded[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestExpandLog()
        {
            var expected = new[]
            {
                new[] { 1.0, 1.0, Math.Log(1.0), Math.Log(1.0, 2), Math.Log10(1.0),
                    2.0, Math.Log(2.0), Math.Log(2.0, 2), Math.Log10(2.0), },
                new[] { 1.0, 2.0, Math.Log(2.0), Math.Log(2.0, 2), Math.Log10(2.0),
                    3.0, Math.Log(3.0), Math.Log(3.0, 2), Math.Log10(3.0), },
            };

            var expander = new FunctionDataExpander(new[] { "log", "log2", "log10" });
            var expanded = expander.Expand(new Matrix<double>(data));

            Assert.Equal(expected.Length, expanded.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(expected[i].Length, expanded[i].Length);

                for (int j = 0; j < expected[i].Length; j++)
                {
                    Assert.Equal(expected[i][j], expanded[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestExpandOther()
        {
            var expected = new[]
            {
                new[] { 1.0, 1.0, 1.0, 1.0, 1.0,
                    2.0, Math.Sqrt(2.0), 0.5, 4.0, },
                new[] { 1.0, 2.0, Math.Sqrt(2.0), 0.5, 4.0,
                    3.0, Math.Sqrt(3.0), 1.0 / 3.0, 9.0, },
            };

            PreprocessingDefinitions.DataFunctions.Add("test", x => x * x);

            var expander = new FunctionDataExpander(new[] { "sqrt", "inv", "test" });
            var expanded = expander.Expand(new Matrix<double>(data));

            Assert.Equal(expected.Length, expanded.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(expected[i].Length, expanded[i].Length);

                for (int j = 0; j < expected[i].Length; j++)
                {
                    Assert.Equal(expected[i][j], expanded[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestReExpand()
        {
            var expander = new FunctionDataExpander(new[] { "sin", "cos" });

            var testing = new[] { 4.0, 5.0 };
            var expected = new[] { 1.0, 4.0, Math.Sin(4.0), Math.Cos(4.0),
                5.0, Math.Sin(5.0), Math.Cos(5.0) };
            var actual = expander.ReExpand(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < data.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }

        [Fact]
        public static void TestInterceptColumnNotSmoothed()
        {
            // [x0, x1] => [1.0, x0, x0^2, x1, x1^2]
            var expander = new FunctionDataExpander(new[] { "sin", "cos" });
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
                    Assert.NotEqual(expanded[i][j], smoothed[i][j], _places);
                }
            }
        }

        [Fact]
        public static void TestSerialise()
        {
            var expander = new FunctionDataExpander(new[] { "sqrt", "inv" });
            expander.Expand(new Matrix<double>(data));

            var serialised = expander.Serialise();
            var deserialised = BaseDataExpander.Deserialise(serialised);

            var testing = new[] { 3.0, 5.0 };
            var expected = new[] { 1.0, 3.0, Math.Sqrt(3.0), 1.0 / 3.0,
                5.0, Math.Sqrt(5.0), 0.2 };
            var actual = deserialised.ReExpand(testing);

            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i], _places);
            }
        }
    }
}