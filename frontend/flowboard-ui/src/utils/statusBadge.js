export const getStatusBadgeClass = (status) => {

  const normalizedStatus = status?.toLowerCase();

  const statusClasses = {

    approved: "bg-success",

    rejected: "bg-danger",

    pending: "bg-warning text-dark",

    completed: "bg-primary",

    draft: "bg-secondary",

    cancelled: "bg-dark",

    inprogress: "bg-info text-dark",

    open: "bg-primary",

    closed: "bg-success",

    overdue: "bg-danger",

  };

  return statusClasses[normalizedStatus] || "bg-secondary";
};