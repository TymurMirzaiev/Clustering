using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data.Contracts
{
    public interface IDataViewClustered : IDataView
    {
        public int[] Clustered { get; set; }
    }
}
