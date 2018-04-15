namespace EasyRegression.Core.Preprocessing.DataPatching
{
    public class ZeroDataPatcher : BaseDataPatcher
    {
        public ZeroDataPatcher() { }

        internal ZeroDataPatcher(double[] parameters)
        {
            _parameters = parameters;
        }
    }
}