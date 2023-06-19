import { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import { Card, CardActionArea, CardMedia, CardContent } from "@mui/material";
import 'react-toastify/dist/ReactToastify.css';

import swal from 'sweetalert';
import useHttp from "../../config/https";

export default function Login() {

  const navigate = useNavigate();
  const [data, setData] = useState({
    username: "",
    password: "",
    errors: {
      username: "",
      password: "",
    },
  });
  const [unAuthorized, setUnAuthorized] = useState(false);

  const [isLogin, setIsLogin] = useState(false);
  const [isManager, setIsManager] = useState(false);
  const [isEmployee, setIsEmployee] = useState(false);

  const getToken = () => {
    setIsLogin(
      localStorage.getItem("token") && localStorage.getItem("token") !== ""
        ? true
        : false
    );
    setIsManager(
      localStorage.getItem("role") && localStorage.getItem("role") === "Manager"
        ? true
        : false
    );
    setIsEmployee(
      localStorage.getItem("role") && localStorage.getItem("role") === "Employee"
        ? true
        : false
    );
  };

  if(isLogin && isManager){
    navigate(`/viewemployees`);
  } else if(isLogin && isEmployee){
    navigate(`/leavedetails`);
  }
  const {axiosInstance, loading} = useHttp();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    let errors = { ...data.errors };
    let hasErrors = false;

    if (!data.username) {
      errors.username = "Username is required";
      hasErrors = true;
    }

    if (!data.password) {
      errors.password = "Password is required";
      hasErrors = true;
    }

    setData((prevState: any) => ({
      ...prevState,
      errors: errors,
    }));

    if (hasErrors) {
      return;
    }

    const newData = {
      username: data.username,
      password: data.password,
    };

    try {
      const response = await axiosInstance.post("/api/Auth/login", newData);
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("role", response.data.role);
      localStorage.setItem("id", response.data.id);
      localStorage.setItem("refreshToken", response.data.refreshToken);

      if (response.data.role === "Manager") {
        swal("Login Successfull !");
        navigate(`/viewemployees`);
        window.location.reload();
      } else if (response.data.role === "Employee") {
        swal("Login Successfull");
        navigate(`/leavedetails`);
        window.location.reload();
      }

      // setTimeout(() => {
      //   window.location.reload();
      // }, 1500); // Set the duration in milliseconds (e.g., 2000ms = 2 seconds)

      if (response.status !== 200) {
        setUnAuthorized(true);
      }
    } catch (error: any) {
      console.log(error);
      setUnAuthorized(true);
    }
    setData({
      username: "",
      password: "",
      errors: {
        username: "",
        password: "",
      },
    });
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let errors = { ...data.errors };

    switch (name) {
      case "username":
        errors.username = value ? "" : "Username is required";
        break;
      case "password":
        errors.password = value ? "" : "Password is required";
        break;
      default:
        break;
    }

    setData((prevState: any) => ({
      ...prevState,
      [name]: value,
      errors: errors,
    }));
    setUnAuthorized(false);
  };

  const handleSnack = () => {
    setUnAuthorized(false);
  };
  
  useEffect(() => {
    getToken();
  }, []);


  return (
    <>
      <div
        style={{
          display: "flex",
          flexDirection: "inherit",
          alignItems: "center",
        }}
      >
        <Card sx={{ maxWidth: 700, mt: 15 }}>
          <CardActionArea>
            <CardContent>
              <Typography gutterBottom variant="h5" component="div">
                <CardMedia
                  sx={{ height: 300, width: 550 }}
                  component="img"
                  height="140"
                  image="https://img.freepik.com/free-vector/quitting-time-concept-illustration_114360-1657.jpg?w=900&t=st=1684495868~exp=1684496468~hmac=512077ad8392c558524c9a8ff98041e5ce3536f71b79f368c5b8a235f1b534f2"
                  alt="leave"
                />
              </Typography>
            </CardContent>
          </CardActionArea>
        </Card>

        <Container component="main" maxWidth="sm" style={{ marginTop: "0px" }}>
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
            <Box
              component="form"
              onSubmit={handleSubmit}
              noValidate
              sx={{ mt: 1 }}
            >
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
                error={Boolean(data.errors.username)}
                helperText={data.errors.username}
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
                error={Boolean(data.errors.password)}
                helperText={data.errors.password}
              />

              {unAuthorized && (
                <span style={{ color: "red", fontSize: "12px" }}>
                  Please enter valid details :)
                </span>
              )}

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
      </div>
      <></>
    </>
  );
}






























// import { useState } from "react";
// import Button from "@mui/material/Button";
// import TextField from "@mui/material/TextField";
// import Link from "@mui/material/Link";
// import Grid from "@mui/material/Grid";
// import Box from "@mui/material/Box";
// import Typography from "@mui/material/Typography";
// import Container from "@mui/material/Container";
// import axios from "axios";
// import { useNavigate } from "react-router-dom";
// import { Card, CardActionArea, CardMedia, CardContent } from "@mui/material";

// import swal from 'sweetalert';

// export default function Login() {
//   const navigate = useNavigate();
//   const [data, setData] = useState({
//     username: "",
//     password: "",
//     errors: {
//       username: "",
//       password: "",
//     },
//   });
//   const [unAuthorized, setUnAuthorized] = useState(false);

//   const handleSubmit = async (event: any) => {
//     event.preventDefault();

//     let errors = { ...data.errors };
//     let hasErrors = false;

//     if (!data.username) {
//       errors.username = "Username is required";
//       hasErrors = true;
//     }

//     if (!data.password) {
//       errors.password = "Password is required";
//       hasErrors = true;
//     }

//     setData((prevState: any) => ({
//       ...prevState,
//       errors: errors,
//     }));

//     if (hasErrors) {
//       return;
//     }

//     const newData = {
//       username: data.username,
//       password: data.password,
//     };

//     try {
//       const response = await axios.post("/api/Auth/login", newData);
//       localStorage.setItem("token", response.data.token);
//       localStorage.setItem("role", response.data.role);
//       localStorage.setItem("id", response.data.id);
//       localStorage.setItem("refreshToken", response.data.refreshToken);

//       if (response.data.role === "Manager") {
//         // swal("Login Successfull !");
//         alert("Login Successfull !");
//         navigate(`/viewemployees`);
//         window.location.reload();
//       } else if (response.data.role === "Employee") {
//         // swal("Login Successfull");
//         alert("Login Successfull !");
//         navigate(`/leavedetails`);
//         window.location.reload();
//       }

//       // setTimeout(() => {
//       //   window.location.reload();
//       // }, 1500); // Set the duration in milliseconds (e.g., 2000ms = 2 seconds)

//       if (response.status !== 200) {
//         setUnAuthorized(true);
//       }
//     } catch (error: any) {
//       console.log(error);
//       setUnAuthorized(true);
//     }
//     setData({
//       username: "",
//       password: "",
//       errors: {
//         username: "",
//         password: "",
//       },
//     });
//   };

//   const handleInputChange = (event: any) => {
//     const { name, value } = event.target;
//     let errors = { ...data.errors };

//     switch (name) {
//       case "username":
//         errors.username = value ? "" : "Username is required";
//         break;
//       case "password":
//         errors.password = value ? "" : "Password is required";
//         break;
//       default:
//         break;
//     }

//     setData((prevState: any) => ({
//       ...prevState,
//       [name]: value,
//       errors: errors,
//     }));
//     setUnAuthorized(false);
//   };

//   const handleSnack = () => {
//     setUnAuthorized(false);
//   };

//   return (
//     <>
//       <div
//         style={{
//           display: "flex",
//           flexDirection: "inherit",
//           alignItems: "center",
//         }}
//       >
//         <Card sx={{ maxWidth: 700, mt: 15 }}>
//           <CardActionArea>
//             <CardContent>
//               <Typography gutterBottom variant="h5" component="div">
//                 <CardMedia
//                   sx={{ height: 300, width: 550 }}
//                   component="img"
//                   height="140"
//                   image="https://img.freepik.com/free-vector/quitting-time-concept-illustration_114360-1657.jpg?w=900&t=st=1684495868~exp=1684496468~hmac=512077ad8392c558524c9a8ff98041e5ce3536f71b79f368c5b8a235f1b534f2"
//                   alt="leave"
//                 />
//               </Typography>
//             </CardContent>
//           </CardActionArea>
//         </Card>

//         <Container component="main" maxWidth="sm" style={{ marginTop: "0px" }}>
//           <Box
//             sx={{
//               boxShadow: 3,
//               borderRadius: 2,
//               px: 4,
//               py: 6,
//               marginTop: 8,
//               display: "flex",
//               flexDirection: "column",
//               alignItems: "center",
//             }}
//           >
//             <Typography component="h1" variant="h5">
//               Sign in
//             </Typography>
//             <Box
//               component="form"
//               onSubmit={handleSubmit}
//               noValidate
//               sx={{ mt: 1 }}
//             >
//               <TextField
//                 margin="normal"
//                 required
//                 fullWidth
//                 id="username"
//                 label="Username"
//                 name="username"
//                 value={data.username}
//                 autoComplete="off"
//                 autoFocus
//                 onChange={handleInputChange}
//                 error={Boolean(data.errors.username)}
//                 helperText={data.errors.username}
//               />
//               <TextField
//                 margin="normal"
//                 required
//                 fullWidth
//                 name="password"
//                 value={data.password}
//                 label="Password"
//                 type="password"
//                 id="password"
//                 autoComplete="off"
//                 onChange={handleInputChange}
//                 error={Boolean(data.errors.password)}
//                 helperText={data.errors.password}
//               />

//               {unAuthorized && (
//                 <span style={{ color: "red", fontSize: "12px" }}>
//                   Please enter valid details :)
//                 </span>
//               )}

//               <Button
//                 type="submit"
//                 fullWidth
//                 variant="contained"
//                 sx={{ mt: 3, mb: 2 }}
//               >
//                 Sign In
//               </Button>
//               <Grid container>
//                 <Grid item>
//                   <Link href="/register" variant="body2">
//                     {"Don't have an account? Sign Up"}
//                   </Link>
//                 </Grid>
//               </Grid>
//             </Box>
//           </Box>
//         </Container>
//       </div>
//       <></>
//     </>
//   );
// }
