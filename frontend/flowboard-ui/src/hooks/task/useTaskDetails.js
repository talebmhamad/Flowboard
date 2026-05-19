import { useEffect, useState } from "react";

import { getTaskDetails } from "../../services/taskService";

import { getDocumentBasicInfo } from "../../services/documentService";

const safeParse = (value, fallback = {}) => {

  try {

    if (!value) return fallback;

    return typeof value === "string"
      ? JSON.parse(value)
      : value;

  } catch {

    return fallback;

  }

};

export default function useTaskDetails(
  taskId,
  initialTask = null
) {

  const [task, setTask] = useState(initialTask);

  const [docInfo, setDocInfo] = useState(null);

  const [loading, setLoading] = useState(true);

  useEffect(() => {

    if (!taskId) return;

    let isMounted = true;

    const loadDetails = async () => {

      try {

        setLoading(true);

        const [taskRes, docRes] = await Promise.all([

          getTaskDetails(taskId),

          getDocumentBasicInfo(taskId)

        ]);

        if (!isMounted) return;

        setTask({

          ...taskRes,

          formDesigner: safeParse(
            taskRes.formDesigner,
            {}
          ),

          formData: safeParse(
            taskRes.formData,
            {}
          )

        });

        setDocInfo(docRes);

      } catch (err) {

        console.error(
          "Task details loading error:",
          err
        );

      } finally {

        if (isMounted) {

          setLoading(false);

        }

      }

    };

    loadDetails();

    return () => {

      isMounted = false;

    };

  }, [taskId]);

  return {

    task,
    docInfo,
    loading,

    setTask,
    setDocInfo

  };

}