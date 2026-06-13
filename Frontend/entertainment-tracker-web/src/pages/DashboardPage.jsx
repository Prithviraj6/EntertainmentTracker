import { useEffect, useState } from "react";

import Navbar from "../components/Navbar";

import { getStats } from "../services/userAnimeService";

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
    <div>

      <Navbar />

      <h1>Dashboard</h1>

      <p>
        Plan To Watch:
        {stats?.planToWatch}
      </p>

      <p>
        Watching:
        {stats?.watching}
      </p>

      <p>
        Completed:
        {stats?.completed}
      </p>

      <p>
        On Hold:
        {stats?.onHold}
      </p>

      <p>
        Dropped:
        {stats?.dropped}
      </p>
    </div>
  );
}