using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library
{
    public interface IKMeans
    {
        IDataViewClustered FitPredict(IDataView data);
    }
}