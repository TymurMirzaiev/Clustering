using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library
{
    public interface IKMeans
    {
        float[] FitPredict(IDataView data);
    }
}