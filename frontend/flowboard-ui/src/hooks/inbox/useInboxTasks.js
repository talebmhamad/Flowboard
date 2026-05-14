import { useState, useCallback } from "react";
import { getActiveTasks } from "../../services/taskService";

/**
 * Hook to manage Inbox Task logic, pagination, and filtering.
 */
export default function useInboxTasks() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalRows, setTotalRows] = useState(0);
  
  // Pagination State
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const loadInbox = useCallback(async (filters = {}) => {
    setLoading(true);

    try {
      // Helper to safely format dates
      const formatDate = (dateStr) => 
        dateStr ? new Date(dateStr).toISOString() : null;

      const requestBody = {
        draw: 1,
        start: (page - 1) * pageSize,
        length: pageSize,
        nodeId: 2,
        documentTypeId: filters.docType?.value ?? 0,
        statusId: filters.status?.value ?? 0,
        referenceNumber: filters.refNumber || "",
        fromDate: formatDate(filters.fromDate),
        toDate: formatDate(filters.toDate),
        read: filters.read,
        locked: filters.locked,
        assigned: filters.assigned,
        overdue: filters.overdue,
      };

      const response = await getActiveTasks(requestBody);

      // Map data and provide defaults
      const mappedTasks = (response.data || []).map((task) => ({
        ...task,
        status: task.status || "Pending",
      }));

      setTasks(mappedTasks);
      setTotalRows(response.recordsFiltered || 0);

    } catch (error) {
      console.error("Failed to load inbox tasks:", error);
      // Optional: Set tasks to empty array on error to prevent stale UI
      setTasks([]); 
    } finally {
      setLoading(false);
    }
  }, [page, pageSize]);

  return {
    // Data & Loading
    tasks,
    loading,
    totalRows,

    // Pagination
    page,
    setPage,
    pageSize,
    setPageSize,

    // Actions
    loadInbox,
  };
}