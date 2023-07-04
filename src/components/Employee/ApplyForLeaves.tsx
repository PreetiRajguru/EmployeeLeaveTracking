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
import useReadLocalStorage from "../useReadLocalStorage";
import useHttp from "../../config/https";
import "react-toastify/dist/ReactToastify.css";
import swal from "sweetalert";

const ApplyForLeaves = () => {
  const navigate = useNavigate();
  // const empId = useReadLocalStorage("id");
  const empId = localStorage.getItem("id");
  console.log(empId);
  const [leaveBalance, setLeaveBalance] = useState();
  const [newLeaveBalance, setNewLeaveBalance] = useState([]);
  const [empManager, setEmpManager] = useState("");
  const { axiosInstance, loading } = useHttp();
  const [isLeaveTypeSelected, setIsLeaveTypeSelected] = useState(false);
  const [leaveTypeName, setLeaveTypeName] = useState<any>([]);
  const [username, setUsername] = useState<string>();
  const [unAuthorized, setUnAuthorized] = useState(false);

  const [leaveTypeDetails, setLeaveTypeDetails] = useState({
    requestComments: "",
    startDate: "",
    endDate: "",
    totalDays: 0,
    managerId: empManager,
    employeeId: empId,
    leaveTypeId: undefined,
    statusId: 1,
    errors: {
      requestComments: "",
      startDate: "",
      endDate: "",
      totalDays: "",
      managerId: "",
      employeeId: "",
      leaveTypeId: "",
      statusId: "",
    },
  });

  const [tryErrors, setTryErrors] = useState({
    requestComments: "",
    startDate: "",
    endDate: "",
    totalDays: "",
    managerId: "",
    employeeId: "",
    leaveTypeId: "",
    statusId: "",
  });

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    let errors = { ...leaveTypeDetails.errors };
    let hasErrors = false;

    if (!leaveTypeDetails.requestComments) {
      errors.requestComments = "Request comments is required";
      hasErrors = true;
    }

    if (!leaveTypeDetails.startDate) {
      errors.startDate = "Start Date is required";
      hasErrors = true;
    }

    if (!leaveTypeDetails.endDate) {
      errors.endDate = "End Date is required";
      hasErrors = true;
    }

    if (!leaveTypeDetails.leaveTypeId) {
      errors.leaveTypeId = "Leave Type is required";
      hasErrors = true;
    }

    setLeaveTypeDetails((prevState: any) => ({
      ...prevState,
      errors: errors,
    }));

    if (hasErrors) {
      return;
    }

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

    //validation for checking balance

    let data: any = newLeaveBalance.filter(
      (s: any) =>
        s.leaveTypeId == newLeaveTypeDetails.leaveTypeId && s.leaveTypeId != 2
    );

    // alert(JSON.stringify(data) + ' '+ newLeaveTypeDetails.totalDays);

    if (data && newLeaveTypeDetails.totalDays > data[0]?.balance) {
      swal(
        "Total days requested should be less than or equal to leave balance."
      );
      return;
    }

    try {
      axiosInstance
        .post("/api/LeaveRequest/newleaverequest", newLeaveTypeDetails)
        .then((response) => {
          swal("Leave Request Sent Succesfully");
          navigate("/leavedetails");
        });
    } catch (error: any) {
      swal(error.response.data.message);

      const existingData = {
        requestComments: "",
        startDate: "",
        endDate: "",
        totalDays: 0,
        managerId: empManager,
        employeeId: empId,
        leaveTypeId: undefined,
        statusId: 1,
        errors: {
          requestComments: "",
          startDate: "",
          endDate: "",
          totalDays: "",
          managerId: "",
          employeeId: "",
          leaveTypeId: "",
          statusId: "",
        },
      };

      setLeaveTypeDetails(existingData);
      setUnAuthorized(true);

      setLeaveTypeDetails({
        requestComments: "",
        startDate: "",
        endDate: "",
        totalDays: 0,
        managerId: empManager,
        employeeId: empId,
        leaveTypeId: undefined,
        statusId: 1,
        errors: {
          requestComments: "",
          startDate: "",
          endDate: "",
          totalDays: "",
          managerId: "",
          employeeId: "",
          leaveTypeId: "",
          statusId: "",
        },
      });
    }
  };

  useEffect(() => {
    const fetchLeaveTypes = async () => {
      try {
        const response = await axiosInstance.get("/api/LeaveType");
        setLeaveTypeName(response.data);
        console.log(response.data);
      } catch (error) {}
    };

    fetchLeaveTypes();

    const fetchLeaveBalances = async () => {
      try {
        const response = await axiosInstance.get(
          `/api/LeaveRequest/balance/${empId}`
        );
        setLeaveBalance(response.data);
        console.log("old balance - ", response.data);
      } catch (error) {}
    };
    fetchLeaveBalances();

    const fetchLeaveBalancesPerType = async () => {
      try {
        const response = await axiosInstance.get(`/employee/${empId}`);
        setNewLeaveBalance(response.data);

        console.log("1st Array Element", response.data[0].leaveTypeId);

        console.log("new leave balance ---", newLeaveBalance);

        console.log("new balance - ", response.data);
      } catch (error) {
        swal("Error Occurred");
      }
    };

    fetchLeaveBalancesPerType();

    const fetchEmpManager = async () => {
      try {
        const response = await axiosInstance.get(`/api/User/${empId}/manager`);
        setEmpManager(response.data);
        console.log(response.data);
      } catch (error) {}
    };

    fetchEmpManager();
  }, []);

  useEffect(() => {
    if (!empId || username) {
      return;
    }

    const fetchUsername = async () => {
      try {
        const response = await axiosInstance.get(
          `/api/User/currentuser/${empId}`
        );
        setUsername(`${response.data.firstName} ${response.data.lastName}`);
      } catch (error) {
        console.error(error);
      }
    };
    fetchUsername();
  }, [empId]);
  function getDateDifference(startDate: any, endDate: any) {
    const oneDay = 24 * 60 * 60 * 1000;
    let totalDays = Math.round(Math.abs((startDate - endDate) / oneDay)) + 1;

    const start = new Date(startDate);
    const end = new Date(endDate);

    // Iterate through each day between start and end dates
    for (let date = start; date <= end; date.setDate(date.getDate() + 1)) {
      const day = date.getDay();
      if (day === 0 || day === 6) {
        // Exclude Sundays (0) and Saturdays (6)
        totalDays--;
      }
    }

    return totalDays;
  }

  const handleInputChange = (event: any) => {
    const { name, value } = event.target;
    let totalDays = leaveTypeDetails.totalDays;

    let isValid = true;
    const newErrors = { ...tryErrors };

    switch (name) {
      case "startDate":
        const endDate = new Date(leaveTypeDetails.endDate);
        const selectedStartDate = new Date(value);

        if (selectedStartDate > endDate) {
          newErrors.startDate = "Start date cannot be greater than end date.";
          isValid = false;
        } else {
          newErrors.requestComments = "";
        }

        totalDays = getDateDifference(selectedStartDate, endDate);
        break;

      case "endDate":
        const startDate = new Date(leaveTypeDetails.startDate);
        const selectedEndDate = new Date(value);

        if (selectedEndDate < startDate) {
          newErrors.endDate = "End date cannot be less than start date.";
          return;
        }

        totalDays = getDateDifference(startDate, selectedEndDate);
        break;

      default:
        break;
    }

    setLeaveTypeDetails((prevState: any) => ({
      ...prevState,
      [name]: value,
      totalDays: totalDays,
      errors: newErrors,
      employeeId: { empId },
      empManager: { empManager },
    }));
  };

  return (
    <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
      <Container>
        <Typography variant="h4" align="left">
          Apply For Leaves
        </Typography>

        <Divider />

        <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
          <br />
          <br />
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
              error={
                !isLeaveTypeSelected &&
                Boolean(leaveTypeDetails.errors.leaveTypeId)
              }
            >
              {leaveTypeName?.map((option: any) => (
                <MenuItem value={option.id}>{option.leaveTypeName}</MenuItem>
              ))}
            </Select>
            {!isLeaveTypeSelected && (
              <Typography variant="caption" color="error">
                {leaveTypeDetails.errors.leaveTypeId}
              </Typography>
            )}
          </FormControl>
          <br></br>
          <br></br>
          From :
          <TextField
            name="startDate"
            type="date"
            autoComplete="off"
            value={leaveTypeDetails.startDate}
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2, ml: 2 }}
            inputProps={{
              min: new Date().toISOString().slice(0, 10),
            }}
            error={Boolean(leaveTypeDetails.errors.startDate)}
            helperText={leaveTypeDetails.errors.startDate}
          />
          To :
          <TextField
            name="endDate"
            type="date"
            autoComplete="off"
            value={leaveTypeDetails.endDate}
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2, ml: 2 }}
            inputProps={{
              min: leaveTypeDetails.startDate,
              max: new Date(
                new Date().setFullYear(new Date().getFullYear() + 1)
              )
                .toISOString()
                .slice(0, 10),
            }}
            error={Boolean(leaveTypeDetails.errors.endDate)}
            helperText={leaveTypeDetails.errors.endDate}
          />
          <br></br>
          <br></br>
          Total Days: {leaveTypeDetails.totalDays}
          <br></br>
          <br></br>
          <br></br>
          <TextField
            name="requestComments"
            type="text"
            autoComplete="off"
            label="Reason for leave"
            multiline
            maxRows={4}
            fullWidth
            value={leaveTypeDetails.requestComments}
            onChange={handleInputChange}
            error={Boolean(leaveTypeDetails.errors.requestComments)}
            helperText={leaveTypeDetails.errors.requestComments}
          />
          <br></br>
          <br></br>
          <Button type="submit" variant="contained" sx={{ mr: 2 }}>
            Submit
          </Button>
          <Button variant="contained" onClick={() => navigate("/leavedetails")}>
            Cancel
          </Button>
        </Box>
      </Container>
    </Box>
  );
};

export default ApplyForLeaves;