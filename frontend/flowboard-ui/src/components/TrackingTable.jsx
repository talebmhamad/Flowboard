import React, { useEffect, useState } from "react";
import DataTableModule from "react-data-table-component";
import { useNavigate, useOutletContext } from "react-router-dom";
import { getTrackingTasks } from "../services/taskService";
import TaskFilters from "./TaskFilters";
import { useStatuses } from "../hooks/status/useStatuses";
import DocumentMetadata from "./DocumentMetadata";
import TrackingStatus from "./TrackingStatus";

const DataTable = DataTableModule.default;

const initialFormState = {
  refNumber: "",
  fromDate: "",
  toDate: "",
  docType: null,
  status: null,
  read: false,
  locked: false,
  assigned: false,
  overdue: false,
};

export default function TrackingTable() {
  const navigate = useNavigate();
  const { workflows = [] } = useOutletContext();
  const { statuses } = useStatuses();

  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRows, setTotalRows] = useState(0);
  const [selectedTaskId, setSelectedTaskId] = useState(null);
  const [selectedTask, setSelectedTask] = useState(null);
  const [activeTab, setActiveTab] = useState("tracking");
  const [formState, setFormState] = useState(initialFormState);

  useEffect(() => {
    loadTracking(initialFormState, 1, pageSize);
  }, []);

  const buildRequest = (filters, pageNumber, size) => ({
    draw: 1,
    start: (pageNumber - 1) * size,
    length: size,
    documentTypeId: filters.docType?.value || 0,
    statusId: filters.status?.value || 0,
    referenceNumber: filters.refNumber || "",
    fromDate: filters.fromDate ? new Date(filters.fromDate).toISOString() : null,
    toDate: filters.toDate ? new Date(filters.toDate).toISOString() : null,
    read: filters.read,
    locked: filters.locked,
    assigned: filters.assigned,
    overdue: filters.overdue,
  });

  const loadTracking = async (filters, pageNumber = 1, size = 10) => {
    try {
      setLoading(true);
      const request = buildRequest(filters, pageNumber, size);
      const res = await getTrackingTasks(request);
      const mapped = (res.data || []).map((t) => ({
        ...t,
        status: t.statusText || "Under Review",
      }));
      setTasks(mapped);
      setTotalRows(res.recordsFiltered || 0);
    } catch (err) {
      console.error("Tracking error:", err);
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

  const handleSearch = () => {
    setPage(1);
    loadTracking(formState, 1, pageSize);
  };

  const handleClear = () => {
    setFormState(initialFormState);
    setPage(1);
    loadTracking(initialFormState, 1, pageSize);
  };

  const handlePageChange = (p) => {
    setPage(p);
    loadTracking(formState, p, pageSize);
  };

  const handlePerRowsChange = (newSize, p) => {
    setPageSize(newSize);
    loadTracking(formState, p, newSize);
  };

  const handleSelectTask = (row) => {
    setSelectedTaskId(row.id);
    setSelectedTask(row);
    setActiveTab("tracking");
  };

  const handleBackToTable = () => {
    setSelectedTaskId(null);
    setSelectedTask(null);
  };

  const docTypeOptions = (workflows || []).map((wf) => ({
    value: wf.id,
    label: wf.text,
  }));

  const statusOptions = statuses.map((s) => ({
    value: s.id,
    label: s.text,
    color: s.color || "#888",
  }));

  const statusMap = Object.fromEntries(statusOptions.map((s) => [s.value, s]));

  const columns = [
    {
      name: "User",
      selector: (row) => row.ownerUserName || "N/A",
      sortable: true,
    },
    {
      name: "Document Type",
      selector: (row) => row.documentType || "N/A",
      sortable: true,
    },
    {
      name: "Reference Number",
      selector: (row) => row.referenceNumber || "---",
      sortable: true,
    },
    {
      name: "Created Date",
      selector: (row) =>
        row.createdDate
          ? new Date(row.createdDate).toISOString().split("T")[0]
          : "-",
      sortable: true,
    },
    {
      name: "Status",
      cell: (row) => {
        const status = statusMap[row.statusId];
        return (
          <span
            className="status-badge"
            style={{ backgroundColor: status?.color || "#ccc", color: "#fff" }}
          >
            {status?.label || "Unknown"}
          </span>
        );
      },
    },
    {
      name: "",
      width: "80px",
      cell: (row) => (
        <button
          className="btn btn-sm btn-outline-primary"
          onClick={() => handleSelectTask(row)}
        >
          <i className="bi bi-eye" />
        </button>
      ),
      ignoreRowClick: true,
      allowOverflow: true,
      button: true,
    },
  ];

  if (selectedTaskId) {
    return (
      <div className="inbox-container">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h5 className="fw-bold mb-0">Application Details</h5>
          <button
            className="btn btn-outline-secondary btn-sm"
            onClick={handleBackToTable}
          >
            <i className="bi bi-arrow-left me-1" />
            Back
          </button>
        </div>

        <ul className="nav nav-tabs mb-4">
          {["tracking", "metadata"].map((tab) => (
            <li className="nav-item" key={tab}>
              <button
                className={`nav-link ${activeTab === tab ? "active" : ""}`}
                onClick={() => setActiveTab(tab)}
              >
                {tab.charAt(0).toUpperCase() + tab.slice(1)}
              </button>
            </li>
          ))}
        </ul>

        {activeTab === "tracking" && <TrackingStatus task={selectedTask} />}
        {activeTab === "metadata" && (
          <DocumentMetadata taskId={selectedTaskId} WhoIs="Manager" />
        )}
      </div>
    );
  }

  return (
    <div className="inbox-container">
      <TaskFilters
        storageKey="trackingFilters"
        formState={formState}
        handleInputChange={handleInputChange}
        handleSearch={handleSearch}
        handleClear={handleClear}
        docTypeOptions={docTypeOptions}
        statusOptions={statusOptions}
        IsCompletedTable={true}
        IsDraftTable={false}
      />

      <div className="table-wrapper">
        <DataTable
          key="tracking-table"
          columns={columns}
          data={tasks}
          progressPending={loading && tasks.length === 0}
          pagination
          paginationServer
          paginationTotalRows={totalRows}
          paginationPerPage={pageSize}
          onChangePage={handlePageChange}
          onChangeRowsPerPage={handlePerRowsChange}
          highlightOnHover
          striped
          responsive
          persistTableHead
        />
      </div>
    </div>
  );
}