import React, { useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import Snackbar from '@mui/material/Snackbar';

export default function Login() {
  const navigate = useNavigate();
  const [data, setData] = useState({
    username: "",
    password: "",
  });
  const [unAuthorized, setUnAuthorized] = useState(false);

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    const newData = {
      username: data.username,
      password: data.password,
    };

    try {
      const response = await axios.post("/api/Auth/login", newData);
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("role", response.data.role);

      if (response.data.role == "Manager") {
        alert("Login Successfull");
        navigate(`/viewemployees`);
      } else if (response.data.role == "Employee") {
        alert("Login Successfull");
        navigate(`/applyforleaves`);
      }

      window.location.reload();
      // response.data.role == "Manager" ? navigate(`/viewemployees`) : navigate(`/applyforleaves`);

      if(response.status != 200){
        setUnAuthorized(true);
      }
    } catch (error: any) {
      console.log(error);
    }
    setData({
      username: "",
      password: "",
    });
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    setData((prevState: any) => ({
      ...prevState,
      [name]: value,
    }));
    setUnAuthorized(false);
  };

  const handleSnack = () => {
    setUnAuthorized(false)
  }

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
          Sign in
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            value={data.username}
            autoComplete="off"
            autoFocus
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
            autoComplete="off"
            onChange={handleInputChange}
          />
          {unAuthorized && <span style={{ color: "red", fontSize: '12px' }}>
            Please enter valid details :)
          </span>}
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign In
          </Button>
          {
            unAuthorized && <Snackbar
            open={unAuthorized}
            autoHideDuration={4000}
            message="Login Attempt Failed"
            action={<IconButton
              size="small"
              aria-label="close"
              color="inherit"
              onClick={handleSnack}
            >
              <CloseIcon fontSize="small" />
            </IconButton>}
          />
          }
          <Grid container>
            <Grid item>
              <Link href="/register" variant="body2">
                {"Don't have an account? Sign Up"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
