import BaseAPI from "@/api/base/baseAPI.js";

class DepartmentAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "v1/Departments";
  }
}

export default new DepartmentAPI();
