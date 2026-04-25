import Sidebar from "../components/Sidebar";
import "../styles/dashboardLayout.css";

export default function DashboardLayout({ children, user }) {
  return (
    <div className="layout">
      
      <Sidebar />

      <div className="layout-content">
        
        {/* Header */}
        <header className="layout-header">
          <span className="header-icon">⚙️</span>
          <span className="header-icon">🌐</span>

          <div className="header-user">
            <span>👤</span>
            <span>{user?.fullName || "Administrator taleb"}</span>
          </div>

          <span className="header-icon">🔒</span>
        </header>

        {/* Main */}
        <main className="layout-main">
          {children}
        </main>

      </div>
    </div>
  );
}