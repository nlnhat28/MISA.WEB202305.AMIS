import BaseService from "./base.js";

class PositionService extends BaseService {
  constructor() {
    super("Positions");
  }
}

const positionService = new PositionService();

export default positionService;
