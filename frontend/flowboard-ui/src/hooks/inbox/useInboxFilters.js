import { useState } from "react";

export default function useInboxFilters() {

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

  const handleInputChange = (e) => {

    const { name, value, type, checked } = e.target;

    setFormState((prev) => ({
      ...prev,
      [name]: type === "checkbox"
        ? checked
        : value,
    }));
  };

  const resetFilters = () => {
    setFormState(initialFormState);
  };

  return {
    formState,
    setFormState,
    initialFormState,
    handleInputChange,
    resetFilters,
  };
}