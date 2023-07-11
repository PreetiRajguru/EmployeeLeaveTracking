import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import { useNavigate, useParams } from "react-router-dom";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import useHttp from "../../config/https";
import { Typography, Divider } from "@mui/material";

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
  "&:nth-of-type(odd)": {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

const EmployeeNotifications = () => {
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
        const response = await axiosInstance.get(`api/Notifications/employee`);
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
      <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 ,mt:2}}>
          Employee Notifications
        </Typography>
        <Divider />

      <Table sx={{ minWidth: 650 ,mt: 5}} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Manager Name</TableCell>
            <TableCell align="right">Leave Type</TableCell>
            <TableCell align="right">Start Date</TableCell>
            <TableCell align="right">End Date</TableCell>
            <TableCell align="right">Total Days</TableCell>
            <TableCell align="right">Request Comments</TableCell>
            <TableCell align="right">Status</TableCell>
            <TableCell align="right">IsViewed</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row:any) => (
            <TableRow
              key={row.name}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.leave.firstName}   {row.leave.lastName}
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
              <TableCell align="right">{row.isViewed ? "True" : "False"}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
      <Button
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
      </Button>
    </div>
  );
};

export default EmployeeNotifications;
