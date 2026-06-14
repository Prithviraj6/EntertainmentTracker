import { useState } from "react";
import { searchAnime } from "../services/animeService";
import { useNavigate } from "react-router-dom";

import Navbar from "../components/Navbar";
import PageContainer from "../components/PageContainer";
import "../components/AnimeCard.css";
import "./SearchPage.css";

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
    <PageContainer>
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

        <div className="search-results">
          {results.map(anime => (
            <div
              className="anime-card"
              key={anime.malId}
              onClick={() =>
                navigate(
                  `/anime/${anime.malId}`
                )
              }
            >
              <img
                src={anime.imageUrl}
                alt={anime.title}
                width="100"
              />

              <div>
                <h3>{anime.title}</h3>

                <p>
                  Score: {anime.score}
                </p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </PageContainer>
  );
}
