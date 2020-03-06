using System;
using System.IO;
using Clustering.KMeans.Library;
using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.MethodInitializations;
using System.Resources;

namespace Clustering.KMeans.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder  = ".\\..\\..\\..\\Data";
            string fileName = "example.xlsx";
            string path = Path.Combine(folder, fileName);

            IKMeansBuilder kMeansBuilder = new KMeansBuilder();
            IKMeans kMeans;

            kMeans = kMeansBuilder
                        .Init(new KMeansInitialization())
                        .SetNumberOfClusters(2)
                        .Build();

            IDataView<Temperatures> data = DataReader.ReadDataFromExcel<Temperatures>(path);
            //kMeans.fit(data);


        }
    }
}
