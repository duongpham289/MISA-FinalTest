using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class ServiceResult
    {
        #region ServiceResultProperty

        /// <summary>
        /// Hợp lệ 
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public object Data { get; set; }

        /// <summary>
        /// Thông tin đi kèm
        /// </summary>
        /// CreatedBY: PHDUONG(27/08/2021)
        public string Message { get; set; }
        #endregion
    }
}
