using System.Collections.Generic;
using System.Data;
using System.IO;
using Clustering.KMeans.Library.Data.Contracts;
using Clustering.KMeans.Library.Data.JsonConverters;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Clustering.KMeans.Library.Data
{
    public static class DataReaderExcel
    {
        public static Contracts.IDataView ReadDataFromExcel(
            string path,
            bool hasHeader = true,
            int worksheet = 3,
            int startColumn = 1)
        {
            using (var excelPack = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    excelPack.Load(stream);
                }

                var ws = excelPack.Workbook.Worksheets[worksheet];
                var endColumn = ws.Dimension.End.Column;
                var endRow = ws.Dimension.End.Row;
                //In-memory data table
                var firstRow = hasHeader ? 2 : 1;

                var countOfActualColumns = endColumn - startColumn + 1;
                var countOfActualRows = endRow - firstRow + 1;
                var array = new string[countOfActualRows, countOfActualColumns];
                for (int k = 0; k < countOfActualColumns; k++)
                {
                    for (int j = 0; j < countOfActualRows; j++)
                    {
                        array[j, k] = string.Empty;
                    }
                }

                //Initialize columns
                var columnNames = new string[countOfActualColumns];
                int i = 0;
                foreach (var firstRowCell in ws.Cells[1, startColumn, 1, ws.Dimension.End.Column])
                {
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        columnNames[i] = firstRowCell.Text;
                        i++;
                    }
                }

                //Get row details
                i = 0;
                for (int startRow = firstRow; startRow <= endRow; startRow++)
                {
                    var wsRow = ws.Cells[startRow, startColumn, startRow, endColumn];
                    //DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        array[i, cell.Start.Column - startColumn] = cell.Text;
                    }
                    i++;
                }
                //Get everything as generics and let end user decides on casting to required type
                //var dataTableSerialized = JsonConvert.SerializeObject(excelasTable);
                var dataTableSerialized = JsonConvert.SerializeObject(array);

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>()
                    {
                        new DoubleJsonConverter(),
                        new FloatJsonConverter()
                    }
                };

                var data = JsonConvert.DeserializeObject<float[,]>(dataTableSerialized, jsonSerializerSettings);

                /*var size = data.GetLength(0);
                Row[] rows = new Row[size];
                for(int k = 0; k < rows.Length; k++)
                {
                    rows[k] = new Row();
                    var featuresSize = data.GetLength(1);
                    rows[k].Features = new float[featuresSize];
                    for(int j = 0; j < featuresSize; j++)
                    {
                        rows[k].Features[j] = data[k, j];
                    }
                }*/

                var res = new DataView(
                    columnNames: columnNames,
                    data: data);

                return res;
            }
        }

        public static Contracts.IDataView ReadDataFromExcel(
            FileStream stream,
            bool hasHeader = true,
            int worksheet = 3,
            int startColumn = 1)
        {
            using (var excelPack = new ExcelPackage())
            {
                excelPack.Load(stream);

                var ws = excelPack.Workbook.Worksheets[worksheet];
                var endColumn = ws.Dimension.End.Column;
                var endRow = ws.Dimension.End.Row;
                //In-memory data table
                var firstRow = hasHeader ? 2 : 1;

                var countOfActualColumns = endColumn - startColumn + 1;
                var countOfActualRows = endRow - firstRow + 1;
                var array = new string[countOfActualRows, countOfActualColumns];
                for (int k = 0; k < countOfActualColumns; k++)
                {
                    for (int j = 0; j < countOfActualRows; j++)
                    {
                        array[j, k] = string.Empty;
                    }
                }

                //Initialize columns
                var columnNames = new string[countOfActualColumns];
                int i = 0;
                foreach (var firstRowCell in ws.Cells[1, startColumn, 1, ws.Dimension.End.Column])
                {
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        columnNames[i] = firstRowCell.Text;
                        i++;
                    }
                }

                //Get row details
                i = 0;
                for (int startRow = firstRow; startRow <= endRow; startRow++)
                {
                    var wsRow = ws.Cells[startRow, startColumn, startRow, endColumn];
                    //DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        array[i, cell.Start.Column - startColumn] = cell.Text;
                    }
                    i++;
                }
                //Get everything as generics and let end user decides on casting to required type
                //var dataTableSerialized = JsonConvert.SerializeObject(excelasTable);
                var dataTableSerialized = JsonConvert.SerializeObject(array);

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>()
                    {
                        new DoubleJsonConverter(),
                        new FloatJsonConverter()
                    }
                };

                var data = JsonConvert.DeserializeObject<float[,]>(dataTableSerialized, jsonSerializerSettings);

                /*var size = data.GetLength(0);
                Row[] rows = new Row[size];
                for (int k = 0; k < rows.Length; k++)
                {
                    rows[k] = new Row();
                    var featuresSize = data.GetLength(1);
                    rows[k].Features = new float[featuresSize];
                    for (int j = 0; j < featuresSize; j++)
                    {
                        rows[k].Features[j] = data[k, j];
                    }
                }
                */
                var res = new DataView(
                    columnNames: columnNames,
                    data: data);


                return res;
            }
        }
    }
}
