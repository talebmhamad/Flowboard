import { useEffect, useRef } from "react";
import "../styles/WorkflowForm.css";

export default function WorkflowFormContent({ data, onBack }) {
  const formRef = useRef(null);
  const formInstanceRef = useRef(null);

  const handleSave = () => {
    const formData = formInstanceRef.current?.submission?.data || {};
    console.log("SAVE:", formData);
    alert("Saved!");
  };

  const handleSend = () => {
    const formData = formInstanceRef.current?.submission?.data || {};
    console.log("SEND:", formData);
    alert("Sent!");
  };

  useEffect(() => {
    if (!data?.form || !formRef.current) return;

    let instance;

    import("formiojs").then((FormioModule) => {
      const FormioLib = FormioModule.default || FormioModule;
      const Formio = FormioLib.Formio || FormioLib;

      let formJson =
        typeof data.form.formDesigner === "string"
          ? JSON.parse(data.form.formDesigner)
          : { ...data.form.formDesigner };

      delete formJson.title;

      const hasActions = formJson.components.some(
        (c) => c.customClass === "form-internal-actions"
      );

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
                  action: "custom",
                  theme: "success",
                  customClass: "btn-inside-save",
                  input: true,
                },
              ],
            },
            {
              width: 2,
              components: [
                {
                  type: "button",
                  label: "Send",
                  key: "internalSend",
                  action: "custom",
                  theme: "primary",
                  customClass: "btn-inside-send",
                  input: true,
                },
              ],
            },
          ],
        });
      }

      if (formRef.current) formRef.current.innerHTML = "";

      Formio.createForm(formRef.current, formJson)
        .then((formInstance) => {
          instance = formInstance;
          formInstanceRef.current = formInstance;

          formInstance.on("customEvent", (event) => {
            if (event.component.key === "internalSave") handleSave();
            if (event.component.key === "internalSend") handleSend();
          });
        })
        .catch((err) => console.error("Formio Error:", err));
    });

    return () => {
      if (instance?.destroy) instance.destroy();
      if (formRef.current) formRef.current.innerHTML = "";
    };
  }, [data]);

  return (
    <div className="workflow-container">
      <div className="workflow-header-row">
        <h2 className="form-title">
          {data.workflow?.text || data.workflow?.name || "Workflow"}
        </h2>
      </div>

      <div className="workflow-card">
        <div ref={formRef} />
      </div>
    </div>
  );
}