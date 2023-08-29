import BaseService from "./base.js";

class EmployeeService extends BaseService {
  constructor() {
    super("Employees");
  }
  /**
   * Get new employee code
   *
   * Author: nlnhat (03/07/2023)
   * @return Response
   */
  async getNewCode() {
    const response = await this.baseRequest.get(this.baseUrl + "/NewCode");
    return response;
  }
  /**
   * Get filtered employees
   *
   * Author: nlnhat (03/07/2023)
   * @param {Object} param Object
   * @return Response
   */
  async filter(param) {
    const response = await this.baseRequest.get(
      this.baseUrl + "/FilterAdvance",
      {
        params: param,
      }
    );
    return response;
  }
  /**
   * Export to excel
   *
   * Author: nlnhat (03/07/2023)
   * @param {Object} data Data
   * @return Response
   */
  async export() {
    const response = await this.baseRequest.get(this.baseUrl + "/Excel", {
      responseType: "blob",
    });
    return response;
  }
}

const employeeService = new EmployeeService();

export default employeeService;
