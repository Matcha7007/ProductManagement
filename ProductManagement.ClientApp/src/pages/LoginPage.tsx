import { useState } from "react";
import axios from "axios";
import { useNavigate, Link } from "react-router-dom";
import { Form, Button, Container, Alert } from "react-bootstrap";
import FloatingLabel from "react-bootstrap/FloatingLabel";

type Props = {
	setToken: (token: string | null) => void;
};

export default function LoginPage({ setToken }: Props) {
	const [username, setUsername] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState("");
	const [success, setSuccess] = useState("");
	const navigate = useNavigate();

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		try {
			const res = await axios.post("https://localhost:7118/api/v1/Auth/login", {
				username: username,
				password,
			});
			if (res.data.isValid) {
				setToken(res.data.bearerToken);
				setSuccess(res.data.message);
				navigate("/products");
				setUsername("");
				setPassword("");
				setSuccess("");
			} else {
				setError(res.data.message);
			}
		} catch (err) {
			setError("Login gagal. Cek UserName atau password.");
		}
	};

	return (
		<Container className="d-flex justify-content-center align-items-center min-vh-100">
			<Form
				onSubmit={handleSubmit}
				className="p-4 border rounded shadow w-100"
				style={{ maxWidth: 400 }}>
				<h2 className="mb-3">Login</h2>
				{error && <Alert variant="danger">{error}</Alert>}
				{success && <Alert variant="success">{success}</Alert>}
				<Form.Group className="mb-3" controlId="userName">
					<FloatingLabel controlId="userName" label="Username" className="mb-3">
						<Form.Control
							type="text"
							placeholder="Username"
							value={username}
							onChange={(e) => setUsername(e.target.value)}
							required
						/>
					</FloatingLabel>
				</Form.Group>
				<Form.Group className="mb-3" controlId="password">
					<FloatingLabel controlId="password" label="Password" className="mb-3">
						<Form.Control
							type="password"
							placeholder="Password"
							value={password}
							onChange={(e) => setPassword(e.target.value)}
							required
						/>
					</FloatingLabel>
				</Form.Group>
				<Button type="submit" variant="primary" className="w-100">
					Login
				</Button>
				<p className="mt-3">
					Don't have an account? <Link to="/register">Register</Link>
				</p>
			</Form>
		</Container>
	);
}
