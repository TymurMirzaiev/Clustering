using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Contracts;
using Clustering.KMeans.Library.MethodInitializations;
using Clustering.KMeans.Library.Common;
using Clustering.KMeans.Library.Data.Calculating;

namespace Clustering.KMeans.Library
{
    public class KMeans : IKMeans
    {
        internal int NumberOfClusters { get; set; }
        internal IMethodInitialization MethodInitialization { get; set; }

        internal KMeans()
        {

        }

        public float[] FitPredict(IDataView data)
        {
            var startCentroids = MethodInitialization.InitStartCentroidsPositions(data, NumberOfClusters);

            IDataViewClustered dataViewClustered = InitDataViewClustered(data, startCentroids);
            var newCentroids = MethodInitialization.CalculateCentroids(dataViewClustered);
            // calculate interemediate value to all features for every centroid -> that's new centroids
            // calulate closes distancec to all objects //
            // calculate interemediate value to all features for every centroid -> that's new centroids

            return new float[] { 1, 2, 3 };
        }

        private IDataViewClustered InitDataViewClustered(IDataView data, ICentroid[] startCentroids)
        {
            IDataViewClustered dataViewClustered = new DataViewClustered(data);
            int lengthOfRows = dataViewClustered.Rows.GetLength(0);

            DistanceDeterminator distanceDeterminator = new DistanceDeterminator(new EuclideanDistance());

            for (int i = 0; i < lengthOfRows; i++)
            {
                int bestCluster = 0;
                float bestDistance = float.MaxValue;

                var row = data.Rows.GetRow(i);

                for (int j = 0; j < startCentroids.Length; j++)
                {
                    float distance = distanceDeterminator.Calculate(row, startCentroids[j].Values);
                    if(distance < bestDistance)
                    {
                        bestCluster = j;
                        bestDistance = distance;
                    }
                }

                dataViewClustered.Clustered[i] = bestCluster;
            }

            return dataViewClustered;
        }
    }
}
