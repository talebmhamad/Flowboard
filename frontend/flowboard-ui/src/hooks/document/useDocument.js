import { useState } from "react";
import { saveDocument, saveAndSendDocument } from "../../services/documentService";

export const useDocument = () => {
  const [saving, setSaving] = useState(false);
  const [sending, setSending] = useState(false);
  const [error, setError] = useState(null);

  // ================= SAVE =================
  const save = async (payload) => {
    if (saving) return; // 🚫 prevent double click

    try {
      setSaving(true);
      setError(null);

      const result = await saveDocument(payload);
      return result;
    } catch (err) {
      setError(err.message || "Save failed");
      throw err;
    } finally {
      setSaving(false); 
    }
  };

  //  SEND 
  const saveAndSend = async (payload) => {
    if (sending) return; 

    try {
      setSending(true);
      setError(null);

      const result = await saveAndSendDocument(payload);
      return result;
    } catch (err) {
      setError(err.message || "Send failed");
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