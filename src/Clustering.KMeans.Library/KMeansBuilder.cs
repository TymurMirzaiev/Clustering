using Clustering.KMeans.Library.MethodInitializations;

namespace Clustering.KMeans.Library
{
    public class KMeansBuilder : IKMeansBuilder
    {
        private readonly KMeans _kMeans;

        public KMeansBuilder()
        {
            _kMeans = new KMeans();
            _kMeans.NumberOfClusters = 1;
        }

        public IKMeans Build()
        {
            return _kMeans;
        }

        public IKMeansBuilder Init(IMethodInitialization initialization)
        {
            _kMeans.MethodInitialization = initialization;

            return this;
        }

        public IKMeansBuilder SetNumberOfClusters(int numberOfClusters)
        {
            _kMeans.NumberOfClusters = numberOfClusters;

            return this;
        }
    }
}
