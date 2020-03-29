using Clustering.KMeans.Library.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data
{
    public class Centroid : ICentroid
    {
        public float[] Values { get; set; }
    }
}
