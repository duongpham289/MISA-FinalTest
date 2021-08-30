﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class BaseEntity
    {
        #region BaseProperty
        /// <summary>
        /// Ngày Tạo
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy:PHDUONG(27/08/2021)
        public string ModifiedBy { get; set; }

        #endregion
    }
}
