using System;
using System.Collections.Generic;
using System.Text;

namespace Clustering.KMeans.Library.Data
{
    public class Row
    {
        public float[] Rows { get; set; }

        public float this[int index]
        {
            get
            {
                return Rows[index];
            }
            set
            {
                Rows[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return Rows.Length;
            }
        }
    }
}
