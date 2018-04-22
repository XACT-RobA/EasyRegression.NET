using EasyRegression.Core.Common.Models;

namespace EasyRegression.Core.Preprocessing.DataFiltering
{
    public interface IDataFilter : IPreprocessingPlugin
    {
        TrainingModel<double> Filter(TrainingModel<double> input);
    }
}