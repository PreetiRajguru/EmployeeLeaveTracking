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

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  "&:nth-of-type(odd)": {
    backgroundColor: theme.palette.action.hover,
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
    const fetchLeaveDetails = async () => {
      try {
        const response = await axiosInstance.get(`api/Notifications/unviewed`);
        setData(response.data);
        console.log("data", data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchLeaveDetails();
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

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number
  ) => {
    setPage(newPage);
    // window.location.reload();
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
    // window.location.reload();
  };

  const emptyRows =
    rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

  return (
    <div>
      <TableContainer component={Paper}>
        <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
          Manager Notifications
        </Typography>
        <Divider />
        <Table
          sx={{ minWidth: 700, border: "15px solid white" }}
          aria-label="simple table"
        >
          <TableHead>
            <TableRow>
              <StyledTableCell>User Id</StyledTableCell>
              <StyledTableCell align="right">Leave Request Id</StyledTableCell>
              <StyledTableCell align="right">Notification Type Id</StyledTableCell>
              <StyledTableCell align="right"> - </StyledTableCell>
              <StyledTableCell align="right"> - </StyledTableCell>
              <StyledTableCell align="right"> - </StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {(rowsPerPage > 0
              ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              : data
            ).map((row: any) => (
              <StyledTableRow key={row.UserId}>
                <StyledTableCell component="th" scope="row">
                  {row.UserId}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.LeaveRequestId}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.NotificationTypeId}
                </StyledTableCell>
                <StyledTableCell align="right">

                </StyledTableCell>
                <StyledTableCell align="right">{row.totalDays}</StyledTableCell>
                <StyledTableCell align="right">
                  {statusId === 2 || row.statusName === "Approved" ? (
                    <Typography variant="body1" color="success">
                      Approved
                    </Typography>
                  ) : statusId === 3 || row.statusName === "Rejected" ? (
                    <Typography variant="body1" color="error">
                      Rejected
                    </Typography>
                  ) : row.statusName === "Pending" ? (
                    <div>
                      <Button
                        sx={{ mr: 2 }}
                        variant="contained"
                        color="success"
                        onClick={() => fetchStatus(row.id, 2)}
                      >
                        Approve
                      </Button>
                      <Button
                        variant="contained"
                        color="error"
                        onClick={() => fetchStatus(row.id, 3)}
                      >
                        Reject
                      </Button>
                    </div>
                  ) : (
                    <div>
                      <Button
                        sx={{ mr: 2 }}
                        variant="contained"
                        color="success"
                        onClick={() => fetchStatus(row.id, 2)}
                      >
                        Approve
                      </Button>
                      <Button
                        variant="contained"
                        color="error"
                        onClick={() => fetchStatus(row.id, 3)}
                      >
                        Reject
                      </Button>
                    </div>
                  )}
                </StyledTableCell>
              </StyledTableRow>
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
