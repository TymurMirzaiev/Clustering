using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using Clustering.KMeans.Library.Data.JsonConverters;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Clustering.KMeans.Library.Data
{
    public static class DataReader
    {
        public static IDataView<T> ReadDataFromExcel<T>(string path, bool hasHeader = true, int worksheet = 3)
        {
            using (var excelPack = new ExcelPackage())
            {
                //Load excel stream
                using (var stream = File.OpenRead(path))
                {
                    excelPack.Load(stream);
                }

                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets[worksheet];

                //Get all details as DataTable - because Datatable make life easy :)
                DataTable excelasTable = new DataTable();

                var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                //Get everything as generics and let end user decides on casting to required type
                var dataTableSerialized = JsonConvert.SerializeObject(excelasTable);
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new JsonConverter[] { new DoubleJsonConverter() }
                };
                var data = JsonConvert.DeserializeObject<IEnumerable<T>>(dataTableSerialized, jsonSerializerSettings);
                var res = new DataView<T>(data);

                return res;
            }
        }
    }
}
