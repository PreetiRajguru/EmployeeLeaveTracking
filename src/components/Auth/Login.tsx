import React, {useState} from 'react';
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const navigate = useNavigate();
  const [data, setData] = useState({
    username: "",
    password: ""
  })
  
  const handleSubmit = async(event: any) => {
    event.preventDefault();
    
    const newData = {
      username: data.username,
      password: data.password,
    }

    try {
      const response = await axios.post("/api/Auth/login", newData);
      localStorage.setItem("token", JSON.stringify(response.data.token));
      localStorage.setItem("role", JSON.stringify(response.data.role));

      // if (response.data.role[0] == "Manager") {
      //   navigate(`/addemployee`);
      // }
      // if (response.data.role[0] == "employee") {
      //   navigate(`/applyforleaves`);
      // } else {
      //   alert();
      // }
      response.data.role[0] == "Manager" ? navigate(`/addemployee`) : navigate(`/applyforleaves`);
          
    }
    catch (error: any) {
      alert(error.response.data.message);
    }
    setData({
      username: "",
      password: ""
    })
  };

  
  const handleInputChange = (
    event: any
  ) => {
    const { name, value } = event.target;
    setData((prevState: any) => ({
      ...prevState,
      [name]: value,
    }));
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
            autoComplete="username"
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
            autoComplete="current-password"
            onChange={handleInputChange}
          />
          {/* <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label="Remember me"
          /> */}
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign In
          </Button>
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