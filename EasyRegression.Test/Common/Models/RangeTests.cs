using EasyRegression.Core.Common.Models;
using Xunit;

namespace EasyRegression.Test.Common.Models
{
    public static class RangeTests
    {
        [Fact]
        public static void TestDoubleRange()
        {
            var upper = 5.0;
            var lower = 2.0;

            var range = new Range<double>(upper, lower);

            Assert.Equal(upper, range.Upper);
            Assert.Equal(lower, range.Lower);
        }
    }
}