using System;
using System.Collections.Generic;
using System.Text;
using Clustering.KMeans.Library.MethodInitializations;

namespace Clustering.KMeans.Library
{
    public class KMeans : IKMeans
    {
        internal int NumberOfClusters { get; set; }
        internal IMethodInitialization MethodInitialization { get; set; }

        internal KMeans()
        {

        }
    }
}
