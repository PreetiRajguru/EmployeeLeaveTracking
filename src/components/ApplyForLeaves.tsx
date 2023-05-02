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
          preeti.rajguru@ix.com
        </Typography>
        <Typography variant="body2" sx={{ mb: 1, fontSize: 14 }}>
          Phone.No:7040647427
          <br />
          Vadgaon Sheri, Pune - 41
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small">Learn More</Button>
      </CardActions>
    </Card>
  );
}

const ApplyForLeaves = () => {
  const [leaveTypeDetails, setLeaveTypeDetails] = useState({
    managerId: "1",
    statusId: 1,
    employeeId: "",
    requestComments: "",
    startDate: "",
    endDate: "",
    totalDays: "",
    leaveTypeId: undefined,
  });
  const [leaveTypeName, setLeaveTypeName] = useState<any>([]);

  
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
      axios.post('/api/LeaveRequest', newLeaveTypeDetails)
        .then((response) => {
          console.log(response.data);
        });
    }
    catch (error: any) {
      alert(error.response.data.message);
    }
    setLeaveTypeDetails({
      managerId: "1",
      statusId: 1,
      employeeId: "",
      requestComments: "",
      startDate: "",
      endDate: "",
      totalDays: "",
      leaveTypeId: undefined,
    })
  };

  useEffect(() => {
    const fetchLeaveTypes = () => {
      axios
        .get("/api/LeaveType")
        .then((response) => setLeaveTypeName(response.data))
        .catch((error) => console.log(error));
    };


    fetchLeaveTypes();
  }, []);

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    setLeaveTypeDetails((prevState: any) => ({
      ...prevState,
      [name]: value,
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
            name="startDate"
            // label="Start Date"
            type="date"
            required
            autoComplete="off"
            value={leaveTypeDetails.startDate}
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2 }}
          />

          <TextField
            name="endDate"
            // label="End Date"
            type="date"
            required
            autoComplete="off"
            value={leaveTypeDetails.endDate}
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

          {/* <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">
              Employee Id/Name
            </InputLabel>
            <Select
              name="employeeId"
              label="Employee Id/Name"
              id="demo-simple-select"
              fullWidth
              sx={{ mb: 2 }}
              value={leaveTypeDetails.employeeId}
              onChange={handleInputChange}
            >
              {
                //   clients.map((val: any) => (
                //     <MenuItem key={val.id} value={val.id}>
                //       {val.firstName} {val.lastName}
                //     </MenuItem>
                //   ))
              }
            </Select>
          </FormControl> */}
          <TextField
            name="employeeId"
            label="Employee Id/Name"
            required
            fullWidth
            autoComplete="off"
            value={leaveTypeDetails.employeeId}
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

          {/* <FormControl fullWidth>
          <InputLabel id="demo-simple-select-label">Status</InputLabel>
          <Select
            name="statusId"
            label="Status Id"
            id="demo-simple-select"
            fullWidth
          >
          </Select>
        </FormControl> */}

          <Button
            type="submit"
            variant="contained"
            color="primary"
            sx={{ mt: 2, mr: 2 }}
          >
            Save
          </Button>
          <Button
            variant="contained"
            //   onClick={handleBackButton}
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
