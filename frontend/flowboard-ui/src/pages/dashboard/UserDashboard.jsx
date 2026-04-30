import DashboardLayout from "../../layouts/DashboardLayout";
import { useState, useEffect } from "react";
import { getUserFromToken } from "../../utils/authUser";
import { getUserSummary } from "../../services/userService";
import { getWorkflows, getWorkflowForm } from "../../services/workflowService";
import WorkflowFormContent from "../../components/WorkflowFormContent";
import HomeDashboard from "../../components/HomeDashboard";
import InboxTable from "../../components/InboxTable";
import "../../styles/userDashboard.css";
import { useAppContext } from "../../context/AppContext";


export default function UserDashboard() {
  const [activeTab, setActiveTab] = useState("home");
  const [user, setUser] = useState(null);
  const [workflows, setWorkflows] = useState([]);
  const [selectedWorkflow, setSelectedWorkflow] = useState(null);
  const [loadingForm, setLoadingForm] = useState(false);
  const { summary, setSummary } = useAppContext();


  useEffect(() => {
    const currentUser = getUserFromToken();
    setUser(currentUser);

    const fetchData = async () => {
      try {
        const [summaryData, workflowData] = await Promise.all([
          getUserSummary(),
          getWorkflows()
        ]);

        setSummary(summaryData);
        setWorkflows(workflowData);
      } catch (err) {
        console.error("Dashboard Data Fetch Error:", err);
      }
    };

    fetchData();
  }, []);

  const handleSelectWorkflow = async (wf) => {
    try {
      setLoadingForm(true);

      const formData = await getWorkflowForm(wf.id);

      setSelectedWorkflow({
        workflow: wf,
        form: formData
      });

      setActiveTab("home"); 

    } catch (err) {
      console.error("Error loading workflow form:", err);
    } finally {
      setLoadingForm(false);
    }
  };

  const handleTabChange = (tab) => {
    setActiveTab(tab);
    setSelectedWorkflow(null);
  };

  return (
    <DashboardLayout
      user={user}
      activeTab={activeTab}
      setActiveTab={handleTabChange} 
      onSelectWorkflow={handleSelectWorkflow}
    >
      {loadingForm ? (
        <div className="dashboard-card">
          <h2>Loading form...</h2>
        </div>
      ) : selectedWorkflow ? (
        <WorkflowFormContent
          data={selectedWorkflow}
          onBack={() => setSelectedWorkflow(null)}
        />
      ) : (
        <DefaultDashboardContent
          activeTab={activeTab}
          summary={summary}
          workflows={workflows}
          onSelectWorkflow={handleSelectWorkflow}
        />
      )}
    </DashboardLayout>
  );
}



function DefaultDashboardContent({
  activeTab,
  workflows,
  onSelectWorkflow
}) {
  switch (activeTab) {
    case "home":
      return (
        <HomeDashboard
          workflows={workflows}
          onSelectWorkflow={onSelectWorkflow}
        />
      );

    case "inbox":
      return <InboxTable documentTypes={workflows} />;

    case "completed":
      return (
        <div className="dashboard-card">
          <h2>Completed Items</h2>
          <p>No completed tasks yet.</p>
        </div>
      );

    case "draft":
      return (
        <div className="dashboard-card">
          <h2>Draft Items</h2>
          <p>No draft tasks yet.</p>
        </div>
      );

    default:
      return null;
  }
}