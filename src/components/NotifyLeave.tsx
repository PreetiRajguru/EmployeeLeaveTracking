import { useLocation } from "react-router-dom";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import useHttp from "../config/https";

import { useNavigate, useParams } from "react-router-dom";
const NotifyLeave = () => {
    const location = useLocation();
    const { selectedRow } = location.state;

    const { axiosInstance, loading } = useHttp();

    const navigate = useNavigate();

    const isPendingStatus = selectedRow.leave.statusName === "Pending";

    const handleApprove = () => {
        axiosInstance.put(`/api/LeaveRequest/${selectedRow.leave.leaveId}/status/2`);
        console.log("Leave request approved");
        navigate("/managernotifications");
    };

    const handleReject = () => {
        axiosInstance.put(`/api/LeaveRequest/${selectedRow.leave.leaveId}/status/3`);
        console.log("Leave request rejected");
        navigate("/managernotifications");
    };

    return (
        <div>
            <h1>Leave Details</h1>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        <TableRow>
                            <TableCell>Employee Name</TableCell>
                            <TableCell>{selectedRow.leave.firstName} {selectedRow.leave.lastName}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Leave Type</TableCell>
                            <TableCell>{selectedRow.leave.leaveTypeName}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Start Date</TableCell>
                            <TableCell>{new Date(selectedRow.leave.startDate).toLocaleDateString()}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>End Date</TableCell>
                            <TableCell>{new Date(selectedRow.leave.endDate).toLocaleDateString()}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Total Days</TableCell>
                            <TableCell>{selectedRow.leave.totalDays}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Request Comments</TableCell>
                            <TableCell>{selectedRow.leave.requestComments}</TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>Status</TableCell>
                            <TableCell>{selectedRow.leave.statusName}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>

            {isPendingStatus && (
                <div>
                    <Button variant="contained" color="success" sx={{ mr: 5, ml: 100, mt: 5 }} onClick={handleApprove}>
                        Approve
                    </Button>
                    <Button variant="contained" color="error" sx={{ mr: 5, ml: 5, mt: 5 }} onClick={handleReject}>
                        Reject
                    </Button>
                </div>
            )}
        </div>
    );
};

export default NotifyLeave;