using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.Models
{
    public interface IDataView
    {
        [JsonProperty("Columns")]
        string[] Columns { get; set; }

        [JsonProperty("Rows")]
        public Row[] Rows { get; set; }
    }
}
