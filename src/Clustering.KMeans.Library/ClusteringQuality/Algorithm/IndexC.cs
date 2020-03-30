using Clustering.KMeans.Library.ClusteringQuality.Contracts;
using Clustering.KMeans.Library.ClusteringQuality.Models;
using Clustering.KMeans.Library.Common;
using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Calculating.Contracts;
using Clustering.KMeans.Library.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clustering.KMeans.Library.ClusteringQuality.Algorithm
{
    public class IndexC : IQualityMeasurement
    {
        public float EvaluateQuality(IDataViewClustered dataView, ICalculationDistance calculationDistance)
        {
            Row[][] rows = Init2DimensionArrayClusteredRow(dataView);
            List<DistanceClustered> distances = CalculateDistancesByClusters(rows, calculationDistance);

            float D = CalculateSumOfDistancesForEachCluster(distances);
            int R = CalculateValueR(dataView);

            Distance[] allDistances = CalculateDistanceForEach(dataView.Rows, calculationDistance);
            Array.Sort(allDistances, new DistanceComparer<Distance>());
            float Dmin = CalculateSumOfDistanceFromDirection(true, R, allDistances);
            float Dmax = CalculateSumOfDistanceFromDirection(false, R, allDistances);

            float res = (D - Dmin) / (Dmax - Dmin);

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">if start is true index position is 0 otherwise index position is (distances.Length - 1 - r)</param>
        /// <param name="r"></param>
        /// <param name="distances"></param>
        /// <returns></returns>
        private float CalculateSumOfDistanceFromDirection(bool start, int r, Distance[] distances)
        {
            Distance[] rangeDistances = null;

            if (start)
            {
                rangeDistances = distances.Take(r).ToArray();
            }
            else
            {
                rangeDistances = distances.TakeLast(r).ToArray();
            }

            var res = rangeDistances.Sum(r => r.Value);

            return res;
        }

        private int CalculateValueR(IDataViewClustered dataView)
        {
            var clusteredArray = dataView.Clustered.GroupBy(c => c).Select(c => c.Count()).ToArray();

            int sygmaByN = 0;

            for (int i = 0; i < clusteredArray.Length; i++)
            {
                var N = clusteredArray[i];
                sygmaByN += N * (N - 1);
            }

            int res = sygmaByN / 2;

            return res;
        }

        private Distance[] CalculateDistanceForEach(Row[] rows, ICalculationDistance calculationDistance)
        {
            int countOfRows = rows.Length;

            //List<Distance> distances = new List<Distance>();
            int size = CalculateSizeOfArray(countOfRows);

            Distance[] distances = new Distance[size];

            int index = 0;

            for (int j = 0; j < countOfRows - 1; j++)
            {
                for (int i = j; i < countOfRows - 1; i++)
                {
                    float distance = calculationDistance.Calculate(rows[i], rows[i + 1]);
                    distances[index] = new Distance()
                    {
                        Value = distance
                    };
                    index++;
                }
            }


            return distances;
        }

        private int CalculateSizeOfArray(int length)
        {
            var size = length * length / 2;
            bool toDelete = false;

            if (length % 2 != 0)
            {
                toDelete = true;
            }

            if (toDelete)
            {
                size -= length / 2;
            }

            return size;
        }

        /*private float CalculateValueR(List<DistanceClustered> distances)
        {
            var distancesGrouped = distances
                .GroupBy(c => c.NumberCluster)
                .ToArray();

            int[] countGroupedByDistances = distancesGrouped.Select(c => c.Count()).ToArray();
            int sygmaByN = 0;

            for (int i = 0; i < countGroupedByDistances.Length; i++)
            {
                var N = countGroupedByDistances[i];
                sygmaByN += N * (N - 1);
            }

            var res = sygmaByN / 2;

            return res;
        }*/

        private float CalculateSumOfDistancesForEachCluster(List<DistanceClustered> distances)
        {
            var distancesGrouped = distances
                .GroupBy(c => c.NumberCluster)
                .ToArray();

            float res = 0;

            for (int i = 0; i < distancesGrouped.Length; i++)
            {
                res += distancesGrouped[i].Sum(c => c.Value);
            }

            return res;
        }

        private List<DistanceClustered> CalculateDistancesByClusters(Row[][] rows, ICalculationDistance calculationDistance)
        {
            int countOfClusters = rows.GetLength(0);

            List<DistanceClustered> distances = new List<DistanceClustered>();

            for (int i = 0; i < countOfClusters; i++)
            {
                int countOfRows = rows[i].Length;
                for (int k = 0; k < countOfRows; k++)
                {
                    for (int j = k; j < countOfRows - 1; j++)
                    {
                        float distance = calculationDistance.Calculate(rows[i][j], rows[i][j + 1]);
                        distances.Add(new DistanceClustered()
                        {
                            NumberCluster = i,
                            Value = distance
                        });
                    }
                }
            }

            return distances;
        }

        /// <summary>
        /// </summary>
        /// <param name="dataView"></param>
        /// <returns>float dimension array where index of row is number of cluster and column is a row</returns>
        private Row[][] Init2DimensionArrayClusteredRow(IDataViewClustered dataView)
        {
            int rowsLength = dataView.Rows.Length;

            var countOfObjectsInClusters = dataView.Clustered
                .GroupBy(c => c)
                .Select(c => new { Key = c.Key, Count = c.Count() })
                .ToList();

            int countOfClusters = countOfObjectsInClusters.Count;

            Row[][] rows = new Row[countOfClusters][];

            for (int i = 0; i < countOfClusters; i++)
            {
                var numberCluster = countOfObjectsInClusters[i].Key;
                var countObjectsInCluster = countOfObjectsInClusters[i].Count;
                rows[numberCluster] = new Row[countObjectsInCluster];
            }

            int[] indexesInClusters = new int[countOfClusters];

            for (int i = 0; i < rowsLength; i++)
            {
                int cluster = dataView.Clustered[i];
                int index = indexesInClusters[cluster];

                rows[cluster][index] = dataView.Rows[i];

                indexesInClusters[cluster] += 1;
            }

            return rows;
        }
    }
}
