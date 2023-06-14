import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import useHttp from "../config/https";

export default function UpdateUserProfile() {
  const [isFormValid, setIsFormValid] = useState(false);
  const [user, setUser] = useState<any>([]);
  const [userdata, setUserdata] = useState<any>([]);
  const { axiosInstance, loading } = useHttp();
  const [data, setData] = useState({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
    phonenumber: "",
    errors: {
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      phonenumber: "",
    },
  });

  const [tryErrors, setTryErrors] = useState({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
    phonenumber: "",
  });

  const [unAuthorized, setUnAuthorized] = useState(false);

// Add a new state variable to track if the data has been fetched
const [dataFetched, setDataFetched] = useState(false);

  useEffect(() => {
    const fetchUserId = async () => {
      setUser(localStorage.getItem("id"));
    };
    fetchUserId();
  }, []);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        if (!dataFetched) { // Only fetch data if it hasn't been fetched before
          const response = await axiosInstance.get(
            `/api/User/currentuserdetails/${localStorage.getItem("id")}`
          );
          const userData = response.data;
          setUserdata(userData);
          setData({
            firstname: userData.firstName,
            lastname: userData.lastName,
            username: userData.userName,
            email: userData.email,
            phonenumber: userData.phoneNumber,
            errors: {
              firstname: "",
              lastname: "",
              username: "",
              email: "",
              phonenumber: "",
            },
          });
          setDataFetched(true); // Set the flag to indicate data has been fetched
        }
      } catch (error) {
        console.log(error);
        setUnAuthorized(true);
      }
    };
  
    fetchUserData();
  }, [axiosInstance, dataFetched, localStorage.getItem("id")]);

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    let errors = { ...data.errors };
    let hasErrors = false;

    if (!data.firstname) {
      errors.firstname = "Firstname is required";
      hasErrors = true;
    }

    if (!data.lastname) {
      errors.lastname = "Lastname is required";
      hasErrors = true;
    }

    if (!data.username) {
      errors.username = "Username is required";
      hasErrors = true;
    }

    if (!data.email) {
      errors.email = "Email is required";
      hasErrors = true;
    }

    if (!data.phonenumber) {
      errors.phonenumber = "Phone Number is required";
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
      id: user,
      firstname: data.firstname,
      lastname: data.lastname,
      username: data.username,
      email: data.email,
      phonenumber: data.phonenumber,
    };

    try {
      axiosInstance
        .put(`/api/User/updateprofile/${user}`, newData)
        .then((response: { data: any }) => {
          console.log(response.data);
          alert("User Profile Updated Successfully");
        });
    } catch (error: any) {
      const existingData = {
        firstname: data.firstname,
        lastname: data.lastname,
        username: data.username,
        email: data.email,
        phonenumber: data.phonenumber,
        errors: {
          firstname: "",
          lastname: "",
          username: "",
          email: "",
          phonenumber: "",
        },
      };
      setData(existingData);
      console.log(error);
      setUnAuthorized(true);
      console.log(error.response.data.message);
    }
    setData({
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      phonenumber: "",
      errors: {
        firstname: "",
        lastname: "",
        username: "",
        email: "",
        phonenumber: "",
      },
    });
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let isValid = true;
    const newErrors = { ...tryErrors };

    switch (name) {
      case "firstname":
        // Check if field contains only alphabets
        if (!/^[a-zA-Z ]*$/.test(value)) {
          newErrors.firstname = "Only alphabets are allowed";
          isValid = false;
        } else {
          newErrors.firstname = "";
        }
        break;

      case "lastname":
        // Check if field contains only alphabets
        if (!/^[a-zA-Z ]*$/.test(value)) {
          newErrors.lastname = "Only alphabets are allowed";
          isValid = false;
        } else {
          newErrors.lastname = "";
        }
        break;

      case "username":
        // Check if field contains only alphanumeric characters and underscore
        if (!/^[a-zA-Z0-9_]*$/.test(value)) {
          newErrors.username =
            "Only alphanumeric characters and underscore are allowed";
          isValid = false;
        } else {
          newErrors.username = "";
        }
        break;

      case "email":
        // Check if email is valid
        if (!/\S+@\S+\.\S+/.test(value)) {
          newErrors.email = "Enter a valid email address";
          isValid = false;
        } else {
          newErrors.email = "";
        }
        break;

      case "phonenumber":
        // Check if phone number is valid
        if (!/^\d{10}$/.test(value)) {
          newErrors.phonenumber = "Enter a valid 10-digit phone number";
          isValid = false;
        } else {
          newErrors.phonenumber = "";
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
          Update Profile
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
            error={Boolean(data.errors.firstname)}
            helperText={data.errors.firstname}
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
            error={Boolean(data.errors.lastname)}
            helperText={data.errors.lastname}
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
            error={Boolean(data.errors.username)}
            helperText={data.errors.username}
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
            error={Boolean(data.errors.email)}
            helperText={data.errors.email}
          />

          <TextField
            margin="normal"
            required
            fullWidth
            name="phonenumber"
            value={data.phonenumber}
            label="Phone Number"
            type="text"
            id="phonenumber"
            autoComplete="phonenumber"
            onChange={handleInputChange}
            error={Boolean(data.errors.phonenumber)}
            helperText={data.errors.phonenumber}
          />

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Update
          </Button>
        </Box>
      </Box>
    </Container>
  );
}