using Newtonsoft.Json;
using System.Collections.Generic;

namespace Clustering.KMeans.Library.Data.Contracts
{
    public interface IDataView
    {
        [JsonProperty("Columns")]
        string[] Columns { get; set; }
        //public Row[] Rows { get; set; }

        [JsonProperty("Rows")]
        float[,] Rows { get; set; }
    }
}
