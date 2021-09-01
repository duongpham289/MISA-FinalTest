using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Test.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE

        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách nhân viên phân trang từ DataBase
        /// </summary>
        /// <returns>Danh sách nhân viên và dữ liệu phân trang</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public ServiceResult GetPaging(int pageIndex, int pageSize, string employeeFilter, Guid? departmentId)
        {

            _serviceResult.Data = _employeeRepository.GetPaging(pageIndex, pageSize, employeeFilter, departmentId);

            return _serviceResult;
        }
        #endregion


        #region ValidateData

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="customer">Dữ liệu cần validate</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        protected override bool ValidateCustom(Employee employee)
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
            if (employee.EmployeeId == Guid.Empty && _employeeRepository.IsDuplicated(employee.EmployeeCode))
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

            //3.Đơn vị ko được phép để trống
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
            }

            return _serviceResult.IsValid;

        }
        #endregion
    }
}
