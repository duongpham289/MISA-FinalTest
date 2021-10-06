using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Infrastructure.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Constructor
        public ProjectRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách Thực thể từ DataBase
        /// </summary>
        /// <returns>List Thực thể</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<Project> GetByUserId(Guid entityId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@UserId", entityId);
                var projects = _dbConnection.Query<Project>($"Proc_GetProjectByUserId", param: parameters, commandType: CommandType.StoredProcedure);

                return projects.AsList();
            }
        }
        #endregion
    }
}
