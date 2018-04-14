using System;

namespace EasyRegression.Core.Common.Models
{
    public struct Range<T> : IEquatable<Range<T>>
    {
        public Range(T upper, T lower)
        {
            Upper = upper;
            Lower = lower;
        }

        public T Upper { get; set; }
        public T Lower { get; set; }

        public bool Equals(Range<T> other)
        {
            return other.Upper.Equals(Upper) &&
                other.Lower.Equals(Lower);
        }
    }
}