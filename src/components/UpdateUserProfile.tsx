import { useState, useEffect, useRef, ChangeEvent } from "react";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import useHttp from "../config/https";
import { Avatar } from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import AutoFixHighIcon from '@mui/icons-material/AutoFixHigh';
import swal from 'sweetalert';
import { useNavigate } from "react-router-dom";

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

  //profile image
  const [image, setImage] = useState<any>(null);
  const [imageExists, setImageExists] = useState(false);
  const navigate = useNavigate();

  const [file, setFile] = useState<string>("");
  const userId = localStorage.getItem("id");
  const inputRef = useRef<HTMLInputElement | null>(null);

  const handleUploadClick = () => {
    inputRef.current?.click();
  };

  const handleFileChange = async (e: ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) {
      return;
    }

    const selectedFile = e.target.files[0];
    const imageUrl = URL.createObjectURL(selectedFile);

    setFile(imageUrl);

    try {
      const formData = new FormData();
      formData.append("UserId", userId!);
      formData.append("Image", selectedFile);

      const response = await axiosInstance.post("/api/ProfileImage", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      console.log(response);
      window.location.reload();
    } catch (error: any) {
      swal(error.response?.data?.message || "An error occurred.");
    }
  };

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

//profile pic
useEffect(() => {
  const empId = localStorage.getItem("id");

  const fetchProfilePic = async () => {
    try {
      const response = await axiosInstance.get(`api/ProfileImage/${empId}`);
      console.log(response);
      setImage(response.data);
      setImageExists(true);
    } catch (error) {
      console.error(error);
      setImageExists(false);
    }
  };
  fetchProfilePic();
}, []);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        if (!dataFetched) { 
          // Only fetch data if it hasn't been fetched before
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
          swal("User Profile Updated Successfully");
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
    // setData({
    //   firstname: "",
    //   lastname: "",
    //   username: "",
    //   email: "",
    //   phonenumber: "",
    //   errors: {
    //     firstname: "",
    //     lastname: "",
    //     username: "",
    //     email: "",
    //     phonenumber: "",
    //   },
    // });
  };
  const handleDelete = () => {
   //delete functionality
   try {
    const id = localStorage.getItem("id");
    const response = axiosInstance.delete(`/api/ProfileImage/${id}`);
    console.log(response);
    window.location.reload();
  } catch (error: any) {
    swal(error.response?.data?.message || "An error occurred.");
  }


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
          marginTop: 3,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h5">
          Update Profile
        </Typography>
        <br></br>

        {imageExists && image ? (
              <img
                // src={image}
                // src={`https://localhost:7033/${image}`}
                src={`https://employeeleavetracking.azurewebsites.net/${image}`}
                style={{
                  display: "flex",
                  alignItems: "center",
                  height: "150px",
                  width: "150px",
                  borderRadius: "100px",
                }}
              />
            ) : (
              <Avatar
                src="/broken-image.jpg"
                style={{
                  display: "flex",
                  alignItems: "center",
                  height: "100px",
                  width: "100px",
                }}
              />
            )}

            <br></br>

            <div>
            <Button variant="outlined" color="success"  onClick={handleUploadClick}>
            <AutoFixHighIcon/>
            </Button>

            <Button variant="outlined" color="error" style={{marginLeft:"20px"}} onClick={handleDelete}>
            <DeleteIcon />
            </Button>
            </div>
            <input
        type="file"
        ref={inputRef}
        onChange={handleFileChange}
        style={{ display: "none" }}
      />

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

          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 1, mb: 2 }}
            onClick={() => navigate("/myprofile")}
          >
            Back
          </Button>
        </Box>
      </Box>
    </Container>
  );
}