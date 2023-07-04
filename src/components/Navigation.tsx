import React, { useEffect, useState } from "react";
import {
  Drawer,
  List,
  ListItemIcon,
  ListItemText,
  IconButton,
  Toolbar,
  Typography,
  Avatar,
  Menu,
  MenuItem,
} from "@mui/material";
import PendingActionsIcon from "@mui/icons-material/PendingActions";
import AppBar from "@mui/material/AppBar";
import Button from "@mui/material/Button";
import {
  Menu as MenuIcon,
  Dashboard as DashboardIcon,
  Add as AddIcon,
  PeopleOutlined as PeopleIcon,
} from "@mui/icons-material";
import ListSubheader from "@mui/material/ListSubheader";
import ListItemButton from "@mui/material/ListItemButton";
import DraftsIcon from "@mui/icons-material/Drafts";
import SendIcon from "@mui/icons-material/Send";
import { useNavigate } from "react-router-dom";
import ViewListIcon from "@mui/icons-material/ViewList";
import AddHomeWorkSharpIcon from "@mui/icons-material/AddHomeWorkSharp";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import AddBoxIcon from "@mui/icons-material/AddBox";
import useHttp from "../config/https";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import LogoutIcon from "@mui/icons-material/Logout";
import { Employee } from "./MyProfile";

const Navigation = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [open, setOpen] = React.useState(true);
  const [isLogin, setIsLogin] = React.useState(false);
  const [isManager, setIsManager] = React.useState(false);
  const [isEmployee, setIsEmployee] = React.useState(false);
  const empId = localStorage.getItem("id");
  const { axiosInstance, loading } = useHttp();
  const [data, setData] = useState<Employee | undefined>(undefined);
  //profile image
  const [image, setImage] = useState<any>(null);
  const [imageExists, setImageExists] = useState(false);

  useEffect(() => {
    const fetchLeaveDetails = async () => {
      try {
        const response = await axiosInstance.get(
          `api/User/currentuserdetails/${empId}`
        );
        setData(response.data);
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLeaveDetails();

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

  const handleClickSidebar = () => {
    setSidebarOpen(!sidebarOpen);
  };

  const handleClickProfile = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleCloseProfile = () => {
    setAnchorEl(null);
  };

  const navigate = useNavigate();

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    localStorage.removeItem("id");
    localStorage.removeItem("refreshToken");
    setIsLogin(false);
    navigate("/");
    window.location.reload();
  };

  useEffect(() => {
    const getToken = () => {
      setIsLogin(
        localStorage.getItem("token") && localStorage.getItem("token") !== ""
          ? true
          : false
      );
      setIsManager(
        localStorage.getItem("role") &&
          localStorage.getItem("role") === "Manager"
          ? true
          : false
      );
      setIsEmployee(
        localStorage.getItem("role") &&
          localStorage.getItem("role") === "Employee"
          ? true
          : false
      );
    };

    getToken();
  }, []);

  return (
    <>
      <AppBar position="static" color="inherit" style={{ width: "100%" }}>
        <Toolbar>
          {isLogin && (
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
              onClick={handleClickSidebar}
            >
              <MenuIcon />
            </IconButton>
          )}
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            <AddHomeWorkSharpIcon style={{ marginRight: "13px" }} />
            Employee Leave Tracking
          </Typography>

          {isLogin && (
            <>
              <IconButton
                size="large"
                edge="end"
                color="inherit"
                aria-label="profile"
                onClick={handleClickProfile}
              >
                {imageExists && image ? (
                  <Avatar
                    src={`https://localhost:7033/${image}`}
                    alt="Profile Picture"
                  />
                ) : (
                  <AccountCircleIcon />
                )}
                &nbsp;&nbsp;
                {data && (
                  <Typography variant="h6" component="div" sx={{ flexGrow: 0 }}>
                    {data.firstName} {data.lastName}
                  </Typography>
                )}
              </IconButton>
              <Menu
                id="profile-menu"
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleCloseProfile}
                PaperProps={{ style: { width: 270, textAlign: "center" } }}
              >
                <MenuItem onClick={() => navigate("/")}>
                  <HomeIcon /> &nbsp;&nbsp; &nbsp; Home
                </MenuItem>
                <MenuItem onClick={() => navigate("/about")}>
                  <InfoIcon /> &nbsp;&nbsp; &nbsp; About
                </MenuItem>
                <MenuItem onClick={handleLogout}>
                  <LogoutIcon /> &nbsp;&nbsp; &nbsp; Logout
                </MenuItem>
              </Menu>
            </>
          )}
        </Toolbar>
      </AppBar>
      <Drawer
        anchor="left"
        open={sidebarOpen}
        onClose={handleClickSidebar}
        sx={{
          width: 240,
          flexShrink: 0,
          "& .MuiDrawer-paper": {
            width: 240,
            boxSizing: "border-box",
          },
        }}
      >
        <List
          sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
          component="nav"
          aria-labelledby="nested-list-subheader"
          subheader={
            <ListSubheader component="div" id="nested-list-subheader">
              Dashboard
            </ListSubheader>
          }
        >
          {isManager ? (
            <>
              <ListItemButton onClick={() => navigate("/addemployee")}>
                <ListItemIcon>
                  <AddIcon />
                </ListItemIcon>
                <ListItemText primary="Add Employee" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/viewemployees")}>
                <ListItemIcon>
                  <ViewListIcon />
                </ListItemIcon>
                <ListItemText primary="View All Employees" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/newrequests")}>
                <ListItemIcon>
                  <PendingActionsIcon />
                </ListItemIcon>
                <ListItemText primary="Pending Requests" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/myprofile")}>
                <ListItemIcon>
                  <AccountCircleIcon />
                </ListItemIcon>
                <ListItemText primary="My Profile" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/compoff")}>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText primary="Comp-Off Request" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/onduty")}>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText primary="On-Duty Request" />
              </ListItemButton>
            </>
          ) : (
            <>
              <ListItemButton onClick={() => navigate("/applyforleaves")}>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText primary="Apply Leave" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/leavedetails")}>
                <ListItemIcon>
                  <ViewListIcon />
                </ListItemIcon>
                <ListItemText primary="Leave Details" />
              </ListItemButton>

              <ListItemButton onClick={() => navigate("/myprofile")}>
                <ListItemIcon>
                  <AccountCircleIcon />
                </ListItemIcon>
                <ListItemText primary="My Profile" />
              </ListItemButton>
            </>
          )}
        </List>
      </Drawer>
    </>
  );
};

export default Navigation;
