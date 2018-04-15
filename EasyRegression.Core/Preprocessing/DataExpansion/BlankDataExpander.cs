using Newtonsoft.Json;

namespace EasyRegression.Core.Preprocessing.DataExpansion
{
    public class BlankDataExpander : BaseDataExpander
    {
        public override string Serialise()
        {
            var data = new
            {
                expanderType = GetPluginType(),
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}