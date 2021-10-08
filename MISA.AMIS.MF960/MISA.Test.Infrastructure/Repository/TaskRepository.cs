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

namespace MISA.Test.Infrastructure.Repository
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        #region Constructor
        public TaskRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách nhân viên phân trang từ DataBase
        /// </summary>
        /// <returns>Danh sách nhân viên và dữ liệu phân trang</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<Task> GetTasksByProjectId(Guid ProjectId)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new();
                parameters.Add("@ProjectId", ProjectId);

                var data = _dbConnection.Query<Task>($"Proc_GetTasksByProjectId", param: parameters, commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }
        #endregion

    }
}