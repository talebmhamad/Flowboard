import { apiFetch } from "../utils/apiClient";
import { API_URL } from "../config";

debugger
export const saveDocument = async ({
  documentTypeId,
  workflowId,
  formData,
  id = "",
  rowVersion = ""
}) => {
  const body = new FormData();

  if (id) body.append("Id", id);
  body.append("DocumentTypeId", documentTypeId);

  if (workflowId) body.append("WorkflowId", workflowId);

  body.append("FormData", JSON.stringify(formData));

  if (rowVersion) body.append("RowVersion", rowVersion);

  return await apiFetch(`${API_URL}/document/save`, {
    method: "POST",
    body
  });
};