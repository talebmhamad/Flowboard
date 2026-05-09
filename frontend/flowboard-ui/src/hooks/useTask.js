import { useState } from "react";
import { saveTask, saveAndSendTask } from "../services/taskService";

export const useTask = () => {
  const [saving, setSaving] = useState(false);
  const [sending, setSending] = useState(false);
  const [error, setError] = useState(null);

  //  SAVE 
  const save = async ({ id, rowVersion, formData }) => {
    if (saving) return;

    if (!id || !rowVersion) {
      throw new Error("Missing id or rowVersion");
    }

    try {
      setSaving(true);
      setError(null);

      return await saveTask({ id, rowVersion, formData });

    } catch (err) {
      setError(err.message || "Save task failed");
      throw err;
    } finally {
      setSaving(false);
    }
  };

  //  SAVE + SEND 
  const saveAndSend = async ({ id, rowVersion, formData }) => {
    if (sending) return;

    if (!id || !rowVersion) {
      throw new Error("Missing id or rowVersion");
    }

    try {
      setSending(true);
      setError(null);

      return await saveAndSendTask({ id, rowVersion, formData });

    } catch (err) {
      setError(err.message || "Send task failed");
      throw err;
    } finally {
      setSending(false);
    }
  };

  return {
    save,
    saveAndSend,
    saving,
    sending,
    error
  };
};