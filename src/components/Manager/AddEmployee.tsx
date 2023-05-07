import React, { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const managerId = localStorage.getItem("id");

export default function AddEmployee() {
  const navigate = useNavigate();
  const [data, setData] = useState({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
    password: "",
    phonenumber: "",
    managerId: "",
    designationId: "",
  });

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    const newData = {
      firstname: data.firstname,
      lastname: data.lastname,
      username: data.username,
      email: data.email,
      password: data.password,
      phonenumber: data.phonenumber,
      managerId: data.managerId,
      designationId: data.designationId,
    };

    try {
      axios.post("/api/Auth/register", newData).then((response) => {
        console.log(response.data);
        alert("Employee Added Successfully.");
      });
    } catch (error: any) {
      alert(error.response.data.message);
    }
    setData({
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      password: "",
      phonenumber: "",
      managerId: "",
      designationId: "",
    });
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    setData((prevState: any) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleBackButton = () => {
    navigate("/");
  };

  return (
    <Container component="main" maxWidth="sm">
      <Box
        sx={{
          boxShadow: 3,
          borderRadius: 2,
          px: 4,
          py: 6,
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h5">
          Create Employee
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="firstname"
            label="First Name"
            name="firstname"
            value={data.firstname}
            autoComplete="firstname"
            autoFocus
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="lastname"
            label="Last Name"
            name="lastname"
            value={data.lastname}
            autoComplete="lastname"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="User Name"
            name="username"
            value={data.username}
            autoComplete="username"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email Address"
            name="email"
            value={data.email}
            autoComplete="email"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            value={data.password}
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="phonenumber"
            value={data.phonenumber}
            label="Mobile Number"
            type="number"
            id="phonenumber"
            autoComplete="phonenumber"
            onChange={handleInputChange}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="managerId"
            value={managerId}
            label="Manager Id"
            id="managerId"
            autoComplete="managerId"
            onChange={handleInputChange}
          />

          <TextField
            margin="normal"
            required
            fullWidth
            name="designationId"
            value={data.designationId}
            label="Designation Id"
            id="designationId"
            autoComplete="designationId"
            onChange={handleInputChange}
          />

          <br></br>
          <br></br>
          <div>
            <Button
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              onClick={() => navigate("/viewemployees")}
            >
              Back
            </Button>

            <Button
              type="submit"
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
              style={{ marginLeft: "300px" }}
            >
              Create
            </Button>
          </div>
        </Box>
      </Box>
    </Container>
  );
}
