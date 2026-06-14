import { useEffect, useState } from "react";

import Navbar from "../components/Navbar";

import { getStats } from "../services/userAnimeService";
import PageContainer from "../components/PageContainer";
import "./DashboardPage.css";

export default function DashboardPage() {
  const [stats, setStats] =
    useState(null);

  const [loading, setLoading] =
    useState(true);

  useEffect(() => {
    loadStats();
  }, []);

  async function loadStats() {
    try {
      const result = await getStats();

      setStats(result);
    }
    catch (error) {
      console.error(error);
    }
    finally {
      setLoading(false);
    }
  }

  if (loading) {
    return <h2>Loading...</h2>
  }

  return (
    <>
      <Navbar />

      <PageContainer>

        <h1>Dashboard</h1>

        <div className="stats-grid">

          <div className="stat-card">
            <p>Plan To Watch</p>
            <h2>{stats?.planToWatch}</h2>
          </div>

          <div className="stat-card">
            <p>Watching</p>
            <h2>{stats?.watching}</h2>
          </div>

          <div className="stat-card">
            <p>Completed</p>
            <h2>{stats?.completed}</h2>
          </div>

          <div className="stat-card">
            <p>On Hold</p>
            <h2>{stats?.onHold}</h2>
          </div>

          <div className="stat-card">
            <p>Dropped</p>
            <h2>{stats?.dropped}</h2>
          </div>

        </div>

      </PageContainer>
    </>
  );
}