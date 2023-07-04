import React, { useEffect, useState } from "react";
import { Container, Box, Typography, TextField, Button } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import useHttp from "../config/https";
import swal from 'sweetalert';

const ChangePassword = () => {
  const [isFormValid, setIsFormValid] = useState(false);
  const [user, setUser] = useState<any>([]);
  const navigate = useNavigate();
  const { axiosInstance, loading } = useHttp();
  const [data, setData] = useState({
    current: "",
    new: "",
    errors: {
      current: "",
      new: "",
    },
  });

  const [tryErrors, setTryErrors] = useState({
    current: "",
    new: "",
  });

  const [unAuthorized, setUnAuthorized] = useState(false);

  useEffect(() => {
    const fetchUserId = async () => {
      setUser(localStorage.getItem("id"));
    };
    fetchUserId();
  }, []);

  const handleSubmit = async (event: any) => {
    event.preventDefault();
  
    let errors = { ...data.errors };
    let hasErrors = false;
  
    if (!data.current) {
      errors.current = "Current Password is required";
      hasErrors = true;
    }
  
    if (!data.new) {
      errors.new = "New Password is required";
      hasErrors = true;
    }
  
    setData((prevState: any) => ({
      ...prevState,
      errors: errors,
    }));
  
    if (hasErrors) {
      return;
    }
  
    try {
      const response = await axiosInstance.post(`/api/Auth/${user}/${data.current}/${data.new}`);
      console.log(response.data);
      swal("Password Changed Successfully");
    } catch (error: any) {
      const existingData = {
        current: data.current,
        new: data.new,
        errors: {
          current: "",
          new: "",
        },
      };
      setData(existingData);
      console.log(error);
      setUnAuthorized(true);
      // alert("Invalid Details");
      console.log(error.response.data.message);
    }
  
    setData({
      current: "",
      new: "",
      errors: {
        current: "",
        new: "",
      },
    });
  };
  
  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let isValid = true;
    const newErrors = { ...tryErrors };

    switch (name) {
      case "current":
        // Check if password is strong
        if (
          !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(
            value
          )
        ) {
          newErrors.current =
            "Password should contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character";
          isValid = false;
        } else {
          newErrors.current = "";
        }
        break;

      case "new":
        // Check if password is strong
        if (
          !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(
            value
          )
        ) {
          newErrors.new =
            "Password should contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character";
          isValid = false;
        } else {
          newErrors.new = "";
        }
        break;

      default:
        break;
    }

    setTryErrors(newErrors);
    setData((prevState: any) => ({
      ...prevState,
      errors: newErrors,
      [name]: value,
    }));
    setIsFormValid(isValid);
    setUnAuthorized(false);
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
          Change Password
        </Typography>

        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            name="current"
            value={data.current}
            label="Current Password"
            type="password"
            id="current"
            autoComplete="current-password"
            onChange={handleInputChange}
            error={Boolean(data.errors.current)}
            helperText={data.errors.current}
          />

          <TextField
            margin="normal"
            required
            fullWidth
            name="new"
            value={data.new}
            label="New Password"
            type="password"
            id="new"
            autoComplete="new-password"
            onChange={handleInputChange}
            error={Boolean(data.errors.new)}
            helperText={data.errors.new}
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
            Change Password
          </Button>
          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 0, mb: 2 }}
            onClick={() => navigate("/myprofile")}
          >
            Back
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default ChangePassword;







































// const handleSubmit = async (event: any) => {
//   event.preventDefault();

//   let errors = { ...data.errors };
//   let hasErrors = false;

//   if (!data.current) {
//     errors.current = "Current Password is required";
//     hasErrors = true;
//   }

//   if (!data.new) {
//     errors.new = "New Password is required";
//     hasErrors = true;
//   }

//   setData((prevState: any) => ({
//     ...prevState,
//     errors: errors,
//   }));

//   if (hasErrors) {
//     return;
//   }

//   try {
//     const response = await axiosInstance.post(`/api/Auth/${user}/${data.current}/${data.new}`);
//     console.log(response.data);
//     alert("Password Changed Successfully");
//   } catch (error: any) {
//     const existingData = {
//       current: data.current,
//       new: data.new,
//       errors: {
//         current: "",
//         new: "",
//       },
//     };
//     setData(existingData);
//     console.log(error);
//     setUnAuthorized(true);
//     console.log(error.response.data.message);
//     alert("Current password is not valid");
//   }

//   setData({
//     current: "",
//     new: "",
//     errors: {
//       current: "",
//       new: "",
//     },
//   });
// };
