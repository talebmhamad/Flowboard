import { useState } from "react";
import { saveDocument } from "../services/documentService";

export const useDocument = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const save = async (payload) => {
    try {
      setLoading(true);
      setError(null);

      const result = await saveDocument(payload);
      return result;
    } catch (err) {
      setError(err.message || "Save failed");
      throw err;
    } finally {
      setLoading(false);
    }
  };

  return {
    save,
    loading,
    error
  };
};