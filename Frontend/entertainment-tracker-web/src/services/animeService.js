import apiClient from "../api/apiClient";

export async function searchAnime(query) {
    const response =
        await apiClient.get(
            `/anime/search?q=${encodeURIComponent(query)}`
        );

    return response.data;
}

export async function getAnime(malId) {
  const response =
    await apiClient.get(
      `/anime/${malId}`
    );

  return response.data;
}