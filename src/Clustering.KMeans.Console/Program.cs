using System;
using System.IO;
using Clustering.KMeans.Library;
using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Contracts;
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

            Library.Data.Contracts.IDataView data = DataReaderExcel.ReadDataFromExcel(
                                                path: path,
                                                hasHeader: true,
                                                worksheet: 3,
                                                startColumn: 2);

            var clustered  = kMeans.FitPredict(data);
            
            foreach(var element in clustered)
            {
                Console.WriteLine(element);
            }
           

            Console.Read();
        }

    }
}
