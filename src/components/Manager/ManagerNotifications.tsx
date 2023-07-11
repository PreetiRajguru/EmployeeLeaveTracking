// import { useState, useEffect } from "react";
// import Button from "@mui/material/Button";
// import { useNavigate, useParams } from "react-router-dom";
// import { styled } from "@mui/material/styles";
// import Table from "@mui/material/Table";
// import TableBody from "@mui/material/TableBody";
// import TableCell, { tableCellClasses } from "@mui/material/TableCell";
// import TableContainer from "@mui/material/TableContainer";
// import TableHead from "@mui/material/TableHead";
// import TableRow from "@mui/material/TableRow";
// import Paper from "@mui/material/Paper";
// import Typography from "@mui/material/Typography";
// import Divider from "@mui/material/Divider";
// import useHttp from "../../config/https";
// import { IconButton, TablePagination } from "@mui/material";
// import { blue, pink } from "@mui/material/colors";

// const StyledTableCell = styled(TableCell)(({ theme }) => ({
//   [`&.${tableCellClasses.head}`]: {
//     backgroundColor: theme.palette.info.light,
//     color: theme.palette.common.white,
//   },
//   [`&.${tableCellClasses.body}`]: {
//     fontSize: 14,
//   },
// }));

// const StyledTableRow = styled(TableRow)(({ theme }) => ({
//   "&:nth-of-type(odd)": {
//     backgroundColor: theme.palette.action.hover,
//   },
//   // hide last border
//   "&:last-child td, &:last-child th": {
//     border: 0,
//   },
// }));

// const ManagerNotifications = () => {
//   const { empId } = useParams();
//   const navigate = useNavigate();
//   const [data, setData] = useState<any[]>([]);
//   const { axiosInstance, loading } = useHttp();
//   const [statusId, setStatusId] = useState();
//   const [page, setPage] = useState(0);
//   const [rowsPerPage, setRowsPerPage] = useState(5);

//   useEffect(() => {
//     const fetchNotificationDetails = async () => {
//       try {
//         const response = await axiosInstance.get(`api/Notifications/manager`);
//         setData(response.data);
//         console.log(response.data);
//       } catch (error) {
//         console.error(error);
//       }
//     };
//     fetchNotificationDetails();
//   }, []);

//   const fetchStatus = async (rowId: any, param: any) => {
//     try {
//       await axiosInstance.put(`/api/LeaveRequest/${rowId}/status/${param}`);
//       const updatedResponse = await axiosInstance.get(
//         `/api/LeaveRequest/${rowId}`
//       );
//       const updatedStatus = updatedResponse.data.status;
//       setStatusId(updatedStatus);
//     } catch (error) {
//       console.log(error);
//     }
//     window.location.reload();
//   };


//   return (
//     <div>
//       <TableContainer component={Paper}>
//         <Typography component="h1" variant="h5" align="center" sx={{ mb: 3, mt: 2}}>
//           Manager Notifications
//         </Typography>
//         <Divider />

//         <Table sx={{ minWidth: 650, mt: 5 }} aria-label="simple table">
//           <TableHead>
//             <TableRow sx={{ backgroundColor: "#03a9f4" }}>
//               <TableCell>Employee Name</TableCell>
//               <TableCell align="right">Leave Type</TableCell>
//               <TableCell align="right">Start Date</TableCell>
//               <TableCell align="right">End Date</TableCell>
//               <TableCell align="right">Total Days</TableCell>
//               <TableCell align="right">Request Comments</TableCell>
//               <TableCell align="right">Status</TableCell>
//               <TableCell align="right">IsViewed</TableCell>
//             </TableRow>
//           </TableHead>
//           <TableBody>
//             {data.map((row: any) => (
//               <TableRow
//                 key={row.isViewed}
//                 sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
//               >
//                 <TableCell component="th" scope="row">
//                   {row.leave.firstName}   {row.leave.lastName}
//                 </TableCell>
//                 <TableCell align="right">{row.leave.leaveTypeName}</TableCell>
//                 <TableCell align="right">
//                   {new Date(row.leave.startDate).toLocaleDateString()}
//                 </TableCell>
//                 <TableCell align="right">
//                   {new Date(row.leave.endDate).toLocaleDateString()}
//                 </TableCell>
//                 <TableCell align="right">{row.leave.totalDays}</TableCell>
//                 <TableCell align="right">{row.leave.requestComments}</TableCell>
//                 <TableCell align="right">{row.leave.statusName}</TableCell>
//                 <TableCell align="right">{row.isViewed ? "True" : "False"}</TableCell>

//                 <TableCell align="right"><Typography variant="h4" sx={row.isViewed === true ? blue : pink}> {row.isViewed} </Typography> </TableCell>


//           </TableRow>
//           ))}
//         </TableBody>
//       </Table>
//     </TableContainer><Button
//       variant="contained"
//       onClick={() => navigate("/viewemployees")}
//       sx={{
//         position: "fixed",
//         bottom: "20px",
//         left: "0px",
//         mt: 1,
//       }}
//     >
//         Back to Employee List
//       </Button>
//     </div>
//   );
// };

// export default ManagerNotifications;




import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import { useNavigate, useParams } from "react-router-dom";
import { styled } from "@mui/system";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import Divider from "@mui/material/Divider";
import useHttp from "../../config/https";
import { IconButton, TablePagination } from "@mui/material";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.info.light,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const CustomTableRow = styled(({ isViewed, ...props }:any) => (
  <TableRow {...props} />
))(({ theme, isViewed }) => ({
  "&:nth-of-type(odd)": {
    backgroundColor: isViewed
      ? theme.palette.primary.light
      : theme.palette.secondary.light,
  },
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

const ManagerNotifications = () => {
  const { empId } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any[]>([]);
  const { axiosInstance, loading } = useHttp();
  const [statusId, setStatusId] = useState();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  useEffect(() => {
    const fetchNotificationDetails = async () => {
      try {
        const response = await axiosInstance.get(`api/Notifications/manager`);
        setData(response.data);
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchNotificationDetails();
  }, []);

  const fetchStatus = async (rowId: any, param: any) => {
    try {
      await axiosInstance.put(`/api/LeaveRequest/${rowId}/status/${param}`);
      const updatedResponse = await axiosInstance.get(
        `/api/LeaveRequest/${rowId}`
      );
      const updatedStatus = updatedResponse.data.status;
      setStatusId(updatedStatus);
    } catch (error) {
      console.log(error);
    }
    window.location.reload();
  };

  return (
    <div>
      <TableContainer component={Paper}>
        <Typography component="h1" variant="h5" align="center" sx={{ mb: 3, mt: 2 }}>
          Manager Notifications
        </Typography>
        <Divider />

        <Table sx={{ minWidth: 650, mt: 5 }} aria-label="simple table">
          <TableHead>
            <TableRow sx={{ backgroundColor: "#03a9f4" }}>
              <StyledTableCell>Employee Name</StyledTableCell>
              <StyledTableCell align="right">Leave Type</StyledTableCell>
              <StyledTableCell align="right">Start Date</StyledTableCell>
              <StyledTableCell align="right">End Date</StyledTableCell>
              <StyledTableCell align="right">Total Days</StyledTableCell>
              <StyledTableCell align="right">Request Comments</StyledTableCell>
              <StyledTableCell align="right">Status</StyledTableCell>
              <StyledTableCell align="right">IsViewed</StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((row: any) => (
              <CustomTableRow
                key={row.isViewed}
                isViewed={row.isViewed}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <StyledTableCell component="th" scope="row">
                  {row.leave.firstName} {row.leave.lastName}
                </StyledTableCell>
                <StyledTableCell align="right">{row.leave.leaveTypeName}</StyledTableCell>
                <StyledTableCell align="right">
                  {new Date(row.leave.startDate).toLocaleDateString()}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {new Date(row.leave.endDate).toLocaleDateString()}
                </StyledTableCell>
                <StyledTableCell align="right">{row.leave.totalDays}</StyledTableCell>
                <StyledTableCell align="right">{row.leave.requestComments}</StyledTableCell>
                <StyledTableCell align="right">{row.leave.statusName}</StyledTableCell>
                <StyledTableCell align="right">
                  {row.isViewed ? "True" : "False"}
                </StyledTableCell>
              </CustomTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <Button
        variant="contained"
        onClick={() => navigate("/viewemployees")}
        sx={{
          position: "fixed",
          bottom: "20px",
          left: "0px",
          mt: 1,
        }}
      >
        Back to Employee List
      </Button>
    </div>
  );
};

export default ManagerNotifications;
