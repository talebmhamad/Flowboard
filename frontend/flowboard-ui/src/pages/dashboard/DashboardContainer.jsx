import DashboardLayout from "../../layouts/DashboardLayout";
import { Outlet, useLocation } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import WorkflowFormContent from "../../components/WorkflowFormContent";
import TaskDetails from "../../components/TaskDetails";
import "../../styles/userDashboard.css";
import useDashboardData from "../../hooks/dashboard/useDashboardData";
import useWorkflowManager from "../../hooks/workflow/useWorkflowManager";
import Loader from "../../components/Loader";
export default function DashboardContainer() {

  const location = useLocation();
  const activeTab =location.pathname.split("/")[2] || "home";
  const { user } = useAuth();
  const {workflows,summary,loading} = useDashboardData();
  const {selectedWorkflow,loadingForm,handleSelectWorkflow,handleOpenDraft,handleBack} = useWorkflowManager();

  if (loading) {
   return (
     <Loader text="Loading dashboard..." />
   );
  }

  return (

    <DashboardLayout
      user={user}
      activeTab={activeTab}
      summary={summary}
      onSelectWorkflow={handleSelectWorkflow}
    >

      {loadingForm ? (

        <div className="dashboard-card">
          <Loader text="Loading form..." />
        </div>

      ) : selectedWorkflow?.type === "task" ? (

        <TaskDetails
          taskId={selectedWorkflow.id}
          onBack={handleBack}
        />

      ) : selectedWorkflow ? (

        <WorkflowFormContent
          data={selectedWorkflow}
          onBack={handleBack}
        />

      ) : (

        <Outlet
          context={{
            workflows,
            handleOpenDraft,
            handleSelectWorkflow
          }}
        />

      )}

    </DashboardLayout>
  );
}