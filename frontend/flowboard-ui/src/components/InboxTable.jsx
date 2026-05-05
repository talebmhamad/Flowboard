import React, { useEffect, useState } from "react";
import DataTableModule from "react-data-table-component";
import { useNavigate } from "react-router-dom";

import { getActiveTasks } from "../services/taskService";
import TaskFilters from "./TaskFilters";
import "../styles/Inbox.css";
import { useStatuses } from "../hooks/useStatuses";
import { useOutletContext } from "react-router-dom";

export default function InboxTable() {
  const DataTable = DataTableModule.default;

  const navigate = useNavigate();

  //  get workflows from context (routing)
  const { workflows = [] } = useOutletContext();

  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const { statuses } = useStatuses();

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRows, setTotalRows] = useState(0);

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

  const [formState, setFormState] = useState(initialFormState);

  useEffect(() => {
    loadInbox(initialFormState, 1, pageSize);
  }, []);

  const loadInbox = async (filters, pageNumber = 1, size = 10) => {
    try {
      setLoading(true);

      const request = {
        draw: 1,
        start: (pageNumber - 1) * size,
        length: size,
        nodeId: 2,

        documentTypeId: filters.docType?.value || 0,
        statusId: filters.status?.value || 0,
        referenceNumber: filters.refNumber || "",

        fromDate: filters.fromDate
          ? new Date(filters.fromDate).toISOString()
          : null,
        toDate: filters.toDate
          ? new Date(filters.toDate).toISOString()
          : null,

        read: filters.read,
        locked: filters.locked,
        assigned: filters.assigned,
        overdue: filters.overdue,
      };

      const res = await getActiveTasks(request);

      const mapped = (res.data || []).map((t) => ({
        ...t,
        status: t.status || "Pending",
      }));

      setTasks(mapped);
      setTotalRows(res.recordsFiltered || 0);
    } catch (err) {
      console.error("Inbox error:", err);
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
    loadInbox(formState, 1, pageSize);
  };

  const handleClear = () => {
    setFormState(initialFormState);
    setPage(1);
    loadInbox(initialFormState, 1, pageSize);
  };

  const handlePageChange = (p) => {
    setPage(p);
    loadInbox(formState, p, pageSize);
  };

  const handlePerRowsChange = (newSize, p) => {
    setPageSize(newSize);
    loadInbox(formState, p, newSize);
  };

  //  FIX: use workflows instead of props
  const docTypeOptions = (workflows || []).map((wf) => ({
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

  //  NEW: ROUTING instead of state
  const handleEdit = (row) => {
    navigate(`/dashboard/form/task/${row.id}`, {
    state: { from: "/dashboard/inbox" }
    });
  };

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
          onClick={() => handleEdit(row)}
        >
          <i className="bi bi-pencil"></i>
        </button>
      ),
      ignoreRowClick: true,
      allowOverflow: true,
      button: true,
    },
  ];

  return (
    <div className="inbox-container">

<TaskFilters
  storageKey="inboxFilters"  
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
          key="inbox-table"
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