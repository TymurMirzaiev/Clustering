using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Calculating.Contracts
{
    public interface ICalculationDistance
    {
        float Calculate(Row first, Row other);
    }
}
