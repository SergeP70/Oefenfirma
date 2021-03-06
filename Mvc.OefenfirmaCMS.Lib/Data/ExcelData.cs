﻿using Excel;
using Mvc.OefenfirmaCMS.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Mvc.OefenfirmaCMS.Lib.Data
{
    // A Basic Class to provide access to ExcelDataReader Functionality
    public class ExcelData
    {
        string _path;
        public ExcelData(string path)
        {
            _path = path;
        }
        
        public IExcelDataReader getExcelReader()
        {
            // ExcelDataReader works with the binary Excel file, so it needs a FileStream
            // to get started. This is how we avoid dependencies on ACE or Interop:
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);

            // We return the interface, so that
            IExcelDataReader reader = null;
            try
            {
                if (_path.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (_path.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Adding a getWorksheetNames Method to the ExcelData Class:
        public IEnumerable<string> getWorksheetNames()
        {
            var reader = this.getExcelReader();
            var workbook = reader.AsDataSet();
            var sheets = from DataTable sheet
                         in workbook.Tables
                         select sheet.TableName;
            return sheets;
        }

        // Adding a getData() method to the ExcelData Class:
        public IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames = true)
        {
            var reader = this.getExcelReader();
            reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
            var workSheet = reader.AsDataSet().Tables[sheet];
            var rows = from DataRow a
                       in workSheet.Rows
                       select a;
            return rows;
        }


        // Filling list with excelData
        public List<Product> fillList(IEnumerable<DataRow> artikelen)
        {
            List<Product> artikellijst = new List<Product>();

            foreach (var row in artikelen)
            {
                // Opvullen artikellijst met excelData
                var artikel = new Product()
                {
                    ProductName = row["ProductName"].ToString(),
                    ProductCode = row["ProductCode"].ToString(),
                    CatId = Convert.ToInt32(row["CatId"]),
                    Description = row["Description"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    UrlImage = row["UrlImage"].ToString()
                };
                artikellijst.Add(artikel);
            }
            return artikellijst;
        }
    }
}