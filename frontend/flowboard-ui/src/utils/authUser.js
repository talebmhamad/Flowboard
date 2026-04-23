import { jwtDecode } from "jwt-decode";
import { getToken } from "./authStorage";

export const getUserFromToken = () => {
  const token = getToken();
  if (!token) return null;

  const decoded = jwtDecode(token);

  return {
    userId: decoded.sub,
    username: decoded.Username,
    fullName: decoded.DisplayName,
    email: decoded.Email,
    role:
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  };
};