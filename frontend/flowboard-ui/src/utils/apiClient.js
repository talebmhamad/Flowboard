import { getToken, logout } from "./authStorage";

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

  if (!response.ok) {
    const errorText = await response.text();

    // Optional: auto logout if unauthorized
    if (response.status === 401) {
      logout();
    }

    throw new Error(errorText || "API request failed");
  }

  return response.json();
};