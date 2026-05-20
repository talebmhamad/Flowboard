import { createContext, useContext, useState, useEffect } from "react";

const AppContext = createContext();

export const AppProvider = ({ children }) => {

  const [summary, setSummary] = useState(() => {
    const saved = localStorage.getItem("summary");
    return saved ? JSON.parse(saved) : null;
  });

  useEffect(() => {
    if (summary) {
      localStorage.setItem("summary", JSON.stringify(summary));
    }
  }, [summary]);

  return (
    <AppContext.Provider value={{ summary, setSummary }}>
      {children}
    </AppContext.Provider>
  );
};

export const useAppContext = () => useContext(AppContext);