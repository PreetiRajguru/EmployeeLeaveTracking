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
  Card,
  CardActions,
  CardContent,
} from "@mui/material";
import axios from "axios";

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

const ApplyForLeaves = () => {
  const navigate = useNavigate();
  const [leaveTypeDetails, setLeaveTypeDetails] = useState({
    managerId: "7f8c62e3-c231-41b7-b401-c7915e8fb705",
    statusId: 1,
    employeeId: "",
    requestComments: "",
    startDate: "",
    endDate: "",
    totalDays: 0,
    leaveTypeId: undefined,
  });
  const [leaveTypeName, setLeaveTypeName] = useState<any>([]);
  const empId = localStorage.getItem('id');

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const newLeaveTypeDetails = {
      managerId: leaveTypeDetails.managerId,
      statusId: leaveTypeDetails.statusId,
      startDate: leaveTypeDetails.startDate,
      endDate: leaveTypeDetails.endDate,
      totalDays: leaveTypeDetails.totalDays,
      requestComments: leaveTypeDetails.requestComments,
      employeeId: leaveTypeDetails.employeeId,
      leaveTypeId: leaveTypeDetails.leaveTypeId,
    };

    try {
      axios.post("/api/LeaveRequest", newLeaveTypeDetails).then((response) => {
        console.log(response.data);
        alert("Leave Request Sent Succesfully");
      });
    } catch (error: any) {
      alert(error.response.data.message);
    }
    setLeaveTypeDetails({
      managerId: "7f8c62e3-c231-41b7-b401-c7915e8fb705",
      statusId: 1,
      employeeId: "",
      requestComments: "",
      startDate: "",
      endDate: "",
      totalDays: 0,
      leaveTypeId: undefined,
    });
    navigate('/leavedetails')
  };

  useEffect(() => {
    const fetchLeaveTypes = () => {
      axios
        .get("/api/LeaveType")
        .then((response) => setLeaveTypeName(response.data))
        .catch((error) => console.log(error));
    };

    fetchLeaveTypes();

    const role = localStorage.getItem("role");
    console.log(role);
  }, []);

  function getDateDifference(startDate: any, endDate: any) {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    const diffDays = Math.round(Math.abs((startDate - endDate) / oneDay));
    return diffDays + 1; // add 1 to include the start date as well
  }

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let totalDays = leaveTypeDetails.totalDays;
    if (name === 'startDate') {
      const endDate = new Date(leaveTypeDetails.endDate);
      totalDays = getDateDifference(new Date(value), endDate);
    } else if (name === 'endDate') {
      const startDate = new Date(leaveTypeDetails.startDate);
      totalDays = getDateDifference(startDate, new Date(value));
    }
    setLeaveTypeDetails((prevState: any) => ({
      ...prevState,
      [name]: value,
      totalDays: totalDays,
      employeeId: empId
    }));
  };

  return (
    <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
      {/* <BasicCard /> */}
      <Container>
        <Typography variant="h4" align="left">
          Apply For Leaves
        </Typography>
        <Divider />

        <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
         
          <TextField
            name="employeeId"
            label="Employee Id/Name"
            required
            fullWidth
            autoComplete="off"
            value={empId}
            onChange={handleInputChange}
            sx={{ mb: 2 }}
          />

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

          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ mt: 2, mr: 2 }}
            onClick={() => navigate('/leavedetails')}
          >
            Save
          </Button>
          <Button
            variant="contained"
            onClick={() => navigate('/leavedetails')}
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
