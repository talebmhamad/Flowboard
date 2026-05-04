import React, { useEffect, useRef, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { getTaskDetails } from "../services/taskService";
import {
  getDocumentBasicInfo,
  getDocumentByTaskId
} from "../services/documentService";

import { useTask } from "../hooks/useTask";
import { showSuccess, showError, showWarning } from "../utils/toast";

export default function TaskDetails({ taskId, task: initialTask, status, onBack }) {
  const [task, setTask] = useState(initialTask || null);
  const [docInfo, setDocInfo] = useState(null);
  const [docFull, setDocFull] = useState(null);

  const formRef = useRef(null);
  const formInstanceRef = useRef(null);

  const metaFormRef = useRef(null);
  const metaFormInstanceRef = useRef(null);

  const { save, saveAndSend, saving, sending } = useTask();

  const [searchParams, setSearchParams] = useSearchParams();
  const activeTab = searchParams.get("tab") || "task";

  //  SAFE PARSE HELPER
  const safeParse = (value, fallback = {}) => {
    try {
      if (!value) return fallback;
      return typeof value === "string" ? JSON.parse(value) : value;
    } catch {
      return fallback;
    }
  };

  //  LOAD DATA
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

    //  INIT MAIN FORM
  useEffect(() => {
    if (!task?.formDesigner || !formRef.current || activeTab !== "task") return;
    let isMounted = true;

    const init = async () => {
      const FormioModule = await import("formiojs");
      const Formio = FormioModule.Formio;

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

  //  LOAD META
  useEffect(() => {
    if (activeTab !== "meta" || !taskId || docFull) return;

    let isMounted = true;

    const loadMeta = async () => {
      try {
        const res = await getDocumentByTaskId(taskId);

        if (!isMounted) return;

        setDocFull({
          ...res,
          formDesigner: safeParse(res.formDesigner, {}),
          formData: safeParse(res.formData, {})
        });

      } catch (err) {
        console.error(err);
      }
    };

    loadMeta();

    return () => { isMounted = false };
  }, [activeTab, taskId]);

  //  INIT META FORM
  useEffect(() => {
    if (!docFull?.formDesigner || !metaFormRef.current || activeTab !== "meta") return;

    let isMounted = true;

    const init = async () => {
      const FormioModule = await import("formiojs");
      const Formio = FormioModule.Formio;

      if (!isMounted) return;

      metaFormRef.current.innerHTML = "";

      const form = await Formio.createForm(metaFormRef.current, docFull.formDesigner, {
        readOnly: true
      });

      form.submission = {
        data: docFull.formData || {}
      };
    };

    init();

    return () => { isMounted = false };
  }, [docFull, activeTab]);

//  ACTIONS
const handleSave = async () => {
  try {
    const data = formInstanceRef.current?.submission?.data || {};

    await save({
      id: task.id,
      rowVersion: task.rowVersion,
      formData: data
    });

    showSuccess("Saved successfully!");

    // optional delay (prevents instant UI changes killing toast)
    await new Promise((res) => setTimeout(res, 300));

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

    // 🔥 IMPORTANT: delay before navigation
    setTimeout(() => {
      onBack?.();
    }, 800);

  } catch (err) {
    console.error("Send error:", err);
    showError("Send failed");
  }
};

  if (!task) {
    return <div className="spinner-border text-primary m-5"></div>;
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

        <button className="btn btn-outline-secondary btn-sm" onClick={onBack}>
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

        <div className="form-actions">
          <div className="btn-group">
<button
  className="btn btn-success" 
  onClick={handleSave}
  disabled={saving}
>
  {saving ? "Saving..." : "Save"}
</button>

<button
  className="btn btn-primary" 
  onClick={handleSend}
  disabled={sending}
>
  {sending ? "Sending..." : "Send"}
</button>
          </div>
        </div>

            </>
          )}

          {activeTab === "meta" && (
            <div className="p-3 border rounded bg-white">
              <div ref={metaFormRef}></div>
            </div>
          )}

        </div>
      </div>
    </div>
  );
}