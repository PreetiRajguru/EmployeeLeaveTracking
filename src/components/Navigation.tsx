import React, { useEffect, useState } from "react";
import {
  Drawer,
  List,
  ListItemIcon,
  ListItemText,
  IconButton,
  Toolbar,
  Typography
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
import AddHomeWorkSharpIcon from '@mui/icons-material/AddHomeWorkSharp';
import PasswordIcon from '@mui/icons-material/Password';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import AutoFixNormalIcon from '@mui/icons-material/AutoFixNormal';
import AddBoxIcon from '@mui/icons-material/AddBox';

const Navigation = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [open, setOpen] = React.useState(true);
  const [isLogin, setIsLogin] = React.useState(false);
  const [isManager, setIsManager] = React.useState(false);
  const [isEmployee, setIsEmployee] = React.useState(false);

  const handleClick1 = () => {
    setOpen(!open);
  };
  const Open = Boolean(anchorEl);
  const navigate = useNavigate();
  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

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

  const toggleDrawer = () => {
    setSidebarOpen(!sidebarOpen);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("role");
    localStorage.removeItem("id");
    localStorage.removeItem("refreshToken");
    setIsLogin(false);
    navigate("/");
  };

  useEffect(() => {
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
              onClick={toggleDrawer}
            >
              <MenuIcon />
            </IconButton>
          )}
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          <AddHomeWorkSharpIcon style={{marginRight : "13px"}}/>
            Employee Leave Tracking
          </Typography>
          <Button color="inherit" onClick={() => navigate("/")}>
            Home
          </Button>
          <Button color="inherit" onClick={() => navigate("/about")}>
            About
          </Button>


          {isLogin ? (
            <Button color="inherit" onClick={handleLogout}>
              Logout
            </Button>
          ) : (
            <Button color="inherit" onClick={() => navigate("/")}>
              Login
            </Button>
          )}

        </Toolbar>
      </AppBar>
      <Drawer
        anchor="left"
        open={sidebarOpen}
        onClose={toggleDrawer}
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
              DashBoard
            </ListSubheader>
          }
        >
          {isManager ? (
            <>
              <ListItemButton>
                <ListItemIcon>
                  <AddIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Add Employee"
                  onClick={() => navigate("/addemployee")}
                />
              </ListItemButton>

              <ListItemButton>
                <ListItemIcon>
                  <ViewListIcon />
                </ListItemIcon>
                <ListItemText
                  primary="View All Employees"
                  onClick={() => navigate("/viewemployees")}
                />
              </ListItemButton>

              <ListItemButton>
                <ListItemIcon>
                  <PendingActionsIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Pending Requests"
                  onClick={() => navigate("/newrequests")}
                />
              </ListItemButton>
              
              <ListItemButton>
                <ListItemIcon>
                  <AccountCircleIcon />
                </ListItemIcon>
                <ListItemText
                  primary="My Profile"
                  onClick={() => navigate("/myprofile")}
                />
              </ListItemButton>
              
              <ListItemButton>
                <ListItemIcon>
                  <AutoFixNormalIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Update Profile"
                  onClick={() => navigate("/updateuserprofile")}
                />
              </ListItemButton>

              
              <ListItemButton>
                <ListItemIcon>
                  <PasswordIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Change Password"
                  onClick={() => navigate("/changepassword")}
                />
              </ListItemButton>


              <ListItemButton>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Comp-Off Request"
                  onClick={() => navigate("/compoff")}
                />
              </ListItemButton>


              <ListItemButton>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText
                  primary="On-Duty Request"
                  onClick={() => navigate("/onduty")}
                />
              </ListItemButton>

              {/* <ListItemButton>
                <ListItemIcon>
                  <AddBoxIcon />
                </ListItemIcon>
                <ListItemText
                  primary="On-Duty Request"
                  onClick={() => navigate("/changepassword")}
                />
              </ListItemButton> */}

            </>
          ) : (
            <>

              <ListItemButton>
                <ListItemIcon>
                  <SendIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Apply For Leaves"
                  onClick={() => navigate("/applyforleaves")}
                />

              </ListItemButton>
              <ListItemButton>
                <ListItemIcon>
                  <DraftsIcon />
                </ListItemIcon>
                <ListItemText
                  primary="My Leave Details"
                  onClick={() => navigate("/leavedetails")}
                />
              </ListItemButton>

              <ListItemButton>
                <ListItemIcon>
                  <AccountCircleIcon />
                </ListItemIcon>
                <ListItemText
                  primary="My Profile"
                  onClick={() => navigate("/myprofile")}
                />
              </ListItemButton>


              <ListItemButton>
                <ListItemIcon>
                  <AutoFixNormalIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Update Profile"
                  onClick={() => navigate("/updateuserprofile")}
                />
              </ListItemButton>

              <ListItemButton>
                <ListItemIcon>
                <PasswordIcon />
                </ListItemIcon>
                <ListItemText
                  primary="Change Password"
                  onClick={() => navigate("/changepassword")}
                />
              </ListItemButton>

            </>
          )}
        </List>
      </Drawer>
    </>
  );
};

export default Navigation;