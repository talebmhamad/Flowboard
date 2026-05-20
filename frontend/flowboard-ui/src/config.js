export const API_URL =
  import.meta.env.VITE_API_URL;

window.FlowBoardUrl = API_URL;

window.IdentityAccessToken =
  sessionStorage.getItem("token") || "";

window.hdUserId =
  sessionStorage.getItem("userId") || "";