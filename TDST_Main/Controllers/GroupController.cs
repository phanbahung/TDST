using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDST.Models;
using TDST_CRUD.Dao;
using TDST_CRUD.ViewModels;

namespace TDST.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            var books = new List<Book>();

            // Add test data
            books.Add(new Book { TieuMuc = "GTGT", Nam = "2018", MaCqThu = "NTR", SoTien = 159.95M });
            books.Add(new Book { TieuMuc = "TNDN", Nam = "2019", MaCqThu = "NTR", SoTien = 23.95M });
            books.Add(new Book { TieuMuc = "LPTB", Nam = "2018", MaCqThu = "VPC", SoTien = 300.00M });
            books.Add(new Book { TieuMuc = "TNDN", Nam = "2019", MaCqThu = "VPC", SoTien = 219.25M });
            books.Add(new Book { TieuMuc = "GTGT", Nam = "2018", MaCqThu = "VPC", SoTien = 195.50M });

            // Group the books by Genre
            var booksGrouped = from b in books
                               group b by new { b.MaCqThu , b.Nam} into g
                               select new Group<string, Book> { Key = g.Key.MaCqThu +" - Năm: "+ g.Key.Nam, Values = g };

            return View(booksGrouped.ToList());
        }

        public ActionResult Index1()
        {
            ChungTuDao chungTuDao = new ChungTuDao();

            List<FileChungTuViewModel> books = chungTuDao.ThongKeFileChungTu("");            
            
            // Group the books by Genre
            var booksGrouped = from b in books
                               group b by new { b.Ma_cqthu, b.TenDonVi, b.Nam_KhoBac } into g
                               select new Group<string, FileChungTuViewModel> { Key = g.Key.TenDonVi +" - Cơ quan thu: "+g.Key.Ma_cqthu +  " - Năm: "+ g.Key.Nam_KhoBac , Values = g };

            return View(booksGrouped.ToList());
        }

        public ActionResult file(string id)
        {
            ChungTuDao chungTuDao = new ChungTuDao();

            List<FileChungTuViewModel> books = chungTuDao.ThongKeFileChungTu("");

            // Group the books by Genre
            var booksGrouped = from b in books
                               group b by new { b.Ma_cqthu, b.TenDonVi, b.Nam_KhoBac } into g
                               select new Group<string, FileChungTuViewModel> { Key = g.Key.TenDonVi + " - Cơ quan thu: " + g.Key.Ma_cqthu + " - Năm: " + g.Key.Nam_KhoBac, Values = g };

            return View(booksGrouped.ToList());
        }
    }
}