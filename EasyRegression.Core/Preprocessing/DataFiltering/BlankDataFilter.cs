using EasyRegression.Core.Common.Models;
using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public class BlankDataFilter : BaseDataFilter
    {
        public override TrainingModel<double> Filter(TrainingModel<double> input)
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