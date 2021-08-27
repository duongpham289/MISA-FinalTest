using MISA.Test.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Employee : BaseEntity
    {
        #region EmployeeProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public Guid EmployeeId { get; set; }

        [MISARequired("Mã nhân viên")]
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string EmployeeCode { get; set; }

        [MISARequired("Họ và tên")]
        /// <summary>
        /// Họ và tên
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính (0 - Nữ, 1 - Nam, 2 - Khác)
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số điện thoại Di động
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại Bàn
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Số Căn cước công dân
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp Căn cước công dân
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp Căn cước công dân
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string IdentityPlace { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string BankBranch { get; set; }
        
        [MISARequired("Đơn vị")]
        /// <summary>
        /// Khóa ngoại
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string PositionName { get; set; }

        #endregion
    }
}
