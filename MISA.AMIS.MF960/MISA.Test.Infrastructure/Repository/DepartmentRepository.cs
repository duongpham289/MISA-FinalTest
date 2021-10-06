using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Test.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách Thực thể từ DataBase
        /// </summary>
        /// <returns>List Thực thể</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<Department> GetByUserId(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@UserId", entityId);
                var departments = _dbConnection.Query<Department>($"Proc_GetDepartmentByUserId", param: parameters, commandType: CommandType.StoredProcedure);

                return departments.AsList();
            }
        }
        #endregion
    }
}
