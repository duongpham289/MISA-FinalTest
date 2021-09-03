using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        public IActionResult GetCustomersPaging([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string employeeFilter)
        {
            try
            {
                var serviceResult = _employeeRepository.GetPaging(pageIndex, pageSize, employeeFilter, true);

                return StatusCode(200, serviceResult);

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

        [HttpGet("export")]
        public IActionResult Export([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] string employeeFilter)
        {

            var stream = _employeeService.ExportEmployee(pageIndex, pageSize, employeeFilter, false);

            string excelName = $"DanhSachNhanVien.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
