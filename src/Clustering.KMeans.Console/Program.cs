using System;
using System.IO;
using Clustering.KMeans.Library.ClusteringQuality.Algorithm;
using Clustering.KMeans.Library.ClusteringQuality.Contracts;
using Clustering.KMeans.Library.Data.Calculating;
using Clustering.KMeans.Library.Data.Contracts;
using Clustering.KMeans.Library.Data.Import;
using Clustering.KMeans.Library.KMeans;
using Clustering.KMeans.Library.MethodInitializations;

namespace Clustering.KMeans.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Combine path

            string folder = ".\\..\\..\\..\\Data";
            string fileName = "example.xlsx";
            string path = Path.Combine(folder, fileName);

            #endregion
            
            IKMeansBuilder kMeansBuilder = new KMeansBuilder();
            IKMeans kMeans;

            kMeans = kMeansBuilder
                        .Init(new KMeansInitialization())
                        .SetNumberOfClusters(2)
                        .Build();

            IDataView data = DataReaderExcel.ReadDataFromExcel(
                                                path: path,
                                                hasHeader: true,
                                                worksheet: 3,
                                                startColumn: 2);

            var clustered  = kMeans.FitPredict(data);

            IQualityMeasurement qualityMeasurementAlgorithm = new IndexC();

            var res = qualityMeasurementAlgorithm.EvaluateQuality(clustered, new EuclideanDistance());

            Console.WriteLine(res);

            Console.Read();
        }
    }
}
