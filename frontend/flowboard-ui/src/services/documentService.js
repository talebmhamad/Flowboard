import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";


//  Save only
export const saveDocument = async ({
  documentTypeId,
  workflowId = null,
  formData,
  id,
  rowVersion 
}) => {
  const body = new FormData();

  if (id) body.append("Id", id);
  body.append("DocumentTypeId", documentTypeId);

  if (workflowId) body.append("WorkflowId", null); 

  body.append("FormData", JSON.stringify(formData));

  if (rowVersion) body.append("RowVersion", rowVersion);

  return await apiFetch(`${API_URL}/document/save`, {
    method: "POST",
    body
  });
};

//  Save + Send
export const saveAndSendDocument = async ({
  documentTypeId,
  workflowId,
  formData,
  id = "",
  rowVersion = ""
}) => {
  const body = new FormData();

  if (id) body.append("Id", id);
  body.append("DocumentTypeId", documentTypeId);

  if (workflowId) body.append("WorkflowId", null);

  body.append("FormData", JSON.stringify(formData));

  if (rowVersion) body.append("RowVersion", rowVersion);

  return await apiFetch(`${API_URL}/document/saveandsend`, {
    method: "POST",
    body
  });
};

export const getDocumentBasicInfo = async (taskId) => {
  try {
    const data = await apiFetch(`${API_URL}/document/basic-info/${taskId}`, {
      method: "GET"
    });

    return data;
  } catch (error) {
    console.error("Error in getDocumentBasicInfo:", error);
    throw error;
  }
};

export const getDocumentByTaskId = async (taskId) => {
  return await apiFetch(`${API_URL}/document/by-task/${taskId}`);
};

export const getDocumentById = async (id) => {
  return await apiFetch(`${API_URL}/document/${id}`);
};

export const getTrackingByTaskId = async (taskId) => {
  try {
    const data = await apiFetch(
      `${API_URL}/admin/document/by-task/${taskId}`,
      {
        method: "GET"
      }
    );

    return data;
  } catch (error) {
    console.error(
      "Error in getTrackingByTaskId:",
      error
    );

    throw error;
  }
};