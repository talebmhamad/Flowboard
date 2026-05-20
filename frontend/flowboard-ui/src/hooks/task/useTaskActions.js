import { useCallback } from "react";
import { showSuccess, showError, showWarning } from "../../utils/toast";
import { getUserSummary } from "../../services/userService";

export default function useTaskActions({
  task,
  formInstanceRef,
  save,
  saveAndSend,
  setSummary,
  onBack,
  fromRef
}) {

  const handleSave = useCallback(async () => {

    try {

      const data = formInstanceRef.current?.submission?.data || {};

      await save({
        id: task.id,
        rowVersion: task.rowVersion,
        formData: data
      });

      showSuccess("Saved successfully!");

      const newSummary = await getUserSummary();

      setSummary(newSummary);

      setTimeout(() => {
        onBack?.(fromRef.current);
      }, 800);

    } catch (err) {

      console.error("Save error:", err);

      showError("Save failed");

    }

  }, [
    task,
    formInstanceRef,
    save,
    setSummary,
    onBack,
    fromRef
  ]);

  const handleSend = useCallback(async () => {

    try {

      const form = formInstanceRef.current;

      if (!form) {

        showError("Form not ready");

        return;

      }

      const isValid = await form.checkValidity(null, true);

      if (!isValid) {

        showWarning("Complete required fields");

        return;

      }

      const data = form.submission?.data || {};

      await saveAndSend({
        id: task.id,
        rowVersion: task.rowVersion,
        formData: data
      });

      showSuccess("Sent successfully!");

      const newSummary = await getUserSummary();

      setSummary(newSummary);

      setTimeout(() => {
        onBack?.(fromRef.current);
      }, 800);

    } catch (err) {

      console.error("Send error:", err);

      showError("Send failed");

    }

  }, [
    task,
    formInstanceRef,
    saveAndSend,
    setSummary,
    onBack,
    fromRef
  ]);

  return {
    handleSave,
    handleSend
  };

}