using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AutomationChallenge_MoisesRivas
{
    [Binding]
    public static class ExcelOperations
    {

        //Method to obtain data from excel file
        private static DataTable ExcelToDataTable(string filename)
        {
            FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);

            DataSet resultSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            stream.Close();
            DataTableCollection table = resultSet.Tables;
            DataTable resultTable = table["MyTable"];
            return resultTable;
        }

        public class DataCollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }

        static List<DataCollection> dataCol = new List<DataCollection>();

        //Method to create a list of registers with the obtained data from excel
        public static void PopulateInCollection(string filename)
        {
            dataCol.Clear();
            DataTable table = ExcelToDataTable(filename);
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dataTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    dataCol.Add(dataTable);
                }
            }
        }

        //Method to obtain an specific row from the list of registers created
        //The row is returned as string
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                string data = (from colData in dataCol where colData.colName == columnName && colData.rowNumber == rowNumber select colData.colValue).SingleOrDefault();
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }
    }
}
