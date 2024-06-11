import axios from "axios";
import { createContext, useContext, useEffect, useMemo, useState } from "react";

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
  // State to hold the authentication token
  const [token, setToken_] = useState(localStorage.getItem("token"));
  const [role, setRole_] = useState(localStorage.getItem("role"));

  // Function to set the authentication token
  const setToken = (newToken, newRole) => {
    setToken_(newToken);
    setRole_(newRole);
  };

  useEffect(() => {
    if (token) {
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
      localStorage.setItem("token", token);
      localStorage.setItem("role", role);
    } else {
      delete axios.defaults.headers.common["Authorization"];
      localStorage.removeItem("token");
      localStorage.removeItem("role");
    }
  }, [token, role]);

  // Memoized value of the authentication context
  const contextValue = useMemo(
    () => ({
      token,
      role,
      setToken,
    }),
    [token, role]
  );

  // Provide the authentication context to the children components
  return (
    <AuthContext.Provider value={contextValue}>{children}</AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};

export default AuthProvider;
