using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.MethodInitializations
{
    public interface IMethodInitialization
    {
        ICentroid[] InitStartCentroidsPositions(IDataView dataView, int n);
        ICentroid[] CalculateCentroids(IDataViewClustered dataViewClustered);
    }
}