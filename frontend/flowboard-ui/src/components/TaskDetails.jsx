import React, { useRef, useEffect } from "react";
import {useSearchParams,useLocation} from "react-router-dom";
import { useTask } from "../hooks/task/useTask";
import { useAppContext } from "../context/AppContext";
import DocumentMetadata from "./DocumentMetadata";
import Loader from "./Loader";
import useFormioForm from "../hooks/task/useFormioForm";
import useTaskDetails from "../hooks/task/useTaskDetails";
import useTaskActions from "../hooks/task/useTaskActions";

export default function TaskDetails({ taskId, task: initialTask, status, onBack }) {
  const { save, saveAndSend, saving, sending } = useTask();
  const { setSummary } = useAppContext();
  const [searchParams, setSearchParams] = useSearchParams();
  const activeTab = searchParams.get("tab") || "task";
  const location = useLocation();
  const fromRef = useRef(location.state?.from || "/dashboard/inbox");

  useEffect(() => {
    fromRef.current = location.state?.from || "/dashboard/inbox";
  }, [location.state?.from]);

  const {task,docInfo,loading} = useTaskDetails(taskId,initialTask);
  const {formRef,formInstanceRef} = useFormioForm(task?.formDesigner,task?.formData);
  const { handleSave, handleSend } = useTaskActions({task,formInstanceRef,save,saveAndSend,setSummary,onBack,fromRef});

  if (loading) {
    return <Loader text="Loading data..." />;
  }

  return (

    <div className="container-fluid py-4">

     <div className="d-flex justify-content-between align-items-center flex-wrap gap-3 mb-4">

  {/* LEFT SIDE */}
  <div className="d-flex align-items-center flex-wrap gap-2">

    <h5 className="mb-0 fw-normal text-secondary">

      {docInfo?.documentTypeName || task.documentType}

      <span className="mx-2 text-muted">
        -
      </span>

      {docInfo?.referenceNumber || task.referenceNumber}

      <span className="mx-2 text-muted">
        -
      </span>

      {docInfo?.createdByUser}

    </h5>

    <span className="badge bg-success small px-2 py-1">

      {status?.label || "For Approval"}

    </span>

  </div>

  {/* RIGHT SIDE */}
  <button
    className="btn btn-outline-secondary btn-sm"
    onClick={() => onBack?.(fromRef.current)}
  >

    <i className="bi bi-arrow-left me-1"></i>

    Back

  </button>

</div>

      <div className="card shadow-sm border-0">
        <div className="card-header bg-white">
          <ul className="nav nav-tabs">
            <li className="nav-item">
              <button
                className={`nav-link ${activeTab === "task" ? "active fw-bold border-primary border-3" : ""}`}
                onClick={() => setSearchParams({ tab: "task" })}
              >
                My Task
              </button>
            </li>

            <li className="nav-item">
              <button
                className={`nav-link ${activeTab === "meta" ? "active fw-bold border-primary border-3" : ""}`}
                onClick={() => setSearchParams({ tab: "meta" })}
              >
                Application Metadata
              </button>
            </li>
          </ul>
        </div>

      <div className="card-body">

  <div
    className={
      activeTab === "task"
        ? "d-block"
        : "d-none"
    }
  >

    <div className="row g-3 mb-3 p-3 bg-light rounded border">

      <div className="col-md-4">

        <label className="small text-muted">
          Document Created
        </label>

        <div>
          {task.documentCreatedDate}
        </div>

      </div>

      <div className="col-md-4">

        <label className="small text-muted">
          Task Date
        </label>

        <div>
          {task.taskDate}
        </div>

      </div>

      <div className="col-md-4">

        <label className="small text-muted">
          Due Date
        </label>

        <div className="text-danger">
          {task.dueDate || "No deadline"}
        </div>

      </div>

    </div>

    <div className="p-3 border rounded bg-white">

      <div ref={formRef}></div>

    </div>

    {/* ACTION BUTTONS */}

    <div className="d-flex justify-content-end mt-4">

      <div className="btn-group shadow-sm">

        <button
          className="btn btn-success px-4"
          onClick={handleSave}
          disabled={saving}
        >

          <i className="bi bi-save me-2"></i>

          {
            saving
              ? "Saving..."
              : "Save"
          }

        </button>

        <button
          className="btn btn-primary px-4"
          onClick={handleSend}
          disabled={sending}
        >

          <i className="bi bi-send me-2"></i>

          {
            sending
              ? "Sending..."
              : "Send"
          }

        </button>

      </div>

    </div>

  </div>

  {
    activeTab === "meta" && (
      <DocumentMetadata
        taskId={taskId}
        onBack={onBack}
        fromPath={fromRef.current}
      />
    )
  }

      </div>

      </div>

    </div>

  );

}