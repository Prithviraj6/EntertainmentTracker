import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getAnime } from "../services/animeService";
import { addAnime } from "../services/userAnimeService";

import Navbar from "../components/Navbar";

export default function AnimeDetailsPage() {
  const { malId } =
    useParams();

  const [anime, setAnime] =
    useState(null);

  const [loading, setLoading] =
    useState(true);

  const [status, setStatus] =
    useState(1);

  useEffect(() => {
    loadAnime();
  }, []);

  async function loadAnime() {
    try {
      const result =
        await getAnime(malId);

      setAnime(result);
    }
    catch (error) {
      console.error(error);
    }
    finally {
      setLoading(false);
    }
  }

  if (loading) {
    return <h2>Loading...</h2>;
  }

  async function handleAdd() {
    try {
      const result =
        await addAnime(
          anime.id,
          status
        );

      alert(
        `${result.animeTitle} added successfully`
      );
    }
    catch (error) {
      alert(
        error.response?.data?.message ??
        "Failed to add anime"
      );
    }
  }

  return (
    <div>
      <Navbar />

      <h1>
        {anime.title}
      </h1>

      <p>
        Score:
        {anime.score}
      </p>

      <p>
        Episodes:
        {anime.episodes}
      </p>

      <p>
        {anime.synopsis}
      </p>

      <h3>Add To List</h3>

      <select
        value={status}
        onChange={(e) =>
          setStatus(
            Number(e.target.value)
          )
        }
      >
        <option value={1}>
          Plan To Watch
        </option>

        <option value={2}>
          Watching
        </option>

        <option value={3}>
          Completed
        </option>

        <option value={4}>
          On Hold
        </option>

        <option value={5}>
          Dropped
        </option>
      </select>

      <button
        onClick={handleAdd}
      >
        Add To List
      </button>
    </div>
  );
}