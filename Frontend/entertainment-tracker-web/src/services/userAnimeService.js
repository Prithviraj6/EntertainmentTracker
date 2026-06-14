import apiClient from "../api/apiClient";

export async function getStats() {
    const response =
        await apiClient.get(
            "/user-anime/stats"
        );

    return response.data;
}

export async function addAnime(animeId, status) {
    const response = await apiClient.post(
        "/user-anime",
        {
            animeId,
            status
        }
    );

    return response.data;
}

export async function getMyAnime(status = null) {
    const url =
        status
            ? `/user-anime?status=${status}`
            : "/user-anime";

    const response =
        await apiClient.get(
            url
        );

    return response.data;
}

export async function deleteAnime(animeId) {
    await apiClient.delete(
        `/user-anime/${animeId}`);
}

export async function updateStatus(animeId, status) {
    await apiClient.patch(
        `/user-anime/${animeId}/status`,
        {
            status
        }
    );
}

export async function updateScore(animeId, score) {
    await apiClient.patch(
        `/user-anime/${animeId}/score`,
        {
            score
        }
    );
}

export async function updateProgress(animeId, progress) {
    await apiClient.patch(
        `/user-anime/${animeId}/progress`,
        {
            progress
        }
    );
}