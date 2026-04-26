import { useEffect, useRef } from "react";
import "../styles/WorkflowForm.css";

export default function WorkflowFormContent({ data, onBack }) {
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);

  useEffect(() => {
    if (!data?.form || !formRef.current) return;

    let instance;

    import("formiojs").then((FormioModule) => {
      const FormioLib = FormioModule.default || FormioModule;
      const Formio = FormioLib.Formio || FormioLib;

      let parsedForm =
        typeof data.form.formDesigner === "string"
          ? JSON.parse(data.form.formDesigner)
          : data.form.formDesigner;

      Formio.createForm(formRef.current, parsedForm)
        .then((formInstance) => {
          instance = formInstance;
          formInstanceRef.current = formInstance;
        })
        .catch((err) => console.error("Formio Error:", err));
    });

    return () => {
      if (instance?.destroy) instance.destroy();
    };
  }, [data]);

  const getFormData = () =>
    formInstanceRef.current?.submission?.data || {};

  const handleSave = () => {
    console.log("SAVE:", getFormData());
    alert("Saved!");
  };

  const handleSend = () => {
    console.log("SEND:", getFormData());
    alert("Sent!");
  };

  return (
    <div className="workflow-container">
      {/* FORM */}
      <div className="workflow-card">
        <div ref={formRef} />
      </div>

      {/* ACTIONS */}
      <div className="workflow-actions">
        <button className="btn-save" onClick={handleSave}>
          Save
        </button>

        <button className="btn-send" onClick={handleSend}>
          Send
        </button>
      </div>

    </div>
  );
}