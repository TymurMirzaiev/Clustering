using Clustering.KMeans.Library.ClusteringQuality.Models;
using Clustering.KMeans.Library.ClusteringQuality.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Clustering.KMeans.Library.Common
{
    public class DistanceComparer<T> : IComparer<T> where T: IDistance
    {
        public int Compare(T x, T y)
        {
            var res = 0;

            if (x.Value > y.Value)
            {
                res = 1;
            }

            if (x.Value < y.Value)
            {
                res = -1;
            }

            return res;
        }
    }
}
