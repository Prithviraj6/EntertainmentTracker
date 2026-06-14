import apiClient from "../api/apiClient";

export async function getStats() {
    const response =
        await apiClient.get(
            "/user-anime/stats",
            {
                headers: {
                    Authorization:
                        `Bearer ${localStorage.getItem(
                            "accessToken"
                        )}`,
                },
            }
        );

    return response.data;
}

export async function addAnime(animeId,status) {
    const response = await apiClient.post(
        "/user-anime",
        {
            animeId,
            status
        },
        {
            headers: {
                Authorization:
                `Bearer ${localStorage.getItem(
                    "accessToken"
                )}`
            }
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
            url,
            {
                headers: {
                    Authorization:
                        `Bearer ${localStorage.getItem(
                            "accessToken"
                        )}`
                }
            }
        );

    return response.data;
}

export async function deleteAnime(animeId) {
    await apiClient.delete(
        `/user-anime/${animeId}`,
        {
            headers: {
                Authorization:
                    `Bearer ${localStorage.getItem(
                        "accessToken"
                    )}`
            }
        });
}

export async function updateStatus(animeId,status) {
    await apiClient.patch(
        `/user-anime/${animeId}/status`,
        {
            status
        },
        {
            headers: {
                Authorization:
                    `Bearer ${localStorage.getItem(
                        "accessToken"
                    )}`
            }
        }
    );
}

export async function updateScore(animeId,score) {
    await apiClient.patch(
        `/user-anime/${animeId}/score`,
        {
            score
        },
        {
            headers: {
                Authorization:
                    `Bearer ${localStorage.getItem(
                        "accessToken"
                    )}`
            }
        }
    );
}

export async function updateProgress(animeId,progress) {
    await apiClient.patch(
        `/user-anime/${animeId}/progress`,
        {
            progress
        },
        {
            headers: {
                Authorization:
                    `Bearer ${localStorage.getItem(
                        "accessToken"
                    )}`
            }
        }
    );
}