using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Contracts
{
    public interface ICentroid
    {
        /// <summary>
        /// Rows by features
        /// </summary>
        Row Row { get; set; }
    }
}
