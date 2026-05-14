import React, { useEffect, useRef, useState } from "react";
import { useSearchParams, useLocation } from "react-router-dom";
import { getTaskDetails } from "../services/taskService";
import { getDocumentBasicInfo } from "../services/documentService";
import { useTask } from "../hooks/task/useTask";
import { showSuccess, showError, showWarning } from "../utils/toast";
import { getUserSummary } from "../services/userService";
import { useAppContext } from "../context/AppContext";
import DocumentMetadata from "./DocumentMetadata";
import Loader from "./Loader";

export default function TaskDetails({ taskId, task: initialTask, status, onBack }) {
  const [task, setTask] = useState(initialTask || null);
  const [docInfo, setDocInfo] = useState(null);
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);
  const { save, saveAndSend, saving, sending } = useTask();
  const { setSummary } = useAppContext();
  const [searchParams, setSearchParams] = useSearchParams();
  const activeTab = searchParams.get("tab") || "task";
  const location = useLocation();
  const fromRef = useRef(location.state?.from || "/dashboard/inbox");

  const safeParse = (value, fallback = {}) => {
    try {
      if (!value) return fallback;
      return typeof value === "string" ? JSON.parse(value) : value;
    } catch {
      return fallback;
    }
  };

  useEffect(() => {
    if (!taskId) return;

    let isMounted = true;

    const loadDetails = async () => {
      try {
        const [taskRes, docRes] = await Promise.all([
          getTaskDetails(taskId),
          getDocumentBasicInfo(taskId)
        ]);

        if (!isMounted) return;

        setTask({
          ...taskRes,
          formDesigner: safeParse(taskRes.formDesigner, {}),
          formData: safeParse(taskRes.formData, {})
        });

        setDocInfo(docRes);

      } catch (err) {
        console.error(err);
      }
    };

    loadDetails();

    return () => { isMounted = false };
  }, [taskId]);

  useEffect(() => {
    if (!task?.formDesigner || !formRef.current || activeTab !== "task") return;

    let isMounted = true;

    const init = async () => {
      const { Formio } = await import("formiojs");

      if (!isMounted) return;

      formInstanceRef.current?.destroy(true);
      formRef.current.innerHTML = "";

      const form = await Formio.createForm(formRef.current, task.formDesigner);

      formInstanceRef.current = form;

      form.submission = {
        data: task.formData || {}
      };
    };

    init();

    return () => {
      isMounted = false;
      formInstanceRef.current?.destroy(true);
    };
  }, [task?.formDesigner, activeTab]);

  // 🔹 SAVE
  const handleSave = async () => {
    try {
      const data = formInstanceRef.current?.submission?.data || {};

      await save({
        id: task.id,
        rowVersion: task.rowVersion,
        formData: data
      });

      showSuccess("Saved successfully!");

      const newSummary = await getUserSummary();
      setSummary(newSummary);

      setTimeout(() => {
        onBack?.(fromRef.current); // 🔥 FIX HERE
      }, 800);

    } catch (err) {
      console.error("Save error:", err);
      showError("Save failed");
    }
  };

  const handleSend = async () => {
    try {
      const form = formInstanceRef.current;

      if (!form) {
        showError("Form not ready");
        return;
      }

      const isValid = await form.checkValidity(null, true);

      if (!isValid) {
        showWarning("Complete required fields");
        return;
      }

      const data = form.submission?.data || {};

      await saveAndSend({
        id: task.id,
        rowVersion: task.rowVersion,
        formData: data
      });

      showSuccess("Sent successfully!");

      const newSummary = await getUserSummary();
      setSummary(newSummary);

      setTimeout(() => {
        onBack?.(fromRef.current); 
      }, 800);

    } catch (err) {
      console.error("Send error:", err);
      showError("Send failed");
    }
  };

  if (!task) {
    return <Loader text="Loading data..." />;
  }

  return (
    <div className="container-fluid py-4">

      <div className="d-flex justify-content-between align-items-start mb-4">
        <div>
          <nav>
            <ol className="breadcrumb mb-1">
              <li className="breadcrumb-item small text-muted">
                {docInfo?.documentTypeName || task.documentType}
              </li>
              <li className="breadcrumb-item active small">
                {docInfo?.referenceNumber || task.referenceNumber}
              </li>
            </ol>
          </nav>

          <h3 className="fw-bold">
            {docInfo?.referenceNumber || task.referenceNumber}
            <span className="badge bg-success-subtle text-success ms-3">
              {status?.label}
            </span>
          </h3>

          <p className="text-muted small">
            Created by: {docInfo?.createdByUser}
          </p>
        </div>

        <button
          className="btn btn-outline-secondary btn-sm"
          onClick={() => onBack?.(fromRef.current)} 
        >
          ← Back
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

          {activeTab === "task" && (
            <>
              <div className="row g-3 mb-3 p-3 bg-light rounded border">
                <div className="col-md-4">
                  <label className="small text-muted">Document Created</label>
                  <div>{task.documentCreatedDate}</div>
                </div>
                <div className="col-md-4">
                  <label className="small text-muted">Task Date</label>
                  <div>{task.taskDate}</div>
                </div>
                <div className="col-md-4">
                  <label className="small text-muted">Due Date</label>
                  <div className="text-danger">{task.dueDate || "No deadline"}</div>
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
            </>
          )}

          {activeTab === "meta" && (
            <DocumentMetadata taskId={taskId} />
          )}

        </div>
      </div>
    </div>
  );
}