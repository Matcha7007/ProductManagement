import { useEffect, useState } from "react";
import axios from "axios";
import {
	Container,
	Table,
	Button,
	Form,
	Row,
	Col,
	Pagination,
	Navbar,
	Nav,
	Modal,
	Alert,
} from "react-bootstrap";

type Product = {
	id: number;
	name: string;
	description: string;
	price: number;
};

type Props = {
	token: string;
	setToken: (token: string | null) => void;
};

export default function ProductPage({ token, setToken }: Props) {
	const [products, setProducts] = useState<Product[]>([]);
	const [page, setPage] = useState(1);
	const [pageSize] = useState(10);
	const [totalPages, setTotalPages] = useState(1);
	const [filterName, setFilterName] = useState("");
	const [filterMinPrice, setFilterMinPrice] = useState<number | undefined>();
	const [filterMaxPrice, setFilterMaxPrice] = useState<number | undefined>();
	const [orderBy, setOrderBy] = useState("name");
	const [isAscending, setIsAscending] = useState(true);
	const [error, setError] = useState("");
	const [success, setSuccess] = useState("");
	const [showModal, setShowModal] = useState(false);
	const [isEditing, setIsEditing] = useState(false);
	const [formProduct, setFormProduct] = useState<Partial<Product>>({});

	const fetchProducts = async () => {
		try {
			const res = await axios.post(
				"https://localhost:7118/api/v1/Product/search",
				{
					pageNumber: page,
					pageSize,
					orderBy,
					isAscending,
					filter: {
						filterName,
						filterMinPrice,
						filterMaxPrice,
					},
				},
				{
					headers: {
						Authorization: `Bearer ${token}`,
					},
				}
			);
			setProducts(res.data.data.listData);
			setTotalPages(res.data.data.totalPages);
		} catch (err) {
			console.error("Failed to fetch products", err);
		}
	};

	useEffect(() => {
		fetchProducts();
	}, [page, orderBy, isAscending]);

	const handleLogout = () => {
		setToken(null);
	};

	const handleSort = (field: string) => {
		if (orderBy === field) {
			setIsAscending(!isAscending);
		} else {
			setOrderBy(field);
			setIsAscending(true);
		}
	};

	const openAddModal = () => {
		setFormProduct({});
		setIsEditing(false);
		setShowModal(true);
	};

	const openEditModal = (product: Product) => {
		setFormProduct(product);
		setIsEditing(true);
		setShowModal(true);
	};

	const handleDelete = async (id: number) => {
		// eslint-disable-next-line no-restricted-globals
		if (!confirm("Are you sure to delete this product?")) return;
		try {
			const res = await axios.post(
				"https://localhost:7118/api/v1/Product/delete",
				{
					id,
				},
				{
					headers: {
						Authorization: `Bearer ${token}`,
					},
				}
			);
			if (res.data.isValid) {
				setSuccess(res.data.message);
				fetchProducts();
			} else {
				setError(res.data.message);
			}
		} catch (err) {
			console.error("Delete failed", err);
		}
	};

	const handleFormSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		try {
			if (isEditing && formProduct.id != null) {
				const res = await axios.post(
					"https://localhost:7118/api/v1/Product/update",
					formProduct,
					{
						headers: {
							Authorization: `Bearer ${token}`,
						},
					}
				);
				if (res.data.isValid) {
					setSuccess(res.data.message);
				} else {
					setError(res.data.message);
				}
			} else {
				const res = await axios.post(
					"https://localhost:7118/api/v1/Product/create",
					formProduct,
					{
						headers: { Authorization: `Bearer ${token}` },
					}
				);
				if (res.data.isValid) {
					setSuccess(res.data.message);
				} else {
					setError(res.data.message);
				}
			}
			setShowModal(false);
			fetchProducts();
		} catch (err) {
			console.error("Save failed", err);
		}
	};

	return (
		<>
			<Navbar bg="light" expand="lg" className="mb-3">
				<Container>
					<Navbar.Brand>Simple App Product Management</Navbar.Brand>
					<Nav>
						<Button variant="outline-danger" onClick={handleLogout}>
							Logout
						</Button>
					</Nav>
				</Container>
			</Navbar>
			{error && <Alert variant="danger">{error}</Alert>}
			{success && <Alert variant="success">{success}</Alert>}
			<Container>
				<Form
					className="mb-3"
					onSubmit={(e) => {
						e.preventDefault();
						fetchProducts();
					}}>
					<Row>
						<Col md={3}>
							<label htmlFor="">Filter by Name</label>
							<Form.Control
								type="text"
								value={filterName}
								onChange={(e) => setFilterName(e.target.value)}
							/>
						</Col>
						<Col md={2}>
							<label htmlFor="">Min Price</label>
							<Form.Control
								type="number"
								value={filterMinPrice ?? 0}
								min={0}
								onChange={(e) =>
									setFilterMinPrice(
										e.target.value ? parseFloat(e.target.value) : undefined
									)
								}
							/>
						</Col>
						<Col md={2}>
							<label htmlFor="">Max Price</label>
							<Form.Control
								type="number"
								value={filterMaxPrice ?? 0}
								min={0}
								onChange={(e) =>
									setFilterMaxPrice(
										e.target.value ? parseFloat(e.target.value) : undefined
									)
								}
							/>
						</Col>
						<Col style={{ paddingTop: "24px" }}>
							<Button type="submit" onClick={fetchProducts}>
								Search
							</Button>
						</Col>
						<Col style={{ paddingTop: "24px" }} className="text-end">
							<Button variant="success" onClick={openAddModal}>
								Add Product
							</Button>
						</Col>
					</Row>
				</Form>

				<Table striped bordered hover>
					<thead>
						<tr>
							<th
								onClick={() => handleSort("name")}
								style={{ cursor: "pointer" }}>
								Name
							</th>
							<th>Description</th>
							<th
								onClick={() => handleSort("price")}
								style={{ cursor: "pointer" }}>
								Price
							</th>
							<th>Action</th>
						</tr>
					</thead>
					<tbody>
						{products.map((p) => (
							<tr key={p.id}>
								<td>{p.name}</td>
								<td>{p.description}</td>
								<td>{p.price}</td>
								<td>
									<Button
										variant="warning"
										size="sm"
										className="me-2"
										onClick={() => openEditModal(p)}>
										Edit
									</Button>
									<Button
										variant="danger"
										size="sm"
										onClick={() => handleDelete(p.id)}>
										Delete
									</Button>
								</td>
							</tr>
						))}
					</tbody>
				</Table>

				<Pagination>
					<Pagination.Prev
						disabled={page === 1}
						onClick={() => setPage(page - 1)}
					/>
					{[...Array(totalPages)].map((_, i) => (
						<Pagination.Item
							key={i}
							active={i + 1 === page}
							onClick={() => setPage(i + 1)}>
							{i + 1}
						</Pagination.Item>
					))}
					<Pagination.Next
						disabled={page === totalPages}
						onClick={() => setPage(page + 1)}
					/>
				</Pagination>
			</Container>

			<Modal show={showModal} onHide={() => setShowModal(false)}>
				<Modal.Header closeButton>
					<Modal.Title>
						{isEditing ? "Edit Product" : "Add Product"}
					</Modal.Title>
				</Modal.Header>
				<Form onSubmit={handleFormSubmit}>
					<Modal.Body>
						<Form.Group className="mb-3">
							<Form.Label>Name</Form.Label>
							<Form.Control
								type="text"
								value={formProduct.name ?? ""}
								onChange={(e) =>
									setFormProduct({ ...formProduct, name: e.target.value })
								}
								required
							/>
						</Form.Group>
						<Form.Group className="mb-3">
							<Form.Label>Description</Form.Label>
							<Form.Control
								as="textarea"
								rows={3}
								value={formProduct.description ?? ""}
								onChange={(e) =>
									setFormProduct({
										...formProduct,
										description: e.target.value,
									})
								}
							/>
						</Form.Group>
						<Form.Group className="mb-3">
							<Form.Label>Price</Form.Label>
							<Form.Control
								type="number"
								value={formProduct.price ?? ""}
								onChange={(e) =>
									setFormProduct({
										...formProduct,
										price: parseFloat(e.target.value),
									})
								}
								required
							/>
						</Form.Group>
					</Modal.Body>
					<Modal.Footer>
						<Button variant="secondary" onClick={() => setShowModal(false)}>
							Cancel
						</Button>
						<Button type="submit" variant="primary">
							Save
						</Button>
					</Modal.Footer>
				</Form>
			</Modal>
		</>
	);
}
