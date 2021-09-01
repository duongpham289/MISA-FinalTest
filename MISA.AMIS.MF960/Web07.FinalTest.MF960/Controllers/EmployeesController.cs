using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web07.FinalTest.MF960.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntityController<Employee>
    {
        #region DECLARE

        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeService, employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Lấy thông tin phân trang nhân viên
        /// </summary>
        /// <returns>Dữ liệu phân trang</returns>
        /// CreatedBy:PHDUONG(27/08/2021)
        [HttpGet("paging")]
        public IActionResult GetCustomersPaging([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string employeeFilter, [FromQuery] Guid? departmentId)
        {
            try
            {
                var serviceResult = _employeeService.GetPaging(pageIndex, pageSize, employeeFilter, departmentId);

                if ((int)serviceResult.Data.GetType().GetProperty("totalRecord").GetValue(serviceResult.Data) != 0)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Test.Core.Resources.ResourceVN.ExceptionError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }

        }
        /// <summary>
        /// Lấy Code cho nhân viên mới
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: PHDUONG(27/08/2021)
        [HttpGet("getAllCode")]
        public virtual IActionResult GetAllCode()
        {
            try
            {
                var entityCode = _employeeRepository.GetAllEmployeeCode();

                if (entityCode != null)
                {
                    return StatusCode(200, entityCode);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Test.Core.Resources.ResourceVN.ExceptionError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }

        /// <summary>
        /// Lấy Code cho nhân viên mới
        /// </summary>
        /// <returns></returns>
        ///  CreatedBy: PHDUONG(27/08/2021)
        [HttpGet("getNewCode")]
        public virtual IActionResult GetNewCode()
        {
            try
            {
                var entityCode = _employeeRepository.GetNewEmployeeCode();

                if (entityCode != null)
                {
                    return StatusCode(200, entityCode);

                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = MISA.Test.Core.Resources.ResourceVN.ExceptionError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                return StatusCode(500, errorObj);
            }
        }
        #endregion
    }
}
