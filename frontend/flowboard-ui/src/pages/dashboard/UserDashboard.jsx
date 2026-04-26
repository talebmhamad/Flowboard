import DashboardLayout from "../../layouts/DashboardLayout";
import { useState, useEffect } from "react";
import { getUserFromToken } from "../../utils/authUser";
import { getUserSummary } from "../../services/userService";
import "../../styles/userDashboard.css";

export default function UserDashboard() {
  const [activeTab, setActiveTab] = useState("inbox");
  const [user, setUser] = useState(null);
  const [summary, setSummary] = useState(null);

  useEffect(() => {
    const currentUser = getUserFromToken();
    setUser(currentUser);

    const fetchSummary = async () => {
      try {
        const data = await getUserSummary();
        setSummary(data);
      } catch (err) {
        console.error("Summary error:", err);
      }
    };
    fetchSummary();
  }, []);

  const getCounts = (key) => ({
    today: summary?.[key]?.today ?? 0,
    total: summary?.[key]?.total ?? 0
  });

  const menu = [
    { key: "inbox", label: "Inbox" },
    { key: "completed", label: "Completed" },
    { key: "draft", label: "Draft" }
  ];

  return (
    <DashboardLayout user={user}>
      <div className="dashboard-wrapper">
        
        {/* TOP NAVIGATION BAR */}
        <header className="dashboard-header">
          <div className="dashboard-tabs">
            {menu.map((item) => {
              const counts = getCounts(item.key);
              return (
                <button
                  key={item.key}
                  onClick={() => setActiveTab(item.key)}
                  className={`dashboard-tab ${activeTab === item.key ? "active" : ""}`}
                >
                  <span className="tab-label">{item.label}</span>
                  <div className="counts">
                    <span className="count-badge today">{counts.today}</span>
                    <span className="count-badge total">{counts.total}</span>
                  </div>
                </button>
              );
            })}
          </div>
        </header>

        {/* MAIN CONTENT AREA */}
        <main className="dashboard-main-content">
          <div className="dashboard-card">
            <h2 className="dashboard-title">
              {activeTab.charAt(0).toUpperCase() + activeTab.slice(1)} Items
            </h2>

            <div className="dashboard-empty">
              <div className="empty-icon">📂</div>
              <p>No {activeTab} tasks found for the selected workflow.</p>
            </div>
          </div>
        </main>
      </div>
    </DashboardLayout>
  );
}