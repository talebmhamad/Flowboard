import { useState } from "react";
import Select from "react-select";
import "../styles/TaskFilters.css";

export default function TaskFilters({
  formState,
  handleInputChange,
  handleSearch,
  handleClear,
  docTypeOptions,
  statusOptions,
  customSelectStyles,
  IsCompletedTable, 
  IsDraftTable
}) {
  const [isCollapsed, setIsCollapsed] = useState(false);
  const toggleCollapse = () => {
    setIsCollapsed(prev => !prev);
  };

  return (
    <div className="filter-card">
      
      {/* HEADER */}
      <div className="filter-header" onClick={toggleCollapse} style={{ cursor: "pointer" }}>
        Search 
        <span className="collapse-icon">
          {isCollapsed ? "▼" : "▲"}
        </span>
      </div>

      {/* BODY (collapsible) */}
      {!isCollapsed && (
        <div className="filter-body">
          

          <div className="filter-row">

          {IsCompletedTable && (
            <div className="filter-group">
              <label>Reference Number</label>
              <input
                type="text"
                name="refNumber"
                value={formState.refNumber}
                onChange={handleInputChange}
              />
            </div>
          )}
            <div className="filter-group">
              <label>From date</label>
              <input
                type="date"
                name="fromDate"
                value={formState.fromDate}
                onChange={handleInputChange}
              />
            </div>

            <div className="filter-group">
              <label>To date</label>
              <input
                type="date"
                name="toDate"
                value={formState.toDate}
                onChange={handleInputChange}
              />
            </div>
          {IsCompletedTable && (

            <div className="filter-group">
              <label>Status</label>
                <Select
                 name="status"
                 options={statusOptions}
                 styles={customSelectStyles}
                 value={formState.status}
                 onChange={(selected) =>
                 handleInputChange({
                 target: { name: "status", value: selected }
                })
                }
             placeholder="Select Status"
            />
            </div>
          )}
          </div>

          <div className="filter-row second-row">
            <div className="filter-group">
              <label>Document type</label>
<Select
  name="docType"
  options={docTypeOptions}
  styles={customSelectStyles}
  value={formState.docType}
  onChange={(selected) =>
    handleInputChange({
      target: { name: "docType", value: selected }
    })
  }
  placeholder="Select Type"
/>
            </div>

           {IsCompletedTable && !IsDraftTable && (
    <div className="checkbox-group">
      <label>
        <input type="checkbox" name="read" checked={formState.read} onChange={handleInputChange}/> Read
      </label>
      <label>
        <input type="checkbox" name="locked" checked={formState.locked} onChange={handleInputChange}/> Locked
      </label>
      <label>
        <input type="checkbox" name="assigned" checked={formState.assigned} onChange={handleInputChange}/> Assigned
      </label>
      <label>
        <input type="checkbox" name="overdue" checked={formState.overdue} onChange={handleInputChange}/> Overdue
      </label>
    </div>
          )}
         
          </div>
          <div className="filter-actions">
            <button className="btn-search" onClick={handleSearch}>Search</button>
            <button className="btn-clear" onClick={handleClear}>Clear</button>
          </div>
        </div>
      )}
    </div>
  );
}