import { useMemo } from "react";
import { getStatusBadgeClass } from "../../utils/statusBadge";

export default function useInboxColumns({
  statusMap,
  handleEdit
}) {

  const columns = useMemo(() => {

    return [

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
              className={`badge rounded-pill px-3 py-2 ${getStatusBadgeClass(status?.label)}`}
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
      },

    ];

  }, [statusMap, handleEdit]);

  return columns;
}