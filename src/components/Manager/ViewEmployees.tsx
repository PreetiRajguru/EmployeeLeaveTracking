
import { useState, useEffect, Key, ReactNode } from "react";
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
import jsPDF from "jspdf";
import "jspdf-autotable";
import { IconButton, TablePagination } from "@mui/material";
import PictureAsPdfIcon from "@mui/icons-material/PictureAsPdf";
import useHttp from "../../config/https";

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

export interface Employee {
  userName: Key | null | undefined;
  firstName: ReactNode;
  lastName: ReactNode;
  email: ReactNode;
  phoneNumber: ReactNode;
  managerId: number;
  id: string;
  designationId: number;
  designationName: ReactNode;
}

export default function CustomizedTables() {
  const navigate = useNavigate();
  const { axiosInstance, loading } = useHttp();
  const [data, setData] = useState<Employee[]>([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  useEffect(() => {
    const managerId = localStorage.getItem("id");
    const fetchAllEmployeesForManager = async () => {
      try {
        const response = await axiosInstance.get(`api/User/employees/${managerId}`);
        console.log(response);
        setData(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchAllEmployeesForManager();
    console.log(data);
  }, []);

  const exportPDF = () => {
    const unit = "pt";
    const size = "A4"; // Use A1, A2, A3 or A4
    const orientation = "portrait"; // portrait or landscape

    const marginLeft = 40;
    const doc = new jsPDF(orientation, unit, size);

    doc.setFontSize(15);

    const title = "My Employee Report";
    const headers = [["FIRSTNAME", "LASTNAME", "USERNAME", "EMAIL", "PHONE NUMBER"]];

    const pdfdata = data.map((elt) => [
      elt.firstName,
      elt.lastName,
      elt.userName,
      elt.email,
      elt.phoneNumber,
    ]);

    let content = {
      startY: 50,
      head: headers,
      body: pdfdata,
    };

    doc.text(title, marginLeft, 40);
    (doc as any).autoTable(content);
    doc.save("employee.pdf");
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
    <TableContainer component={Paper}>
      <Typography component="h1" variant="h5" align="center">
        View All Employees
        <br />
        <br />
      </Typography>
      <StyledTableCell align="right">
        <Button variant="outlined" onClick={exportPDF}>
          Generate Report
          <IconButton size="small" edge="end" color="inherit" aria-label="menu" sx={{ mr: 2 }}>
            <PictureAsPdfIcon sx={{ ml: 2, mr: 0 }} />
          </IconButton>
        </Button>
      </StyledTableCell>
      <Table sx={{ minWidth: 700, border: "15px solid white" }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Employee Name</StyledTableCell>
            <StyledTableCell>Username</StyledTableCell>
            <StyledTableCell>Email</StyledTableCell>
            <StyledTableCell>Phone Number</StyledTableCell>
            <StyledTableCell>Designation Name</StyledTableCell>
            <StyledTableCell align="right"></StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {(rowsPerPage > 0
            ? data.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            : data
          ).map((row) => (
            <StyledTableRow key={row.userName}>
              <StyledTableCell component="th" scope="row">
                {row.firstName} {row.lastName}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.userName}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.email}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.phoneNumber}
              </StyledTableCell>
              <StyledTableCell component="th" scope="row">
                {row.designationName}
              </StyledTableCell>
              <StyledTableCell align="right">
                <Button variant="outlined" onClick={() => navigate(`/viewempdetails/${row.id}`)}>
                  Leave Details
                </Button>
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
  );
}
