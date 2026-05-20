import { useEffect, useState } from "react";
import { getStatuses } from "../../services/statusService";

export const useStatuses = () => {
  const [statuses, setStatuses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchStatuses = async () => {
      try {
        setLoading(true);
        const data = await getStatuses();
        setStatuses(data);
      } catch (err) {
        console.error("Status hook error:", err);
        setError(err);
      } finally {
        setLoading(false);
      }
    };

    fetchStatuses();
  }, []);

  return {
    statuses,
    loading,
    error
  };
};