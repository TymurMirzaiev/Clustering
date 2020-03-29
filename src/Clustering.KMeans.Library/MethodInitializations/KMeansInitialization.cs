using Clustering.KMeans.Library.Common;
using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clustering.KMeans.Library.MethodInitializations
{
    public class KMeansInitialization : IMethodInitialization
    {
        public ICentroid[] CalculateCentroids(IDataViewClustered dataViewClustered)
        {
            var clusters = dataViewClustered.Clustered.Distinct();
            var rowsSize = dataViewClustered.Rows.GetLength(0);
            var countOfClusters = clusters.Count();

            // Get index's grouped by values of cluster
            var dict = dataViewClustered.Clustered
                .Select((x, i) => new { Value = x, Index = i })
                .GroupBy(x => x.Value)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Index)
                .ToArray());

            ICentroid[] centroids = new Centroid[countOfClusters];
            for (int i = 0; i < countOfClusters; i++)
            {
                centroids[i] = new Centroid();
            }
            
            for (int j = 0; j < countOfClusters; j++)
            {

                float sumByCluster = 0;
                for (int i = 0; i < dict[j].Count(); i++)
                {
                    int index = dict[j][i];
                    centroids[j].Values[i] += dataViewClustered.Rows[j, i];
                }
            }


            

            return null;
        }

        public ICentroid[] InitStartCentroidsPositions(IDataView dataView, int n)
        {
            int size = dataView.Rows.GetLength(0);
            int[] randomNumbers = InitRandomNumbers(n, size);
            var centroids = InitCentroids(randomNumbers, dataView);

            return centroids;
        }

        #region Init Centroids
        private Centroid[] InitCentroids(int[] random, IDataView dataView)
        {
            Centroid[] centroids = new Centroid[random.Length];
            int i = 0;
            do
            {
                centroids[i] = new Centroid();
                centroids[i].Values = dataView.Rows.GetRow(random[i]);
                i++;
            } while (random.Length != i);

            return centroids;
        }
        #endregion

        #region Init random numbers
        private int[] InitRandomNumbers(int n, int size)
        {
            int[] randomNumbers = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int randomNumber = random.Next(1, size);
                if (!randomNumbers.Contains(randomNumber))
                {
                    randomNumbers[i] = randomNumber;
                }
                else
                {
                    i--;
                }
            }

            return randomNumbers;
        }
        #endregion

    }
}
