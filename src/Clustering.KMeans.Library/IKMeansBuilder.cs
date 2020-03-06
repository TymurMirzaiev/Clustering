using System;
using System.Collections.Generic;
using System.Text;
using Clustering.KMeans.Library.MethodInitializations;

namespace Clustering.KMeans.Library
{
    public interface IKMeansBuilder
    {
        IKMeans Build();
        IKMeansBuilder SetNumberOfClusters(int numberOfClusters);
        IKMeansBuilder Init(IMethodInitialization initialization);
    }
}
