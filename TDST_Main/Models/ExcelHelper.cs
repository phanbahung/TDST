using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDST_CRUD.EF;
using TDST;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Data;
using OfficeOpenXml;
using ViewModels;

namespace TDST.Models
{
    public class ExcelHelper
    {       

        public ExcelHelper()
        { }

        [HttpGet]        
        public void ImportDuToan()
        {

            string rootFolder= "";//= _hostingEnvironment.WebRootPath;
            string fileName = @"ImportCustomers.xlsx";
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Customer"];
                int totalRows = workSheet.Dimension.Rows;

                List<BCDuToanViewModel> duToanList = new List<BCDuToanViewModel>();

                for (int i = 2; i <= totalRows; i++)
                {
                    duToanList.Add(new BCDuToanViewModel
                    {
                        IdChiTieu = int.Parse(workSheet.Cells[i, 1].Value.ToString()),
                        //SoThue = Decimal.Parse(workSheet.Cells[i, 2].Value),
                        //MaDonVi =int.Parse( workSheet.Cells[i, 3].Value),
                    });
                }

                //_db.Customers.AddRange(customerList);
                //_db.SaveChanges();

               // return customerList;
            }
        }





    }


    }