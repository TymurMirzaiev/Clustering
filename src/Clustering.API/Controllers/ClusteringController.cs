using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clustering.KMeans.Library.ClusteringQuality.Algorithm;
using Clustering.KMeans.Library.ClusteringQuality.Contracts;
using Clustering.KMeans.Library.Data;
using Clustering.KMeans.Library.Data.Calculating;
using Clustering.KMeans.Library.Data.Contracts;
using Clustering.KMeans.Library.Data.Import;
using Clustering.KMeans.Library.KMeans;
using Clustering.KMeans.Library.MethodInitializations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clustering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClusteringController : ControllerBase
    {
        private readonly IKMeansBuilder _kMeansBuilder;

        public ClusteringController(IKMeansBuilder kMeansBuilder)
        {
            _kMeansBuilder = kMeansBuilder;
        }

        [HttpPost, Route("/api/clustering/evaluate")]
        public ActionResult<float> EvaluateClustering(DataViewClustered dataViewClustered)
        {
            IQualityMeasurement evaluator = new IndexC();
            var res = evaluator.EvaluateQuality(dataViewClustered, new EuclideanDistance());

            return Ok(res);
        }

        [HttpPost, Route("/api/clustering")]
        [Produces("application/json")]
        public IDataViewClustered GetClustered(DataView dataView)
        {
            var kMeans = _kMeansBuilder
                .SetNumberOfClusters(2)
                .Init(new KMeansInitialization())
                .Build();

            var clustered = kMeans.FitPredict(dataView);

            return clustered;
        }

        [HttpGet, Route("/api/dataview")]
        [Produces("application/json")]
        public async Task<IDataView> GetDataViewFromFile(IFormFile file)    // check extension
        {
            var filePath = Path.GetTempFileName();
            IDataView data = null;

            if (file != null)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    data = DataReaderExcel.ReadDataFromExcel(stream, true, 3, 2 );
                }
            }

            return data;
        }
    }
}