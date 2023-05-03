import { useState, useEffect } from "react";
import axios from "axios";
import { styled } from '@mui/material/styles';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';
import Typography from "@mui/material/Typography";
import { useNavigate } from 'react-router-dom';
import {
    Divider,
    Card,
    CardActions,
    CardContent
} from '@mui/material'

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
    '&:nth-of-type(odd)': {
        backgroundColor: theme.palette.action.hover,
    },
    // hide last border
    '&:last-child td, &:last-child th': {
        border: 0,
    },
}));

const rows = [
    {
        firstname: "Preeti",
        lastname: "Rajguru",
        email: "preeti@gmail.com"
    }
];


export default function CustomizedTables() {
    const navigate = useNavigate();
    const [data, setData] = useState<any>([]);

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
                        preeti@ix.com
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
                const response = await axios.get("/api/LeaveRequest/employee/8922e768-ed48-43ec-8740-9201c0fdae46");
                setData(response.data);
                console.log(data)
            } catch (error) {
                console.error(error);
            }
        };

        fetchLeaveDetails();
    }, []);
    return (
        <>
            <BasicCard />
            <TableContainer component={Paper}>
            <Button variant="outlined" onClick={() => navigate("/applyforleaves")}>Apply For Leaves</Button>

                <Typography component="h1" variant="h5" align="center" sx={{ mb: 3 }}>
                    My Leave Details
                </Typography>
                <Divider />
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
                                    {row.startDate}
                                    {/* new Date(matter.date).toLocaleDateString() */}
                                </StyledTableCell>
                                <StyledTableCell align="right">
                                    {row.endDate}
                                </StyledTableCell>
                                <StyledTableCell align="right">
                                    {row.leaveTypeName}
                                </StyledTableCell>
                                <StyledTableCell align="right">
                                    {row.totalDays}
                                </StyledTableCell>
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