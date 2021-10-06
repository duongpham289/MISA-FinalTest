using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        #region Methods

        /// <summary>
        /// Lấy danh sách nhân viên phân trang
        /// </summary>
        /// <param name="userFilter">Dữ liệu lọc phân trang</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        List<User> GetUserByName( string userFilter);

        #endregion
    }
}
