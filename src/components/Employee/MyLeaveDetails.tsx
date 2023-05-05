import { useState, useEffect, useCallback } from "react";
import axios from "axios";
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
import { Divider, Card, CardActions, CardContent } from "@mui/material";
import CalendarMonthIcon from "@mui/icons-material/CalendarMonth";
import TablePagination from '@mui/material/TablePagination';
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Switch from "@material-ui/core/Switch";

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
  // const { empId } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any>([]);
  const [leaveBalance, setLeaveBalance] = useState();
  const [type, setType] = useState<any>([]);
  
  const [page, setPage] = useState(0);
  const [dense, setDense] = useState(false);
  const [visibleRows, setVisibleRows] = useState(null);
  const [rowsPerPage, setRowsPerPage] = useState(DEFAULT_ROWS_PER_PAGE);
  
  const empId = localStorage.getItem('id');
  var colors = ['red', 'green', 'blue', 'orange', 'yellow', 'grey', 'brown'];

  function BasicCard() {
    return (
      <Card sx={{ minWidth: 275 }}>
        <CardContent>
          <Typography variant="h6" color="text.secondary" gutterBottom>
            Employee Details
          </Typography>
          <Divider sx={{ mb: 1 }} />
          <Typography component="div" sx={{ fontSize: 14, mb: 0.5 }}>
            Preeti Rajguru
          </Typography>
          <Typography sx={{ mb: 1, fontSize: 14 }} color="text.secondary">
            preeti@gmail.com
          </Typography>
          <Typography variant="body2" sx={{ mb: 1, fontSize: 14 }}>
            Phone.No:7654567543
            <br></br>
            Wadgaon Sheri, Pune - 41
          </Typography>
        </CardContent>
      </Card>
    );
  }

  useEffect(() => {
    const fetchLeaveDetails = async () => {
      try {
        const response = await axios.get(`/api/LeaveRequest/employee/${empId}`);
        setData(response.data);
      } catch (error) {
        console.error(error);
      }
    };


    const fetchLeaveTypesTotal = async () => {
      try {
        const response = await axios.get(
          "/api/LeaveType/employee/leavetypes"
        );
        setType(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    const fetchLeaveBalances = async () => {
      try {
        const response = await axios.get(`/api/LeaveRequest/balance/${empId}`);
        setLeaveBalance(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLeaveDetails();
    fetchLeaveBalances();
    fetchLeaveTypesTotal();
  }, []);

  // const carddetails1 = [
  //   {
  //     header: "Paid Leaves",
  //     color: "red",
  //   },
  //   {
  //     header: "Unpaid Leaves",
  //     color: "grey",
  //   },
  //   {
  //     header: "Sick Leaves",
  //     color: "green",
  //   },
  //   {
  //     header: "Personal Leave",
  //     color: "purple",
  //   },
  //   {
  //     header: "Study Leave",
  //     color: "brown",
  //   },
  //   {
  //     header: "Bereavement Leaves",
  //     color: "lightblue",
  //   },
  // ];

  const carddetails = type?.map((val: { leaveTypeName: any; totalDaysTaken: any; }): any => {return {
    header: val.leaveTypeName,
    totalDaysTaken: val.totalDaysTaken 
  }})
  

  const handleChangePage = useCallback(
    (event: unknown, newPage: number) => {
      setPage(newPage);

      const updatedRows = data.slice(
        newPage * rowsPerPage,
        newPage * rowsPerPage + rowsPerPage,
      );
      setVisibleRows(updatedRows);

      // Avoid a layout jump when reaching the last page with empty rows.
      const numEmptyRows =
        newPage > 0 ? Math.max(0, (1 + newPage) * rowsPerPage - rows.length) : 0;

      const newPaddingHeight = (dense ? 33 : 53) * numEmptyRows;
    },
    [ dense, rowsPerPage],
  );

  const handleChangeRowsPerPage = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      const updatedRowsPerPage = parseInt(event.target.value, 10);
      setRowsPerPage(updatedRowsPerPage);

      setPage(0);

      const updatedRows = data.slice(
        0 * updatedRowsPerPage,
        0 * updatedRowsPerPage + updatedRowsPerPage,
      );
      setVisibleRows(updatedRows);

    },
    [],
  );

  return (
    <>
      {/* <BasicCard /> */}
      <TableContainer component={Paper}>
        <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
          My Leave Details
        </Typography>
        <Divider />

        <h2>Leave Balance: {leaveBalance} </h2>
        <div style={{ display: "flex" }}>
          {carddetails.map((val:any) => (
            <CardContent
              style={{
                border: "1px solid lightgrey",
                borderRadius: "15px",
                width: "230px",
                height: "270px",
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
                sx={{ fontSize: 20 }}
                color="text.secondary"
                gutterBottom
              >
                {val.header}
              </Typography>
              <Typography variant="h5" component="div"></Typography>
              <CalendarMonthIcon
                style={{ width: "60%", height: "60%", color: colors[Math.floor(Math.random() * colors.length)] }}
              />
              <Typography variant="body2" component="h1">
                {val.totalDaysTaken}
                {/* <br /> */}
              </Typography>
            </CardContent>
          ))}
        </div>
        
        <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', justifyContent: 'center',  }}>
        <Typography component="h1" variant="h6" align="center" sx={{ mb: 3 }}>
          Leave Requests
          <Button
            variant="outlined"
            onClick={() => navigate("/applyforleaves")}
            size="small"
            sx={{ml: 120}}
          >
            Apply For Leaves
          </Button>
        </Typography>
        </div>
        <Table sx={{ minWidth: 700 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <StyledTableCell>Employee Name</StyledTableCell>
              <StyledTableCell align="right">Start Date</StyledTableCell>
              <StyledTableCell align="right">End Date</StyledTableCell>
              <StyledTableCell align="right">Leave Type</StyledTableCell>
              <StyledTableCell align="right">Total Days</StyledTableCell>
              <StyledTableCell align="right">Manager Name</StyledTableCell>
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
                  {new Date(row.startDate).toLocaleDateString()}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {new Date(row.endDate).toLocaleDateString()}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.leaveTypeName}
                </StyledTableCell>
                <StyledTableCell align="right">{row.totalDays}</StyledTableCell>
                <StyledTableCell align="right">
                  {row.managerName}
                </StyledTableCell>
                <StyledTableCell align="right">
                  {row.statusName}
                </StyledTableCell>
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
    </>
  );
}
