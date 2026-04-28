import { useEffect, useRef, useCallback } from "react";
import "../styles/WorkflowForm.css";
import { useDocument } from "../hooks/useDocument";

export default function WorkflowFormContent({ data, onBack }) {
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);
  const { save, loading } = useDocument();
  console.log("WorkflowFormContent data:", data);

  //  SAVE
  const handleSave = useCallback(async () => {
    if (loading) return;

    try {
      const formData =
        formInstanceRef.current?.submission?.data || {};

      console.log("SAVE:", formData);

      await save({
        documentTypeId: data.workflow.id,
        workflowId: data.workflow.id,
        formData,
        id: data.id || "",
        rowVersion: data.rowVersion || ""
      });

      alert("✅ Saved successfully!");
    } catch (err) {
      console.error(err);
      alert("❌ Save failed");
    }
  }, [data, save, loading]);

  //  SEND
  const handleSend = useCallback(() => {
    const formData =
      formInstanceRef.current?.submission?.data || {};

    console.log("SEND:", formData);

  }, []);

  //  FORM INIT
  useEffect(() => {
    if (!data?.form || !formRef.current) return;

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

      const hasActions = formJson.components.some(
        (c) => c.customClass === "form-internal-actions"
      );

      //  ADD BUTTONS IF NOT EXISTS
      if (!hasActions) {
        formJson.components.push({
          type: "columns",
          customClass: "form-internal-actions",
          columns: [
            { width: 8, components: [] },
            {
              width: 2,
              components: [
                {
                  type: "button",
                  label: "Save",
                  key: "internalSave",
                  action: "event",          
                  event: "internalSave",   
                  theme: "success",
                  customClass: "btn-inside-save",
                  input: true
                }
              ]
            },
            {
              width: 2,
              components: [
                {
                  type: "button",
                  label: "Send",
                  key: "internalSend",
                  action: "event",          
                  event: "internalSend",    
                  theme: "primary",
                  customClass: "btn-inside-send",
                  input: true
                }
              ]
            }
          ]
        });
      }

      formRef.current.innerHTML = "";

      Formio.createForm(formRef.current, formJson)
        .then((formInstance) => {
          instance = formInstance;
          formInstanceRef.current = formInstance;

          formInstance.on("customEvent", (event) => {
            if (event.type === "internalSave") {
              handleSave();
            }

            if (event.type === "internalSend") {
              handleSend();
            }
          });
        })
        .catch((err) => console.error("Formio Error:", err));
    });

    return () => {
      if (instance?.destroy) instance.destroy();
      if (formRef.current) formRef.current.innerHTML = "";
    };
  }, [data, handleSave, handleSend]);

  return (
    <div className="workflow-container">
      <div className="workflow-header-row">
        <h2 className="form-title">
          {data.workflow?.text ||
            data.workflow?.name ||
            "Workflow"}
        </h2>
      </div>

      {loading && (
        <div className="form-loading">
          Saving...
        </div>
      )}

      <div className="workflow-card">
        <div ref={formRef} />
      </div>
    </div>
  );
}