import { createContext, useContext, useState, useEffect } from "react";
import { getUserFromToken } from "../utils/authUser";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(getUserFromToken());
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const checkAuth = () => {
      const u = getUserFromToken();
      setUser(u);
      setLoading(false);
    };

    checkAuth();

    window.addEventListener("storage", checkAuth);

    return () => window.removeEventListener("storage", checkAuth);
  }, []);

  return (
    <AuthContext.Provider value={{ user, setUser, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);