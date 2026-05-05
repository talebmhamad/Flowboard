import DashboardLayout from "../../layouts/DashboardLayout";
import { useState, useEffect } from "react";
import {
  Outlet,
  useLocation,
  useParams,
  useNavigate
} from "react-router-dom";

import { getUserFromToken } from "../../utils/authUser";
import { getUserSummary } from "../../services/userService";
import { getWorkflows, getWorkflowForm } from "../../services/workflowService";
import { getDocumentById } from "../../services/documentService";
import WorkflowFormContent from "../../components/WorkflowFormContent";
import TaskDetails from "../../components/TaskDetails";
import "../../styles/userDashboard.css";
import { useAppContext } from "../../context/AppContext";
import { useAuth } from "../../context/AuthContext";

export default function UserDashboard() {
  const [workflows, setWorkflows] = useState([]);
  const [selectedWorkflow, setSelectedWorkflow] = useState(null);
  const [loadingForm, setLoadingForm] = useState(false);
  const { summary, setSummary } = useAppContext();
  const location = useLocation();
  const { mode, id } = useParams();
  const navigate = useNavigate();
  const from = location.state?.from || "/dashboard/home";
  const activeTab = location.pathname.split("/")[2] || "home";
  const { user } = useAuth();

  useEffect(() => {
    const currentUser = user;

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

  //  Unified routing handler
  useEffect(() => {
    if (!mode) {
      setSelectedWorkflow(null);
      return;
    }

    if (mode === "new" && id) {
      loadWorkflowForm(id);
    }

    if (mode === "draft" && id) {
      loadDraft(id);
    }

    if (mode === "task" && id) {
      setSelectedWorkflow({
        type: "task",
        id
      });
    }

  }, [mode, id]);

  //  Load workflow (new)
  const loadWorkflowForm = async (workflowId) => {
    try {
      setLoadingForm(true);

      const formData = await getWorkflowForm(workflowId);

      setSelectedWorkflow({
        type: "new",
        workflow: { id: workflowId },
        form: formData
      });

    } catch (err) {
      console.error("Workflow load error:", err);
    } finally {
      setLoadingForm(false);
    }
  };

  //  Load draft
  const loadDraft = async (draftId) => {
    try {
      setLoadingForm(true);

      const res = await getDocumentById(draftId);

      setSelectedWorkflow({
        type: "draft",
        id: draftId,
        rowVersion: res.rowVersion,
        workflow: {
          id: res.documentTypeId,
          text: res.documentType
        },
        form: {
          formDesigner: res.formDesigner
        },
        formData: res.formData
      });

    } catch (err) {
      console.error("Draft load error:", err);
    } finally {
      setLoadingForm(false);
    }
  };

  //  Dashboard click
  const handleSelectWorkflow = (wf) => {
    navigate(`/dashboard/form/new/${wf.id}`, {
      state: { from: "/dashboard/home" }
    });
  };

  //  Draft click (if reused anywhere)
  const handleOpenDraft = (row) => {
    navigate(`/dashboard/form/draft/${row.id}`, {
      state: { from: "/dashboard/draft" }
    });
  };

  return (
    <DashboardLayout
      user={user}
      activeTab={activeTab}
      summary={summary}
      onSelectWorkflow={handleSelectWorkflow}
    >
      {loadingForm ? (
        <div className="dashboard-card">
          <h2>Loading form...</h2>
        </div>

      ) : selectedWorkflow?.type === "task" ? (

<TaskDetails
  taskId={selectedWorkflow.id}
  onBack={(fromPath) => {
    setSelectedWorkflow(null);
    navigate(fromPath || "/dashboard/inbox");
  }}
/>

      ) : selectedWorkflow ? (

        <WorkflowFormContent
          data={selectedWorkflow}
          onBack={() => {
            setSelectedWorkflow(null);
            navigate(from); 
          }}
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