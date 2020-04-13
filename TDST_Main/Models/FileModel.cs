using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TDST.Models
{
   public class FileModel
    {
        [Required(ErrorMessage ="Vui lòng chọn file XML.")]
        [Display(Name ="Chọn file")]
        public HttpPostedFileBase[] files { get; set; }
       
    }
}
