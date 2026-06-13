import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getAnime } from "../services/animeService";

import Navbar from "../components/Navbar";

export default function AnimeDetailsPage() {
  const { malId } =
    useParams();
  const [anime, setAnime] =
    useState(null);

  const [loading, setLoading] =
    useState(true);

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
    </div>
  );
}