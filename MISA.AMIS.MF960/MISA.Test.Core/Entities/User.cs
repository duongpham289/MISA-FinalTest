using MISA.Test.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class User : BaseEntity
    {
        #region UserProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public Guid UserId { get; set; }

        /// <summary>
        /// Mã người dùng
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public string FullName { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public string Mobile { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public string Email { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Màu ảnh đại diện
        /// </summary>
        /// CreatedBy:PHDUONG(30/09/2021)
        public string AvatarColor { get; set; }

        #endregion
    }
}
