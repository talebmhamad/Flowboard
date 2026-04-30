import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

export const getActiveTasks = async (filters = {}) => {
  try {
    const request = {
      draw: 1,
      start: filters.start || 0,
      length: filters.length || 10,

      nodeId: 1, 

      documentTypeId: filters.docType?.[0] || 0,
      statusId: filters.status?.[0] || 0,
      referenceNumber: filters.refNumber || "",

      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,

      read: filters.read || false,
      locked: filters.locked || false,
      assigned: filters.assigned || false,
      overdue: filters.overdue || false
    };

    console.log("getActiveTasks request:", request);

    const data = await apiFetch(`${API_URL}/tasks/active`, {
      method: "POST",
      body: JSON.stringify(request),
      headers: {
        "Content-Type": "application/json"
      }
    });
    console.log("getActiveTasks response:", data);
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