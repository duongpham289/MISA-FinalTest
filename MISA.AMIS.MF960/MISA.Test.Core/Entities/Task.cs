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
        public Guid TaskId { get; set; }

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
        /// Id người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid AssigneeId { get; set; }

        /// <summary>
        /// Người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssigneeName { get; set; }

        /// <summary>
        /// Email người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssigneeEmail { get; set; }

        /// <summary>
        /// Ava người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssigneeAvatar { get; set; }

        /// <summary>
        /// Màu Ava người thực hiện
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssigneeAvatarColor { get; set; }

        /// <summary>
        /// Người giao việc
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string AssignerName { get; set; }


        /// <summary>
        /// Id dự án
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public Guid ProjectId { get; set; }

        /// <summary>
        /// NTên dự án
        /// </summary>
        /// CreatedBY: PHDUONG(30/09/2021)
        public string ProjectName { get; set; }

        #endregion
    }
}
