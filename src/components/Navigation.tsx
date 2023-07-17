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
  Divider,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  tableCellClasses,
} from "@mui/material";
import PendingActionsIcon from "@mui/icons-material/PendingActions";
import AppBar from "@mui/material/AppBar";
import {
  Menu as MenuIcon,
  Dashboard as DashboardIcon,
  Add as AddIcon,
  PeopleOutlined as PeopleIcon,
} from "@mui/icons-material";
import ListSubheader from "@mui/material/ListSubheader";
import ListItemButton from "@mui/material/ListItemButton";
import { useLocation, useNavigate } from "react-router-dom";
import ViewListIcon from "@mui/icons-material/ViewList";
import AddHomeWorkSharpIcon from "@mui/icons-material/AddHomeWorkSharp";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import AddBoxIcon from "@mui/icons-material/AddBox";
import useHttp from "../config/https";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import LogoutIcon from "@mui/icons-material/Logout";
import { Employee } from "./MyProfile";
import NotificationsIcon from '@mui/icons-material/Notifications';
import { profileSubject } from "../components/Auth/profileSubject";
import { Badge } from "@mui/material";
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import CloseIcon from '@mui/icons-material/Close';

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.info.light,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({

  "&.is-viewed": {
    // backgroundColor: theme.palette.secondary.light,
    backgroundColor: "#DDDDDD",
  },

}));

const BootstrapDialog = styled(Dialog)(({ theme }) => ({
  '& .MuiDialogContent-root': {
    padding: theme.spacing(2),
  },
  '& .MuiDialogActions-root': {
    padding: theme.spacing(1),
  },
}));
export interface DialogTitleProps {
  id: string;
  children?: React.ReactNode;
  onClose: () => void;
}
function BootstrapDialogTitle(props: DialogTitleProps) {
  const { children, onClose, ...other } = props;

  return (
    <DialogTitle sx={{ m: 0, p: 2 }} {...other}>
      {children}
      {onClose ? (
        <IconButton
          aria-label="close"
          onClick={onClose}
          sx={{
            position: 'absolute',
            right: 8,
            top: 8,
            color: (theme) => theme.palette.grey[500],
          }}
        >
          <CloseIcon />
        </IconButton>
      ) : null}
    </DialogTitle>
  );
}

const Navigation = () => {

  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  const [sidebarOpen, setSidebarOpen] = useState(false);
  const { state } = useLocation();
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const [isLogin, setIsLogin] = React.useState(false);
  const [isManager, setIsManager] = React.useState(false);
  const [isEmployee, setIsEmployee] = React.useState(false);
  const { axiosInstance, loading } = useHttp();
  const empId = localStorage.getItem("id");
  const [data, setData] = useState<any | undefined>(state);
  const [image, setImage] = useState<any>(null);
  const [imageExists, setImageExists] = useState(false);
  const [notificationCount, setNotificationCount] = useState(0);
  const [newData, setNewData] = useState<any[]>([]);

  const handleNotificationsClick = () => {
    if (isManager) {
      navigate("/managernotifications");
    }
    else if (isEmployee) {
      navigate("/employeenotifications");
    }
  };

  useEffect(() => {
    const fetchNotificationCount = async () => {
      try {
        const response = await axiosInstance.get("api/Notifications/count");
        setNotificationCount(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchNotificationCount();
  }, [state]);

  useEffect(() => {

    const fetchNotificationDetails = async () => {
      try {
        const response = await axiosInstance.get(`api/Notifications/top`);
        setNewData(response.data);
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchNotificationDetails();

    fetchDetails();
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
    getToken()
  }, [state]);

  const fetchDetails = async () => {
    try {
      const response = await axiosInstance.get(
        `api/User/currentuserdetails/${localStorage.getItem("id")}`
      );
      setData(response.data);
    } catch (error) {
      console.error(error);
    }
  };

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

  const handleRowClick = async (rowId: any) => {
    try {
      const selectedRow = newData.find((row:any) => row.id === rowId);
      navigate("/notifyleave", { state: { selectedRow} });
      // navigate("/notifyleave", { state: { selectedRowId: rowId } });
      await axiosInstance.post(`/api/notifications/${rowId}/viewed`);
      const updatedData = newData.map((row: any) => {
        if (row.id === rowId) {
          return {
            ...row,
            isViewed: true,
          };
        }
        return row;
      });
    } catch (error) {
      console.log(error);
    }
    window.location.reload();
  };

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

              <Badge badgeContent={notificationCount} color="secondary">
                <NotificationsIcon onClick={handleClickOpen} />
              </Badge>

              <BootstrapDialog
                onClose={handleClose}
                aria-labelledby="customized-dialog-title"
                open={open}
                sx={{ml:110, mt:3}}
              >
                <BootstrapDialogTitle id="customized-dialog-title" onClose={handleClose}>
                  Notifications (Top 10)
                </BootstrapDialogTitle>
                <DialogContent dividers>
                  <div>
                    <TableContainer component={Paper}>
                      <Typography component="h1" variant="h5" align="center" sx={{ mb: 3, mt: 2 }}>
                        Notifications
                      </Typography>
                      <Divider />

                      <Table sx={{ minWidth: 650, mt: 5 }} aria-label="simple table">
                        <TableHead>
                          <TableRow sx={{ backgroundColor: "#fafafa" }}>
                            <TableCell>Manager Name</TableCell>
                            <TableCell align="right">Leave Type</TableCell>
                            <TableCell align="right">Start Date</TableCell>
                            <TableCell align="right">End Date</TableCell>
                            <TableCell align="right">Total Days</TableCell>
                            <TableCell align="right">Request Comments</TableCell>
                            <TableCell align="right">Status</TableCell>
                          </TableRow>
                        </TableHead>
                        <TableBody>
                          {newData.map((row: any) => (
                            <StyledTableRow
                              key={row.id}
                              className={!row.isViewed ? "is-viewed" : ""}
                              onClick={() => handleRowClick(row.id)}
                            >
                              <TableCell component="th" scope="row">
                                {row.leave.firstName} {row.leave.lastName}
                              </TableCell>
                              <TableCell align="right">{row.leave.leaveTypeName}</TableCell>
                              <TableCell align="right">
                                {new Date(row.leave.startDate).toLocaleDateString()}
                              </TableCell>
                              <TableCell align="right">
                                {new Date(row.leave.endDate).toLocaleDateString()}
                              </TableCell>
                              <TableCell align="right">{row.leave.totalDays}</TableCell>
                              <TableCell align="right">{row.leave.requestComments}</TableCell>
                              <TableCell align="right">{row.leave.statusName}</TableCell>
                            </StyledTableRow>
                          ))}
                        </TableBody>
                      </Table>
                    </TableContainer>
                    {/* <Button
        variant="contained"
        onClick={() => navigate("/leavedetails")}
        sx={{
          position: "fixed",
          bottom: "20px",
          left: "0px",
          mt: 1,
        }}
      >
        Back to Leave Details
      </Button> */}
                  </div>
                </DialogContent>
                <DialogActions>
                  <Button autoFocus onClick={handleNotificationsClick}>
                    View More
                  </Button>
                </DialogActions>
              </BootstrapDialog>

              &nbsp;&nbsp;&nbsp;&nbsp;

              <IconButton
                size="large"
                edge="end"
                color="inherit"
                aria-label="profile"
                onClick={handleClickProfile}
              >
                {imageExists && image ? (
                  <Avatar
                    // src={`https://localhost:7033/${image}`}
                    src={`https://employeeleavetracking.azurewebsites.net/${image}`}
                    alt="Profile Picture"
                  />
                ) : (
                  <AccountCircleIcon />
                )}
                &nbsp;&nbsp;

                {/* {state && state.data && (
                  <Typography variant="h6" component="div" sx={{ flexGrow: 0 }}>
                    {}
                    {state.data.firstName} {state.data.lastName}
                  </Typography>
                )} */}

                {data && data.firstName && (
                  <Typography variant="h6" component="div" sx={{ flexGrow: 0 }}>
                    { }
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







































// import React, { useEffect, useState } from "react";
// import {
//   Drawer,
//   List,
//   ListItemIcon,
//   ListItemText,
//   IconButton,
//   Toolbar,
//   Typography,
//   Avatar,
//   Menu,
//   MenuItem,
// } from "@mui/material";
// import PendingActionsIcon from "@mui/icons-material/PendingActions";
// import AppBar from "@mui/material/AppBar";
// import {
//   Menu as MenuIcon,
//   Dashboard as DashboardIcon,
//   Add as AddIcon,
//   PeopleOutlined as PeopleIcon,
// } from "@mui/icons-material";
// import ListSubheader from "@mui/material/ListSubheader";
// import ListItemButton from "@mui/material/ListItemButton";
// import { useLocation, useNavigate } from "react-router-dom";
// import ViewListIcon from "@mui/icons-material/ViewList";
// import AddHomeWorkSharpIcon from "@mui/icons-material/AddHomeWorkSharp";
// import AccountCircleIcon from "@mui/icons-material/AccountCircle";
// import AddBoxIcon from "@mui/icons-material/AddBox";
// import useHttp from "../config/https";
// import HomeIcon from "@mui/icons-material/Home";
// import InfoIcon from "@mui/icons-material/Info";
// import LogoutIcon from "@mui/icons-material/Logout";
// import { Employee } from "./MyProfile";
// import NotificationsIcon from '@mui/icons-material/Notifications';
// import { profileSubject } from "../components/Auth/profileSubject";
// import { Badge } from "@mui/material";


// const Navigation = () => {
//   const [sidebarOpen, setSidebarOpen] = useState(false);

//   const { state } = useLocation();

//   const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
//   const [open, setOpen] = React.useState(true);
//   const [isLogin, setIsLogin] = React.useState(false);
//   const [isManager, setIsManager] = React.useState(false);
//   const [isEmployee, setIsEmployee] = React.useState(false);
//   const { axiosInstance, loading } = useHttp();
//   const empId = localStorage.getItem("id");
//   const [data, setData] = useState<Employee | undefined>(state);
//   //profile image
//   const [image, setImage] = useState<any>(null);
//   const [imageExists, setImageExists] = useState(false);
//   const [notificationCount, setNotificationCount] = useState(0);

//   const handleNotificationsClick = () => {
//     if (isManager) {
//       navigate("/managernotifications");
//     }
//     else if (isEmployee) {
//       navigate("/employeenotifications");
//     }
//   };

//   useEffect(() => {
//     const fetchNotificationCount = async () => {
//       try {
//         const response = await axiosInstance.get("api/Notifications/count");
//         setNotificationCount(response.data);
//       } catch (error) {
//         console.error(error);
//       }
//     };

//     fetchNotificationCount();
//   }, [state]);


//   useEffect(() => {
//     fetchDetails();
//     const fetchProfilePic = async () => {
//       try {
//         const response = await axiosInstance.get(`api/ProfileImage/${empId}`);
//         console.log(response);
//         setImage(response.data);
//         setImageExists(true);
//       } catch (error) {
//         console.error(error);
//         setImageExists(false);
//       }
//     };
//     fetchProfilePic();
//     getToken()
//   }, [state]);

//   const fetchDetails = async () => {
//     try {
//       const response = await axiosInstance.get(
//         `api/User/currentuserdetails/${localStorage.getItem("id")}`
//       );
//       setData(response.data);
//     } catch (error) {
//       console.error(error);
//     }
//   };

//   const handleClickSidebar = () => {
//     setSidebarOpen(!sidebarOpen);
//   };

//   const handleClickProfile = (event: React.MouseEvent<HTMLElement>) => {
//     setAnchorEl(event.currentTarget);
//   };

//   const handleCloseProfile = () => {
//     setAnchorEl(null);
//   };

//   const navigate = useNavigate();

//   const handleLogout = () => {
//     localStorage.removeItem("token");
//     localStorage.removeItem("role");
//     localStorage.removeItem("id");
//     localStorage.removeItem("refreshToken");
//     setIsLogin(false);
//     navigate("/");
//     window.location.reload();
//   };

//   const getToken = () => {
//     setIsLogin(
//       localStorage.getItem("token") && localStorage.getItem("token") !== ""
//         ? true
//         : false
//     );
//     setIsManager(
//       localStorage.getItem("role") &&
//         localStorage.getItem("role") === "Manager"
//         ? true
//         : false
//     );
//     setIsEmployee(
//       localStorage.getItem("role") &&
//         localStorage.getItem("role") === "Employee"
//         ? true
//         : false
//     );
//   };


//   return (
//     <>
//       <AppBar position="static" color="inherit" style={{ width: "100%" }}>
//         <Toolbar>
//           {isLogin && (
//             <IconButton
//               size="large"
//               edge="start"
//               color="inherit"
//               aria-label="menu"
//               sx={{ mr: 2 }}
//               onClick={handleClickSidebar}
//             >
//               <MenuIcon />
//             </IconButton>
//           )}
//           <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
//             <AddHomeWorkSharpIcon style={{ marginRight: "13px" }} />
//             Employee Leave Tracking
//           </Typography>

//           {isLogin && (
//             <>

//               <Badge badgeContent={notificationCount} color="secondary">
//                 <NotificationsIcon onClick={handleNotificationsClick} />
//               </Badge>

//               &nbsp;&nbsp;&nbsp;&nbsp;

//               <IconButton
//                 size="large"
//                 edge="end"
//                 color="inherit"
//                 aria-label="profile"
//                 onClick={handleClickProfile}
//               >
//                 {imageExists && image ? (
//                   <Avatar
//                     // src={`https://localhost:7033/${image}`}
//                     src={`https://employeeleavetracking.azurewebsites.net/${image}`}
//                     alt="Profile Picture"
//                   />
//                 ) : (
//                   <AccountCircleIcon />
//                 )}
//                 &nbsp;&nbsp;

//                 {/* {state && state.data && (
//                   <Typography variant="h6" component="div" sx={{ flexGrow: 0 }}>
//                     {}
//                     {state.data.firstName} {state.data.lastName}
//                   </Typography>
//                 )} */}

//                 {data && data.firstName && (
//                   <Typography variant="h6" component="div" sx={{ flexGrow: 0 }}>
//                     { }
//                     {data.firstName} {data.lastName}
//                   </Typography>
//                 )}
//               </IconButton>
//               <Menu
//                 id="profile-menu"
//                 anchorEl={anchorEl}
//                 open={Boolean(anchorEl)}
//                 onClose={handleCloseProfile}
//                 PaperProps={{ style: { width: 270, textAlign: "center" } }}
//               >
//                 <MenuItem onClick={() => navigate("/")}>
//                   <HomeIcon /> &nbsp;&nbsp; &nbsp; Home
//                 </MenuItem>
//                 <MenuItem onClick={() => navigate("/about")}>
//                   <InfoIcon /> &nbsp;&nbsp; &nbsp; About
//                 </MenuItem>
//                 <MenuItem onClick={handleLogout}>
//                   <LogoutIcon /> &nbsp;&nbsp; &nbsp; Logout
//                 </MenuItem>
//               </Menu>
//             </>
//           )}
//         </Toolbar>
//       </AppBar>
//       <Drawer
//         anchor="left"
//         open={sidebarOpen}
//         onClose={handleClickSidebar}
//         sx={{
//           width: 240,
//           flexShrink: 0,
//           "& .MuiDrawer-paper": {
//             width: 240,
//             boxSizing: "border-box",
//           },
//         }}
//       >
//         <List
//           sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
//           component="nav"
//           aria-labelledby="nested-list-subheader"
//           subheader={
//             <ListSubheader component="div" id="nested-list-subheader">
//               Dashboard
//             </ListSubheader>
//           }
//         >
//           {isManager ? (
//             <>

//               <ListItemButton onClick={() => navigate("/addemployee")}>
//                 <ListItemIcon>
//                   <AddIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="Add Employee" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/viewemployees")}>
//                 <ListItemIcon>
//                   <ViewListIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="View All Employees" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/newrequests")}>
//                 <ListItemIcon>
//                   <PendingActionsIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="Pending Requests" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/myprofile")}>
//                 <ListItemIcon>
//                   <AccountCircleIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="My Profile" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/compoff")}>
//                 <ListItemIcon>
//                   <AddBoxIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="Comp-Off Request" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/onduty")}>
//                 <ListItemIcon>
//                   <AddBoxIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="On-Duty Request" />
//               </ListItemButton>
//             </>
//           ) : (
//             <>

//               <ListItemButton onClick={() => navigate("/applyforleaves")}>
//                 <ListItemIcon>
//                   <AddBoxIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="Apply Leave" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/leavedetails")}>
//                 <ListItemIcon>
//                   <ViewListIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="Leave Details" />
//               </ListItemButton>

//               <ListItemButton onClick={() => navigate("/myprofile")}>
//                 <ListItemIcon>
//                   <AccountCircleIcon />
//                 </ListItemIcon>
//                 <ListItemText primary="My Profile" />
//               </ListItemButton>
//             </>
//           )}
//         </List>
//       </Drawer>
//     </>
//   );
// };

// export default Navigation;