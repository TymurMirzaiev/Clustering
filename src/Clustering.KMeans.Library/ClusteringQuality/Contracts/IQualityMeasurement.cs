using Clustering.KMeans.Library.Data.Calculating.Contracts;
using Clustering.KMeans.Library.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.ClusteringQuality.Contracts
{
    public interface IQualityMeasurement
    {
        public float EvaluateQuality(IDataViewClustered dataView, ICalculationDistance calculationDistance);
    }
}
