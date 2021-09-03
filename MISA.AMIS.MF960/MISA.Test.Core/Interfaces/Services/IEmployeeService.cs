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
        /// <summary>
        /// Lấy danh sách nhân viên phân trang
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="employeeFilter">Dữ liệu lọc phân trang</param>
        /// <param name="check">Check export hay paging</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        dynamic ExportEmployee(int pageIndex, int pageSize, string employeeFilter, bool check);
    }
}
