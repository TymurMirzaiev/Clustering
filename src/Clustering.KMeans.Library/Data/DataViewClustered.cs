using Clustering.KMeans.Library.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data
{
    public class DataViewClustered : IDataViewClustered
    {
        public int[] Clustered { get; set; }
        public string[] Columns { get; set; }
        public Row[] Rows { get; set; }

        public DataViewClustered()
        {

        }

        public DataViewClustered(IDataView dataView)
        {
            this.Columns = dataView.Columns;
            this.Rows = dataView.Rows;

            this.Clustered = new int[Rows.Length];
        }

    }
}
