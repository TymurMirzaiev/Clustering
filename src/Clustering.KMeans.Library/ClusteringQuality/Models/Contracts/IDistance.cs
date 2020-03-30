using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.ClusteringQuality.Models.Contracts
{
    public interface IDistance
    {
        public float Value { get; set; }
    }
}
