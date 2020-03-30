using Clustering.KMeans.Library.ClusteringQuality.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.ClusteringQuality.Models
{
    public class Distance : IDistance
    {
        public float Value { get; set; }
    }
}
