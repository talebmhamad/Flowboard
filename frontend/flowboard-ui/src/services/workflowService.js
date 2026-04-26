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