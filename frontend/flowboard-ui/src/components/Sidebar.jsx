import { useEffect, useState } from "react";
import { getWorkflows } from "../services/workflowService";
import { useNavigate, useLocation } from "react-router-dom";
import { logout } from "../utils/authStorage";
import "../styles/sidebar.css";

export default function Sidebar() {
  const [workflows, setWorkflows] = useState([]);
  const [activeId, setActiveId] = useState(null);
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout(); 
    navigate("/login");
  };

  useEffect(() => {
    const fetchWorkflows = async () => {
      try {
        const data = await getWorkflows();
        setWorkflows(data);
      } catch (err) {
        console.error(err);
      }
    };
    fetchWorkflows();
  }, []);

  return (
    <aside className="sidebar">
      <div className="sidebar-content">
        <h2 className="sidebar-title">WORKFLOWS</h2>
        <nav className="sidebar-nav">
          {workflows.map((wf) => (
            <div 
              key={wf.id} 
              className={`nav-item ${activeId === wf.id ? 'active' : ''}`}
              onClick={() => setActiveId(wf.id)}
            >
              <span className="label">{wf.text || wf.name}</span>
            </div>
          ))}
        </nav>
      </div>
      
      <button onClick={handleLogout} className="logout-btn">Log Out</button>
    </aside>
  );
}