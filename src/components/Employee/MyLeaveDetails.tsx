import { useState, useEffect, useCallback } from "react";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import { useNavigate } from "react-router-dom";
import { Divider, Card, CardActions, CardContent, Box, TablePagination } from "@mui/material";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import useHttp from "../../config/https";
import Loader from "../Loader/Loader";


const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.info.dark,
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

const rows = [
  {
    firstname: "Preeti",
    lastname: "Rajguru",
    email: "preeti@gmail.com",
  },
];
const DEFAULT_ORDER = 'asc';
const DEFAULT_ORDER_BY = 'employeeName';
const DEFAULT_ROWS_PER_PAGE = 5;

export default function CustomizedTables() {
  const navigate = useNavigate();
  const [data, setData] = useState<any>([]);
  const [leaveBalance, setLeaveBalance] = useState();
  const [type, setType] = useState<any>([]);
  const [dense, setDense] = useState(false);
  const { axiosInstance, loading } = useHttp();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const empId = localStorage.getItem('id');
  var colors = ['#afd6d0', '#d3afd6', '#edbcbc', '#c7ebba', '#e8e4a6', '#f0c7f4', '#f7d2a7', '#e3aff4'];

  useEffect(() => {
    const fetchLeaveDetails = async () => {
      try {
        const response = await axiosInstance.get(`/api/LeaveRequest/employee/${empId}`);
        setData(response.data);
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    const fetchLeaveTypesTotal = async () => {
      try {
        const response = await axiosInstance.get(
          "/api/LeaveType/employee/leavetypes"
        );
        setType(response.data);
        console.log(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLeaveDetails();
    fetchLeaveTypesTotal();
  }, []);

  console.log(type)
  const carddetails = type.length > 0 ? type.map((val: { leaveTypeName: any; bookedDays: any; availableDays: any; }): any => {
    return {
      header: val.leaveTypeName,
      bookedDays: val.bookedDays,
      availableDays: val.availableDays
    }
  }) : []


  // const handleChangePage = useCallback(
  //   (event: unknown, newPage: number) => {
  //     setPage(newPage);


  //     const fetchLeaveDetails = async () => {
  //       try {
  //         const response = await axiosInstance.get(`/api/LeaveRequest/employeeId/${rowsPerPage}/${rowsPerPage * newPage}`);
  //         setData(response.data);
  //       } catch (error) {
  //         console.error(error);
  //       }
  //     };
  //   },
  //   [ dense, rowsPerPage],
  // );
  const handleChangePage = (event: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

  return (
    <>
      {loading ? <Loader /> :
        <TableContainer component={Paper}>
          <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
            My Leave Details
          </Typography>
          <Divider />

          <div style={{ display: "flex" }}>
            {carddetails.map((val: any) => (
              <CardContent
                style={{
                  border: "1px solid lightgrey",
                  borderRadius: "15px",
                  width: "230px",
                  height: "220px",
                  display: "flex",
                  flexDirection: "column",
                  alignItems: "center",
                  justifyContent: "center",
                }}
                sx={{
                  display: "inline-block",
                  mx: "2px",
                  transform: "scale(0.8)",
                }}
              >
                <Typography
                  sx={{ fontSize: 22 }}
                  color="text.secondary"
                  gutterBottom
                >
                  {val.header}
                </Typography>
                <Typography variant="h5" component="div"></Typography>
                <CalendarMonthIcon
                  style={{ width: "50%", height: "50%", color: colors[Math.floor(Math.random() * colors.length)] }}
                />
                <Typography sx={{ fontSize: 20 }} variant="body2" component="h1">
                  Available: {val.availableDays}
                </Typography>

                <Typography sx={{ fontSize: 20 }} variant="body2" component="h1">
                  Booked: {val.bookedDays}
                </Typography>

              </CardContent>
            ))}
          </div>

          <div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', justifyContent: 'center', }}>
            <Typography component="h1" variant="h6" align="center" sx={{ mb: 3 }}>
              Leave Requests
              <Button
                variant="outlined"
                onClick={() => navigate("/applyforleaves")}
                size="medium"
                sx={{ ml: 120 }}
              >
                Apply For Leaves
              </Button>
            </Typography>
          </div>
          <Box sx={{ margin: '0 16px' }}>
            <Table sx={{ minWidth: 700, border: '1px solid #ddd' }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <StyledTableCell align="left">Leave Type</StyledTableCell>
                  <StyledTableCell align="left">Comments</StyledTableCell>
                  <StyledTableCell align="right">Start Date</StyledTableCell>
                  <StyledTableCell align="right">End Date</StyledTableCell>
                  <StyledTableCell align="right">Total Days</StyledTableCell>
                  <StyledTableCell align="right">Status</StyledTableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {/* {data?.map((row: any) => ( */}
                {(rowsPerPage > 0
                  ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  : data
                ).map((row: any) => (

                  <StyledTableRow key={row.id}>
                    <StyledTableCell align="left">
                      {row.leaveTypeName}
                    </StyledTableCell>
                    <StyledTableCell component="th" scope="row">
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
                      {row.statusName}
                    </StyledTableCell>
                  </StyledTableRow>
                ))}
                {emptyRows > 0 && (
                  <StyledTableRow style={{ height: 53 * emptyRows }}>
                    <StyledTableCell colSpan={6} />
                  </StyledTableRow>
                )}
              </TableBody>
            </Table>
          </Box>
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
      }
    </>
  );
}