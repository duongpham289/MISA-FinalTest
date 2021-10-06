﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;

namespace MISA.Test.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        #region Constructor
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách nhân viên phân trang từ DataBase
        /// </summary>
        /// <returns>Danh sách nhân viên và dữ liệu phân trang</returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        public List<User> GetUserByName(string userName)
        {
            using (_dbConnection = new MySqlConnection(_configuration.GetConnectionString("SqlConnection")))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", userName ?? String.Empty);

                var data = _dbConnection.Query<User>($"Proc_GetUserByName", param: parameters, commandType: CommandType.StoredProcedure);

                return data.AsList();

            }
        }
        #endregion

    }
}
