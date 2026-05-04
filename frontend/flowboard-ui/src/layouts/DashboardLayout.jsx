import Sidebar from "../components/Sidebar";
import "../styles/dashboardLayout.css";
import { logout } from "../utils/authStorage";

export default function DashboardLayout({
  children,
  user,
  activeTab,
  summary,
  onSelectWorkflow
}) {
  return (
    <div className="layout">
      <Sidebar 
        onSelectWorkflow={onSelectWorkflow} 
        activeTab={activeTab}
        summary={summary}
        user={user}
      />

      <div className="layout-content">
        <header className="layout-header">
          <div className="header-left">
            <i className="bi bi-layout-text-sidebar-reverse breadcrumb-icon"></i>
            <div className="header-title-group">
              <h2 className="page-title">
                {activeTab ? activeTab.charAt(0).toUpperCase() + activeTab.slice(1) : "Dashboard"}
              </h2>
              <p className="page-subtitle">Overview of your applications</p>
            </div>
          </div>

          <div className="header-right">
            <div className="search-bar">
              <i className="bi bi-search"></i>
              <input type="text" placeholder="Search..." />
            </div>

            <div className="header-actions">
              <div className="user-pill">
                <span className="user-name">{user?.fullName || "Administrator"}</span>
                <span className="user-avatar-small">
                  {user?.fullName?.charAt(0)}
                </span>

                <button 
                  className="logout-pill-btn" 
                  onClick={() => {
                    logout();
                    window.location.href = "/login";
                  }}
                  title="Logout"
                >
                  <i className="bi bi-box-arrow-right"></i>
                </button>
              </div>
            </div>
          </div>
        </header>

        <main className="layout-main">
          {children}
        </main>
      </div>
    </div>
  );
}