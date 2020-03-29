using Clustering.KMeans.Library.Data.Contracts;

namespace Clustering.KMeans.Library.Data
{
    public class Centroid : ICentroid
    {
        public Row Row { get; set; }

        public Centroid()
        {
            this.Row = new Row();
        }
    }
}
