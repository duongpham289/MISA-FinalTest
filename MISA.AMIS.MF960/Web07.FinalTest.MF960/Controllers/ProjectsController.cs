using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web07.FinalTest.MF960.Controllers;

namespace MISA.AMIS.MF960.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectsController : BaseEntityController<Project>
    {
        #region DECLARE
        IProjectService _projectService;
        IProjectRepository _projectRepository;
        #endregion

        #region Constructor
        public ProjectsController(IProjectService projectService, IProjectRepository projectRepository) : base(projectService, projectRepository)
        {
            _projectService = projectService;
            _projectRepository = projectRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới 1 bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpPost]
        public override IActionResult Insert(Project project)
        {
            try
            {
                var serviceReSult = _projectRepository.AddProject(project);

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
        [HttpPost("addProjectUser")]
        public IActionResult InsertProjectUser(ProjectUser projectUser)
        {
            try
            {
                var serviceReSult = _projectRepository.AddProjectUser(projectUser);

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
                var entities = _projectRepository.GetByUserId(userId);

                if (entities.Count > 0 )
                {
                    return StatusCode(200, entities);
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
