import { Link, useNavigate } from "react-router-dom";

export default function Navbar() {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem(
            "accessToken"
        );

        localStorage.removeItem(
            "refreshToken"
        );

        navigate("/")
    };

    return (
        <nav>
            <Link to="/dashboard">
                Dashboard
            </Link>

            {" | "}

            <Link to="/search">
                Search
            </Link>

            {" | "}

            <Link to="/my-list">
                My List
            </Link>

            {" | "}

            <button onClick={handleLogout}>
                Logout
            </button>
        </nav>
    );
}