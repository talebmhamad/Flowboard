import React, {
  useEffect,
  useMemo,
  useCallback,
} from "react";

import { useNavigate, useOutletContext } from "react-router-dom";
import DataTableModule from "react-data-table-component";
import TaskFilters from "./TaskFilters";
import useInboxFilters from "../hooks/inbox/useInboxFilters";
import useInboxTasks from "../hooks/inbox/useInboxTasks";
import useInboxColumns from "../hooks/inbox/useInboxColumns";
import { useStatuses } from "../hooks/status/useStatuses";

export default function InboxTable() {
  const DataTable = DataTableModule.default;
  const {formState,initialFormState,handleInputChange,resetFilters,} = useInboxFilters();
  const navigate = useNavigate();
  const { workflows = [] } = useOutletContext();
  const { statuses } = useStatuses();
  const {tasks,loading,totalRows,page,setPage,pageSize,setPageSize,loadInbox,} = useInboxTasks();

 useEffect(() => {

   loadInbox(initialFormState, 1, pageSize);

  }, [pageSize]);

  const handleSearch = useCallback(() => {

  setPage(1);

  loadInbox(formState, 1, pageSize);

  }, [formState, pageSize, loadInbox, setPage]);

  const handleClear = useCallback(() => {

  resetFilters();

  setPage(1);

  loadInbox(initialFormState, 1, pageSize);

}, [
  resetFilters,
  setPage,
  loadInbox,
  initialFormState,
  pageSize
  ]);

  const handlePageChange = useCallback((p) => {

  setPage(p);

  loadInbox(formState, p, pageSize);

}, [
  formState,
  pageSize,
  loadInbox,
  setPage
  ]);

  const handlePerRowsChange = useCallback((newSize, p) => {

  setPageSize(newSize);

  loadInbox(formState, p, newSize);

}, [
  formState,
  loadInbox,
  setPageSize
  ]);

  const docTypeOptions = useMemo(() => {

  return (workflows || []).map((wf) => ({
    value: wf.id,
    label: wf.text,
  }));

  }, [workflows]);

  const statusOptions = useMemo(() => {

  return statuses.map((s) => ({
    value: s.id,
    label: s.text,
    color: s.color || "#888",
  }));

  }, [statuses]);

  const statusMap = useMemo(() => {

  return Object.fromEntries(
    statusOptions.map((s) => [
      s.value,
      s,
    ])
  );

  }, [statusOptions]);

  const handleEdit = useCallback((row) => {

  const taskStatus = statusMap[row.status];

  navigate(`/dashboard/form/task/${row.id}`, {
    state: {
      from: "/dashboard/inbox",
      status: taskStatus
    }
  });

  }, [navigate, statusMap]);

  const columns = useInboxColumns({ statusMap, handleEdit });

  return (

  <div className="inbox-container">

    {/* Filters */}
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

    {/* Table */}
    <div className="table-wrapper">
      <DataTable
        key="inbox-table"
        columns={columns}
        data={tasks}

        progressPending={
          loading && tasks.length === 0
        }

        pagination
        paginationServer

        paginationTotalRows={totalRows}

        paginationPerPage={pageSize}

        onChangePage={handlePageChange}

        onChangeRowsPerPage={
          handlePerRowsChange
        }

        highlightOnHover
        striped
        responsive
        persistTableHead
      />
    </div>

  </div>
  );

}