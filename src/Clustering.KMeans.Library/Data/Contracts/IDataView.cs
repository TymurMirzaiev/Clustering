using Newtonsoft.Json;
using System.Collections.Generic;

namespace Clustering.KMeans.Library.Data.Contracts
{
    public interface IDataView
    {
        [JsonProperty("Columns")]
        string[] Columns { get; set; }

        [JsonProperty("Rows")]
        public Row[] Rows { get; set; }
    }
}
