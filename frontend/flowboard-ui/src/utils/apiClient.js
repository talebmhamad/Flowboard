import { getToken, clearToken } from "./authStorage";

export const apiFetch = async (url, options = {}) => {
  const token = getToken();

  const isFormData = options.body instanceof FormData;

  const headers = {
    ...(isFormData ? {} : { "Content-Type": "application/json" }),
    ...(options.headers || {}),
    ...(token && { Authorization: `Bearer ${token}` })
  };

  const response = await fetch(url, {
    ...options,
    headers
  });

  //  Handle errors
  if (!response.ok) {
    const errorText = await response.text();

if (response.status === 401) {
  clearToken();             
  window.location.href = "/login"; 
}

    throw new Error(errorText || "API request failed");
  }

  //  FIX STARTS HERE
  const text = await response.text();

  // If empty response → return null safely
  if (!text) return null;

  try {
    return JSON.parse(text);
  } catch {
    return text; // fallback if not JSON
  }
};