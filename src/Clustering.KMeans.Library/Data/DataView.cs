using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data
{
    public class DataView<T> : IDataView<T>
    {
        public IEnumerable<T> Data { get; set; }

        public DataView(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
