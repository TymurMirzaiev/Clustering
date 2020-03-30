using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Contracts;
using Clustering.KMeans.Library.MethodInitializations;
using Clustering.KMeans.Library.Common;
using Clustering.KMeans.Library.Data.Calculating;
using System.Linq;

namespace Clustering.KMeans.Library.KMeans
{
    public class KMeans : IKMeans
    {
        internal int NumberOfClusters { get; set; }
        internal IMethodInitialization MethodInitialization { get; set; }

        internal KMeans()
        {

        }

        public IDataViewClustered FitPredict(IDataView data)
        {
            Row[] startCentroids = MethodInitialization.InitStartCentroidsPositions(data, NumberOfClusters);
            IDataViewClustered dataViewClustered = InitDataViewClustered(data, startCentroids);

            bool exit = false;
            do
            {
                Row[] nextCentroids = MethodInitialization.CalculateCentroids(dataViewClustered);

                if (!startCentroids.SequenceEqual(nextCentroids, new RowComparer()))
                {
                    dataViewClustered = InitDataViewClustered(data, nextCentroids);
                    startCentroids = (Row[])nextCentroids.Clone();
                }
                else
                {
                    exit = true;
                }
            } while (exit == false);

            return dataViewClustered;
        }

        private IDataViewClustered InitDataViewClustered(IDataView data, Row[] startCentroids)
        {
            IDataViewClustered dataViewClustered = new DataViewClustered(data);
            int lengthOfRows = dataViewClustered.Rows.Length;

            DistanceDeterminator distanceDeterminator = new DistanceDeterminator(new EuclideanDistance());

            for (int i = 0; i < lengthOfRows; i++)
            {
                int bestCluster = 0;
                float bestDistance = float.MaxValue;

                var row = data.Rows[i];

                for (int j = 0; j < startCentroids.Length; j++)
                {
                    float distance = distanceDeterminator.Calculate(row, startCentroids[j]);
                    if (distance < bestDistance)
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
