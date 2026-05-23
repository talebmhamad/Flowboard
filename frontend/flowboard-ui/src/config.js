export const API_URL =
  window.APP_CONFIG.API_URL;

window.FlowBoardUrl = API_URL;

window.IdentityAccessToken =
  sessionStorage.getItem("token") || "";

window.hdUserId =
  sessionStorage.getItem("userId") || "";