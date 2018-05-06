using System;
using Xunit;
using EasyRegression.Core;
using System.Linq;

namespace EasyRegression.Test.Common.Models
{
    public static class TrainingModelTests
    {
        public static readonly int _places = 6;
        public static readonly double _error = 1e-6;

        [Fact]
        public static void TestValid()
        {
            var x = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
                new[] { 5.0, 6.0 }
            };

            var y = new[] { 1.5, 3.5, 5.5 };

            var model = new TrainingModel<double>(x, y);

            Assert.Equal(x.Length, model.Length);

            for (int il = 0; il < model.Length; il++)
            {
                Assert.Equal(y[il], model.Y[il]);

                for (int iw = 0; iw < model[il].Length; iw++)
                {
                    Assert.Equal(x[il][iw], model[il][iw]);
                    Assert.Equal(x[il][iw], model.X[il][iw]);
                    Assert.Equal(x[il][iw], model.X.Data[il][iw]);
                }
            }
        }

        [Fact]
        public static void TestXYLengthMismatch()
        {
            var x = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };

            var y = new[] { 1.5, 3.5, 5.5 };

            Assert.Throws<ArgumentException>(() => new TrainingModel<double>(x, y));
        }

        [Fact]
        public static void TestInvalidXDimension()
        {
            var x = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0 },
                new[] { 5.0, 6.0 },
            };

            var y = new[] { 1.5, 3.5, 5.5 };

            Assert.Throws<ArgumentException>(() => new TrainingModel<double>(x, y));
        }

        [Fact]
        public static void TestUpdateX()
        {
            var x = new[] { new[] { 1.0, 2.0 } };
            var newX = new[] { new[] { 3.0, 4.0 } };
            var y = new[] { 1.5 };

            var model = new TrainingModel<double>(x, y);

            model.UpdateX(new Matrix<double>(newX));

            Assert.Equal(newX.Length, model.Length);

            for (int il = 0; il < newX.Length; il++)
            {
                Assert.Equal(y[il], model.Y[il]);
                Assert.Equal(newX[il].Length, model[il].Length);
                Assert.Equal(newX[il].Length, model.X.Width);

                for (int iw = 0; iw < newX[il].Length; iw++)
                {
                    Assert.Equal(newX[il][iw], model[il][iw]);
                }
            }
        }

        [Fact]
        public static void TestInvalidUpdateX()
        {
            var x = new[] { new[] { 1.0, 2.0 } };
            var newX = new[] { new[] { 3.0, 4.0 }, new[] { 5.0, 6.0 } };
            var y = new[] { 1.5 };

            var model = new TrainingModel<double>(x, y);

            Assert.Throws<ArgumentException>(() => model.UpdateX(new Matrix<double>(newX)));
        }

        [Fact]
        public static void TestUpdateY()
        {
            var x = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };

            var y = new[] { 1.5, 3.5 };
            var newY = new[] { 3.0, 7.0 };

            var model = new TrainingModel<double>(x, y);

            model.UpdateY(newY);

            Assert.Equal(newY.Length, model.Y.Length);
            Assert.Equal(newY.Length, model.Length);

            for (int i = 0; i < model.Length; i++)
            {
                Assert.Equal(newY[i], model.Y[i]);
            }
        }

        [Fact]
        public static void TestInvalidUpdateY()
        {
            var x = new[]
            {
                new[] { 1.0, 2.0 },
                new[] { 3.0, 4.0 },
            };

            var y = new[] { 1.5, 3.5 };
            var newY = new[] { 1.5, 3.5, 5.5 };

            var model = new TrainingModel<double>(x, y);

            Assert.Throws<ArgumentException>(() => model.UpdateY(newY));
        }

        [Fact]
        public static void TestUpdateXAndY()
        {
            var x = new[] { new[] { 1.0, 2.0 } };
            var newX = new[] { new[] { 3.0, 4.0 } };
            var y = new[] { 1.5 };
            var newY = new[] { 3.5 };

            var model = new TrainingModel<double>(x, y);

            model.UpdateXAndY(new Matrix<double>(newX), newY);

            Assert.Equal(newX.Length, model.Length);
            Assert.Equal(newY.Length, model.Length);

            for (int il = 0; il < newX.Length; il++)
            {
                Assert.Equal(newY[il], model.Y[il]);
                Assert.Equal(newX[il].Length, model[il].Length);
                Assert.Equal(newX[il].Length, model.X.Width);

                for (int iw = 0; iw < newX[il].Length; iw++)
                {
                    Assert.Equal(newX[il][iw], model[il][iw]);
                }
            }
        }

        [Fact]
        public static void TestInvalidUpdateXAndY()
        {
            var x = new[] { new[] { 1.0, 2.0 } };
            var newX = new[] { new[] { 3.0, 4.0 } };
            var y = new[] { 1.5 };
            var newY = new[] { 3.5, 4.5 };

            var model = new TrainingModel<double>(x, y);

            Assert.Throws<ArgumentException>(() =>
            {
                model.UpdateXAndY(new Matrix<double>(newX), newY);
            });
        }

        [Fact]
        public static void TestFilter()
        {
            var arr = new[]
            {
                1.0, 2.0, 2.0, 2.25, 2.5, 2.5,
                2.75, 3.0, 3.5, 4.0, 5.0, 1000.0,
            };
            
            var x = arr.Select(a => new[] {a})
                          .ToArray();

            var y = arr.Select((a, i) => (double)i)
                       .ToArray();

            // filter anything greater than 10,
            // and anything not evenly divisible by 1
            var expected = x.Where(a => a[0] < 10.0)
                            .Where(a => Math.Abs(a[0] % 1.0) < _error )
                            .ToArray();

            var toFilter = new[] { 3, 4, 5, 6, 8, 11 };

            var model = new TrainingModel<double>(x, y);

            model.Filter(toFilter);

            Assert.Equal(expected.Length, model.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i][0], model[i][0]);
            }
        }
    }
}