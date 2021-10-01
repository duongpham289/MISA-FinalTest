using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Services
{
    public interface IDepartmentService : IBaseService<Department>
    {
        #region Methods

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        ServiceResult GetDepartmentsWithProjects();

        #endregion
    }
}
