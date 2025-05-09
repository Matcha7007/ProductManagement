// File: src/pages/RegisterPage.tsx
import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Form, Button, Container, Alert } from "react-bootstrap";
import FloatingLabel from "react-bootstrap/FloatingLabel";

export default function RegisterPage() {
	const [username, setUsername] = useState("");
	const [fullName, setFullName] = useState("");
	const [email, setEmail] = useState("");
	const [phone, setPhone] = useState("");
	const [password, setPassword] = useState("");
	const [error, setError] = useState("");
	const [success, setSuccess] = useState("");
	const navigate = useNavigate();

	const handleRegister = async (e: React.FormEvent) => {
		e.preventDefault();
		try {
			const res = await axios.post(
				`${process.env.REACT_APP_API_URL}/Auth/register`,
				{
					userName: username,
					fullName,
					email,
					phone,
					password,
				}
			);
			if (res.data.isValid) {
				setSuccess(res.data.message);
				setUsername("");
				setFullName("");
				setEmail("");
				setPhone("");
				setPassword("");
				setTimeout(() => navigate("/login"), 2000);
			} else {
				setError(res.data.message);
			}
		} catch (err) {
			setError("Registrasi gagal. Coba lagi.");
		}
	};

	return (
		<Container className="d-flex justify-content-center align-items-center min-vh-100 flex-column">
			<h3 className="mb-3">Product Management App</h3>
			<Form
				onSubmit={handleRegister}
				className="p-4 border rounded shadow w-100"
				style={{ maxWidth: 400 }}>
				<h2 className="mb-3">Register</h2>
				{error && <Alert variant="danger">{error}</Alert>}
				{success && <Alert variant="success">{success}</Alert>}

				<Form.Group className="mb-3" controlId="registerUserName">
					<FloatingLabel controlId="registerUserName" label="Username">
						<Form.Control
							type="text"
							placeholder="Username"
							value={username}
							onChange={(e) => setUsername(e.target.value)}
							required
						/>
					</FloatingLabel>
				</Form.Group>

				<Form.Group className="mb-3" controlId="registerFullName">
					<FloatingLabel controlId="registerFullName" label="Full Name">
						<Form.Control
							type="text"
							placeholder="Full Name"
							value={fullName}
							onChange={(e) => setFullName(e.target.value)}
							required
						/>
					</FloatingLabel>
				</Form.Group>

				<Form.Group className="mb-3" controlId="registerEmail">
					<FloatingLabel controlId="registerEmail" label="Email">
						<Form.Control
							type="email"
							placeholder="Email"
							value={email}
							onChange={(e) => setEmail(e.target.value)}
							required
						/>
					</FloatingLabel>
				</Form.Group>

				<Form.Group className="mb-3" controlId="registerPhone">
					<FloatingLabel controlId="registerPhone" label="Phone">
						<Form.Control
							type="text"
							placeholder="Phone"
							value={phone}
							onChange={(e) => {
								const onlyNums = e.target.value.replace(/[^0-9]/g, "");
								setPhone(onlyNums);
							}}
							required
						/>
					</FloatingLabel>
				</Form.Group>

				<Form.Group className="mb-3" controlId="registerPassword">
					<FloatingLabel controlId="registerPassword" label="Password">
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
					Register
				</Button>
				<p className="mt-3">
					Already have an account? <a href="/login">Login</a>
				</p>
			</Form>
		</Container>
	);
}
