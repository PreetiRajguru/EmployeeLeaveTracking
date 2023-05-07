import React, {useState, useEffect, Key, ReactNode} from 'react';
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
import axios from 'axios';

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
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
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
  designationId:number;
  }




export default function CustomizedTables() {
  const navigate = useNavigate();
  const [data, setData] = useState<Employee[]>([]);

  useEffect(() => {
    const managerId = localStorage.getItem("id");
    const fetchAllEmployeesForManager = async () => {
      try {
        const response = await axios.get(`/User/employees/${managerId}`);
        console.log(response)
        setData(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchAllEmployeesForManager();
    console.log(data)
  }, []);

  return (
    <TableContainer component={Paper}>

      <Typography component="h1" variant="h5" align="center">
        View All Employees
        <br></br><br></br>
      </Typography>
      <Table sx={{ minWidth: 700 }} aria-label="customized table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Employee Name</StyledTableCell>
            <StyledTableCell>Username</StyledTableCell>
            <StyledTableCell>Email</StyledTableCell>
            <StyledTableCell>Phone Number</StyledTableCell>
            <StyledTableCell>Designation Id</StyledTableCell>
            <StyledTableCell align="right">Actions</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
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
                {row.designationId}
              </StyledTableCell>
              <StyledTableCell align="right">
                <Button variant="outlined" onClick={() => navigate(`/viewempdetails/${row.id}`)}>Leave Details</Button>
              </StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}