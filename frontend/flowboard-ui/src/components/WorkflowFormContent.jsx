import { useEffect, useRef, useCallback } from "react";
import "../styles/WorkflowForm.css";
import { useDocument } from "../hooks/useDocument";
import { toast } from "react-toastify";
import { useAppContext } from "../context/AppContext";
import { getUserSummary } from "../services/userService";

export default function WorkflowFormContent({ data, onBack }) {
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);
  const { save, saveAndSend, saving, sending } = useDocument();
  const { setSummary } = useAppContext();

  //  SAVE
  const handleSave = useCallback(async () => {
    if (saving) return;

    try {
      const form = formInstanceRef.current;

      const isValid = await form.checkValidity(null, true);

      if (!isValid) {
        toast.error("Please fill all required fields");
        return;
      }

      const formData = form.submission.data;

      await save({
        documentTypeId: data.workflow.id,
        workflowId: null,
        formData,
        id: data.id || "",
        rowVersion: data.rowVersion || "",
      });

      toast.success("Saved successfully!");

      const newSummary = await getUserSummary();
      setSummary(newSummary);

      setTimeout(() => {
        onBack();
      }, 800);
    } catch (err) {
      console.error(err);
      toast.error("Save failed");
    }
  }, [data, save, saving]);

  //  SEND
  const handleSend = useCallback(async () => {
    if (sending) return;

    try {
      const form = formInstanceRef.current;

      const isValid = await form.checkValidity(null, true);

      if (!isValid) {
        toast.error("Please fill all required fields");
        return;
      }

      const formData = form.submission.data;

      await saveAndSend({
        documentTypeId: data.workflow.id,
        workflowId: null,
        formData,
        id: data.id || "",
        rowVersion: data.rowVersion || "",
      });

      toast.success("Sent successfully!");

      const newSummary = await getUserSummary();
      setSummary(newSummary);

      setTimeout(() => {
        onBack();
      }, 800);
    } catch (err) {
      console.error(err);
      toast.error("Send failed");
    }
  }, [data, saveAndSend, sending]);

  //  FORM INIT
  useEffect(() => {
    if (!data?.form || !formRef.current) return;
    if (formInstanceRef.current) return;

    let instance;

    import("formiojs").then((FormioModule) => {
      const FormioLib = FormioModule.default || FormioModule;
      const Formio = FormioLib.Formio || FormioLib;

      let formJson = {};

      try {
        formJson =
          typeof data.form.formDesigner === "string"
            ? JSON.parse(data.form.formDesigner)
            : { ...data.form.formDesigner };
      } catch (e) {
        console.error("Invalid form JSON", e);
        return;
      }

      delete formJson.title;

      if (!formJson.components) {
        formJson.components = [];
      }

      formRef.current.innerHTML = "";

      Formio.createForm(formRef.current, formJson)
       .then((formInstance) => {
       instance = formInstance;
       formInstanceRef.current = formInstance;

       if (data?.formData) {
         let parsedData = {};

         if (data?.formData) {
           try {
             parsedData =
               typeof data.formData === "string"
                 ? JSON.parse(data.formData)
                 : data.formData;
           } catch (e) {
             console.warn("Invalid formData (not JSON):", data.formData);
             parsedData = {}; 
           }
         }

         setTimeout(() => {
           formInstance.submission = {
             data: parsedData
           };
         }, 0);
       }
       })
       .catch((err) => console.error("Formio Error:", err));
    });

    return () => {
      if (instance?.destroy) instance.destroy();
      if (formRef.current) formRef.current.innerHTML = "";
      formInstanceRef.current = null;
    };
  }, [data?.form]);

  return (
    <div className="workflow-container">
      <div className="workflow-header-row">
        <h2 className="form-title">
          {data.workflow?.text || data.workflow?.name || "Workflow"}
        </h2>
        
        <button className="btn btn-outline-secondary btn-sm" onClick={onBack}>
    <i className="bi bi-arrow-left me-2"></i> Back
        </button>
      </div>

      <div className="workflow-card">
        <div ref={formRef} />
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
      </div>
    </div>
  );
}