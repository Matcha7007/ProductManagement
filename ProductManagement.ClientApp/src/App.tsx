import { Routes, Route, Navigate } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import ProductPage from "./pages/ProductPage";
import RegisterPage from "./pages/RegisterPage";
import { useEffect, useState, useRef } from "react";

function App() {
	const [token, setToken] = useState<string | null>(() =>
		localStorage.getItem("token")
	);
	const logoutTimerRef = useRef<NodeJS.Timeout | null>(null);

	useEffect(() => {
		if (token) {
			localStorage.setItem("token", token);

			if (logoutTimerRef.current) clearTimeout(logoutTimerRef.current);

			logoutTimerRef.current = setTimeout(() => {
				alert("Your session has expired. Please log in again.");
				setToken(null);
			}, 5 * 60 * 1000); // 5 menit
		} else {
			localStorage.removeItem("token");

			if (logoutTimerRef.current) {
				clearTimeout(logoutTimerRef.current);
				logoutTimerRef.current = null;
			}
		}
	}, [token]);

	return (
		<Routes>
			<Route
				path="/"
				element={<Navigate to={token ? "/products" : "/login"} />}
			/>
			<Route path="/login" element={<LoginPage setToken={setToken} />} />
			<Route path="/register" element={<RegisterPage />} />
			<Route
				path="/products"
				element={
					token ? (
						<ProductPage setToken={setToken} token={token} />
					) : (
						<Navigate to="/login" replace />
					)
				}
			/>
		</Routes>
	);
}

export default App;
