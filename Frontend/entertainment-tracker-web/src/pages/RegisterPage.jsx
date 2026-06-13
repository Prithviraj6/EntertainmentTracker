import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../services/authService";

export default function RegisterPage() {
  const navigate = useNavigate();

  const [displayName, setDisplayName] =
    useState("");

  const [handle, setHandle] =
    useState("");

  const [email, setEmail] =
    useState("");

  const [password, setPassword] =
    useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await register({
        displayName,
        handle,
        email,
        password,
      });

      navigate("/");
    } catch (error) {
      alert(
        error.response?.data?.message ??
        "Registration failed"
      );
    }
  };

  return (
    <div>
      <h1>Register</h1>

      <form onSubmit={handleSubmit}>
        <input
          placeholder="Display Name"
          value={displayName}
          onChange={(e) =>
            setDisplayName(
              e.target.value
            )
          }
        />

        <input
          placeholder="Handle"
          value={handle}
          onChange={(e) =>
            setHandle(
              e.target.value
            )
          }
        />

        <input
          placeholder="Email"
          value={email}
          onChange={(e) =>
            setEmail(
              e.target.value
            )
          }
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) =>
            setPassword(
              e.target.value
            )
          }
        />

        <button type="submit">
          Register
        </button>
      </form>
    </div>
  )
}