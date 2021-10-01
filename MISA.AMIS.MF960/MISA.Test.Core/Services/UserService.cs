using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using MISA.Test.Core.MISAAttribute;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Test.Core.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        #region DECLARE

        IUserRepository _employeeRepository;
        #endregion

        #region Constructor
        public UserService(IUserRepository employeeRepository, IBaseRepository<User> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region ValidateData

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="customer">Dữ liệu cần validate</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        protected override bool ValidateCustom(User employee)
        {
            //Kiểm tra thông tin của nhân viên đã hợp lệ chưa
            //1.Mã nhân viên ko được phép để trống
            if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.EmployeeCodeValidateError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }

            //2. Mã nhân viên không được phép trùng
            if (employee.UserId == Guid.Empty && _employeeRepository.IsDuplicated(employee.EmployeeCode))
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.EmployeeCodeDuplicateValidateError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }

            /*//3.Đơn vị ko được phép để trống
            if (employee.DepartmentId == null && employee.DepartmentName == null)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.DepartmentEmptyError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }*/

            return _serviceResult.IsValid;

        }
        #endregion
    }
}
