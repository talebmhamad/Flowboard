import './config';

import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.jsx';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { BrowserRouter } from "react-router-dom";
import "formiojs/dist/formio.full.min.css";

import { AuthProvider } from "./context/AuthContext";

window.Common = {
  mask: function () {

    if (document.getElementById('global-formio-loader')) {
      return;
    }

    const loader = document.createElement('div');

    loader.id = 'global-formio-loader';

    loader.innerHTML = `
      <div
        style="
          position:fixed;
          inset:0;
          background:rgba(255,255,255,.5);
          z-index:999999;
          display:flex;
          flex-direction:column;
          justify-content:center;
          align-items:center;
          gap:10px;
        "
      >
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>

        <span class="text-muted small">
          Loading...
        </span>
      </div>
    `;

    document.body.appendChild(loader);
  },

  unmask: function () {
    const loader = document.getElementById('global-formio-loader');

    if (loader) {
      loader.remove();
    }
  }
};

ReactDOM.createRoot(document.getElementById("root")).render(
  <BrowserRouter>
    <AuthProvider>
      <App />
    </AuthProvider>
  </BrowserRouter>
);