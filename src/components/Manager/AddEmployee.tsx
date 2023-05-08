import React, { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";

const managerId = localStorage.getItem("id");

export default function AddEmployee() {
  const navigate = useNavigate();
  const [designation, setDesignation] = useState<any>([]);
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
  
  const [errors, setErrors] = useState({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
    password: "",
    phonenumber: "",
    managerId: "",
    designationId:""
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
      managerId: managerId,
      designationId: data.designationId.toString(),
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
    let isValid = true;
    const newErrors = { ...errors };

    // Add validations for each field
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
      case "password":
        // Check if password is strong
        if (
          !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(
            value
          )
        ) {
          newErrors.password =
            "Password should contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character";
          isValid = false;
        } else {
          newErrors.password = "";
        }
        break;
      // case "phonenumber":
      //   // Check if phone number is valid
      //   if (!/^[0-9]{10}$/.test(value)) {
      //     newErrors.phonenumber = "Enter a valid 10 digit phone number";
      //     isValid = false;
      //   } else {
      //     newErrors.password = "";
      //   }
      //   break;


      // case "managerId":
      //   // Check if field contains only numbers
      //   if (!/^[0-9]*$/.test(value)) {
      //     errorMsg = "Only numbers are allowed";
      //   }
      //   break;
      default:
        break;
    }
    setErrors(newErrors);
    setData((prevState: any) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleBackButton = () => {
    navigate("/");
  };

  useEffect(() => {
    const fetchDesignations = async () => {
      try {
        const response = await axios.get("/api/DesignationMaster");
        setDesignation(response.data);
      } catch (error) {
        console.log(error);
      }
    };
    fetchDesignations();
  }, []);

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
            error={errors.firstname !== ""}
            helperText={errors.firstname}
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
            error={errors.lastname !== ""}
            helperText={errors.lastname}
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
            error={errors.username !== ""}
            helperText={errors.username}
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
            error={errors.email !== ""}
            helperText={errors.email}
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
            error={errors.password !== ""}
            helperText={errors.password}
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
            sx={{ mb: 2 }}
            autoComplete="phonenumber"
            onChange={handleInputChange}
            error={errors.phonenumber !== ""}
            helperText={errors.phonenumber}
          />
          {/* <TextField
            margin="normal"
            required
            fullWidth
            name="managerId"
            value={managerId}
            label="Manager Id"
            id="managerId"
            autoComplete="managerId"
            onChange={handleInputChange}
            sx={{mb: 2}}
            error={errors.managerId !== ""}
            helperText={errors.managerId}
          /> */}

          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label"> Designation Type </InputLabel>
            <Select
              name="designationId"
              label="Designation Name"
              id="demo-simple-select"
              fullWidth
              sx={{ mb: 2 }}
              value={data.designationId}
              onChange={handleInputChange}
            >
              {designation?.map((option: any) => (
                <MenuItem value={option.id}>{option.designationName}</MenuItem>
              ))}
            </Select>
          </FormControl>

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