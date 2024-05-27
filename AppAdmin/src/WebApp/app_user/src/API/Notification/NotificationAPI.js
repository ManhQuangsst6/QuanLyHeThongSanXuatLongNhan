import apiService from "../../Common/ServiceAPI/apiService";
const name = "/Notifications";
export const GetCount = () => {
  return apiService.get(`${name}/GetCount`);
};
export const ComfirmByEmployee = (id) => {
  return apiService.get(`${name}/ComfirmByEmployee?id=${id}`);
};

export const GetListByPage = (pageNum, pageSize) => {
  let query = `${name}/GetAll?pageNum=${pageNum}&pageSize=${pageSize}`;
  return apiService.get(query);
};
