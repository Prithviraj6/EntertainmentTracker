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