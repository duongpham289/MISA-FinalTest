using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        #region Methods

        /// <summary>
        /// Xử lí dữ liệu nhân viên phân trang
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="employeeFilter">Dữ liệu lọc phân trang</param>
        /// <param name="departmentId">Mã định danh Phòng ban</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(17/08/2021)
        ServiceResult GetPaging(int pageIndex, int pageSize, string employeeFilter, Guid? departmentId);

        #endregion
    }
}
