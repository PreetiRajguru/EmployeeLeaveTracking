import { useState, useEffect } from "react";
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

export default function CustomizedTables() {
  // const { empId } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any>([]);
  const [leaveBalance, setLeaveBalance] = useState();

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
        const response = await axios.get(
          "/api/LeaveRequest/employee/8922e768-ed48-43ec-8740-9201c0fdae46"
        );
        setData(response.data);
        console.log(data);
      } catch (error) {
        console.error(error);
      }
    };

    const fetchLeaveBalances = async () => {
      try {
        // const response = await axios.get(`/api/LeaveRequest/balance/${empId}`);
        // setLeaveBalance(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchLeaveDetails();
    fetchLeaveBalances();
  }, []);

  const carddetails = [
    {
      header: "Paid Leaves",
      color: "red",
    },
    {
      header: "Unpaid Leaves",
      color: "grey",
    },
    {
      header: "Sick Leaves",
      color: "green",
    },
    {
      header: "Personal Leave",
      color: "purple",
    },
    {
      header: "Study Leave",
      color: "brown",
    },
    {
      header: "Bereavement Leaves",
      color: "lightblue",
    },
  ];

  return (
    <>
      {/* <BasicCard /> */}
      <TableContainer component={Paper}>
        <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
          My Leave Details
        </Typography>
        <Divider />

        <div style={{ display: "flex" }}>
          {carddetails.map((val) => (
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
                style={{ width: "60%", height: "60%", color: `${val.color}` }}
              />
              <Typography variant="body2">
                well meaning and kindly.
                <br />
                {'"a benevolent smile"'}
              </Typography>
            </CardContent>
          ))}
        </div>
        
        <div style={{display: 'flex', flexDirection: 'row', alignItems: 'center', justifyContent: 'center',  }}>
        <Typography component="h1" variant="h6" align="center" sx={{ mb: 3 }}>
          Employee Leave Requests
          <Button
            variant="outlined"
            onClick={() => navigate("/applyforleaves")}
            size="small"
            sx={{ml: 110}}
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
                {/* <StyledTableCell align="right">
                                    <Button variant="outlined" onClick={() => navigate("/viewempdetails")}>View Details</Button>
                                </StyledTableCell> */}
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
