import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useNavigate } from "react-router-dom";
import { FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import useHttp from "../../config/https";
import swal from 'sweetalert';

const managerId = localStorage.getItem("id");

export default function AddEmployee() {
  const [isFormValid, setIsFormValid] = useState(false);
  const {axiosInstance, loading} = useHttp();
  const navigate = useNavigate();
  const [designation, setDesignation] = useState<any>([]);
  const [unAuthorized, setUnAuthorized] = useState(false);
  const [isDesignationSelected, setIsDesignationSelected] = useState(false);

  const [data, setData] = useState({
    firstname: "",
    lastname: "",
    username: "",
    email: "",
    password: "",
    phonenumber: "",
    managerId: "",
    designationId: "",
    errors: {
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      password: "",
      phonenumber: "",
      managerId: "",
      designationId: "",
    },
  });

  const [tryErrors, setTryErrors] = useState({
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

    if (!data.password) {
      errors.password = "Password is required";
      hasErrors = true;
    }

    if (!data.phonenumber) {
      errors.phonenumber = "Phone Number is required";
      hasErrors = true;
    }

    if (!data.designationId) {
      errors.designationId = "Please select a designation";
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
      axiosInstance.post("/api/Auth/register", newData).then((response) => {
        console.log(response.data);
        swal("Employee Added Successfully.");
      });
    } catch (error: any) {
      alert(error.response.data.message);
    }
    const existingData = {
      firstname: data.firstname,
      lastname: data.lastname,
      username: data.username,
      email: data.email,
      password: data.password,
      phonenumber: data.phonenumber,
      managerId: "",
      designationId: "4",
      errors: {
        firstname: "",
        lastname: "",
        username: "",
        email: "",
        password: "",
        phonenumber: "",
        managerId: "",
        designationId: "",
      },
    };
    setData(existingData);
    setUnAuthorized(true);

    setData({
      firstname: "",
      lastname: "",
      username: "",
      email: "",
      password: "",
      phonenumber: "",
      managerId: "",
      designationId: "",
      errors: {
        firstname: "",
        lastname: "",
        username: "",
        email: "",
        password: "",
        phonenumber: "",
        managerId: "",
        designationId: "",
      },
    });
  };

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let isValid = true;
    const newErrors = { ...tryErrors };

    // Add validations for each field
    switch (name) {
      case "firstname":
        // Check if field contains only alphabets
        if (!value || !/^[a-zA-Z ]*$/.test(value)) {
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

      case "phonenumber":
        // Check if phone number is valid
        if (!/^\d{10}$/.test(value)) {
          newErrors.phonenumber = "Enter a valid 10-digit phone number";
          isValid = false;
        } else {
          newErrors.phonenumber = "";
        }
        break;

      case "designationId":
        // Check if designation is selected
        if (!value) {
          newErrors.designationId = "Please select a designation";
          isValid = false;
        } else {
          newErrors.designationId = "";
        }
        break;

      case "designationId":
        if (!value) {
          newErrors.designationId = "Please select a designation";
          setIsDesignationSelected(false);
          isValid = false;
        } else {
          newErrors.designationId = "";
          setIsDesignationSelected(true);
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

    // Set the overall form validity
    setIsFormValid(isValid);
  };

  const handleBackButton = () => {
    navigate("/");
  };

  useEffect(() => {
    const fetchDesignations = async () => {
      try {
        const response = await axiosInstance.get("/api/DesignationMaster");
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
            name="password"
            value={data.password}
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            onChange={handleInputChange}
            error={Boolean(data.errors.password)}
            helperText={data.errors.password}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="phonenumber"
            value={data.phonenumber}
            label="Mobile Number"
            type="text"
            id="phonenumber"
            sx={{ mb: 2 }}
            autoComplete="phonenumber"
            onChange={handleInputChange}
            error={Boolean(data.errors.phonenumber)}
            helperText={data.errors.phonenumber}
          />

          <br></br>
          <br></br>

          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">
              {" "}
              Designation Type{" "}
            </InputLabel>

            <Select
              name="designationId"
              label="Designation Name"
              id="demo-simple-select"
              fullWidth
              sx={{ mb: 2 }}
              value={data.designationId}
              onChange={handleInputChange}
              required
              error={
                !isDesignationSelected && Boolean(data.errors.designationId)
              }
            >
              {designation?.map((option: any) => (
                <MenuItem value={option.id}>{option.designationName}</MenuItem>
              ))}
            </Select>
            {!isDesignationSelected && (
              <Typography variant="caption" color="error">
                {data.errors.designationId}
              </Typography>
            )}
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
              style={{ marginLeft: "320px" }}
            >
              Create
            </Button>
          </div>
        </Box>
      </Box>
    </Container>
  );
}