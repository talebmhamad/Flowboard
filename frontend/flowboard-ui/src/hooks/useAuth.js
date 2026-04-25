import { useState } from "react";
import * as authService from "../services/authService";

export const useAuth = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const loginUser = async (username, password) => {
    try {
      setLoading(true);
      setError(null);

      const data = await authService.login(username, password);

      sessionStorage.setItem("token", data.token);

      return true;
    } catch (err) {
      setError(err.message);
      return false;
    } finally {
      setLoading(false);
    }
  };

  return { loginUser, loading, error };
};