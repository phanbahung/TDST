using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhanQuyen.Models
{
    public class UserChangePassModel
    {
        
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu cũ")]        
        [Required(ErrorMessage = "Bạn vui lòng nhập mật khẩu cũ")]        
        public string OldPass { set; get; }


        [Display(Name = "Mật khẩu mới")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu ít nhất 5 ký tự.")]
        [Required(ErrorMessage = "Bạn vui lòng nhập mật khẩu mới")]
        public string NewPass1 { set; get; }

        [Display(Name = "Xác nhận mật khẩu mới")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Độ dài mật khẩu ít nhất 5 ký tự.")]
        [Required(ErrorMessage = "Bạn vui lòng nhập xác nhận mật khẩu mới")]
        public string NewPass2 { set; get; }
    }
}