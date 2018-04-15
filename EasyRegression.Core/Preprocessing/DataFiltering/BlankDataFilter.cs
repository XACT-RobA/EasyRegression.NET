using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class BlankDataFilter : BaseDataFilter
    {
        public override Matrix<double> Filter(Matrix<double> input)
        {
            return input;
        }

        public override string Serialise()
        {
            var data = new
            {
                filterType = GetPluginType(),
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}