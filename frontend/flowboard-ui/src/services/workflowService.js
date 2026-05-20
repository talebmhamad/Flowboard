import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";


export const getWorkflows = async () => {
  try {
    const data = await apiFetch(`${API_URL}/workflow`);
    return data;
  } catch (error) {
    console.error("Error in getWorkflows:", error);
    throw error;
  }
};

export const getWorkflowForm = async (documentTypeId) => {
  try {
    const data = await apiFetch(
      `${API_URL}/workflow/form/${documentTypeId}`
    );

    if (data && data.formDesigner) {
      data.formDesigner = JSON.parse(data.formDesigner);
    }

    return data;
  } catch (error) {
    console.error("Error in getWorkflowForm:", error);
    throw error;
  }
};