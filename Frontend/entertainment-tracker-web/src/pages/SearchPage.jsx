import { useState } from "react";
import { searchAnime } from "../services/animeService";
import { useNavigate } from "react-router-dom";

import Navbar from "../components/Navbar";

export default function SearchPage() {
  const navigate =
    useNavigate();

  const [query, setQuery] =
    useState("");

  const [results, setResults] =
    useState([]);

  const [loading, setLoading] =
    useState(false);

  async function handleSearch() {
    if (!query.trim()) {
      return;
    }

    try {
      setLoading(true);

      const data =
        await searchAnime(query);
      console.log(data);
      setResults(data);
    }
    catch (error) {
      console.error(error);
    }
    finally {
      setLoading(false);
    }
  }

  return (
    <div>
      <Navbar />

      <h1>Search Anime</h1>

      <input
        type="text"
        placeholder="Search anime..."
        value={query}
        onChange={(e) =>
          setQuery(e.target.value)
        }
      />

      <button
        onClick={handleSearch}
      >
        Search
      </button>

      {loading && (
        <p>Loading...</p>
      )}

      {results.map(anime => (
        <div
          key={anime.malId}
          onClick={() =>
            navigate(
              `/anime/${anime.malId}`
            )
          }
        >
          <h3>{anime.title}</h3>

          <p>
            Score: {anime.score}
          </p>
        </div>
      ))}
    </div>
  );
}
