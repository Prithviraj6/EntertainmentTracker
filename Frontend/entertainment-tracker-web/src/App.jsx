import { BrowserRouter, Routes, Route } from "react-router-dom";

import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import DashboardPage from "./pages/DashboardPage";
import SearchPage from "./pages/SearchPage";
import AnimeDetailsPage from "./pages/AnimeDetailsPage";
import MyListPage from "./pages/MyListPage";

import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/dashboard" element={<ProtectedRoute><DashboardPage /></ProtectedRoute>} />
        <Route path="/search" element={<ProtectedRoute><SearchPage /></ProtectedRoute>} />
        <Route path="/anime/:malId" element={<ProtectedRoute><AnimeDetailsPage /></ProtectedRoute>} />
        <Route path="/my-list" element={<ProtectedRoute><MyListPage /></ProtectedRoute>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
