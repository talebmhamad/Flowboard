import { Routes, Route, Navigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

import { AppProvider } from "./context/AppContext";

// Pages
import Login from "./pages/Login";
import UserDashboard from "./pages/dashboard/UserDashboard";

// Components
import ProtectedRoute from "./components/ProtectedRoute";

// Child pages
import HomeDashboard from "./components/HomeDashboard";
import InboxTable from "./components/InboxTable";
import CompleteTable from "./components/CompletedTable";
import DraftTable from "./components/DraftTable";

function App() {
  return (
    <AppProvider>
      <Routes>

        {/* PUBLIC */}
        <Route path="/login" element={<Login />} />

        {/* PROTECTED + DASHBOARD */}
        <Route
          path="/dashboard"
          element={
            <ProtectedRoute>
              <UserDashboard />
            </ProtectedRoute>
          }
        >
          {/* Default */}
          <Route index element={<Navigate to="home" replace />} />

          {/* Main pages */}
          <Route path="home" element={<HomeDashboard />} />
          <Route path="inbox" element={<InboxTable />} />
          <Route path="completed" element={<CompleteTable />} />
          <Route path="draft" element={<DraftTable />} />

          {/* 🔥 NEW: Unified Form Route */}
          <Route path="form/:mode/:id?" element={<div />} />

        </Route>

        {/* Redirects */}
        <Route path="/" element={<Navigate to="/dashboard" replace />} />
        <Route path="*" element={<Navigate to="/dashboard" replace />} />

      </Routes>

      <ToastContainer
        position="top-right"
        autoClose={3000}
        newestOnTop
        closeOnClick
        pauseOnHover
        draggable
        theme="colored"
        limit={3}
      />
    </AppProvider>
  );
}

export default App;