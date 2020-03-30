using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.MethodInitializations
{
    public interface IMethodInitialization
    {
        Row[] InitStartCentroidsPositions(IDataView dataView, int n);
        Row[] CalculateCentroids(IDataViewClustered dataViewClustered);
    }
}