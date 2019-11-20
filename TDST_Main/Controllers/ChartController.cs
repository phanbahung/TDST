using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST_CRUD.Dao;
using TDST_CRUD.EF;
using TDST_CRUD.ViewModels;

namespace TDST.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Chart_SoThucHien()
        {
            List<object> iData = new List<object>();
            //Creating sample data
            ChartDao chartDao = new ChartDao();
            var lstDonVi = chartDao.TongHopSoThucHien_LuyKe(0);

            //Creating sample data
            DataTable dt = new DataTable();

            dt.Columns.Add("TenVietTat1", System.Type.GetType("System.String"));
            dt.Columns.Add("SoThucHien_LuyKe", System.Type.GetType("System.Decimal"));


            foreach (Chart_LuyKeViewModel dv in lstDonVi)
            {
                DataRow dr = dt.NewRow();
                dr["TenVietTat1"] = dv.TenVietTat1;
                dr["SoThucHien_LuyKe"] = Math.Round( dv.SoThucHien_LuyKe,2);
                dt.Rows.Add(dr);
            }


            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON
            return Json(iData, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult NewChart()
        {
            List<object> iData = new List<object>();
            //Creating sample data
            DanhMucDao dmDao = new DanhMucDao();
            var  lstDonVi= dmDao.Get_DmDonVi();
                        
            //Creating sample data
            DataTable dt = new DataTable();

            dt.Columns.Add("TenVietTat1", System.Type.GetType("System.String"));
            dt.Columns.Add("Id", System.Type.GetType("System.Int32"));


            foreach(DonVi dv in lstDonVi)
            {
                DataRow dr = dt.NewRow();
                dr["TenVietTat1"] =dv.TenVietTat1;
                dr["Id"] = dv.Id;
                dt.Rows.Add(dr);
            }
                   

            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON
            return Json(iData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewChart11()
        {
            List<object> iData = new List<object>();
            //Creating sample data
            DataTable dt = new DataTable();


            dt.Columns.Add("Employee", System.Type.GetType("System.String"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Employee"] = "Sam";
            dr["Credit"] = 123;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Alex";
            dr["Credit"] = 456;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Employee"] = "Michael";
            dr["Credit"] = 587;
            dt.Rows.Add(dr);

            //Looping and extracting each DataColumn to List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON
            return Json(iData, JsonRequestBehavior.AllowGet);
        }
    }
}