import React, { useEffect, useState } from "react";
import DataTableModule from "react-data-table-component";
import { useOutletContext, useNavigate } from "react-router-dom";


import { getDraftTasks } from "../services/taskService";
import TaskFilters from "./TaskFilters";

export default function DraftTable() {
  const DataTable = DataTableModule.default;

  const navigate = useNavigate();

  //  get workflows from routing context
  const { workflows = [] } = useOutletContext();

  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedRows, setSelectedRows] = useState([]);

  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRows, setTotalRows] = useState(0);

  const initialFormState = {
    fromDate: "",
    toDate: "",
    docType: null,
  };

  const [formState, setFormState] = useState(initialFormState);

  // 🔥 Load data on mount
  useEffect(() => {
    loadDraft(initialFormState, 1, pageSize);
  }, []);

  const loadDraft = async (filters, pageNumber = 1, size = 10) => {
    try {
      setLoading(true);

      const request = {
        start: (pageNumber - 1) * size,
        length: size,
        nodeId: 1,

        documentTypeId: filters.docType?.value || 0,

        fromDate: filters.fromDate
          ? new Date(filters.fromDate).toISOString()
          : null,
        toDate: filters.toDate
          ? new Date(filters.toDate).toISOString()
          : null,
      };

      const res = await getDraftTasks(request);

      setTasks(res.data || []);
      setTotalRows(res.recordsFiltered || 0);

    } catch (err) {
      console.error("Draft error:", err);
    } finally {
      setLoading(false);
    }
  };

  //  Filters handlers
  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setFormState((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSearch = () => {
    setPage(1);
    loadDraft(formState, 1, pageSize);
  };

  const handleClear = () => {
    setFormState(initialFormState);
    setPage(1);
    loadDraft(initialFormState, 1, pageSize);
  };

  const handlePageChange = (p) => {
    setPage(p);
    loadDraft(formState, p, pageSize);
  };

  const handlePerRowsChange = (newSize, p) => {
    setPageSize(newSize);
    loadDraft(formState, p, newSize);
  };

  //  Safe mapping
  const docTypeOptions = (workflows || []).map((wf) => ({
    value: wf.id,
    label: wf.text,
  }));

  //  Columns
  const columns = [
    {
      name: "Document Type",
      selector: (row) => row.documentType,
      sortable: true,
    },
    {
      name: "Created Date",
      selector: (row) => row.createdDate,
      sortable: true,
    },
    {
      name: "Modified Date",
      selector: (row) => row.modifiedDate || "-",
      sortable: true,
    },
    {
      name: "",
      width: "80px",
      cell: (row) => (
        <button
          className="btn btn-sm btn-outline-primary"
          onClick={() =>
            navigate(`/dashboard/form/draft/${row.id}`, {
              state: { from: "/dashboard/draft" } 
            })
          }
        >
          <i className="bi bi-pencil"></i>
        </button>
      ),
      ignoreRowClick: true,
      button: true,
    },
  ];

  return (
    <div className="inbox-container">

      {/* Filters */}
      <TaskFilters
        storageKey="draftFilters" 
        formState={formState}
        handleInputChange={handleInputChange}
        handleSearch={handleSearch}
        handleClear={handleClear}
        docTypeOptions={docTypeOptions}
        IsCompletedTable={false}
        IsDraftTable={true}
      />

      {/* Table */}
      <div className="table-wrapper">
        <DataTable
          key="draft-table"
          columns={columns}
          data={tasks}
          progressPending={loading && tasks.length === 0}
          pagination
          paginationServer
          paginationTotalRows={totalRows}
          paginationPerPage={pageSize}
          onChangePage={handlePageChange}
          onChangeRowsPerPage={handlePerRowsChange}
          selectableRows
          onSelectedRowsChange={(state) =>
            setSelectedRows(state.selectedRows)
          }
          highlightOnHover
          striped
          responsive
        />
      </div>
    </div>
  );
}