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
        /// <param name="userId">Dữ liệu truyền vào</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        ServiceResult GetDepartmentsWithProjects(Guid userId);

        #endregion
    }
}
