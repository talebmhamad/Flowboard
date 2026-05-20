import { jwtDecode } from "jwt-decode";
import { getToken } from "./authStorage";

export const getUserFromToken = () => {
  const token = getToken();
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);

    if (decoded.exp * 1000 < Date.now()) {
      return null; // ❌ don't logout here
    }

    return {
      userId: decoded.sub,
      username: decoded.Username,
      fullName: decoded.DisplayName,
      email: decoded.Email,
      role:
        decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      exp: decoded.exp
    };
  } catch (error) {
    return null; 
  }
};