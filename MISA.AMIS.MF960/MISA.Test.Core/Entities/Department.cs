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
        public int DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Danh sách Projects
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public List<Project> ListProjects { get; set; } = new List<Project>();

        #endregion
    }
}
