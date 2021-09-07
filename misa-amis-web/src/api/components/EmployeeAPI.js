import BaseAPI from "@/api/base/baseAPI.js";
import BaseAPIConfig from "../base/baseAPIConfig.js";

class EmployeeAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "v1/Employees";
  }

  /**
   * Hàm lấy dữ liệu phân trang
   */
  paging(pageIndex, pageSize, entityFilter) {
    return BaseAPIConfig.get(`${this.controller}/paging?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${entityFilter}`);
  }
  /**
   * Hàm export dữ liệu ra file excel
   */
  export (pageIndex, pageSize, entityFilter) {
    return BaseAPIConfig.get(`${this.controller}/export?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${entityFilter}`, {
      responseType: "blob",
    });
  }
}

export default new EmployeeAPI();