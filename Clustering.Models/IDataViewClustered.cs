using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.Models
{
    public interface IDataViewClustered : IDataView
    {
        public int[] Clustered { get; set; }
    }
}
