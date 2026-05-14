import React, { useEffect, useState } from "react";
import DataTableModule from "react-data-table-component";
import { getCompletedTasks } from "../services/taskService"
import TaskFilters from "./TaskFilters";
import { useStatuses } from "../hooks/status/useStatuses";
import DocumentMetadata from "./DocumentMetadata"; 

export default function CompletedTable({ documentTypes = [] }) {
  const DataTable = DataTableModule.default;

  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRows, setTotalRows] = useState(0);
  const { statuses } = useStatuses();
  const [selectedTaskId, setSelectedTaskId] = useState(null);

  const initialFormState = {
    refNumber: "",
    fromDate: "",
    toDate: "",
    docType: null,
    status: null,
  };

  const [formState, setFormState] = useState(initialFormState);

  useEffect(() => {
    loadCompleted(initialFormState, 1, pageSize);
  }, []);

  const loadCompleted = async (filters, pageNumber = 1, size = 10) => {
    try {
      setLoading(true);

      const request = {
        draw: 1,
        start: (pageNumber - 1) * size,
        length: size,
        nodeId: 3,
        documentTypeId: filters.docType?.value || 0,
        statusId: filters.status?.value || 0,
        referenceNumber: filters.refNumber || "",
        fromDate: filters.fromDate
          ? new Date(filters.fromDate).toISOString()
          : null,
        toDate: filters.toDate
          ? new Date(filters.toDate).toISOString()
          : null,
      };

      const res = await getCompletedTasks(request);

      setTasks(res.data || []);
      setTotalRows(res.recordsFiltered || 0);
    } catch (err) {
      console.error("Completed error:", err);
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setFormState((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSearch = () => {
    setPage(1);
    loadCompleted(formState, 1, pageSize);
  };

  const handleClear = () => {
    setFormState(initialFormState);
    setPage(1);
    loadCompleted(initialFormState, 1, pageSize);
  };

  const handlePageChange = (p) => {
    setPage(p);
    loadCompleted(formState, p, pageSize);
  };

  const handlePerRowsChange = (newSize, p) => {
    setPageSize(newSize);
    loadCompleted(formState, p, newSize);
  };

  const docTypeOptions = documentTypes.map((wf) => ({
    value: wf.id,
    label: wf.text,
  }));

  const statusOptions = statuses.map((s) => ({
    value: s.id,
    label: s.text,
    color: s.color || "#888",
  }));

  const statusMap = Object.fromEntries(
    statusOptions.map((s) => [s.value, s])
  );

  //  UPDATED COLUMNS
  const columns = [
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
      name: "Task Date",
      selector: (row) => row.taskDate || "-",
      sortable: true,
    },
    {
      name: "Owner",
      selector: (row) => row.createdByUser || "-",
      sortable: true,
    },
    {
      name: "Created Date",
      selector: (row) => row.createdDate || "-",
      sortable: true,
    },
    {
      name: "Status",
      cell: (row) => {
        const status = statusMap[row.status];
        return (
          <span
            className="status-badge"
            style={{
              backgroundColor: status?.color || "#ccc",
              color: "#fff",
            }}
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
          onClick={() => setSelectedTaskId(row.id)}
        >
          <i className="bi bi-eye"></i>
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

        <div className="d-flex justify-content-between mb-3">
          <h5>Application Metadata</h5>

           <button
           className="btn btn-outline-secondary btn-sm"
           onClick={() => setSelectedTaskId(null)}
           ><i className="bi bi-arrow-left me-1" />
            Back
          </button>
        </div>

        <DocumentMetadata taskId={selectedTaskId} />
      </div>
    );
  }

  return (
    <div className="inbox-container">

      <TaskFilters
        storageKey="completedFilters"
        formState={formState}
        handleInputChange={handleInputChange}
        handleSearch={handleSearch}
        handleClear={handleClear}
        docTypeOptions={docTypeOptions}
        statusOptions={statusOptions}
        IsCompletedTable={true}
        IsDraftTable={true}
      />

      <div className="table-wrapper">
        <DataTable
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
        />
      </div>
    </div>
  );
}