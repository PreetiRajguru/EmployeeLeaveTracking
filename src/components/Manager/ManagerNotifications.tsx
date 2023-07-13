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
import { Modal } from "@mui/material";
import { Box } from "@mui/system";

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
    backgroundColor: "#DDDDDD",
  },
}));

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  // bgcolor: 'background.paper',
  bgcolor: 'white',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const ManagerNotifications = () => {
  const { empId } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<any[]>([]);
  const { axiosInstance, loading } = useHttp();
  const [statusId, setStatusId] = useState();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const [openArray, setOpenArray] = useState<boolean[]>([]); // Array of open states

  useEffect(() => {
    const fetchNotificationDetails = async () => {
      try {
        const response = await axiosInstance.get(`api/Notifications/manager`);
        setData(response.data);
        console.log(response.data);
        // Initialize the open state for each row
        setOpenArray(response.data.map(() => false));
      } catch (error) {
        console.error(error);
      }
    };
    fetchNotificationDetails();
  }, []);

  
  const handleRowClick = async (rowId: any) => {
    try {
      const selectedRow = data.find((row) => row.id === rowId);
      
      //sending state to next comp
      navigate("/notifyleave", { state: { selectedRow } });

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
  };

  const handleOpen = (index: number) => {
    const updatedOpenArray = openArray.map((item, i) => i === index);
    setOpenArray(updatedOpenArray);
  };

  const handleClose = () => {
    setOpenArray(openArray.map(() => false));
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
            <TableRow sx={{ backgroundColor: "#fafafa" }}>
              <TableCell>Employee Name</TableCell>
              <TableCell align="right">Leave Type</TableCell>
              {/* <TableCell align="right">Start Date</TableCell>
              <TableCell align="right">End Date</TableCell> */}
              {/* <TableCell align="right">Total Days</TableCell> */}
              {/* <TableCell align="right">Request Comments</TableCell> */}
              <TableCell align="right">Status</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((row: any, index: number) => (
              <StyledTableRow
                key={row.id}
                className={!row.isViewed ? "is-viewed" : ""}
                onClick={() => handleRowClick(row.id)}
              >
                <TableCell component="th" scope="row">
                  {row.leave.firstName} {row.leave.lastName}
                </TableCell>
                <TableCell align="right">{row.leave.leaveTypeName}</TableCell>
                {/* <TableCell align="right">
                  {new Date(row.leave.startDate).toLocaleDateString()}
                </TableCell>
                <TableCell align="right">
                  {new Date(row.leave.endDate).toLocaleDateString()}
                </TableCell>
                <TableCell align="right">{row.leave.totalDays}</TableCell>
                <TableCell align="right">{row.leave.requestComments}</TableCell> */}
                <TableCell align="right">{row.leave.statusName}
                  {/* {row.leave.statusId === 1 ? (
                    <Button variant="contained" sx={{ ml: 2 }} onClick={() => handleOpen(index)}>
                      {row.leave.statusName}
                    </Button>
                  ) : (
                    row.leave.statusName
                  )} */}
                </TableCell>
                
                <Modal
                  open={openArray[index]}
                  onClose={handleClose}
                  aria-labelledby="modal-modal-title"
                  aria-describedby="modal-modal-description"
                >
                  <Box sx={style}>
                    <Typography
                      id="modal-modal-title"
                      variant="h6"
                      component="h2"
                    >
                      Leave Request 
                    </Typography>
                    <br></br>
                    {/* <Typography
                      id="modal-modal-description"
                      sx={{ mt: 2 }}
                    >
                      Duis mollis, est non commodo luctus, nisi erat
                      porttitor ligula.
                    </Typography> */}
                    <Button variant="outlined" color="success" sx={{mb:0, ml:0}}>Approve</Button>
                    <Button variant="outlined" color="error"sx={{mb:0, ml:25}}>Reject</Button>
                  </Box>
                </Modal>
              </StyledTableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
        
    </div>
  );
};

export default ManagerNotifications;
