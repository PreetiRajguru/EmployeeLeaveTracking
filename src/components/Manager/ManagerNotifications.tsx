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
import { Modal, TablePagination } from "@mui/material";
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
        const response = await axiosInstance.get(`api/Notifications/all`);
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
              <TableCell>Employee Name</TableCell>
              <TableCell align="right">Leave Type</TableCell>
              <TableCell align="right">Status</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {(rowsPerPage > 0
            ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            : data
          ).map((row: any, index: number) => (
              <StyledTableRow
                key={row.id}
                className={!row.isViewed ? "is-viewed" : ""}
                onClick={() => handleRowClick(row.id)}
              >
                <TableCell component="th" scope="row">
                  {row.leave.firstName} {row.leave.lastName}
                </TableCell>
                <TableCell align="right">{row.leave.leaveTypeName}</TableCell>
                <TableCell align="right">{row.leave.statusName}
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
    </div>
  );
};

export default ManagerNotifications;
