using Clustering.KMeans.Library.Data.Calculating.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Calculating
{
    public class EuclideanDistance : ICalculationDistance
    {
        public float Calculate(float[] first, float[] other)
        {
            int arraySize = first.Length;
            if (first.Length != other.Length)
            {
                throw new ArgumentException("Array lengths are differents");
            }

            float distance = 0;
            for (int i = 0; i < arraySize; i++)
            {
                distance += MathF.Pow(first[i] - other[i], 2);
            }

            float res = MathF.Sqrt(distance);

            return res;
        }
    }
}
