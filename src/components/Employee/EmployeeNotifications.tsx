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
import { TablePagination } from "@mui/material";

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
        const response = await axiosInstance.get(`api/Notifications/all`);
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

  const handleRowClick = async (rowId: any) => {
  try {
      console.log(data)
      console.log("Row Id : ",rowId)
    await axiosInstance.post(`/api/notifications/${rowId}/viewed`);
    const updatedData = data.map((row: any) => {
      if (row.id === rowId) {
        return {
          ...row,
          isViewed: true,
        };
      }
      return row;
    });
    setData(updatedData);
  } catch (error) {
    console.log(error);
  }
  window.location.reload();
};

const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
  setPage(newPage);
};

const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
  setRowsPerPage(parseInt(event.target.value, 10));
  setPage(0);
};

const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

  return (
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
            {/* {data.map((row: any) => ( */}
            {(rowsPerPage > 0
            ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            : data
          ).map((row: any) => (
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
            {emptyRows > 0 && (
              <StyledTableRow style={{ height: 53 * emptyRows }}>
                <StyledTableCell colSpan={6} />
              </StyledTableRow>
            )}
          </TableBody>
        </Table>
        <TablePagination
        rowsPerPageOptions={[5, 10, 25]}
        component="div"
        count={data.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
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
  );
};

export default EmployeeNotifications;