import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  TextField,
  Button,
  Typography,
  Box,
  Container,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Divider,
} from "@mui/material";
import axios from "axios";
import useReadLocalStorage from "../useReadLocalStorage";

// function BasicCard() {
//   return (
//     <Card sx={{ minWidth: 275 }}>
//       <CardContent>
//         <Typography variant="h6" color="text.secondary" gutterBottom>
//           Employee Details
//         </Typography>
//         <Divider sx={{ mb: 1 }} />
//         <Typography component="div" sx={{ fontSize: 14, mb: 0.5 }}>
//           Preeti Rajguru
//         </Typography>
//         <Typography sx={{ mb: 1, fontSize: 14 }} color="text.secondary">
//           preeti.rajguru@ix.com
//         </Typography>
//         <Typography variant="body2" sx={{ mb: 1, fontSize: 14 }}>
//           Phone.No:7040647427
//           <br />
//           Vadgaon Sheri, Pune - 41
//         </Typography>
//       </CardContent>
//       <CardActions>
//         <Button size="small">Learn More</Button>
//       </CardActions>
//     </Card>
//   );
// }

//manager id

const ApplyForLeaves = () => {
  const navigate = useNavigate();
  const empId = useReadLocalStorage("id");
  console.log(empId);
  const [leaveBalance, setLeaveBalance] = useState();
  const [empManager, setEmpManager] = useState("");
  const [leaveTypeDetails, setLeaveTypeDetails] = useState({
    requestComments: "",
    startDate: "",
    endDate: "",
    totalDays: 0,
    managerId: empManager,
    employeeId: empId,
    leaveTypeId: undefined,
    statusId: 1,
  });
  const [leaveTypeName, setLeaveTypeName] = useState<any>([]);
  const [username, setUsername] = useState<string>();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    const newLeaveTypeDetails = {
      managerId: empManager,
      statusId: leaveTypeDetails.statusId,
      startDate: leaveTypeDetails.startDate,
      endDate: leaveTypeDetails.endDate,
      totalDays: leaveTypeDetails.totalDays,
      requestComments: leaveTypeDetails.requestComments,
      employeeId: empId,
      leaveTypeId: leaveTypeDetails.leaveTypeId,
    };

    if (leaveBalance && newLeaveTypeDetails.totalDays > +leaveBalance) {
      alert("Total days requested should be less than or equal to leave balance.");
      return;
    }

    try {
      axios.post("/api/LeaveRequest", newLeaveTypeDetails).then((response) => {
        alert("Leave Request Sent Succesfully");

        navigate("/leavedetails");
      });
    } catch (error: any) {
      alert(error.response.data.message);

      setLeaveTypeDetails({
        managerId: "",
        statusId: 1,
        employeeId: "",
        requestComments: "",
        startDate: "",
        endDate: "",
        totalDays: 0,
        leaveTypeId: undefined,
      });
    }
  };

  useEffect(() => {
    const fetchLeaveTypes = async () => {
      try {
        const response = await axios.get("/api/LeaveType");
        setLeaveTypeName(response.data);
        console.log(response.data);
      } catch (error) {}
    };

    fetchLeaveTypes();

    // const role = localStorage.getItem("role");
    // console.log(role);
    const fetchLeaveBalances = async () => {
      try {
        const response = await axios.get(`/api/LeaveRequest/balance/${empId}`);
        setLeaveBalance(response.data);
        console.log(response.data);
      } catch (error) {
      }
    };
    fetchLeaveBalances();

    const fetchEmpManager = async () => {
      try{
        const response = await axios.get(`/api/User/${empId}/manager`)
        setEmpManager(response.data);
        console.log(response.data);
      }
        catch(error){  }
    };

    fetchEmpManager();
  }, []);

  useEffect(() => {
    if (!empId || username) {
      return;
    }

    const fetchUsername = async () => {
      try {
        const response = await axios.get(`/api/User/currentuser/${empId}`);
        setUsername(`${response.data.firstName} ${response.data.lastName}`);
      } catch (error) {
        console.error(error);
      }
    };
    fetchUsername();
  }, [empId]);

  function getDateDifference(startDate: any, endDate: any) {
    const oneDay = 24 * 60 * 60 * 1000; 
    const diffDays = Math.round(Math.abs((startDate - endDate) / oneDay));
    return diffDays + 1; 
  }

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let totalDays = leaveTypeDetails.totalDays;
    if (name === "startDate") {
      const endDate = new Date(leaveTypeDetails.endDate);
      totalDays = getDateDifference(new Date(value), endDate);
    } else if (name === "endDate") {
      const startDate = new Date(leaveTypeDetails.startDate);
      totalDays = getDateDifference(startDate, new Date(value));
    }
    setLeaveTypeDetails((prevState: any) => ({
      ...prevState,
      [name]: value,
      totalDays: totalDays,
      employeeId: { empId },
      empManager:{empManager}
      // managerId: {empManager},
    }));
  };

  return (
    <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
      {/* <BasicCard /> */}
      <Container>
        <Typography variant="h4" align="left">
          Apply For Leaves
        </Typography>

        <h2 style = {{ float: "right" , backgroundColor: '#bcdbf3', display: 'inline-block', padding: '5px 10px'}}>Leave Balance: {leaveBalance} </h2>
        <Divider />

        <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
       
          {/* <TextField
            name="employeeId"
            label="Employee Id"
            required
            fullWidth
            autoComplete="on"
            value={empId}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
            // style={{ display: "none" }}
          /> */}

          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Leave Type</InputLabel>
            <Select
              name="leaveTypeId"
              label="Leave Type Id/Name"
              id="demo-simple-select"
              fullWidth
              sx={{ mb: 2 }}
              value={leaveTypeDetails.leaveTypeId}
              onChange={handleInputChange}
            >
              {leaveTypeName?.map((option: any) => (
                <MenuItem value={option.id}>{option.leaveTypeName}</MenuItem>
              ))}
            </Select>
          </FormControl>

          <TextField
            name="startDate"
            type="date"
            required
            autoComplete="off"
            value={leaveTypeDetails.startDate}
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2 }}
          />

          <TextField
            name="endDate"
            type="date"
            required
            autoComplete="off"
            value={leaveTypeDetails.endDate}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
          />

          <TextField
            name="totalDays"
            label="Total Days"
            required
            fullWidth
            autoComplete="off"
            value={leaveTypeDetails.totalDays}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
          />

          <TextField
            name="requestComments"
            label="Request Comments"
            required
            fullWidth
            multiline
            rows={3}
            autoComplete="off"
            value={leaveTypeDetails.requestComments}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
          />

          {/* <TextField
            name="managerId"
            label="Manager Id"
            required
            fullWidth
            autoComplete="off"
            value={empManager}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
            // style={{ display: "none" }}
          /> */}

          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ mt: 2, mr: 2 }}
            onClick={handleSubmit}
          >
            Save
          </Button>
          <Button
            variant="contained"
            onClick={() => navigate("/leavedetails")}
            sx={{ mt: 2 }}
          >
            Back
          </Button>
        </Box>
      </Container>
    </Box>
  );
};

export default ApplyForLeaves;
