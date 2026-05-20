import { useEffect, useState } from "react";

import { getUserSummary } from "../../services/userService";

import { getWorkflows } from "../../services/workflowService";

import { useAppContext } from "../../context/AppContext";

export default function useDashboardData() {

  const [workflows, setWorkflows] = useState([]);

  const [loading, setLoading] = useState(true);

  const { summary, setSummary } = useAppContext();

  useEffect(() => {

    let isMounted = true;

    const loadDashboardData = async () => {

      try {

        setLoading(true);

        const [
          summaryData,
          workflowData
        ] = await Promise.all([
          getUserSummary(),
          getWorkflows()
        ]);

        if (!isMounted) return;

        setSummary(summaryData);

        setWorkflows(workflowData);

      } catch (err) {

        console.error(
          "Dashboard Load Error:",
          err
        );

      } finally {

        if (isMounted) {
          setLoading(false);
        }

      }
    };

    loadDashboardData();

    return () => {
      isMounted = false;
    };

  }, []);

  return {
    summary,
    workflows,
    loading
  };
}