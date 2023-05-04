import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
  Drawer,
  List,
  ListItemIcon,
  ListItemText,
  IconButton,
  Toolbar,
  Typography,
  Menu,
  MenuItem,
} from "@mui/material";

import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";
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
import Collapse from "@mui/material/Collapse";
import InboxIcon from "@mui/icons-material/MoveToInbox";
import DraftsIcon from "@mui/icons-material/Drafts";
import SendIcon from "@mui/icons-material/Send";
import StarBorder from "@mui/icons-material/StarBorder";
import { useNavigate } from "react-router-dom";
import ViewListIcon from "@mui/icons-material/ViewList";

const Navigation = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [open, setOpen] = React.useState(true);
  const [isLogin, setIsLogin] = React.useState(false);

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
    setIsLogin(localStorage.getItem("token") && localStorage.getItem("token") != "" ? true : false);
  };

  const toggleDrawer = () => {
    setSidebarOpen(!sidebarOpen);
  };

  useEffect(() => {

    getToken(); 
  },[]);


  return (
    <>
      <AppBar position="static" color="transparent" style={{ width: "100%" }}>
        <Toolbar>
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
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Employee Leave Tracking
          </Typography>
          <Button color="inherit" onClick={() => navigate("/")}>
            Home
          </Button>
          <Button color="inherit" onClick={() => navigate("/about")}>
            About
          </Button>
          {/* { isLogin ? <Button color="inherit" onClick={() => navigate("/")}>
            Logout
          </Button> : <Button color="inherit" onClick={() => navigate("/")}>
            Login
          </Button> } */}
 
 
          <Button color="inherit" onClick={() => navigate("/")}>
            {isLogin ? "Logout" : "Login"}
          </Button>

          {/* <Button color="inherit" onClick={() => navigate("/")}>
            Logout
          </Button> */}
        </Toolbar>
      </AppBar>
      <Menu
        id="customers-menu"
        anchorEl={anchorEl}
        open={Open}
        onClose={handleClose}
      >
        <MenuItem component={Link} to="/customers/add" onClick={handleClose}>
          <ListItemIcon>
            <AddIcon />
          </ListItemIcon>
          <ListItemText primary="Add Customer" />
        </MenuItem>
        <MenuItem component={Link} to="/customers" onClick={handleClose}>
          <ListItemIcon>
            <PeopleIcon />
          </ListItemIcon>
          <ListItemText primary="View Customers" />
        </MenuItem>
      </Menu>
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
          {/* if(localStorage.getItem("role") == "Manager") */}
          {
            <ListItemButton>
              <ListItemIcon>
                <AddIcon />
              </ListItemIcon>
              <ListItemText
                primary="Add Employee"
                onClick={() => navigate("/addemployee")}
              />
            </ListItemButton>
          }
          {/* <ListItemButton>
        <ListItemIcon>
          <AddIcon />
        </ListItemIcon>
        <ListItemText primary="Add Employee"  onClick={() => navigate("/addemployee")}/>
      </ListItemButton> */}
          <ListItemButton>
            <ListItemIcon>
              <ViewListIcon />
            </ListItemIcon>
            <ListItemText
              primary="View All Employees"
              onClick={() => navigate("/viewemployees")}
            />
          </ListItemButton>

          <ListItemButton onClick={handleClick1}>
            <ListItemIcon>
              <InboxIcon />
            </ListItemIcon>
            <ListItemText primary="Inbox" />
            {open ? <ExpandLess /> : <ExpandMore />}
          </ListItemButton>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <List component="div" disablePadding>
              <ListItemButton sx={{ pl: 4 }}>
                <ListItemIcon>
                  <StarBorder />
                </ListItemIcon>
                <ListItemText primary="Starred" />
              </ListItemButton>
            </List>
          </Collapse>
        </List>
      </Drawer>
    </>
  );
};

export default Navigation;
