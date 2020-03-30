using Clustering.KMeans.Library.ClusteringQuality.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.ClusteringQuality.Models
{
    public class DistanceClustered : IDistance
    {
        public int NumberCluster { get; set; }
        public float Value { get; set; }
    }
}
