using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Project : BaseEntity
    {
        #region ProjectProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Tên dự án
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string ProjectName { get; set; }

        /// <summary>
        /// Biểu tượng
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string Icon { get; set; }

        /// <summary>
        /// Màu biểu tượng
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string IconColor { get; set; }
        
        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string Description { get; set; }
        
        /// <summary>
        /// Khóa ngoại
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string DepartmentName { get; set; }

        #endregion
    }
}
