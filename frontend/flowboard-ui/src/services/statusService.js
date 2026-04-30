import { useEffect, useState } from "react";
import { getStatuses } from "../services/statusService";

const [statuses, setStatuses] = useState([]);

useEffect(() => {
  const fetchStatuses = async () => {
    try {
      const data = await getStatuses();
      setStatuses(data);
    } catch (err) {
      console.error(err);
    }
  };

  fetchStatuses();
}, []);