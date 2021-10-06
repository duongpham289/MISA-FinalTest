using System;

namespace MISA.Test.Core.Entities
{
    public class Task : BaseEntity
    {
        #region TaskProperty

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public int TaskId { get; set; }

        /// <summary>
        /// Tên công việc
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string TaskName { get; set; }

        /// <summary>
        /// Tình trạng
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public int? Process { get; set; }

        /// <summary>
        /// Hạn hoàn thành
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssigneeName { get; set; }

        #endregion
    }
}
