import { useEffect, useRef, useState } from "react";
import { getDocumentByTaskId } from "../services/documentService";
import { getTrackingByTaskId } from "../services/documentService";
import Loader from "./Loader";

export default function DocumentMetadata({ taskId, WhoIs = "User", onBack, fromPath }) {
  const [docFull, setDocFull] = useState(null);
  const metaFormRef = useRef(null);

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

    const loadMeta = async () => {
      try {
        const res =WhoIs === "Manager" ? await getTrackingByTaskId(taskId) : await getDocumentByTaskId(taskId);

        setDocFull({
          ...res,
          formDesigner: safeParse(res.formDesigner, {}),
          formData: safeParse(res.formData, {})
        });
      } catch (err) {
        console.error("Meta load error:", err);
      }
    };

    loadMeta();
  }, [taskId]);

  useEffect(() => {
    if (!docFull?.formDesigner || !metaFormRef.current) return;

    const init = async () => {
      const { Formio } = await import("formiojs");

      metaFormRef.current.innerHTML = "";

      const form = await Formio.createForm(
        metaFormRef.current,
        docFull.formDesigner,
        { readOnly: true }
      );

      form.submission = {
        data: docFull.formData || {}
      };
    };

    init();
  }, [docFull]);

if (!docFull) {
  return <Loader text="Loading metadata..." />;
}

  return (
    <div>
      {onBack && (
        <div className="d-flex justify-content-between align-items-center mb-3">
          <h6 className="mb-0">Metadata</h6>
          <button
            className="btn btn-sm btn-outline-secondary"
            onClick={() => onBack(fromPath)}
          >
            <i className="bi bi-x me-1"></i>
            Close
          </button>
        </div>
      )}
      <div className="p-3 border rounded bg-white">
        <div ref={metaFormRef}></div>
      </div>
    </div>
  );
}