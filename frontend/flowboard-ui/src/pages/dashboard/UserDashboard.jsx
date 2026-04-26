import DashboardLayout from "../../layouts/DashboardLayout";
import { useState, useEffect } from "react";
import { getUserFromToken } from "../../utils/authUser";
import { getUserSummary } from "../../services/userService";
import WorkflowFormContent from "../../components/WorkflowFormContent";
import "../../styles/userDashboard.css";
import InboxTable from "../../components/InboxTable";

export default function UserDashboard() {
  const [activeTab, setActiveTab] = useState("inbox");
  const [user, setUser] = useState(null);
  const [summary, setSummary] = useState(null);
  const [selectedWorkflow, setSelectedWorkflow] = useState(null);

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

  return (
    <DashboardLayout
      user={user}
      activeTab={activeTab}
      setActiveTab={setActiveTab}
      summary={summary}
      onSelectWorkflow={setSelectedWorkflow}
    >

      {/* ✅ SWITCH CONTENT */}
      {selectedWorkflow ? (
        <WorkflowFormContent
          data={selectedWorkflow}
          onBack={() => setSelectedWorkflow(null)}
        />
      ) : (
        <DefaultDashboardContent activeTab={activeTab} />
      )}

    </DashboardLayout>
  );
}

function DefaultDashboardContent({ activeTab }) {
  switch (activeTab) {
    case "inbox":
      return <InboxTable />;

    case "completed":
      return (
        <div className="dashboard-card">
          <h2 className="dashboard-title">Completed Items</h2>
          <p>No completed tasks yet.</p>
        </div>
      );

    case "draft":
      return (
        <div className="dashboard-card">
          <h2 className="dashboard-title">Draft Items</h2>
          <p>No draft tasks yet.</p>
        </div>
      );

    default:
      return null;
  }
}