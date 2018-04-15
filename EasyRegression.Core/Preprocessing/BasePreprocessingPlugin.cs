namespace EasyRegression.Core.Preprocessing
{
    public abstract class BasePreprocessingPlugin : IPreprocessingPlugin
    {
        public virtual string GetPluginType()
        {
            return this.GetType().Name;
        }

        public abstract string Serialise();
    }
}