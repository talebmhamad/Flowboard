import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

export const getActiveTasks = async (filters = {}) => {
  
  try {
    const request = {
      draw: 1,
      start: filters.start || 0,
      length: filters.length || 10,

      nodeId: 1, 

      documentTypeId: filters.documentTypeId || 0,
      statusId: filters.statusId || 0,
      referenceNumber: filters.referenceNumber || "",

      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,

      read: filters.read || false,
      locked: filters.locked || false,
      assigned: filters.assigned || false,
      overdue: filters.overdue || false
    };

    const data = await apiFetch(`${API_URL}/tasks/active`, {
      method: "POST",
      body: JSON.stringify(request),
      headers: {
        "Content-Type": "application/json"
      }
   
    });
    return data;
  } catch (error) {
    console.error("Error in getActiveTasks:", error);
    throw error;
  }
};
/**
 *  Get Completed Tasks
 */
export const getCompletedTasks = async (filters = {}) => {
  try {
    const request = {
      draw: 1,
      start: filters.start || 0,
      length: filters.length || 10,
      nodeId: 1,
      documentTypeId: filters.documentTypeId || 0,
      statusId: filters.statusId || 0,
      referenceNumber: filters.referenceNumber || "",
      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,
    };

    const data = await apiFetch(`${API_URL}/tasks/completed`, {
      method: "POST",
      body: JSON.stringify(request),
      headers: {
        "Content-Type": "application/json"
      }
    });

    return data;
  } catch (error) {
    console.error("Error in getCompletedTasks:", error);
    throw error;
  }
};

/**
 *  Get Draft Tasks
 */
export const getDraftTasks = async (filters = {}) => {
  try {
    const request = {
      draw: 1,
      start: filters.start || 0,
      length: filters.length || 10,
      nodeId: 1, 
      documentTypeId: filters.documentTypeId || 0,
      fromDate: filters.fromDate || null,
      toDate: filters.toDate || null,
    };

    return await apiFetch(`${API_URL}/tasks/draft`, {
      method: "POST",
      body: JSON.stringify(request),
      headers: {
        "Content-Type": "application/json"
      }
    });

  } catch (error) {
    console.error("Error in getDraftTasks:", error);
    throw error;
  }
};

/**
 * Get Task Details (by taskId)
 */
export const getTaskDetails = async (taskId) => {
  try {
    const data = await apiFetch(`${API_URL}/tasks/details/${taskId}`, {
      method: "GET"
    });

    return data;
  } catch (error) {
    console.error("Error in getTaskDetails:", error);
    throw error;
  }
};

/**
 * Save Task 
 */
export const saveTask = async ({ id, rowVersion, formData }) => {
  try {
    const body = new FormData();

    if (id) body.append("Id", id.toString());
    if (rowVersion) body.append("rowVersion", rowVersion);

    body.append("FormData", JSON.stringify(formData));

    return await apiFetch(`${API_URL}/tasks/save`, {
      method: "POST",
      body
    });

  } catch (error) {
    console.error("Error in saveTask:", error);
    throw error;
  }
};

/**
 * Save and Send Task 
 */
export const saveAndSendTask = async ({ id, rowVersion, formData }) => {
  try {
    const body = new FormData();

    if (id) body.append("Id", id.toString());
    if (rowVersion) body.append("rowVersion", rowVersion.toString());

    body.append("FormData", JSON.stringify(formData));


    const data = await apiFetch(`${API_URL}/tasks/saveandsend`, {
      method: "POST",
      body
    });

    return data;

  } catch (error) {
    console.error("Error in saveAndSendTask:", error);
    throw error;
  }
};
