import React, { useState, useEffect } from "react";
import "../styles/sidebar.css";
import { useAppContext } from "../context/AppContext";
import { useNavigate } from "react-router-dom";

export default function Sidebar({
  activeTab,
  user
}) {
  const navigate = useNavigate();
  const { summary } = useAppContext();

  //  Persist sidebar state
  const [isCollapsed, setIsCollapsed] = useState(() => {
    const saved = localStorage.getItem("sidebarCollapsed");
    return saved === "true"; // convert string → boolean
  });

  //  Save state on change
  useEffect(() => {
    localStorage.setItem("sidebarCollapsed", isCollapsed);
  }, [isCollapsed]);

  // Safe counts
  const getCounts = (key) => ({
    today: summary?.[key]?.today ?? 0,
    total: summary?.[key]?.total ?? 0
  });

  const menu = [
    { key: "home", label: "Dashboard", icon: "bi-grid-1x2-fill" },
    { key: "inbox", label: "Inbox", icon: "bi-chat-square-text-fill" },
    { key: "completed", label: "Completed", icon: "bi-patch-check-all" },
    { key: "draft", label: "Drafts", icon: "bi-file-earmark-diff-fill" }
  ];

  return (
    <aside className={`sidebar ${isCollapsed ? "collapsed" : ""}`}>
      
      {/* HEADER */}
      <div className="sidebar-header">
        <div className="logo-wrapper">
          <div className="logo-icon">F</div>
          {!isCollapsed && <span className="brand-name">FlowBoard</span>}
        </div>

        <button
          className="toggle-btn"
          onClick={() => setIsCollapsed(prev => !prev)}
        >
          <i
            className={`bi ${
              isCollapsed ? "bi-chevron-right" : "bi-chevron-left"
            }`}
          ></i>
        </button>
      </div>

      {/* NAV */}
      <nav className="sidebar-nav">
        {menu.map((item) => {
          const counts = getCounts(item.key);

          return (
            <div
              key={item.key}
              className={`nav-item ${
                activeTab === item.key ? "active" : ""
              }`}
              onClick={() => navigate(`/dashboard/${item.key}`)}
              title={isCollapsed ? item.label : ""}
            >
              <div className="nav-icon-box">
                <i className={`bi ${item.icon}`}></i>
              </div>

              {!isCollapsed && (
                <>
                  <span className="label">{item.label}</span>

                  {/* COUNTS */}
                  {item.key !== "home" && (
                    <div className="badge-container">
                      <span className="badge-today">
                        {counts.today}
                      </span>
                      <span className="badge-total">
                        {counts.total}
                      </span>
                    </div>
                  )}
                </>
              )}

              {activeTab === item.key && (
                <div className="active-pill"></div>
              )}
            </div>
          );
        })}
      </nav>

      {/* FOOTER */}
      <div className="sidebar-footer">
        <div className="user-card">
          <div className="user-avatar">
            {user?.fullName?.charAt(0) || "Y"}
          </div>

          {!isCollapsed && (
            <div className="user-info">
              <span className="user-name">{user?.fullName}</span>
              <span className="user-role">Administrator</span>
            </div>
          )}
        </div>
      </div>

    </aside>
  );
}