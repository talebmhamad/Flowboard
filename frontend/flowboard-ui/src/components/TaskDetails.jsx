import React, { useEffect, useRef, useState } from "react";
import { getTaskDetails } from "../services/taskService";
import { getDocumentBasicInfo, saveDocument } from "../services/documentService";
import { getDocumentByTaskId } from "../services/documentService";
import { useTask } from "../hooks/useTask";
import { showSuccess, showError } from "../utils/toast";

export default function TaskDetails({ taskId, task: initialTask, status, onBack }) {
  const [task, setTask] = useState(initialTask || null);
  const [activeTab, setActiveTab] = useState("task");
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);
  const [docInfo, setDocInfo] = useState(null); 
  const [docFull, setDocFull] = useState(null);
  const metaFormRef = useRef(null);
  const metaFormInstanceRef = useRef(null);
  const { save, saveAndSend, saving, sending } = useTask();

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

      const parsedFormDesigner =
        typeof taskRes?.formDesigner === "string"
          ? JSON.parse(taskRes.formDesigner)
          : taskRes?.formDesigner;

      const parsedFormData =
        typeof taskRes?.formData === "string" && taskRes.formData
          ? JSON.parse(taskRes.formData)
          : taskRes?.formData;

      setTask({
        ...taskRes,
        formDesigner: parsedFormDesigner,
        formData: parsedFormData
      });

      setDocInfo(docRes);

    } catch (err) {
      console.error(" Load error:", err);
    }
  };

  loadDetails();

   return () => {
    isMounted = false;
   };
   }, [taskId]);

   useEffect(() => {
  if (!task?.formDesigner || !formRef.current || activeTab !== "task") return;

  let isMounted = true;

  const initForm = async () => {
    try {
      const FormioModule = await import("formiojs");
      const Formio = FormioModule.Formio || FormioModule.default?.Formio;

      if (!isMounted) return;

      if (formInstanceRef.current) {
        formInstanceRef.current.destroy(true);
        formInstanceRef.current = null;
      }

      const formJson =
        typeof task.formDesigner === "string"
          ? JSON.parse(task.formDesigner)
          : task.formDesigner;

      formRef.current.innerHTML = "";

      const formInstance = await Formio.createForm(formRef.current, formJson);

      if (!isMounted) return;

      formInstanceRef.current = formInstance;

      let safeData = {};

      if (task.formData) {
        safeData =
          typeof task.formData === "string"
            ? JSON.parse(task.formData)
            : task.formData;
      }

      formInstance.submission = {
        data: safeData,
      };

    } catch (err) {
      console.error("❌ Form init error:", err);
    }
  };

  initForm();

  return () => {

    isMounted = false;

    if (formInstanceRef.current) {
      formInstanceRef.current.destroy(true);
      formInstanceRef.current = null;
    }

    if (formRef.current) {
      formRef.current.innerHTML = "";
    }
  };

   }, [task?.formDesigner, activeTab]);

   useEffect(() => {
    if (activeTab !== "meta" || !taskId || docFull) return;

  let isMounted = true;

  const loadMeta = async () => {
    try {
      const res = await getDocumentByTaskId(taskId);
      if (!isMounted) return;

      const parsedDesigner =
        typeof res.formDesigner === "string"
          ? JSON.parse(res.formDesigner)
          : res.formDesigner;

      const parsedData =
        typeof res.formData === "string" && res.formData
          ? JSON.parse(res.formData)
          : res.formData;

      setDocFull({
        ...res,
        formDesigner: parsedDesigner,
        formData: parsedData
      });

    } catch (err) {
      console.error("🔥 Meta load error:", err);
    }
  };

    loadMeta();

    return () => {
    isMounted = false;
  };

   }, [activeTab, taskId, docFull]);

   useEffect(() => {
  if (!docFull?.formDesigner || !metaFormRef.current || activeTab !== "meta") return;

  let isMounted = true;

  const initMetaForm = async () => {
    try {
      const FormioModule = await import("formiojs");
      const Formio = FormioModule.Formio || FormioModule.default?.Formio;

      if (!isMounted) return;

      // destroy old
      if (metaFormInstanceRef.current) {
        metaFormInstanceRef.current.destroy(true);
        metaFormInstanceRef.current = null;
      }

      metaFormRef.current.innerHTML = "";

      const form = await Formio.createForm(
       metaFormRef.current,
       docFull.formDesigner,
      {
      readOnly: true 
      }
      );

      if (!isMounted) return;

      metaFormInstanceRef.current = form;

      form.submission = {
        data: docFull.formData || {}
      };

    } catch (err) {
      console.error("❌ Meta form error:", err);
    }
  };

  initMetaForm();

  return () => {
    isMounted = false;

    if (metaFormInstanceRef.current) {
      metaFormInstanceRef.current.destroy(true);
      metaFormInstanceRef.current = null;
    }

    if (metaFormRef.current) {
      metaFormRef.current.innerHTML = "";
    }
  };

   }, [docFull, activeTab]);

   if (!task) {
    return (
      <div className="d-flex justify-content-center align-items-center p-5" style={{ minHeight: "300px" }}>
        <div className="spinner-border text-primary"></div>
      </div>
    );
   }

   const handleSave = async () => {
  if (saving || sending) return;

  try {
    const formData = formInstanceRef.current?.submission?.data || {};

    await save({
      id: task.id,
      rowVersion: task.rowVersion,
      formData
    });

    showSuccess("Saved successfully!");
  } catch (err) {
    console.error(err);
    showError("Save failed");
  }
   };

   const handleSend = async () => {
   if (saving || sending) return;

  try {
     const form = formInstanceRef.current;

     if (!form) {
      showError("Form not initialized");
      return;
     }

     const isValid = await form.checkValidity(null, true);
     if (!isValid) {
      showWarning("Please complete all required fields");
      return;
     }

     const formData = form.submission?.data || {};

     await saveAndSend({
      id: task.id,
      rowVersion: task.rowVersion,
      formData
     });

     showSuccess("Sent successfully!");
 
     setTimeout(() => onBack?.(), 800);

     } 
     catch (err) {
      console.error(err);
      showError("Send failed");
     }

   };

   return (
  <div className="container-fluid py-4">

    {!task && (
      <div className="text-center py-5">
        <div className="spinner-border text-primary" />
        <p className="mt-2 text-muted">Loading task...</p>
      </div>
    )}

    {task && (
      <>
         {/* HEADER */}
        <div className="d-flex justify-content-between align-items-start mb-4">
        <div>
    <nav>
      <ol className="breadcrumb mb-1">
        <li className="breadcrumb-item text-muted small">
          {docInfo?.documentTypeName || task?.documentType || "-"}
        </li>
        <li className="breadcrumb-item active small">
          {docInfo?.referenceNumber || task?.referenceNumber || "-"}
        </li>
      </ol>
    </nav>

    <h3 className="fw-bold mb-0 d-flex align-items-center">
      {docInfo?.referenceNumber || task?.referenceNumber || "-"}

      <span className="badge bg-success-subtle text-success border ms-3">
        {status?.label}
      </span>
    </h3>

    <p className="text-muted small mt-1">
      Created by:{" "}
      <span className="fw-bold text-dark">
        {docInfo?.createdByUser || task?.createdByUser || "-"}
      </span>
    </p>
        </div>

        <button className="btn btn-outline-secondary btn-sm" onClick={onBack}>
    <i className="bi bi-arrow-left me-2"></i> Back
        </button>
        </div>

        <div className="card shadow-sm border-0 overflow-hidden">

        <div className="card-header bg-white pt-3 px-4">
            <ul className="nav nav-tabs">
              <li className="nav-item">
                <button
                  className={`nav-link ${
                    activeTab === "task"
                      ? "active fw-bold border-bottom border-primary border-3"
                      : "text-muted"
                  }`}
                  onClick={() => setActiveTab("task")}
                >
                  My Task
                </button>
              </li>

              <li className="nav-item">
                <button
                  className={`nav-link ${
                    activeTab === "meta"
                      ? "active fw-bold border-bottom border-primary border-3"
                      : "text-muted"
                  }`}
                  onClick={() => setActiveTab("meta")}
                >
                  Application Metadata
                </button>
              </li>
            </ul>
        </div>

        <div className="card-body p-4">
            {activeTab === "task" && (
              <>
                <div className="row g-3 mb-4 p-3 bg-light rounded border">
                  <div className="col-md-4">
                    <label className="text-muted small">Document Created</label>
                    <div>{task.documentCreatedDate || "-"}</div>
                  </div>

                  <div className="col-md-4">
                    <label className="text-muted small">Task Date</label>
                    <div>{task.taskDate || "-"}</div>
                  </div>

                  <div className="col-md-4">
                    <label className="text-muted small">Due Date</label>
                    <div className={task.dueDate ? "text-danger" : ""}>
                      {task.dueDate || "No deadline"}
                    </div>
                  </div>
                </div>
                <div className="p-3 border rounded bg-white">
                  {!task.formDesigner ? (
                    <p className="text-muted text-center">
                      No form available
                    </p>
                  ) : (
                    <div ref={formRef} />
                  )}
                </div>
                <div className="card-footer">
                  <div className="form-actions">
                   <div className="btn-group">
<button
  className="btn btn-primary"
  onClick={handleSave}
  disabled={saving || sending}
>
  {saving ? "Saving..." : "Save"}
</button>

<button
  className="btn btn-success"
  onClick={handleSend}
  disabled={saving || sending}
>
  {sending ? "Sending..." : "Send"}
</button>

                     </div>
                  </div>
               </div>

              </>
            )}

           {activeTab === "meta" && (
  <>
    <div className="p-3 border rounded bg-white">
      {!docFull?.formDesigner ? (
        <p className="text-muted text-center">No metadata form</p>
      ) : (
        <div ref={metaFormRef}></div>
      )}
    </div>
  </>
            )}
        </div>

        </div>
      </>
    )}
  </div>
);
}