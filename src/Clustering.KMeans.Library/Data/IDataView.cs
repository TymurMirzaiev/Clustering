using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data
{
    public interface IDataView<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}
