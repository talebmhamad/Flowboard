import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

export const getStatuses = async () => {
  try {
    return await apiFetch(`${API_URL}/status`);
  } catch (error) {
    console.error("Error in getStatuses:", error);
    throw error;
  }
};