using Clustering.KMeans.Library.MethodInitializations;

namespace Clustering.KMeans.Library.KMeans
{
    public interface IKMeansBuilder
    {
        IKMeans Build();
        IKMeansBuilder SetNumberOfClusters(int numberOfClusters);
        IKMeansBuilder Init(IMethodInitialization initialization);
    }
}
