import React from "react";
import { useOutletContext } from "react-router-dom";
import { useAppContext } from "../context/AppContext";
import "../styles/dashboard.css";

export default function HomeDashboard() {

  const outletContext = useOutletContext() || {};

  const {
  workflows = [],
  handleSelectWorkflow = () => {}
  } = outletContext;

  const { summary } = useAppContext();

  const getCounts = (key) => {
    return {
      today: summary?.[key]?.today ?? 0,
      total: summary?.[key]?.total ?? 0
    };
  };

  const cards = [
    {
      key: "inbox",
      label: "Inbox",
      icon: "bi-envelope-paper-fill",
      class: "inbox"
    },
    {
      key: "completed",
      label: "Completed",
      icon: "bi-check-square-fill",
      class: "completed"
    },
    {
      key: "draft",
      label: "Drafts",
      icon: "bi-pencil-square",
      class: "drafts"
    }
  ];

  return (
    <div className="dashboard-container">

      {/* Summary */}
      <div className="stats-grid">

        {cards.map((card) => {

          const counts = getCounts(card.key);

          return (
            <div
              key={card.key}
              className={`stat-card ${card.class}`}
            >

              <div className="stat-icon">
                <i className={`bi ${card.icon}`}></i>
              </div>

              <div className="stat-content">
                <h3>{counts.total}</h3>

                <p>{card.label}</p>

                <span className="today-tag">
                  +{counts.today} today
                </span>
              </div>

            </div>
          );
        })}

      </div>

      {/* Workflows */}
      <div className="section-header">
        <h2 className="white-text">
          Available Workflows
        </h2>
      </div>

      <div className="workflow-grid">

        {workflows.length > 0 ? (

          workflows.map((wf) => (

            <div
              key={wf.id}
              className="workflow-cards workflow-card"
              onClick={() => handleSelectWorkflow(wf)}
            >

              <div className="workflow-body">

                <div className="wf-icon-box">
                  <i className="bi bi-gear-fill"></i>
                </div>

                <h4>
                  {wf.text || wf.name}
                </h4>

                <p>
                  Click to start workflow
                </p>

              </div>

            </div>
          ))

        ) : (

          <div className="empty-state">

            <p>
              No workflows available
            </p>

          </div>

        )}

      </div>

    </div>
  );
  
}