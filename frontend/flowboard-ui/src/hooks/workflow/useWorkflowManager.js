import { useEffect, useState } from "react";

import {
  useLocation,
  useNavigate,
  useParams
} from "react-router-dom";

import {
  getWorkflowForm
} from "../../services/workflowService";

import {
  getDocumentById
} from "../../services/documentService";

export default function useWorkflowManager() {

  const [selectedWorkflow, setSelectedWorkflow] =
    useState(null);

  const [loadingForm, setLoadingForm] =
    useState(false);

  const { mode, id } = useParams();

  const navigate = useNavigate();

  const location = useLocation();

  // Load workflow form
  const loadWorkflowForm = async (
    workflowId
  ) => {

    try {

      setLoadingForm(true);

      const formData =
        await getWorkflowForm(workflowId);

      setSelectedWorkflow({
        type: "new",
        workflow: {
          id: workflowId
        },
        form: formData,
        from: location.state?.from || "/dashboard/home"
      });

    } catch (err) {

      console.error(
        "Workflow load error:",
        err
      );

    } finally {

      setLoadingForm(false);

    }
  };

  // Load draft
  const loadDraft = async (
    draftId
  ) => {

    try {

      setLoadingForm(true);

      const res =
        await getDocumentById(draftId);

      setSelectedWorkflow({

        type: "draft",

        id: draftId,

        rowVersion:
          res.rowVersion,

        workflow: {

          id:
            res.documentTypeId,

          text:
            res.documentType
        },

        form: {
          formDesigner:
            res.formDesigner
        },

        formData:
          res.formData,

        from: location.state?.from || "/dashboard/draft"
      });

    } catch (err) {

      console.error(
        "Draft load error:",
        err
      );

    } finally {

      setLoadingForm(false);

    }
  };

  // Handle route changes
  useEffect(() => {

    if (!mode) {

      setSelectedWorkflow(null);

      return;
    }

    switch (mode) {

      case "new":

        if (id) {
          loadWorkflowForm(id);
        }

        break;

      case "draft":

        if (id) {
          loadDraft(id);
        }

        break;

      case "task":

        if (id) {

          setSelectedWorkflow({
            type: "task",
            id,
            status: location.state?.status,
            from: location.state?.from || "/dashboard/inbox"
          });

        }

        break;

      default:

        setSelectedWorkflow(null);

        break;
    }

  }, [mode, id, location.state]);

  // Open workflow
  const handleSelectWorkflow = (
    wf
  ) => {

    navigate(
      `/dashboard/form/new/${wf.id}`,
      {
        state: {
          from: "/dashboard/home"
        }
      }
    );
  };

  // Open draft
  const handleOpenDraft = (
    row
  ) => {

    navigate(
      `/dashboard/form/draft/${row.id}`,
      {
        state: {
          from: "/dashboard/draft"
        }
      }
    );
  };

  // Back handler
const handleBack = (
  customFrom
) => {

  if (
    customFrom &&
    typeof customFrom === "object"
  ) {
    customFrom = null;
  }

  const from =
    customFrom ||
    selectedWorkflow?.from ||
    "/dashboard/home";

  setSelectedWorkflow(null);

  navigate(from);

};

  return {

    selectedWorkflow,

    loadingForm,

    handleSelectWorkflow,

    handleOpenDraft,

    handleBack
  };
}