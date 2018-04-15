using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class BlankDataFilter : BaseDataFilter
    {
        public override Matrix<double> Filter(Matrix<double> input)
        {
            return input;
        }
    }
}