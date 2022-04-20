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
    public class UsersController : BaseEntityController<User>
    {
        #region DECLARE

        IUserService _userService;
        IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UsersController(IUserService userService, IUserRepository userRepository) : base(userService, userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Lấy thông tin phân trang nhân viên
        /// </summary>
        /// <returns>Dữ liệu phân trang</returns>
        /// CreatedBy:PHDUONG(27/08/2021)
        [HttpGet("getUserByName")]
        public IActionResult GetUsersPaging([FromQuery] string userName)
        {
            try
            {
                var serviceResult = _userRepository.GetUserByName(userName);

                if (serviceResult.Count() > 0)
                    return StatusCode(200, serviceResult);
                else
                    return NoContent();
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
        /// Lấy thông tin phân trang nhân viên
        /// </summary>
        /// <returns>Dữ liệu phân trang</returns>
        /// CreatedBy:PHDUONG(27/08/2021)
        [HttpGet("getUserProject/{projectId}")]
        public IActionResult GetUserByProject(Guid projectId)
        {
            try
            {
                var serviceResult = _userRepository.GetUserByProject(projectId);

                if (serviceResult.Count() > 0)
                    return StatusCode(200, serviceResult);
                else
                    return NoContent();
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
