import { useEffect, useState } from "react";
import { getWorkflows, getWorkflowForm } from "../services/workflowService";
import { logout } from "../utils/authStorage";
import "../styles/sidebar.css";

export default function Sidebar({ onSelectWorkflow }) {
  const [workflows, setWorkflows] = useState([]);
  const [activeId, setActiveId] = useState(null);

  const handleLogout = () => {
    logout();
    window.location.href = "/login";
  };

  useEffect(() => {
    const fetchWorkflows = async () => {
      try {
        const data = await getWorkflows();
        setWorkflows(data);
      } catch (err) {
        console.error("Error loading workflows:", err);
      }
    };

    fetchWorkflows();
  }, []);

  const handleWorkflowClick = async (wf) => {
    try {
      setActiveId(wf.id);

      const formData = await getWorkflowForm(wf.id);

      onSelectWorkflow({
        form: formData,
        workflow: wf
      });

    } catch (err) {
      console.error("Error loading workflow form:", err);
    }
  };

  return (
    <aside className="sidebar">
      <div className="sidebar-content">
        <h2 className="sidebar-title">WORKFLOWS</h2>

        <nav className="sidebar-nav">
          {workflows.map((wf) => (
            <div
              key={wf.id}
              className={`nav-item ${activeId === wf.id ? "active" : ""}`}
              onClick={() => handleWorkflowClick(wf)}
            >
              <span className="label">{wf.text || wf.name}</span>
            </div>
          ))}
        </nav>
      </div>

      <button onClick={handleLogout} className="logout-btn">
        Log Out
      </button>
    </aside>
  );
}