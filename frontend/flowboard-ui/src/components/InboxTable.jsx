import React, { useEffect, useState } from "react";
import { getActiveTasks } from "../services/taskService"; // Adjust path as needed
import "../styles/Inbox.css";

export default function InboxTable() {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadInbox();
  }, []);

  const loadInbox = async () => {
    try {
      setLoading(true);
      const data = await getActiveTasks();
      setTasks(Array.isArray(data) ? data : data.tasks || []);
      console.log("Fetched inbox tasks:", data);
    } catch (err) {
      console.error("Failed to fetch inbox:", err);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div className="loader">Loading your inbox...</div>;

  return (
    <div className="inbox-card">
      <table className="custom-table">
        <thead>
          <tr>
            <th></th> 
            <th>Document type</th>
            <th>Reference Number</th>
            <th>Task date</th>
            <th>Created date</th>
            <th>Status</th>
            <th className="text-right">Actions</th>
          </tr>
        </thead>
        <tbody>
          {tasks.map((task) => (
            <tr key={task.id}>
              <td className="expand-cell"><span className="icon-plus">+</span></td>
              <td className="font-medium">{task.documentType || task.name}</td>
              <td>{task.referenceNumber}</td>
              <td>{new Date(task.taskDate).toLocaleDateString()}</td>
              <td>{new Date(task.createdDate).toLocaleDateString()}</td>
              <td>
                <span className={`status-badge ${task.status?.toLowerCase().replace(" ", "-")}`}>
                  {task.status}
                </span>
              </td>
              <td className="actions-cell">
                <button title="View" className="action-btn view"><i className="icon-mail"></i></button>
                <button title="User" className="action-btn user"><i className="icon-user"></i></button>
                <button title="Edit" className="action-btn edit"><i className="icon-edit"></i></button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}