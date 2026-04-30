import React, { useEffect, useState } from "react";
import DataTableModule from "react-data-table-component";
import { getActiveTasks } from "../services/taskService";
import { getStatuses } from "../services/statusService";
import TaskFilters from "./TaskFilters";
import "../styles/Inbox.css";

export default function InboxTable({ documentTypes = [] }) {
  const DataTable = DataTableModule.default;

  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRows, setTotalRows] = useState(0);

  const [statuses, setStatuses] = useState([]);

  // ✅ FIXED: single values (not arrays)
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

  //  LOAD STATUS 
  useEffect(() => {
    const fetchStatuses = async () => {
      try {
        const data = await getStatuses();
        setStatuses(data);
      } catch (err) {
        console.error("Status load error:", err);
      }
    };

    fetchStatuses();
  }, []);

  //  LOAD INBOX 
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

        //  FIXED (no array)
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

  //  HANDLERS 
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

  //  PAGINATION 
  const handlePageChange = (p) => {
    setPage(p);
    loadInbox(formState, p, pageSize);
  };

  const handlePerRowsChange = (newSize, p) => {
    setPageSize(newSize);
    loadInbox(formState, p, newSize);
  };

  //  OPTIONS 
  const docTypeOptions = documentTypes.map((wf) => ({
    value: wf.id,
    label: wf.text,
  }));

  const statusOptions = statuses.map((s) => ({
    value: s.id,
    label: s.text,
    color: s.color,
  }));

  //  COLUMNS 
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
        const statusText = String(row.status || "Pending");

        return (
          <span
            className={`status-badge ${statusText
              .toLowerCase()
              .replace(/\s+/g, "-")}`}
          >
            {statusText}
          </span>
        );
      },
    },
    {
      name: "",
      cell: () => (
        <div style={{ display: "flex", justifyContent: "flex-end" }}>
          <button className="btn-edit-square">
            <i className="bi bi-pencil"></i>
          </button>
        </div>
      ),
    },
  ];

  //  UI 
  return (
    <div className="inbox-container">
      <TaskFilters
        formState={formState}
        handleInputChange={handleInputChange}
        handleSearch={handleSearch}
        handleClear={handleClear}
        docTypeOptions={docTypeOptions}
        statusOptions={statusOptions}
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