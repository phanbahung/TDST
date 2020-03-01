using System.IO;
using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using TDST.Models;
using System.Collections.Generic;
using TDST_CRUD.EF;
using TDST.Common;
using TDST_CRUD.Dao;
using TDST_CRUD.ViewModels;
using ViewModels;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class DuToanController : BaseController
    {
        public ActionResult Index()
        { 
            ViewBag.Title = "Home Page";
            return View();
        }

        //[HasCredential(RoleID = "DUTOAN_TraCuu_Get", MoTa = "Hiển thị màn hình tra cứu dự toán")]
        public ActionResult tracuu(int? SelectedNamDuToan, int? SelectedBoChiTieu)
        {
            var duToanDao = new DuToanDao();
            ViewBag.SelectedNamDuToan = new SelectList(duToanDao.Get_NamDuToan(), "NamDuToan", "NamDuToan", SelectedNamDuToan);
            var boCT = new BoChiTieuDao();
            ViewBag.SelectedBoChiTieu = new SelectList(boCT.Ds_BoChiTieu(), "IdBoChiTieu", "TenBoChiTieu", SelectedNamDuToan);

            int selectedNamDuToan = SelectedNamDuToan.GetValueOrDefault();
            int selectedBoChiTieu = SelectedBoChiTieu.GetValueOrDefault();

            var model = duToanDao.Get_DS_DuToan(selectedNamDuToan, selectedBoChiTieu);
            return View(model);
        }


        [HttpGet]
        //[HasCredential(RoleID = "DUTOAN_UploadExcel_Get", MoTa = "Hiển thị màn hình upload")]
        public ActionResult UploadExcel(int? SelectedDuToan)
        {
            //SetViewBag_DuToan(SelectedDuToan);
            var dao = new DuToanDao();
            ViewBag.SelectedDuToan = new SelectList(dao.Ds_DuToan(), "IdDuToan", "TenDuToan");
           // ViewData["SelectedDuToan"] = new SelectList(dao.Ds_DuToan(), "IdDuToan", "TenDuToan");
          
            return View();
            /////   https://www.c-sharpcorner.com/article/upload-files-in-asp-net-mvc-5/
        }


        [HttpPost]
        [HasCredential(RoleID = "DUTOAN_UploadExcel_Post", MoTa = "Cho phép upload file excel dự toán vào UD")]
        public ActionResult UploadExcel(HttpPostedFileBase file, int? SelectedDuToan)
        {

            string _FileName = "";

            int selectedDuToan = SelectedDuToan.GetValueOrDefault();
            // Bawts buoojc phair cos sau khi PosstBack dđể có list Năm dự toán
            var dao = new DuToanDao();
            ViewBag.SelectedDuToan = new SelectList(dao.Ds_DuToan(), "IdDuToan", "TenDuToan");

            //try
            //{
            //------ 1. Upload file to folder on server
            UploadHelper upload = new UploadHelper();
            _FileName = upload.UploadExcel(file, Server.MapPath("~/UploadXML"));
            //2. ---- Import db from file uploaded - Exxcel file 
            // -- Map file name to local path
            FileInfo fileUploaded = new FileInfo(Server.MapPath("~/UploadXML/") + _FileName);
            // Model cập nhật dự toán
            DuToanDao duToanModel = new DuToanDao();
            if(selectedDuToan!=0)
            { 
                    using (ExcelPackage package = new ExcelPackage(fileUploaded))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["MauNhapDuToan"];
                        int totalRows = workSheet.Dimension.Rows;  // tổng cột có data              
                        int totalColumns = workSheet.Dimension.Columns; // tỏng cột có data
                        int startCol_Data = 4;  // Cột bắt đàu có dữ liệu
                        int startRow_Data = 4;  // Hàng bắt đầu có dữ liệu
                        int row_Quy = 1;        // Quý: hàng ko thay đổi
                        int row_MaDonVi = 3;    // MaDonVi: hàng khong thay đổi
                        int col_ChiTieu = 2;    

                        int quy = 0;
                        int maDonVi;

                        int idChiTieu = 0;
                        Decimal soThue = 0;

                        string str_IdChiTieu;
                        string str_SoThue;

                        ViewBag.Message = workSheet.Cells[4, 4].Value.ToString();// totalRows.ToString()+"-"+totalColumns.ToString();

                        for (int col = startCol_Data; col <= totalColumns; col++)
                        {
                            quy = int.Parse(workSheet.Cells[row_Quy, col].Value.ToString());
                            maDonVi = int.Parse(workSheet.Cells[row_MaDonVi, col].Value.ToString());

                            for (int row = startRow_Data; row <= totalRows; row++)
                            {
                                str_IdChiTieu = workSheet.Cells[row, col_ChiTieu].Text.Trim();
                                str_SoThue = workSheet.Cells[row, col].Text.Trim();

                                if (str_IdChiTieu != "")
                                    idChiTieu = int.Parse(str_IdChiTieu);
                                else
                                    idChiTieu = 0;

                                if (str_SoThue != "")
                                    soThue = Decimal.Parse(str_SoThue);
                                else
                                    soThue = 0;

                                duToanModel.UpDate_DuToan(selectedDuToan, maDonVi, idChiTieu, quy, soThue);
                            }// Duyệt qua các hàng
                        }// Duyệt qua các cột (quý, mã đơn vị)              

                        // Xong thì xóa
                    fileUploaded.Delete();

                    }  //---------- End using
            }
            else
            {
                ViewBag.Message = "Chưa chọn dư toán";

            }
            
            // Update xong thì xóa file excel
            fileUploaded.Delete();

            //ViewBag.Message = _FileName;////"Nhận thành công. Đề nghị kiểm tra lại!!";
            return View();
            //}
            //catch
            //{
            //    ViewBag.Message =  "Quá trình nhận bị lỗi!";
            //    return View();
            //}
        }
       

        [HttpGet]        
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }      

        [HttpPost]
        [HasCredential(RoleID = "DUTOAN_Create_TaoDuToan_Post", MoTa = "Tạo tệp dự toán")]
        public ActionResult Create(DuToan duToan)
        {
            if (ModelState.IsValid)
            {
                var dao = new DuToanDao();

                long id = dao.Insert(duToan);
                if (id > 0)
                {
                    //SetAlert("Thêm sản phẩm thành công", "success");
                    //return RedirectToAction("trac", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
            SetViewBag(duToan.IdBoChiTieu);
            return View("index");
        }

        private void SetViewBag(long? selectedId = null)
        {
            var dao = new BoChiTieuDao();
            ViewBag.IdBoChiTieu = new SelectList(dao.Ds_BoChiTieu(), "IdBoChiTieu", "TenBoChiTieu", selectedId);
        }






        #region CreateExcelFile
        private Stream CreateExcelFile(int idDuToan, Stream stream = null)
        {
            //var list = CreateTestItems();
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "pbhung.khh";
                excelPackage.Workbook.Properties.Company = "Cục Thuế tỉnh Khánh Hòa";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "TDST - pbhung";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "File kết xuất từ UD TDST";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("TheoDoiST- Mau");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                
                Fill_Data_DuToan(workSheet, idDuToan);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        #endregion

        #region Báo cáo tổng hợp
        private Stream CreateExcelFile_DuToan( string report,int idDuToan,Stream stream = null)
        {
            //var list = CreateTestItems();
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "pbhung.khh";
                excelPackage.Workbook.Properties.Company = "Cục Thuế tỉnh Khánh Hòa";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "TDST - pbhung";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "File kết xuất từ UD TDST";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("DuToan");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];

                // workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.None);
                if(report == "export")
                    Fill_Data_DuToan(workSheet, idDuToan);
                else if (report == "maunhapdutoan")
                    Fill_Mau_DuToan(workSheet, idDuToan);
                else if (report == "sothuchien")
                    Fill_Data_SoThucHien(workSheet, idDuToan);
                else if (report == "sothuchien_chotso")
                    Fill_Data_SoThucHien_Sau_KhoaSo(workSheet, idDuToan);
                else if (report == "chotso_khobac")
                    Fill_Data_Sau_ChotSo_KhoBac(workSheet, idDuToan);

                
                //else if (report == "sophantram")
                // Fill_Data_SoPhanTram(workSheet, idDuToan);

                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        #endregion

        #region  In ra Số thực hiện Lũy kế (tức là khóa sổ) theo ngày kho bạc

        private void Fill_Data_Sau_ChotSo_KhoBac(ExcelWorksheet worksheet, int idDuToan)
        {
            // Current Row and current Column 
            DuToanDao duToanDao = new DuToanDao();
            DuToan modelDuToan = duToanDao.Details(idDuToan);
            int idBoChiTieu_cua_DuToan = duToanDao.Get_IdBoChiTieu_By_IdDuToan(idDuToan);

            Fill_Report_Header(worksheet, modelDuToan.TenDuToan);

            int row_TenCot = 10;
            int row_SoSanh = row_TenCot;
            int current_Row = row_TenCot;
            int row_ChiTieu = 12;

            int soLuongDonVi;
            int soLuongChiTieu;

            List<BCDuToanViewModel> duToan_By_Quy_DV = null;
            // Set default width cho tất cả column
            // worksheet.DefaultColWidth = 10;
            // Set cột Chi tiêu rộng hơn
            //worksheet.Column(2).BestFit = true;
            worksheet.Column(2).Width = 60;

            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;
            // Tạo header - column name
            worksheet.Cells[row_TenCot, 1].Value = "STT";
            worksheet.Cells[row_TenCot, 2].Value = "Chỉ tiêu";
            worksheet.Cells[row_TenCot, 1, row_TenCot + 1, 1].Merge = true;
            worksheet.Cells[row_TenCot, 2, row_TenCot + 1, 2].Merge = true;

            // DS đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();

            List<BCDuToanViewModel> duToanChotSo = duToanDao.Get_ChotSo_CuaCqThue_KhoBac(idDuToan);

            //-- List đơn vị có trong dự toán            
            List<string> lstDonVi_in_DuToan = duToanChotSo.OrderBy(x => x.STT).Select(x => x.MaDonVi).Distinct().ToList();
            soLuongDonVi = lstDonVi_in_DuToan.Count();
            //-- List chỉ tiêu có trong dự toán            
            List<string> lstChiTieu = duToanChotSo.Select(i => new { i.IdChiTieu, i.TenChiTieu }).Distinct().ToList().Select(x => x.TenChiTieu).ToList();
            //var data = TESTDB.Where(i => i.ALPHA == 1).Select(i => new {i.A, i.B, i.C}).Distinct();
            soLuongChiTieu = lstChiTieu.Count();
            
            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = row_ChiTieu;
            for (int index = 0; index < soLuongChiTieu; index++)
            {
                worksheet.Cells[current_Row, 2].Value = lstChiTieu[index];
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }

            //----- -Lấy dự toán theo Id


            string maDonVi;

            int col_DonVi = 3;
            int q;
            string quyName = "";

            for (int quy = 0; quy <= 4; quy++)
            {
                q = quy + 1;
                if (quy == 0) quyName = "NĂM 2019";
                else quyName = "QUÝ  " + quy.ToString() + "/" + DateTime.Now.Year.ToString();

                worksheet.Cells[row_SoSanh, 3 + (q - 1) * soLuongDonVi].Value = quyName;
                // Merge cells
                worksheet.Cells[row_SoSanh, (q - 1) * soLuongDonVi + 3, row_SoSanh, q * soLuongDonVi + 2].Merge = true;


                for (int i = 0; i < soLuongDonVi; i++)
                {
                    // Reset lại dòng
                    current_Row = row_TenCot + 1;

                    maDonVi = lstDonVi_in_DuToan[i];
                    // Column name = TenVietTat1
                    worksheet.Cells[current_Row, col_DonVi].Value = listDV.Where(x => x.MaDonVi == maDonVi).Select(x => x.TenVietTat1).SingleOrDefault();

                    current_Row++;

                    //duToan = duToanModel.Get_SoThucHien(idDuToan, listDV[i].MaDonVi.ToString(), quy);
                    duToan_By_Quy_DV = duToanChotSo.Where(x => x.MaDonVi == maDonVi && x.Quy == quy).ToList();

                    for (int ct = 0; ct < soLuongChiTieu; ct++)
                    {
                        //worksheet.Cells[current_Row, col_DonVi].Value = Decimal.Parse(duToan[ct].SoThucHien)/1000000;                        
                        //worksheet.Cells[current_Row, col_DonVi].Value = duToan[ct].PhanTramThucHien ;
                        worksheet.Cells[current_Row, col_DonVi].Value = duToan_By_Quy_DV[ct].SoThucHien_KBac;
                        current_Row = current_Row + 1; // tăng lên sau khi điền
                    }
                    // Dich cột đơn vị sang phải
                    col_DonVi = col_DonVi + 1;
                } // end dơn vị

            }// end quý



        }


        #endregion




        #region  In ra Số thực hiện Lũy kế (tức là khóa sổ) theo ngày hạch toán

        private void Fill_Data_SoThucHien_Sau_KhoaSo(ExcelWorksheet worksheet, int idDuToan)
        {
            // Current Row and current Column 
            DuToanDao duToanDao = new DuToanDao();
            DuToan modelDuToan = duToanDao.Details(idDuToan);
            int idBoChiTieu_cua_DuToan = duToanDao.Get_IdBoChiTieu_By_IdDuToan(idDuToan);

            Fill_Report_Header(worksheet, modelDuToan.TenDuToan);

            int row_TenCot = 10;
            int row_SoSanh = row_TenCot;
            int current_Row = row_TenCot;
            int row_ChiTieu = 12;

            int soLuongDonVi;
            int soLuongChiTieu;

            List<BCDuToanViewModel> duToan_By_Quy_DV = null;
            // Set default width cho tất cả column
            // worksheet.DefaultColWidth = 10;
            // Set cột Chi tiêu rộng hơn
            //worksheet.Column(2).BestFit = true;
            worksheet.Column(2).Width = 60;

            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;
            // Tạo header - column name
            worksheet.Cells[row_TenCot, 1].Value = "STT";
            worksheet.Cells[row_TenCot, 2].Value = "Chỉ tiêu";
            worksheet.Cells[row_TenCot, 1, row_TenCot + 1, 1].Merge = true;
            worksheet.Cells[row_TenCot, 2, row_TenCot + 1, 2].Merge = true;

            // DS đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();

            List<BCDuToanViewModel> duToanChotSo = duToanDao.Get_ThucHien_Sau_ChotSo(idDuToan);

            //-- List đơn vị có trong dự toán
            //List<string> lstDonVi_in_DuToan = duToanChotSo.Select(x => x.MaDonVi).Distinct().ToList();
            List<string> lstDonVi_in_DuToan = duToanChotSo.OrderBy(x=>x.STT).Select(x => x.MaDonVi).Distinct().ToList();
            soLuongDonVi = lstDonVi_in_DuToan.Count();
            //-- List chỉ tiêu có trong dự toán            
            List<string> lstChiTieu = duToanChotSo.Select(i => new { i.IdChiTieu, i.TenChiTieu}).Distinct().ToList().Select(x=>x.TenChiTieu).ToList();
            //var data = TESTDB.Where(i => i.ALPHA == 1).Select(i => new {i.A, i.B, i.C}).Distinct();
            //DistinctBy
            soLuongChiTieu = lstChiTieu.Count();


            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = row_ChiTieu;
            for (int index = 0; index < soLuongChiTieu; index++)
            {
                worksheet.Cells[current_Row, 2].Value = lstChiTieu[index];
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }

            //----- -Lấy dự toán theo Id


            string maDonVi;
                      
            int col_DonVi = 3;
            int q;
            string quyName = "";

            for (int quy = 0; quy <= 4; quy++)
            {
                q = quy + 1;
                if (quy == 0) quyName = "NĂM 2019";
                else quyName = "QUÝ  " + quy.ToString() + "/" + DateTime.Now.Year.ToString();

                worksheet.Cells[row_SoSanh, 3 + (q - 1) * soLuongDonVi].Value = quyName;
                // Merge cells
                worksheet.Cells[row_SoSanh, (q - 1) * soLuongDonVi + 3, row_SoSanh, q * soLuongDonVi + 2].Merge = true;


                for (int i = 0; i < soLuongDonVi; i++)
                {
                    // Reset lại dòng
                    current_Row = row_TenCot + 1;

                    maDonVi = lstDonVi_in_DuToan[i];
                    // Column name = TenVietTat1
                    worksheet.Cells[current_Row, col_DonVi].Value =  listDV.Where(x=>x.MaDonVi==maDonVi).Select(x=>x.TenVietTat1).SingleOrDefault() ;
                  
                    current_Row++;

                    //duToan = duToanModel.Get_SoThucHien(idDuToan, listDV[i].MaDonVi.ToString(), quy);
                    duToan_By_Quy_DV = duToanChotSo.Where(x => x.MaDonVi == maDonVi&&x.Quy==quy).ToList();
                 
                    for (int ct = 0; ct < soLuongChiTieu; ct++)
                    {
                        //worksheet.Cells[current_Row, col_DonVi].Value = Decimal.Parse(duToan[ct].SoThucHien)/1000000;                        
                        //worksheet.Cells[current_Row, col_DonVi].Value = duToan[ct].PhanTramThucHien ;
                        worksheet.Cells[current_Row, col_DonVi].Value = duToan_By_Quy_DV[ct].SoThucHien;
                        current_Row = current_Row + 1; // tăng lên sau khi điền
                    }
                    // Dich cột đơn vị sang phải
                    col_DonVi = col_DonVi + 1;
                } // end dơn vị

            }// end quý



        }


        #endregion


        #region  In ra Số thực hiện realtime
        private void Fill_Data_SoThucHien(ExcelWorksheet worksheet, int idDuToan)
        {
            // Current Row and current Column 
            DuToanDao duToanDao = new DuToanDao();
            DuToan modelDuToan = duToanDao.Details(idDuToan);
            int idBoChiTieu_cua_DuToan = duToanDao.Get_IdBoChiTieu_By_IdDuToan(idDuToan);

            Fill_Report_Header(worksheet, modelDuToan.TenDuToan);

            int row_TenCot = 10;
            int row_SoSanh = row_TenCot;
            int current_Row = row_TenCot;
            int row_ChiTieu = 12;

            // Set default width cho tất cả column
            // worksheet.DefaultColWidth = 10;
            // Set cột Chi tiêu rộng hơn
            //worksheet.Column(2).BestFit = true;
            worksheet.Column(2).Width = 60;

            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;
            // Tạo header - column name
            worksheet.Cells[row_TenCot, 1].Value = "STT";
            worksheet.Cells[row_TenCot, 2].Value = "Chỉ tiêu";
            worksheet.Cells[row_TenCot, 1, row_TenCot + 1, 1].Merge = true;
            worksheet.Cells[row_TenCot, 2, row_TenCot + 1, 2].Merge = true;

            // tiêu đề là các đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();
            int soLuongDonVi = listDV.Count;
            BaoCaoModel baocao = new BaoCaoModel();
            DuToanDao duToanModel = new DuToanDao();
            BoChiTieuDao boChiTieuModel = new BoChiTieuDao();


            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = row_ChiTieu;
            List<ChiTieuViewModel> boChiTieu = boChiTieuModel.Get_DsChiTieu(idBoChiTieu_cua_DuToan);
            for (int index = 0; index < boChiTieu.Count; index++)
            {
                worksheet.Cells[current_Row, 2].Value = boChiTieu[index].TenChiTieu;
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }

            //----- -Điền số liệu

            List<BCDuToanViewModel> duToan;
            int sodongDuToan;//= bcTongHop.Count;
            int col_DonVi = 3;
            int q;
            string quyName = "";
            for (int quy = 0; quy <= 4; quy++)
            {
                q = quy + 1;
                if (quy == 0) quyName = "NĂM 2019";
                else      quyName=   "QUÝ  " + quy.ToString() + "/" + DateTime.Now.Year.ToString();

                worksheet.Cells[row_SoSanh, 3 + (q - 1) * soLuongDonVi].Value = quyName;
                // Merge cells
                worksheet.Cells[row_SoSanh, (q - 1) * soLuongDonVi + 3, row_SoSanh, q * soLuongDonVi + 2].Merge = true;


                for (int i = 0; i < soLuongDonVi; i++)
                {
                    // Reset lại dòng
                    current_Row = row_TenCot + 1;
                    // Column name = TenVietTat1
                    worksheet.Cells[current_Row, col_DonVi].Value = listDV[i].TenVietTat1;
                    current_Row++;

                    duToan = duToanModel.Get_SoThucHien(idDuToan, listDV[i].MaDonVi.ToString(), quy);
                    
                    sodongDuToan = duToan.Count;
                    for (int ct = 0; ct < sodongDuToan; ct++)
                    {
                        //worksheet.Cells[current_Row, col_DonVi].Value = Decimal.Parse(duToan[ct].SoThucHien)/1000000;                        
                        //worksheet.Cells[current_Row, col_DonVi].Value = duToan[ct].PhanTramThucHien ;
                        worksheet.Cells[current_Row, col_DonVi].Value = duToan[ct].SoThucHien;
                        current_Row = current_Row + 1; // tăng lên sau khi điền
                    }
                    // Dich cột đơn vị sang phải
                    col_DonVi = col_DonVi + 1;
                } // end dơn vị

            }// end quý



        }
        
        #endregion

        private void Fill_Report_Header(ExcelWorksheet worksheet, string reportName)
        {

            int row_CqThue_CapTren = 1;
            int row_CqThue_CapDuoi = 2;
            int row_TenBaoCao = 6;
            worksheet.Cells[row_CqThue_CapTren, 2].Value = "TỔNG CỤC THUẾ";
            worksheet.Cells[row_CqThue_CapDuoi, 2].Value = "CỤC THUẾ TỈNH KHÁNH HÒA";
            worksheet.Cells[row_TenBaoCao, 6].Value = reportName;
            worksheet.Cells[row_TenBaoCao, 6].Style.Font.Size = 16;
            worksheet.Cells[row_TenBaoCao, 6].Style.Font.Bold = true;
            worksheet.Cells[row_TenBaoCao + 2, 14].Value = "Đơn vị tính: " + "triệu đồng";


        }


        #region  In ra dự toán

        private void Fill_Data_DuToan(ExcelWorksheet worksheet, int idDuToan)
        {
            // Current Row and current Column 
            DuToanDao duToanDao = new DuToanDao();
            DuToan modelDuToan = duToanDao.Details(idDuToan);
            int idBoChiTieu_cua_DuToan = duToanDao.Get_IdBoChiTieu_By_IdDuToan(idDuToan);

            Fill_Report_Header(worksheet,modelDuToan.TenDuToan);

            int row_TenCot = 10;           
            int row_SoSanh = row_TenCot;
            int current_Row = row_TenCot;
            int row_ChiTieu = 12;


            // Set default width cho tất cả column
            // worksheet.DefaultColWidth = 10;
            // Set cột Chi tiêu rộng hơn
            //worksheet.Column(2).BestFit = true;
            worksheet.Column(2).Width = 60;

            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;
            // Tạo header - column name
            worksheet.Cells[row_TenCot, 1].Value = "STT";
            worksheet.Cells[row_TenCot, 2].Value = "Chỉ tiêu";
            worksheet.Cells[row_TenCot, 1, row_TenCot + 1, 1].Merge = true;
            worksheet.Cells[row_TenCot, 2, row_TenCot + 1, 2].Merge = true;

            //=========== Content tiêu đề là các đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();
            int soLuongDonVi = listDV.Count;
            BaoCaoModel baocao = new BaoCaoModel();
            DuToanDao duToanModel = new DuToanDao();
            BoChiTieuDao boChiTieuModel = new BoChiTieuDao();
            

            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = row_ChiTieu;
            List<ChiTieuViewModel> boChiTieu = boChiTieuModel.Get_DsChiTieu(idBoChiTieu_cua_DuToan);
            for (int index = 0; index < boChiTieu.Count; index++)
            {
                worksheet.Cells[current_Row, 2].Value = boChiTieu[index].TenChiTieu;
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }

           //----- -Điền số liệu

            List<BCDuToanViewModel> duToan;
            int sodongDuToan;//= bcTongHop.Count;
            int col_DonVi = 3;
            int q;
            for (int quy = 0; quy <= 4; quy++)
            {
                q = quy + 1;
                worksheet.Cells[row_SoSanh, 3 + (q - 1) * soLuongDonVi].Value = "QUÝ  " + quy.ToString() + "/" + DateTime.Now.Year.ToString();
                // Merge cells
                worksheet.Cells[row_SoSanh, (q - 1) * soLuongDonVi + 3, row_SoSanh, q * soLuongDonVi + 2].Merge = true;


                for (int i = 0; i < soLuongDonVi; i++)
                {
                    // Reset lại dòng
                    current_Row = row_TenCot + 1;
                    // Column name = Ten Viettat don vi
                    worksheet.Cells[current_Row, col_DonVi].Value = listDV[i].TenVietTat1;
                    current_Row++;

                    duToan = duToanModel.Get_DuToan(idDuToan, listDV[i].MaDonVi.ToString(), quy);
                    sodongDuToan = duToan.Count;
                    for (int ct = 0; ct < sodongDuToan; ct++)
                    {
                        worksheet.Cells[current_Row, col_DonVi].Value = duToan[ct].SoThue;                      
                        current_Row = current_Row + 1; // tằng lên sau khi điền
                    }
                    // Dich cột đơn vị sang phải
                    col_DonVi = col_DonVi + 1;
                } // end dơn vị

            }// end quý



        }


        #endregion

        #region Fill_Mau_DuToan
        private void Fill_Mau_DuToan(ExcelWorksheet worksheet, int idDuToan)
        {
            
            int row_Quy= 1;
            int row_TieuDe = row_Quy + 2;
            int row_TenVietTat = 2;
            int row_MaDonVi = 3;            
            int row_ChiTieu = 4;            
            int current_Row;


            worksheet.Column(3).Width = 60;
          
            worksheet.Cells[row_TieuDe, 1].Value = "Tên Bộ chỉ tiêu";
            worksheet.Cells[row_TieuDe, 2].Value = "ID chỉ tiêu";
            worksheet.Cells[row_TieuDe, 3].Value = "Chỉ tiêu";
            worksheet.Cells[row_Quy, 1, row_Quy + 1, 1].Merge = true;
            worksheet.Cells[row_Quy, 2, row_Quy + 1, 2].Merge = true;

            // tiêu đề là các đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();
            int soLuongDonVi = listDV.Count;
            BaoCaoModel baocao = new BaoCaoModel();
            DuToanDao duToanDao = new DuToanDao();
            int idBoChiTieu = duToanDao.Get_IdBoChiTieu_By_IdDuToan(idDuToan);
            BoChiTieuDao boChiTieuModel = new BoChiTieuDao();


            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = row_ChiTieu;
            List<ChiTieuViewModel > boChiTieu = boChiTieuModel.Get_DsChiTieu(idBoChiTieu);
            for (int index = 0; index < boChiTieu.Count; index++)
            {
                worksheet.Cells[current_Row, 1].Value = boChiTieu[index].IdBoChiTieu;
                worksheet.Cells[current_Row, 2].Value = boChiTieu[index].IdChiTieu;
                worksheet.Cells[current_Row, 3].Value = boChiTieu[index].TenChiTieu;               
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }            
            int q;
            for (int quy = 0; quy <= 4; quy++)
            {
                q = quy + 1;
                for (int dv = 0; dv < soLuongDonVi; dv++)
                {
                    worksheet.Cells[row_Quy, 4 + (q - 1)*soLuongDonVi+dv].Value = quy;
                    worksheet.Cells[row_TenVietTat, 4 + (q - 1) * soLuongDonVi + dv].Value = listDV[dv].TenVietTat1;
                    worksheet.Cells[row_MaDonVi, 4 + (q - 1) * soLuongDonVi + dv].Value = listDV[dv].MaDonVi;
                    //current_Row++;
                } // end dơn vị
            }// end quy
          
        }
        #endregion


        [HttpGet]
        [HasCredential(RoleID = "DUTOAN_UploadExcel_DuToanGiao_Get", MoTa = "Hiển thị màn hình upload file excel giao dự toán")]
        public FileContentResult DuToan_Giao(int id)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_DuToan("export", id);
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "DuToan.xlsx");  // FileContentResult
        }

        [HttpGet]        
        public FileContentResult DuToan_FileMau(int id)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_DuToan("maunhapdutoan", id); ;
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "DuToan.xlsx");  // FileContentResult
        }

        [HttpGet]
        //[HasCredential(RoleID = "DUTOAN_UploadExcel_Post", MoTa = "Cho phép upload file excel dự toán vào UD")]
        public FileContentResult DuToan_SoThucHien(int id)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_DuToan("sothuchien", id); ;
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "SoThucHien.xlsx");  // FileContentResult
        }

        [HttpGet]
        public FileContentResult DuToan_SoThucHien_ChotSo(int id)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_DuToan("sothuchien_chotso", id); ;
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "SoThucHien.xlsx");  // FileContentResult
        }

        [HttpGet]
       // [HasCredential(RoleID = "DUTOAN_UploadExcel_Post", MoTa = "Cho phép upload file excel dự toán vào UD")]
        public FileContentResult ChotSo_KhoBac(int id)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_DuToan("chotso_khobac", id); ;
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "SoThucHien.xlsx");  // FileContentResult
        }


    }
}