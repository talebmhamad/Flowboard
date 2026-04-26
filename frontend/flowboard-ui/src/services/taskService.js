import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

/**
 *  Get Active Tasks (Inbox)
 */
export const getActiveTasks = async () => {
  try {
    const data = await apiFetch(`${API_URL}/tasks/active`);
    return data;
  } catch (error) {
    console.error("Error in getActiveTasks:", error);
    throw error;
  }
};

/**
 *  Get Completed Tasks
 */
export const getCompletedTasks = async () => {
  try {
    const data = await apiFetch(`${API_URL}/tasks/completed`);
    return data;
  } catch (error) {
    console.error("Error in getCompletedTasks:", error);
    throw error;
  }
};

/**
 *  Get Draft Tasks
 */
export const getDraftTasks = async () => {
  try {
    const data = await apiFetch(`${API_URL}/tasks/draft`);
    return data;
  } catch (error) {
    console.error("Error in getDraftTasks:", error);
    throw error;
  }
};

/**
 *  Get My Requests
 */
export const getMyRequests = async () => {
  try {
    const data = await apiFetch(`${API_URL}/tasks/myrequests`);
    return data;
  } catch (error) {
    console.error("Error in getMyRequests:", error);
    throw error;
  }
};