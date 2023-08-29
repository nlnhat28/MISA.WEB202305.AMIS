import BaseService from "./base.js";

class DepartmentService extends BaseService {
  constructor() {
    super("Departments");
  }
}

const departmentService = new DepartmentService();

export default departmentService;
