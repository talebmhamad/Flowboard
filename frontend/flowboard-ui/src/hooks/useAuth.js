import { useState } from "react";
import * as authService from "../services/authService";
import { getUserFromToken } from "../utils/authUser";
import { useAuth as useAuthContext } from "../context/AuthContext";
import { clearToken } from "../utils/authStorage";

export const useLogin = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const { setUser } = useAuthContext();

  const loginUser = async (username, password) => {
    try {
      setLoading(true);
      setError(null);

      const data = await authService.login(username, password);

      sessionStorage.setItem("token", data.token);

      const user = getUserFromToken();
      setUser(user);

      return true;
    } catch (err) {
      setError(err.message);
      return false;
    } finally {
      setLoading(false);
    }
  };

  const logoutUser = () => {
    clearToken();  
    setUser(null);  
  };

  return { loginUser, logoutUser, loading, error };
};