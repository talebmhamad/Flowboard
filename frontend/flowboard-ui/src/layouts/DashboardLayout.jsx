import Sidebar from "../components/Sidebar";
import "../styles/dashboardLayout.css";
import React from "react";

export default function DashboardLayout({
  children,
  user,
  activeTab,
  setActiveTab,
  summary,
  onSelectWorkflow
}) {

  const menu = [
    { key: "inbox", label: "Inbox" },
    { key: "completed", label: "Completed" },
    { key: "draft", label: "Draft" }
  ];

  const getCounts = (key) => ({
    today: summary?.[key]?.today ?? 0,
    total: summary?.[key]?.total ?? 0
  });

  return (
    <div className="layout">
      <Sidebar onSelectWorkflow={onSelectWorkflow} />

      <div className="layout-content">

        {/* HEADER */}
        <header className="layout-header">
          <div className="dashboard-tabs">
            {menu.map((item) => {
              const counts = getCounts(item.key);
              return (
                <button
                  key={item.key}
                  onClick={() => {
                     setActiveTab(item.key);
                     if (onSelectWorkflow) {
                         onSelectWorkflow(null);
                       }
                  }}
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

          <div className="header-right-section">
            <div className="header-user">
              <span>👤</span>
              <span>{user?.fullName || "Administrator"}</span>
            </div>
            <span className="header-icon">🔒</span>
          </div>
        </header>

        {/* MAIN CONTENT */}
        <main className="layout-main">
          {children}
        </main>

      </div>
    </div>
  );
}