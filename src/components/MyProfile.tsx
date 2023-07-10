import {
  Card,
  CardContent,
  Typography,
  Avatar,
  Button,
  ButtonGroup,
} from "@mui/material";
import { Key, ReactNode, useEffect, useState } from "react";
import useHttp from "../config/https";
import AutoFixHighIcon from '@mui/icons-material/AutoFixHigh';
import PasswordIcon from '@mui/icons-material/Password';
import { useNavigate } from "react-router-dom";

export interface Employee {
  userName: Key | null | undefined;
  firstName: ReactNode;
  lastName: ReactNode;
  email: ReactNode;
  phoneNumber: ReactNode;
  managerId: number;
  id: string;
  designationId: number;
  designationName: ReactNode;
}

export default function CustomizedTables() {
  const [data, setData] = useState<Employee[]>([]);
  const { axiosInstance, loading } = useHttp();
  const navigate = useNavigate();

  //profile image
  const [image, setImage] = useState<any>(null);
  const [imageExists, setImageExists] = useState(false);

  useEffect(() => {
  
    const empId = localStorage.getItem("id");

    const role = localStorage.getItem("role");
    if (role == "Employee") {
      const fetchEmployeeForManager = async () => {
        try {
          const response = await axiosInstance.get(
            `api/User/employee/${empId}`
          );
          console.log(response);
          setData(response.data);
        } catch (error) {
          console.error(error);
        }
      };
      fetchEmployeeForManager();
      console.log(data);
    } else if (role == "Manager") {
      const fetchManager = async () => {
        try {
          const response = await axiosInstance.get(`api/User/manager/${empId}`);
          console.log(response);
          setData(response.data);
        } catch (error) {
          console.error(error);
        }
      };
      fetchManager();
      console.log(data);
    }

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

  return (
    <div
      style={{ display: "flex", flexDirection: "column", alignItems: "center" }}
    >
      <br></br>            

      {data.map((user) => (
        <Card sx={{ minWidth: 675, mt: 5, backgroundColor: "white" }}>
          <CardContent
            style={{
              display: "flex",
              alignItems: "center",
              flexDirection: "column",
            }}
          >
            {imageExists && image ? (
              <img
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

            <Typography
              gutterBottom
              variant="h5"
              component="div"
              sx={{ mt: 1 }}
              style={{ display: "flex", alignItems: "center" }}
            >
              Personal Details
            </Typography>
            <br></br>

            <table style={{ fontSize: "20px" }}>
              <tr>
                <td>
                  <b>Firstname</b>
                </td>
                <td>{user.firstName}</td>
              </tr>
              <tr>
                <td>
                  <b>Lastname</b>
                </td>
                <td>{user.lastName}</td>
              </tr>
              <tr>
                <td>
                  <b>Username</b>
                </td>
                <td>{user.userName}</td>
              </tr>
              <tr>
                <td>
                  <b>Email</b>
                </td>
                <td>{user.email}</td>
              </tr>

              <tr>
                <td>
                  <b>Contact Number &nbsp;&nbsp;&nbsp;&nbsp;</b>
                </td>
                <td>{user.phoneNumber}</td>
              </tr>
            </table>
          </CardContent>
        </Card>
        
      ))}
      <br></br>
       <ButtonGroup style={{marginLeft:"0px"}}>
            <Button variant="outlined" color="success" onClick={() => navigate("/updateuserprofile")}>
            <AutoFixHighIcon/> &nbsp; Edit Profile
            </Button>
            <Button variant="outlined" color="info" onClick={() => navigate("/changepassword")}>
            <PasswordIcon/> &nbsp; Change Password
            </Button>
            </ButtonGroup>
    </div>
  );
}