import { useState } from "react";
import { saveTask } from "../services/taskService";

export const useTask = () => {
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState(null);

  const save = async ({ id, rowVersion, formData }) => {
    if (saving) return; 

    if (!id || !rowVersion) {
      throw new Error("Missing id or rowVersion");
    }

    try {
      setSaving(true);
      setError(null);

      const result = await saveTask({
        id,
        rowVersion,
        formData
      });

      return result;
    } catch (err) {
      setError(err.message || "Save task failed");
      throw err;
    } finally {
      setSaving(false);
    }
  };

  return {
    save,
    saving,
    error
  };
};