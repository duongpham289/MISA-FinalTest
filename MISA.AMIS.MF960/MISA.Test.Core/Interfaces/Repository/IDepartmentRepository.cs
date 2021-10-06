using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Repository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Lấy thông tin entity qua Id
        /// </summary>
        /// <param name="userId">Mã định danh user</param>
        /// <returns>Thông tin entity</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        List<Department> GetByUserId(Guid userId);
    }
}
