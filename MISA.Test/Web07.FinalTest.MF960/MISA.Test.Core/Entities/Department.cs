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
        /// CreatedBY: PHDUONG(27/08/2021)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public string Description { get; set; }

        #endregion
    }
}
