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
        public Row[] CalculateCentroids(IDataViewClustered dataViewClustered)
        {
            var clusters = dataViewClustered.Clustered.Distinct();
            var rowsSize = dataViewClustered.Rows.Length;
            var columnsLength = dataViewClustered.Columns.Length;
            var countOfClusters = clusters.Count();

            Row[] centroids = new Row[countOfClusters];
            for (int i = 0; i < countOfClusters; i++)
            {
                centroids[i] = new Row();
                centroids[i].Rows = new float[columnsLength];
            }

            for (int i = 0; i < rowsSize; i++)
            {
                int indexCluster = dataViewClustered.Clustered[i];
                for (int j = 0; j < dataViewClustered.Columns.Length; j++)
                {
                    centroids[indexCluster].Rows[j] += dataViewClustered.Rows[i].Rows[j];
                }
            }

            var clustredBy = dataViewClustered.Clustered
                .GroupBy(c => c)
                .Select(c => new { Value = c.Key, Count = c.Count() })
                .ToDictionary(c => c.Value, i => i.Count);

            for (int i = 0; i < centroids.Length; i++)
            {
                for(int j = 0; j < columnsLength; j++)
                {
                    centroids[i].Rows[j] /= clustredBy[i];
                }
            }

            return centroids;
        }

        public Row[] InitStartCentroidsPositions(IDataView dataView, int n)
        {
            int size = dataView.Rows.Length;
            int[] randomNumbers = InitRandomNumbers(n, size);
            var centroids = InitCentroids(randomNumbers, dataView);

            return centroids;
        }

        #region Init Centroids
        private Row[] InitCentroids(int[] random, IDataView dataView)
        {
            Row[] centroids = new Row[random.Length];
            int i = 0;
            do
            {
                centroids[i] = new Row();
                centroids[i] = dataView.Rows[random[i]];
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
