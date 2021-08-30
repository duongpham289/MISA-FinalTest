using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web07.FinalTest.MF960.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        #region DECLARE
        IBaseService<MISAEntity> _baseService;
        IBaseRepository<MISAEntity> _baseRepository;
        string _className;
        #endregion

        #region Constructor
        public BaseEntityController(IBaseService<MISAEntity> baseService, IBaseRepository<MISAEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
            _className = typeof(MISAEntity).Name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách Thực thể dạng Json</returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpGet]
        public virtual IActionResult Get()
        {

            try
            {
                var entities = _baseRepository.GetAll();

                if (entities.Count > 0)
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

        /// <summary>
        /// Lấy theo Id
        /// </summary>
        /// <param name="entityId">Id của Thực thể</param>
        /// <returns>Thông tin Thực thể dạng Json</returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpGet("{entityId}")]
        public virtual IActionResult GetById(Guid entityId)
        {
            try
            {
                var entity = _baseRepository.GetById(entityId);

                if (entity != null)
                {
                    return StatusCode(200, entity);

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
        /// Thêm mới 1 bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpPost]
        public virtual IActionResult Insert(MISAEntity entity)
        {
            try
            {
                var serviceReSult = _baseService.Add(entity);

                if (serviceReSult.IsValid == true)
                {
                    return StatusCode(201, serviceReSult.Data);

                }
                else
                {
                    return BadRequest(serviceReSult.Data);
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
        /// Sửa thông tin của đối tượng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpPut("{entityId}")]
        public virtual IActionResult Update(MISAEntity entity, Guid entityId)
        {
            try
            {
                var serviceReSult = _baseService.Update(entity, entityId);

                if (serviceReSult.IsValid == true)
                {
                    return StatusCode(200, serviceReSult.Data);
                }
                else
                {
                    return BadRequest(serviceReSult.Data);
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
        /// Xóa một đối tượng theo khóa chính
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(07/08/2021)
        [HttpDelete("{entityId}")]
        public virtual IActionResult Delete(Guid entityId)
        {
            try
            {
                var rowsEffect = _baseRepository.Delete(entityId);

                if (rowsEffect > 0)
                {
                    return StatusCode(200, rowsEffect);
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
        /// Delete nhiều bản ghi theo ListId
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public virtual IActionResult DeleteList(List<Guid> listId)
        {
            try
            {
                var rowsEffect = _baseRepository.DeleteList(listId);

                if (rowsEffect > 0)
                {
                    return StatusCode(200, rowsEffect);
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
