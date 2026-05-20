import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

/**
 * Get user summary (counts for sidebar)
 */
export const getUserSummary = async () => {
  try {
    const data = await apiFetch(`${API_URL}/user/summary`);
    return data;
  } catch (error) {
    console.error("Error in getUserSummary:", error);
    throw error;
  }
};