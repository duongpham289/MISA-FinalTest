using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Web07.FinalTest.MF960.Controllers;

namespace MISA.AMIS.MF960.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TasksController : BaseEntityController<Task>
    {
        #region DECLARE
        ITaskRepository _taskRepository;
        #endregion

        #region Constructor
        public TasksController(ITaskService taskService, ITaskRepository taskRepository) : base(taskService, taskRepository)
        {
            _taskRepository = taskRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả Công việc theo ProjectId
        /// </summary>
        /// <returns>Danh sách Thực thể dạng Json</returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpGet("getByProjectId/{ProjectId}")]
        public IActionResult GetTasksByProjectId(Guid ProjectId)
        {

            try
            {
                var entities = _taskRepository.GetTasksByProjectId(ProjectId);

                //if (entities)
                //{
                    return StatusCode(200, entities);
                //}
                //else
                //{
                //    return NoContent();
                //}

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
