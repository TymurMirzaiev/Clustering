using Clustering.KMeans.Library.Data.Calculating.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Calculating
{
    public class DistanceDeterminator
    {
        private readonly ICalculationDistance _calculationDistance;

        public DistanceDeterminator(ICalculationDistance calculationDistance)
        {
            _calculationDistance = calculationDistance;
        }

        public float Calculate(float[] first, float[] other)
        {
            return _calculationDistance.Calculate(first, other);
        }
    }
}
