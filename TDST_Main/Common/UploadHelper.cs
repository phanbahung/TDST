using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDST_CRUD.EF;
using TDST.Models;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Xml;

namespace TDST.Common
{
    public class UploadHelper
    {

        public string  UploadXML(HttpPostedFileBase file, string uploadFolder)
        {
            string _path = "";
            string _FileName = "";            

            //------ 1. Upload file to folder on server
            if (file != null && file.ContentLength > 0)
            {
                _FileName = Path.GetFileName(file.FileName);
                string now = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                _FileName = now +"_"+ _FileName;//15-05-2019-09-02-33_CT_TNS_TTA0000000_11_879564.xml
                 _path = Path.Combine(uploadFolder, _FileName);
                file.SaveAs(_path);
            }

            return _FileName;
        } // end UploadXML


        public string UploadExcel(HttpPostedFileBase file, string uploadFolder)
        {

            string _path = "";
            string _FileName = "";

            //------ 1. Upload file to folder on server
            if (file != null && file.ContentLength > 0)
            {
                //string _FileName = Path.GetFileName(file.FileName);
                string now = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                _FileName = now + ".xlsx";
                _path = Path.Combine(uploadFolder, _FileName);
                file.SaveAs(_path);
            }

            return _FileName;
        } // end UploadXML

    }  // End class


}

   

