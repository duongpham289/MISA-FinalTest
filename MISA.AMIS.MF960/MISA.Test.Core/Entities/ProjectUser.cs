using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class ProjectUser : BaseEntity
    {
        #region ProjectUserProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Danh sách Users
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public List<Guid> ListUserId { get; set; } = new List<Guid>();

        #endregion
    }
}
