using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TDST.Common;
using TDST.Models;
using TDST_CRUD.Dao;
using TDST_CRUD.EF;
using TDST_CRUD.ViewModels;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class ChungTuController : BaseController
    {
        
        [HttpGet]        
        public ActionResult UploadXMLFiles()
        {
            return View();
            /////   https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
        }

        [HttpPost]
        [HasCredential(RoleID = "CHUNGTU_UploadXMLFiles_Post", MoTa = "Cho pehsp upload file XML vào UD")]
        public ActionResult UploadXMLFiles(HttpPostedFileBase[] files)
        {

            int countChungTu = 0;
            //Ensure model state is valid
            if (ModelState.IsValid)
            {   //iterating through multiple file collection 

                
                NguoiNopThueDao nntDao = new NguoiNopThueDao();
                DanhMucDao dmDao = new DanhMucDao();
                ChungTuDao chungTuDao = new ChungTuDao();
                List<NhomTieuMuc> dsNhomTM = dmDao.Get_NhomTieuMuc_ScanIsTrue();
                List<NhomChuong> dsNhomCH = dmDao.Get_DmNhomChuong();
                int soLuongNhomTM = dsNhomTM.Count;
                int soLuongNhomCH = dsNhomCH.Count;
                //int tm, ch;
                string maDonVi_QL_KhoanThu;
                long nhomChuong=0, nhomTieuMuc=0;


                string _FileName = "";
                string[] arr_MaCqThu = { "1056198", "1056196", "1056258", "1056256", "1056259", "1056257", "1056351", "1056348" };
                //List<string> lstMaCqThu;
                
                //------ 1. Init ppload file to folder to server
                UploadHelper upload = new UploadHelper();
                // ----- 2. Init ppload file to folder to server
                XmlDocument doc = new XmlDocument();
                // ----- 3. Init insert chung tu
                var chungtu = new ChungTuDao();
                // Init result return
                long res = 0;
                string _OnlyFileName ="";
                foreach (HttpPostedFileBase file in files)
                {
                    _OnlyFileName = Path.GetFileName(file.FileName);
                    //Checking file is available to save.
                    if (file != null)
                    {
                        //==== 1. Upload to sserver
                        _FileName = upload.UploadXML(file, Server.MapPath("~/UploadXML")); 
                                             
                        //==== 2. Load the XML file in XmlDocument.
                        doc.Load(Server.MapPath("~/UploadXML/") + _FileName);

                        //==== 3. ---- Import db from file uploaded - XML file                     

                        //Loop through the selected Nodes.
                        foreach (XmlNode node in doc.SelectNodes("/Data/Body/Transaction"))
                        {
                            //Fetch the Node values and assign it to Model.
                            if ((node["tk_co_dtl"].InnerText == "7111") && (node["so_tkno"].InnerText != "HOAN")
                                && (arr_MaCqThu.Contains(node["ma_cqthu"].InnerText)))
                            {
                                // Xác định đơn vị nào quản lý khoản thu theo thứ tự ưu tiên:  1. Tiểu mục 2. NNT
                                maDonVi_QL_KhoanThu = nntDao.Get_MaDonViQuanLy_KhoanThu(node["tin"].InnerText, node["ma_tmuc"].InnerText, node["ngay_kbac"].InnerText, node["ma_cqthu"].InnerText);

                                // Xác đinh thuôc nhóm chương và TM

                                nhomTieuMuc = Check_TM_Thuoc_NhomTM(dsNhomTM, node["ma_tmuc"].InnerText);

                                nhomChuong = Check_CH_Thuoc_NhomCH(dsNhomCH, node["ma_chuong"].InnerText);
                              

                                // LƯU Ý CHỮ không VIẾT HOA CỦA CÁC TRƯỜNG TRONG node 
                                res = chungtu.Insert_ChungTu(node["khct_soct"].InnerText, node["ten_nnthue"].InnerText,
                                                            node["ngay_kbac"].InnerText, node["ngay_kbac"].InnerText,
                                                            node["ngay_ht"].InnerText, node["ky_thue"].InnerText,
                                                            node["ma_chuong"].InnerText, node["ma_tmuc"].InnerText,
                                                            node["so_tien"].InnerText, node["tk_co_dtl"].InnerText,
                                                            node["tin"].InnerText, node["ma_cqthu"].InnerText,
                                                            nhomChuong, nhomTieuMuc,
                                                            maDonVi_QL_KhoanThu, _OnlyFileName);

                            }// end if

                            countChungTu++;

                            // then every 1k cycles, see if we have > 100mb allocated
                            // and force the GC to free the memory
                            if (countChungTu % 800 == 0 & GC.GetTotalMemory(false) > 100000000)
                            {
                                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                                //GC.Collect();
                                // countChungTu = 0;
                            }


                        } // end for                    
                          //---------- End insert into db

                        ViewBag.UploadStatus = files.Count().ToString() + " file(s) upload thành công!";
                    } // End if

                } // End foreach
            }

            return View();
        }


        [HttpGet]
        public ActionResult UploadXML()
        {
            return View();
            /////   https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
        }   


        [HttpPost]
        public ActionResult UploadXML(HttpPostedFileBase file)
        {

            NguoiNopThueDao nntDao = new NguoiNopThueDao();
            string _FileName = "";
            string[] arr_MaCqThu = { "1056198", "1056196", "1056258", "1056256", "1056259", "1056257", "1056351", "1056348" };
            //try
            //{
            //------ 1. Upload file to folder on server
            UploadHelper upload = new UploadHelper();
            _FileName = upload.UploadXML(file, Server.MapPath("~/UploadXML"));


            //2. ---- Import db from file uploaded - XML file

            //Load the XML file in XmlDocument.
            XmlDocument doc = new XmlDocument();
            //doc.Load(Server.MapPath("~/XML/chứng từ.xml"));
            doc.Load(Server.MapPath("~/UploadXML/") + _FileName);

            var chungtu = new ChungTuDao();
            //long res = 0;
            
            //Loop through the selected Nodes.
            foreach (XmlNode node in doc.SelectNodes("/Data/Body/Transaction"))
            {
                //Fetch the Node values and assign it to Model.
                if ((node["tk_co_dtl"].InnerText == "7111") && (node["so_tkno"].InnerText != "HOAN")
                    && (arr_MaCqThu.Contains(node["ma_cqthu"].InnerText)))

                {
                    string maDonVi = nntDao.Get_MaDonViQuanLy_KhoanThu(node["tin"].InnerText, node["ma_tmuc"].InnerText, node["ngay_kbac"].InnerText, node["ma_cqthu"].InnerText);
                    // LƯU Ý CHỮ không VIẾT HOA CỦA CÁC TRƯỜNG TRONG node 
                    //res = chungtu.Insert_ChungTu(node["khct_soct"].InnerText, node["ten_nnthue"].InnerText
                    //    , node["ngay_kbac"].InnerText, node["ngay_kb"].InnerText
                    //    , node["ngay_ht"].InnerText, node["ky_thue"].InnerText, node["ma_chuong"].InnerText
                    //    , node["ma_tmuc"].InnerText, node["so_tien"].InnerText, node["tk_co_dtl"].InnerText
                    //    , node["tin"].InnerText, node["ma_cqthu"].InnerText, maDonVi, _FileName);

                }// end if

            } // end for                    
              //---------- End insert into db



            //    ViewBag.Message = _path;////"Nhận thành công. Đề nghị kiểm tra lại!!";
            return View();
            //}
            //catch
            //{
            //    ViewBag.Message =  "Quá trình nhận bị lỗi!!";
            //    return View();
            //}
        }

        private long Check_TM_Thuoc_NhomTM(List<NhomTieuMuc> dsNhomTM, string maTieuMuc)
        {
            long result = -1;

            for (int tm = 0; tm < dsNhomTM.Count; tm++)
            {
                //node["ma_chuong"].InnerText
                if (dsNhomTM[tm].Ds_MaTieuMuc.Contains(maTieuMuc.Trim()))
                    result = dsNhomTM[tm].IdNhomTieuMuc;
                
            }
            return result;   
        }

        private long Check_CH_Thuoc_NhomCH(List<NhomChuong> dsNhomCH, string maChuong)
        {
            long result = -1;

            for (int ch = 0; ch < dsNhomCH.Count; ch++)
            {
                //node["ma_chuong"].InnerText
                if (dsNhomCH[ch].Ds_MaChuong.Contains(maChuong.Trim()))
                    result = dsNhomCH[ch].IdNhomChuong;                
            }
            return result;

        }

        #region Tra cuu chứng từ đã nhập
        [HttpGet]
        public ActionResult TraCuu(string id)
        {
            ChungTuDao chungtu = new ChungTuDao();
            if (id == null) id = "0";

            var model = chungtu.TraCuuChungTu(id);
            return View(model);
        }

        #endregion
        

        #region Thông kê file chứng từ
        public ActionResult thongkefile(string id)
        {
            ChungTuDao chungTuDao = new ChungTuDao();

            List<FileChungTuViewModel> books = chungTuDao.ThongKeFileChungTu(id);

            // Group the books by Genre
            var booksGrouped = from b in books
                               group b by new { b.Ma_cqthu, b.TenDonVi, b.Nam_KhoBac } into g
                               select new Group<string, FileChungTuViewModel> { Key = g.Key.TenDonVi + " - Cơ quan thu: " + g.Key.Ma_cqthu + " - Năm: " + g.Key.Nam_KhoBac, Values = g };

            return View(booksGrouped.ToList());
        }
        #endregion    



    }
}