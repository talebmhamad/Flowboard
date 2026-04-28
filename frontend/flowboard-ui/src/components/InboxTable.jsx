import React, { useEffect, useState } from "react";
import { getActiveTasks } from "../services/taskService";
import TaskFilters from "./TaskFilters";
import "../styles/Inbox.css";

export default function InboxTable({ documentTypes = [] }) {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);

  const statusOptions = [
    { value: "", label: "" },
    { value: "Delayed", label: "Delayed" },
    { value: "Pending", label: "Pending" },
    { value: "Postponed", label: "Postponed" },
    { value: "For Approval", label: "For Approval" },
    { value: "In Progress", label: "In Progress" },
    { value: "Approved", label: "Approved" },
    { value: "Rejected", label: "Rejected" },
  ];

  const docTypeOptions = documentTypes.map((wf) => ({
    value: wf.name,
    label: wf.text || wf.name,
  }));

  const initialFormState = {
    refNumber: "",
    fromDate: "",
    toDate: "",
    docType: [],
    status: [],
    read: false,
    locked: false,
    assigned: false,
    overdue: false,
  };

  const [formState, setFormState] = useState(initialFormState);

  useEffect(() => {
    loadInbox({});
  }, []);

  const loadInbox = async (searchFilters) => {
    try {
      setLoading(true);

      const formattedFilters = {
        ...searchFilters,
        docType: searchFilters.docType?.map((o) => o.value) || [],
        status: searchFilters.status?.map((o) => o.value) || [],
      };

      const data = await getActiveTasks(formattedFilters);
      const taskList = Array.isArray(data) ? data : data.tasks || [];
      setTasks(taskList);
    } catch (err) {
      console.error("Failed to fetch inbox:", err);
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormState((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value,
    }));
  };

  const handleMultiSelectChange = (selectedOptions, actionMeta) => {
    setFormState((prev) => ({
      ...prev,
      [actionMeta.name]: selectedOptions,
    }));
  };

  const handleSearch = () => {
    loadInbox(formState);
  };

  const handleClear = () => {
    setFormState(initialFormState);
    loadInbox({});
  };

  const customSelectStyles = {
    control: (base, state) => ({
      ...base,
      borderColor: state.isFocused ? "#4e9776" : "#d2d6de",
      minHeight: "34px",
      borderRadius: "3px",
      fontSize: "13px",
      boxShadow: state.isFocused ? "0 0 0 1px rgba(78, 151, 118, 0.2)" : null,
      "&:hover": { borderColor: "#4e9776" },
    }),
    multiValue: (base) => ({
      ...base,
      backgroundColor: "#4e9776",
      borderRadius: "2px",
    }),
    multiValueLabel: (base) => ({
      ...base,
      color: "white",
      padding: "2px 6px",
    }),
    multiValueRemove: (base) => ({
      ...base,
      color: "white",
      "&:hover": { backgroundColor: "#3d7a5d", color: "white" },
    }),
  };

  if (loading)
    return <div className="loader-container">Loading your inbox...</div>;

  return (
    <div className="inbox-container">

      <TaskFilters
        formState={formState}
        handleInputChange={handleInputChange}
        handleMultiSelectChange={handleMultiSelectChange}
        handleSearch={handleSearch}
        handleClear={handleClear}
        docTypeOptions={docTypeOptions}
        statusOptions={statusOptions}
        customSelectStyles={customSelectStyles}
      />

      <div className="table-wrapper">
        <table className="inbox-table">
          <thead>
            <tr>
              <th width="40"></th>
              <th>Document type <span className="sort-arrows">⇅</span></th>
              <th>Reference Number <span className="sort-arrows">⇅</span></th>
              <th>Task date <span className="sort-arrows">⇅</span></th>
              <th>Created date <span className="sort-arrows">⇅</span></th>
              <th>Status <span className="sort-arrows">⇅</span></th>
              <th className="text-right"></th>
            </tr>
          </thead>

          <tbody>
            {tasks.length > 0 ? (
              tasks.map((task, idx) => (
                <tr key={task.id || idx}>
                  <td className="center-cell">
                    <button className="btn-plus">+</button>
                  </td>

                  <td className="doc-type-cell">
                    {task.documentType || "N/A"}
                  </td>

                  <td>{task.referenceNumber || "---"}</td>
                  <td>{task.taskDate}</td>
                  <td>{task.createdDate}</td>

                  <td>
                    <span
                      className={`status-badge ${
                        task.status?.toLowerCase().replace(/\s+/g, "-") ||
                        "pending"
                      }`}
                    >
                      {task.status || "Pending"}
                    </span>
                  </td>

                  <td className="action-cell">
                    <div className="action-icons">
                      <span className="icon-msg">✉</span>
                      <span className="icon-user">👤</span>
                      <button className="btn-edit-square">📝</button>
                    </div>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan="7" className="text-center">
                  No tasks found.
                </td>
              </tr>
            )}
          </tbody>
        </table>

        <div className="table-footer">
          <div className="entry-info">
            Showing 1 to {tasks.length} of {tasks.length} entries
          </div>

          <div className="pagination-controls">
            <button className="page-nav" disabled>
              Previous
            </button>
            <button className="page-number active">1</button>
            <button className="page-nav" disabled>
              Next
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}