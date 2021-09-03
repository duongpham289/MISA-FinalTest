using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repository;
using MISA.Test.Core.Interfaces.Services;
using MISA.Test.Core.MISAAttribute;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Test.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region DECLARE

        IEmployeeRepository _employeeRepository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IBaseRepository<Employee> baseRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public dynamic ExportEmployee(int pageIndex, int pageSize, string employeeFilter, bool check)
        {
            var stream = new MemoryStream();
            var employees = _employeeRepository.GetPaging(pageIndex, pageSize, employeeFilter, check);

            var genderList = new List<string> { "Nữ", "Nam", "Khác" ,string.Empty};

            var properties = typeof(Employee).GetProperties();
            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Danh Sách Nhân Viên");

                // Chình tiêu đề trong bảng.

                // STT
                workSheet.Cells[3, 1].Value = "STT";
                workSheet.Cells[3, 1].Style.Font.Bold = true;
                workSheet.Cells[3, 1].Style.Fill.SetBackground(Color.LightGray);
                workSheet.Cells[3, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin,Color.Black);


                var column = 2;

                foreach (var prop in properties)
                {
                    var propMISAExport = prop.GetCustomAttributes(typeof(MISAPropExport), true);

                    //Xét các trường có được export không?
                    if (propMISAExport.Length == 1)
                    {

                        // định dạng ngày/tháng/năm
                        if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                        {
                            workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        else
                        {
                            workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }

                        workSheet.Cells[3, column].Value = (propMISAExport[0] as MISAPropExport).Name;
                        workSheet.Cells[3, column].Style.Font.Bold = true;
                        workSheet.Cells[3, column].Style.Fill.SetBackground(Color.LightGray);
                        workSheet.Cells[3, column].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                        column++;
                    }
                }
                
                // Chỉnh bản ghi vào hàng, cell
                for (int i = 0; i < employees.Count; i++)
                {
                    workSheet.Cells[i + 4, 1].Value = i + 1;
                    workSheet.Cells[i + 4, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                    int col = 2;

                    foreach (var prop in properties)
                    {
                        var propMISAExport = prop.GetCustomAttributes(typeof(MISAPropExport), true);

                        //Xét các trường có được export không?
                        if (propMISAExport.Length == 1)
                        {

                            if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                            {
                                var tmp = employees[i].GetType().GetProperty(prop.Name).GetValue(employees[i], null);
                                workSheet.Cells[i + 4, col].Value = tmp == null ? "" : Convert.ToDateTime(tmp).ToString("dd/MM/yyyy");
                            }
                            else if ((propMISAExport[0] as MISAPropExport).Name == "Giới tính")
                            {
                                var genderName = employees[i].GetType().GetProperty(prop.Name).GetValue(employees[i], null);
                                workSheet.Cells[i + 4, col].Value = genderList[genderName  != null ? (int)genderName : 3];
                            }
                            else
                            {
                                workSheet.Cells[i + 4, col].Value = employees[i].GetType().GetProperty(prop.Name).GetValue(employees[i], null);
                            }

                            workSheet.Cells.AutoFitColumns();
                            workSheet.Cells[i + 4, col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, Color.Black);

                            col++;
                        }
                    }
                }

                // Chỉnh tiêu đề cho workSheet
                workSheet.Cells["A1:I1"].Merge = true;
                workSheet.Cells["A2:I2"].Merge = true;
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
        #endregion


        #region ValidateData

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="customer">Dữ liệu cần validate</param>
        /// <returns></returns>
        /// CreatedBy: PHDUONG(27/08/2021)
        protected override bool ValidateCustom(Employee employee)
        {
            //Kiểm tra thông tin của nhân viên đã hợp lệ chưa
            //1.Mã nhân viên ko được phép để trống
            if (employee.EmployeeCode == "" || employee.EmployeeCode == null)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.EmployeeCodeValidateError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }

            //2. Mã nhân viên không được phép trùng
            if (employee.EmployeeId == Guid.Empty && _employeeRepository.IsDuplicated(employee.EmployeeCode))
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.EmployeeCodeDuplicateValidateError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }

            //3.Đơn vị ko được phép để trống
            if (employee.DepartmentId == null && employee.DepartmentName == null)
            {
                var errorObj = new
                {
                    userMsg = Resources.ResourceVN.DepartmentEmptyError_Msg,
                    errorCode = "misa-001",
                    moreInfo = "https://openapi.misa.com.vn/errorcode/misa-001",
                    traceId = ""
                };
                _serviceResult.IsValid = false;
                _serviceResult.Data = errorObj;
                return _serviceResult.IsValid;
            }

            return _serviceResult.IsValid;

        }
        #endregion
    }
}
