using System.Collections.Generic;
using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.Data
{
    public class DataView : IDataView
    {
        public string[] Columns { get; set; }
        public float[,] Rows { get; set; }

        public DataView(Contracts.IDataView dataView)
        {
            this.Columns = dataView.Columns;
            this.Rows = dataView.Rows;
        }

        public DataView(string[] columnNames, float[,] data)
        {
            Columns = columnNames;
            Rows = data;
        }
    }
}
