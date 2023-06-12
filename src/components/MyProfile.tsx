import {
  Card,
  CardContent,
  Typography,
  Avatar,
  Stack,
  TableCell,
  TableRow,
  styled,
  tableCellClasses,
} from "@mui/material";
import axios from "axios";
import { Key, ReactNode, useEffect, useState } from "react";

const empId = localStorage.getItem("id");

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
  const [data, setData] = useState<Employee[]>([]);

  useEffect(() => {
    const empId = localStorage.getItem("id");
    const fetchAllEmployeesForManager = async () => {
      try {
        const response = await axios.get(`api/User/employee/${empId}`);
        console.log(response);
        setData(response.data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchAllEmployeesForManager();
    console.log(data);
  }, []);

  return (
    <div
      style={{ display: "flex", flexDirection: "column", alignItems: "center" }}
    >
      <br></br>

      {data.map((user) => (
        <Card sx={{ minWidth: 675, mt: 5, backgroundColor: "#ebf4f8" }}>
          <CardContent
            style={{
              display: "flex",
              alignItems: "center",
              flexDirection: "column",
            }}
          >
            <Stack direction="row" spacing={2}>
              <Avatar
                src="/broken-image.jpg"
                style={{
                  display: "flex",
                  alignItems: "center",
                  height: "100px",
                  width: "100px",
                }}
              />
            </Stack>
            <br></br>
            <Typography
              gutterBottom
              variant="h5"
              component="div"
              sx={{ mt: 1 }}
              style={{ display: "flex", alignItems: "center" }}
            >
              Personal Details
            </Typography>
            <br></br>

            <Typography variant="h6" component="div">
              <b>Firstname :</b> {user.firstName}
            </Typography>
            <Typography variant="h6" component="div">
              <b>Lastname :</b> {user.lastName}
            </Typography>
            <Typography variant="h6" component="div">
              <b>Username :</b> {user.userName}
            </Typography>
            <Typography variant="h6" component="div">
              <b>Email :</b> {user.email}
            </Typography>
            {/* <Typography variant="h6" component="div">
            Designation : {user.designationName}
            </Typography> */}
            <Typography variant="h6" component="div">
              <b>Contact Details :</b> {user.phoneNumber}
            </Typography>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}