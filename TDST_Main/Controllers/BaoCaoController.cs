using System.Drawing;
using System.IO;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using TDST.Models;
using System.Collections.Generic;
using TDST_CRUD.EF;
using TDST;
using System.Data.SqlClient;
using TDST_CRUD.Dao;
using TDST_CRUD.ViewModels;
using PhanQuyen.Models;

namespace TDST.Controllers
{
    public class BaoCaoController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        [HasCredential(RoleID = "BaoCao_sotheodoi", MoTa = "In sổ theo dõi")]
        public ActionResult sotheodoi(int? SelectedNamDuToan, int? SelectedBoChiTieu)
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

        [HasCredential(RoleID = "BaoCao_ChotSo_View", MoTa = "Hiển thị màn hình chốt số")]
        public ActionResult chotso()
        {
            //=== Năm dự toán : NamDuToan
            var duToanDao = new DuToanDao();
            ViewBag.SelectedNam = new SelectList(duToanDao.Get_NamDuToan(), "NamDuToan", "NamDuToan", 0);

            //==== Dự toán: IdDuToan
            ViewBag.SelectedDuToan = new SelectList(duToanDao.Ds_DuToan(), "IdDuToan", "TenDuToan", 0);

            //=== Quý: SelectedQuy
            List<Quy_ViewModel> lstQuy = new List<Quy_ViewModel>()
            {
                new Quy_ViewModel(){ IdQuy = 1, TenQuy = "Quý 1" },
                new Quy_ViewModel(){ IdQuy = 2, TenQuy = "Quý 2" },
                new Quy_ViewModel(){ IdQuy = 3, TenQuy = "Quý 3" },
                new Quy_ViewModel(){ IdQuy = 4, TenQuy = "Quý 4" },
                new Quy_ViewModel(){ IdQuy = 0, TenQuy = "Cả năm" }
            };
            // Hoặc lstQuy.Add(new Quy_ViewModel(5, "kekke"));
            ViewBag.SelectedQuy = new SelectList(lstQuy, "IdQuy", "TenQuy", "");

           
            
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "BaoCao_ChotSo_Update", MoTa = "Thao tác chốt số")]
        public ActionResult chotso(int? SelectedNam, int? SelectedQuy, int? SelectedDuToan)
        {
            //=== Năm dự toán : NamDuToan
            var duToanDao = new DuToanDao();
            ViewBag.SelectedNam = new SelectList(duToanDao.Get_NamDuToan(), "NamDuToan", "NamDuToan", SelectedNam);

            //==== Dự toán: IdDuToan
            ViewBag.SelectedDuToan = new SelectList(duToanDao.Ds_DuToan(), "IdDuToan", "TenDuToan", SelectedDuToan);

            //=== Quý: SelectedQuy
            List<Quy_ViewModel> lstQuy = new List<Quy_ViewModel>()
            {
                new Quy_ViewModel(){ IdQuy = 1, TenQuy = "Qúy 1" },
                new Quy_ViewModel(){ IdQuy = 2, TenQuy = "Quý 2" },
                new Quy_ViewModel(){ IdQuy = 3, TenQuy = "Quý 3" },
                new Quy_ViewModel(){ IdQuy = 4, TenQuy = "Quý 4" },
                new Quy_ViewModel(){ IdQuy = 0, TenQuy = "Cả năm" }
            };
            // Hoặc lstQuy.Add(new Quy_ViewModel(5, "kekke"));
            ViewBag.SelectedQuy = new SelectList(lstQuy, "IdQuy", "TenQuy", SelectedQuy);


            int selectedNam = SelectedNam.GetValueOrDefault();
            int selectedQuy = SelectedQuy.GetValueOrDefault();
            int selectedDuToan = SelectedDuToan.GetValueOrDefault();

            var model = duToanDao.ChotSoThu(selectedDuToan, selectedQuy, selectedNam.ToString());
            return View();
        }



        #region CreateExcelFile
        private Stream CreateExcelFile(Stream stream = null)
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
                // Fill data vào Excel file
                var chungtu = new ChungTuDao();
                var list = chungtu.TraCuuChungTu("0");

                // workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.None);
                BindingFormatForExcel_BCTongHop(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        #endregion

        #region Báo cao tổng hợp
        private Stream CreateExcelFile_BCTongHop(Stream stream = null)
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
                // Fill data vào Excel file
                var chungtu = new ChungTuDao();
                var list = chungtu.TraCuuChungTu("0");

                // workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.None);
                BindingFormatForExcel_BCTongHop(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }

        #endregion

        private void BindingFormatForExcel_BCTongHop(ExcelWorksheet worksheet, List<GiaoDich> listItems)
        {
            // Current Row and current Column 
            int current_Row = 9;
            int row_TieuDe = 9;


            // Set default width cho tất cả column
            // worksheet.DefaultColWidth = 10;
            // Set cột Chi tiêu rộng hơn
            //worksheet.Column(2).BestFit = true;
            worksheet.Column(2).Width = 50;           

            // Tự động xuống hàng khi text quá dài
            // worksheet.Cells.Style.WrapText = true;
            // Tạo header - column name
            worksheet.Cells[row_TieuDe, 1].Value = "STT";
            worksheet.Cells[row_TieuDe, 2].Value = "Chỉ tiêu";
            worksheet.Cells[row_TieuDe, 1, row_TieuDe+1, 1].Merge = true;
            worksheet.Cells[row_TieuDe, 2, row_TieuDe+1, 2].Merge = true;

            // tiêu đề là các đơn vị
            DanhMucDao danhmuc = new DanhMucDao();
            List<DonVi> listDV = danhmuc.Get_DmDonVi();
            int soLuongDonVi = listDV.Count;
            BaoCaoModel baocao = new BaoCaoModel();
            BoChiTieuDao boChiTieuDao = new BoChiTieuDao();

            //--------------- DANH SACH CHI TIÊU CỘT 2 -------------------------------------
            current_Row = 11;
            List<ChiTieuViewModel> boChiTieu= boChiTieuDao.Get_DsChiTieu(1);
            for (int index = 0; index < boChiTieu.Count; index++)
            {
                worksheet.Cells[current_Row, 2].Value = boChiTieu[index].TenChiTieu;
                current_Row = current_Row + 1; // tằng lên sau khi điền
            }

            // - Điền số liệu

            List<BaoCaoTongHop> bcTongHop;
            int sodongBaoCao;//= bcTongHop.Count;
            int col_DonVi = 3;

            int row_SoSanh = 9;

            for (int quy = 1; quy <= 4; quy++)
            {
                worksheet.Cells[row_SoSanh, 3 + (quy - 1) * soLuongDonVi].Value = "SO SÁNH QUÍ " + quy.ToString() + "/" + DateTime.Now.Year.ToString();
                // Merge cells
                worksheet.Cells[row_SoSanh, (quy - 1) * soLuongDonVi + 3, row_SoSanh, quy * soLuongDonVi + 2].Merge = true;


                for (int i = 0; i < soLuongDonVi; i++)
                    {
                        // Reset lại dòng
                        current_Row = row_TieuDe+1;
                        // Column name = Ten Viettat don vi
                        worksheet.Cells[current_Row, col_DonVi].Value = listDV[i].TenVietTat1;
                        current_Row++;

                        bcTongHop = baocao.TongHopBaoCao(1, listDV[i].MaCqThu);
                        sodongBaoCao= bcTongHop.Count;                
                        for (int ct = 0; ct < sodongBaoCao; ct++)
                        {                    
                            worksheet.Cells[current_Row, col_DonVi].Value = Decimal.Parse( bcTongHop[ct].SoThue)/1000000;
                            current_Row = current_Row + 1; // tằng lên sau khi điền
                        }
                        // Dich cột đơn vị sang phải
                        col_DonVi = col_DonVi + 1;                             

                    } // end dơn vị
            }// end quy



        }





        [HttpGet]
        public FileContentResult Export1()
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "ExcelDemo.xlsx");  // FileContentResult
        }

       

        [HttpGet]
        public FileContentResult BCTongHop()
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile_BCTongHop();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(buffer.ToArray(), contentType, "BCTongHop.xlsx");  // FileContentResult
        }


    }
}