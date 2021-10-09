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
    public class DepartmentsController : BaseEntityController<Department>
    {
        #region DECLARE
        IDepartmentService _departmentService;
        IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentService departmentService, IDepartmentRepository departmentRepository) : base(departmentService, departmentRepository)
        {
            _departmentService = departmentService;
            _departmentRepository = departmentRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm mới 1 bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpPost]
        public override IActionResult Insert(Department department)
        {
            try
            {
                var serviceReSult = _departmentRepository.AddDepartment(department);

                if (serviceReSult != Guid.Empty)
                {
                    return StatusCode(201, serviceReSult);

                }
                else
                {
                    return BadRequest();
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
        /// Thêm mới 1 bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpPost("addDepartmentUser")]
        public IActionResult InsertDepartmentUser(DepartmentUser departmentUser)
        {
            try
            {
                var serviceReSult = _departmentRepository.AddDepartmentUser(departmentUser);

                if (serviceReSult > 0)
                {
                    return StatusCode(201, serviceReSult);

                }
                else
                {
                    return BadRequest();
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
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách Thực thể dạng Json</returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpGet("getByUserId/{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {

            try
            {
                var entities = _departmentService.GetDepartmentsWithProjects(userId);

                if (entities.IsValid)
                {
                    return StatusCode(200, entities.Data);
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
