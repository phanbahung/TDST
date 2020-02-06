using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST_CRUD.Dao;
using TDST_CRUD.EF;

namespace TDST.Controllers
{
    public class NhomTieuMucController : Controller
    {
        // GET: NhomTieuMuc
        public ActionResult Index()
        {
            return View();
        }

        #region Danh muc TM - NhómTM
        
        public ActionResult NhomTieuMuc()
        {
            DanhMucDao dao = new DanhMucDao();

            List<NhomTieuMuc> nhomTMs = dao.Get_DmNhomTieuMuc();
            int j;
            string ketQua;
            ketQua = "";
            int soLuongNhomTMs = nhomTMs.Count();

            for (int i = 0; i < soLuongNhomTMs - 1; i++)
            {
                for (j = i + 1; j < soLuongNhomTMs; j++)
                {
                    if (intersection2(nhomTMs[i].Ds_MaTieuMuc, nhomTMs[j].Ds_MaTieuMuc) != "")
                    {
                        ketQua = ketQua + nhomTMs[i].TenNhomTieuMuc + "-" + nhomTMs[i].TenNhomTieuMuc;
                    }
                }
            }

            return View(nhomTMs);

        }
        #endregion


        private string intersection2(string x1, string x2)
        {
            x1 = x1.Replace(" ", "");
            x2 = x2.Replace(" ", "");
            string[] string1 = x1.Split(',');
            string[] string2 = x2.Split(',');
            var m = string1.Distinct();
            var n = string2.Distinct();
            var test = " ";

            var results = m.Intersect(n, StringComparer.OrdinalIgnoreCase);
            foreach (var k in results) test += k + " ";
            return test;
        }

    }
}