using System.Collections.Generic;
using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.Data
{
    public class DataView : IDataView
    {
        public string[] Columns { get; set; }
        public Row[] Rows { get; set; }

        public DataView(IDataView dataView)
        {
            this.Columns = dataView.Columns;
            this.Rows = dataView.Rows;
        }

        public DataView()
        {

        }

        public DataView(string[] columnNames, Row[] data)
        {
            Columns = columnNames;
            Rows = data;
        }
    }
}
