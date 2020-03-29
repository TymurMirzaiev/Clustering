using Clustering.KMeans.Library.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Clustering.KMeans.Library.Common
{
    public class RowComparer : IEqualityComparer<Row>
    {
        public bool Equals(Row x, Row y)
        {
            bool res = false;
            
            if(x.Rows.Length != y.Rows.Length || x == null || y == null)
            {
                return res;
            }

            if (x.Rows.SequenceEqual(y.Rows))
            {
                res = true;
            }
            
            return res;
        }

        public int GetHashCode(Row obj)
        {
            int hash = 17;
            
            for(int i =0;i<obj.Rows.Length; i++)
            {
                hash = hash * 23 + obj.Rows[i].GetHashCode();
            }

            return hash;
        }
    }
}
