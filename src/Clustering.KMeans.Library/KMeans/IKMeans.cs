using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.KMeans
{
    public interface IKMeans
    {
        IDataViewClustered FitPredict(IDataView data);
    }
}