using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Department : BaseEntity
    {
        #region DepartmentProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Biến check phòng ban cóp thuộc người dùng hiện đang đăng nhập ko
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Boolean IsBelongToCurrentUser { get; set; }

        /// <summary>
        /// Danh sách Projects
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public List<Project> ListProjects { get; set; } = new List<Project>();

        /// <summary>
        /// Khóa ngoại
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid UserId { get; set; }

        #endregion
    }
}
