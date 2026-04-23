import { useState } from "react";
import { useAuth } from "../hooks/useAuth";
import { Eye, EyeOff, Lock, User } from "lucide-react"; 

export default function Login() {
  const { loginUser, loading, error } = useAuth();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const success = await loginUser(username, password);

    if (success) {
      const token = localStorage.getItem("token");

      const user = jwtDecode(token);

      if (user.role === "Manager")
      {
        window.location.href = "/manager-dashboard";
      }
      else if (user.role === "User") 
      {
        window.location.href = "/user-dashboard";
      }
      
    }

  };

  return (
    <div className="container-fluid min-vh-100 d-flex align-items-center justify-content-center bg-light px-3">
      <div 
        className="card border-0 shadow-lg p-4 p-md-5" 
        style={{ width: "100%", maxWidth: "420px", borderRadius: "1rem" }}
      >
        <div className="text-center mb-4">
          <h2 className="fw-bold">
          <span className="text-primary">Flow</span>
          <span style={{ color: "#6c757d" }}>Board</span> 
          </h2>
        </div>

        <form onSubmit={handleSubmit} noValidate>
          <div className="mb-3">
            <label className="form-label small fw-bold">Username</label>
            <div className="input-group">
              <span className="input-group-text bg-white border-end-0">
                <User size={18} className="text-muted" />
              </span>
              <input
                type="text"
                className="form-control border-start-0 ps-0"
                placeholder="Enter username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                required
              />
            </div>
          </div>

          <div className="mb-4">
            <label className="form-label small fw-bold">Password</label>
            <div className="input-group">
              <span className="input-group-text bg-white border-end-0">
                <Lock size={18} className="text-muted" />
              </span>
              <input
                type={showPassword ? "text" : "password"}
                className="form-control border-start-0 border-end-0 ps-0"
                placeholder="••••••••"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
              <button
                type="button"
                className="input-group-text bg-white border-start-0"
                onClick={() => setShowPassword(!showPassword)}
                style={{ cursor: "pointer" }}
              >
                {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
              </button>
            </div>
          </div>

          <button
            type="submit"
            className="btn btn-primary w-100 py-2 fw-bold shadow-sm"
            disabled={loading}
          >
            {loading ? (
              <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
            ) : null}
            {loading ? "Authenticating..." : "Sign In"}
          </button>
        </form>

        {error && (
          <div className="alert alert-danger mt-4 small border-0 text-center" role="alert">
            {error}
          </div>
        )}
      </div>
    </div>
  );
}