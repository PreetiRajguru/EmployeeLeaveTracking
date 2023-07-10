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

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    // backgroundColor: theme.palette.info.dark,
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

const ViewEmployeeDetails = () => {
  const { empId } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any>([]);
  const {axiosInstance, loading} = useHttp();
  const [statusId, setStatusId] = useState();
  useEffect(() => {
    const fetchLeaveDetails = async () => {
      try {
        const response = await axiosInstance.get(`/api/LeaveRequest/employee/${empId}`);
        setData(response.data);
        console.log(data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLeaveDetails();
  }, []);

  // const fetchStatus = (rowId: any, param: any) => {
  //   axiosInstance
  //     .put(`/api/LeaveRequest/${rowId}/status/${param}`)
  //     .then((response) => setStatusId(response.data))
  //     .catch((error) => console.log(error));

  //     window.location.reload()
  // };


  const fetchStatus = async (rowId: any, param: any) => {
    try {
      await axiosInstance.put(`/api/LeaveRequest/${rowId}/status/${param}`);
      const updatedResponse = await axiosInstance.get(`/api/LeaveRequest/${rowId}`);
      const updatedStatus = updatedResponse.data.status;
      setStatusId(updatedStatus);
    } catch (error) {
      console.log(error);
    }
    window.location.reload()
  };
  

  return (
    <div>
      <TableContainer component={Paper}>
        <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
          Employee Leave Details
        </Typography>
        <Divider />
        {/* simple table , customized table*/}
        {/* <Table sx={{ minWidth: 700 }} aria-label="simple table"> */}
        <Table sx={{ minWidth: 700, border: '15px solid white' }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <StyledTableCell>Employee Name</StyledTableCell>
              <StyledTableCell align="right">Leave Type</StyledTableCell>
              <StyledTableCell align="right">Comments</StyledTableCell>
              <StyledTableCell align="right">Start Date</StyledTableCell>
              <StyledTableCell align="right">End Date</StyledTableCell>
              <StyledTableCell align="right">Total Days</StyledTableCell>
              <StyledTableCell align="right">Status</StyledTableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data?.map((row: any) => (
              <StyledTableRow key={row.employeeName}>
                <StyledTableCell component="th" scope="row">
                  {row.employeeName}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.leaveTypeName}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.requestComments}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {new Date(row.startDate).toLocaleDateString()}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {new Date(row.endDate).toLocaleDateString()}
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
                  ) : row.statusName == "Pending" ? (
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
          // right: "20px",
          left: "0px",
          mt: 1,
        }}
      >
        Back to Employee List
      </Button>
    </div>
  );
};

export default ViewEmployeeDetails;

// "No Leaves Taken By This Employee"