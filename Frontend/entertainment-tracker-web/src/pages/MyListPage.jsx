import { useState, useEffect } from "react";
import {
  getMyAnime,
  deleteAnime,
  updateStatus,
  updateScore,
  updateProgress
} from "../services/userAnimeService";
import { UserAnimeStatus } from "../constants/userAnimeStatus";

import Navbar from "../components/Navbar";

export default function MyListPage() {
  const [animeList, setAnimeList] =
    useState([]);

  const [statusFilter, setStatusFilter] =
    useState("");

  useEffect(() => {
    loadAnime();
  }, [statusFilter]);

  async function loadAnime() {
    const result =
      await getMyAnime(
        statusFilter || null
      );

    setAnimeList(result);
  }

  async function handleDelete(animeId) {
    const confirmed =
      window.confirm(
        "Delete this anime?"
      );

    if (!confirmed) {
      return;
    }

    await deleteAnime(
      animeId
    );

    await loadAnime();
  }

  async function handleStatusChange(animeId, status) {
    await updateStatus(
      animeId,
      Number(status)
    );

    await loadAnime();
  }

  async function handleScoreChange(animeId, score) {
    await updateScore(
      animeId,
      Number(score)
    );

    await loadAnime();
  }

  async function handleProgressChange(animeId, progress) {
    await updateProgress(
      animeId,
      Number(progress)
    );

    await loadAnime();
  }

  return (
    <div>
      <Navbar />

      <h1>My Anime List</h1>

      <div>
        <label>
          Filter:
        </label>

        <select
          value={statusFilter}
          onChange={(e) =>
            setStatusFilter(
              e.target.value
            )
          }
        >
          <option value="">
            All
          </option>

          <option value="1">
            Plan To Watch
          </option>

          <option value="2">
            Watching
          </option>

          <option value="3">
            Completed
          </option>

          <option value="4">
            On Hold
          </option>

          <option value="5">
            Dropped
          </option>
        </select>
      </div>

      {animeList.map(anime => (
        <div key={anime.animeId}>
          <h3>
            {anime.animeTitle}
          </h3>

          <div>
            Status :

            <select
              value={anime.status}
              onChange={(e) =>
                handleStatusChange(
                  anime.animeId,
                  e.target.value
                )
              }
            >
              <option value="1">
                Plan To Watch
              </option>

              <option value="2">
                Watching
              </option>

              <option value="3">
                Completed
              </option>

              <option value="4">
                On Hold
              </option>

              <option value="5">
                Dropped
              </option>
            </select>
          </div>

          <div>
            Progress :

            <input
              type="number"
              min="0"
              defaultValue={
                anime.progress
              }
            />

            <button
              onClick={(e) =>
                handleProgressChange(
                  anime.animeId,
                  e.target
                    .previousSibling
                    .value
                )
              }
            >
              Save Progress
            </button>
          </div>

          <div>
            Score :

            <input
              type="number"
              min="1"
              max="10"
              defaultValue={
                anime.userScore ?? ""
              }
            />

            <button
              onClick={(e) =>
                handleScoreChange(
                  anime.animeId,
                  e.target
                    .previousSibling
                    .value
                )
              }
            >
              Save Score
            </button>
          </div>

          <button
            onClick={() =>
              handleDelete(
                anime.animeId
              )
            }
          >
            Delete
          </button>
        </div>
      ))}
    </div>
  );
}