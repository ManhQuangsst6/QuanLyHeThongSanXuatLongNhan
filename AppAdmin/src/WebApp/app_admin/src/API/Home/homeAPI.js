import apiService from "../../Common/ServiceAPI/apiService";
const name = "/Home";
export const GetCountEmployye = () => {
  return apiService.get(`${name}/GetCountEmployye`);
};
export const GetCountShipment = () => {
  return apiService.get(`${name}/GetCountShipment`);
};
export const GetCountLogan = () => {
  return apiService.get(`${name}/GetCountLogan`);
};
export const GetLonganCommon = () => {
  return apiService.get(`${name}/GetLonganCommon`);
};
export const GetLoganByCategory = () => {
  return apiService.get(`${name}/GetLoganByCategory`);
};
