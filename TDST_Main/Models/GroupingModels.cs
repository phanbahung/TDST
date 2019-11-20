using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDST.Models
{
    public class Group<K, T>
    {
        public K Key;
        public IEnumerable<T> Values;
    }

    public class Book
    {
        public string TieuMuc;
        public string Nam;        
        public string MaCqThu;
        public decimal SoTien;
    }
}