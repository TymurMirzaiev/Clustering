using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Contracts
{
    public interface ICentroid
    {
        /// <summary>
        /// Values by features
        /// </summary>
        float[] Values { get; set; }
    }
}
