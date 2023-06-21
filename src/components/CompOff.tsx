// import { useEffect, useState } from "react";
// import { useNavigate } from "react-router-dom";
// import {
//   TextField,
//   Button,
//   Typography,
//   Box,
//   Container,
//   Select,
//   MenuItem,
//   InputLabel,
//   FormControl,
//   Divider,
// } from "@mui/material";
// import useReadLocalStorage from "./useReadLocalStorage";
// import useHttp from "../config/https";
// import swal from "sweetalert";

// const CompOff = () => {
//   const navigate = useNavigate();

//   const managerId = useReadLocalStorage("id");
//   console.log(managerId);

//   const { axiosInstance, loading } = useHttp();

//   const [leaveDetails, setLeaveDetails] = useState({
//     requestComments: "",
//     workedDate: "",
//     balance: 0,
//     userId: undefined,
//     errors: {
//       requestComments: "",
//       workedDate: "",
//       balance: "",
//       userId: "",
//     },
//   });

//   const [tryErrors, setTryErrors] = useState({
//     requestComments: "",
//     workedDate: "",
//     balance: "",
//     userId: "",
//   });

//   const [isEmployeeSelected, setEmployeeSelected] = useState(false);

//   const [employeeName, setEmployeeName] = useState<any>([]);

//   const [username, setUsername] = useState<string>();

//   const [unAuthorized, setUnAuthorized] = useState(false);

//   const handleSubmit = async (event: any) => {
//     event.preventDefault();

//     let errors = { ...leaveDetails.errors };
//     let hasErrors = false;

//     if (!leaveDetails.requestComments) {
//       errors.requestComments = "Request comments is required";
//       hasErrors = true;
//     }

//     if (!leaveDetails.workedDate) {
//       errors.workedDate = "Start Date is required";
//       hasErrors = true;
//     }

//     setLeaveDetails((prevState: any) => ({
//       ...prevState,
//       errors: errors,
//     }));

//     if (hasErrors) {
//       return;
//     }

//     const newLeaveTypeDetails = {
//       startDate: leaveDetails.workedDate,
//       totalDays: leaveDetails.balance,
//       requestComments: leaveDetails.requestComments,
//       employeeId: leaveDetails.userId,
//     };

//     try {
//       axiosInstance
//         .put("/api/CompOff", newLeaveTypeDetails)
//         .then((response) => {
//           swal("Comp-Off Leave Added Succesfully");

//           navigate("/viewemployees");
//         });
//     } catch (error: any) {
//       swal(error.response.data.message);

//       const existingData = {
//         requestComments: "",
//         workedDate: "",
//         balance: 0,
//         userId: undefined,
//         errors: {
//           requestComments: "",
//           workedDate: "",
//           balance: "",
//           userId: "",
//         },
//       };

//       setLeaveDetails(existingData);
//       setUnAuthorized(true);

//       setLeaveDetails({
//         requestComments: "",
//         workedDate: "",
//         balance: 0,
//         //select empid from dropdown menu
//         userId: undefined,
//         errors: {
//           requestComments: "",
//           workedDate: "",
//           balance: "",
//           userId: "",
//         },
//       });
//     }
//   };

//   useEffect(() => {
//     const mId = localStorage.getItem("id");
//     const fetchEmployees = async () => {
//       try {
//         const response = await axiosInstance.get(`/api/User/employees/${mId}`);
//         setEmployeeName(response.data);
//         console.log(response.data);
//       } catch (error) {}
//     };

//     fetchEmployees();
//   }, []);

//   useEffect(() => {
//     if (!managerId || username) {
//       return;
//     }
//   }, [managerId, username]);

//   function getDateDifference(startDate: any, endDate: any) {
//     const oneDay = 24 * 60 * 60 * 1000;
//     let totalDays = Math.round(Math.abs((startDate - endDate) / oneDay)) + 1;

//     const start = new Date(startDate);
//     const end = new Date(endDate);

//     // Iterate through each day between start and end dates
//     for (let date = start; date <= end; date.setDate(date.getDate() + 1)) {
//       const day = date.getDay();
//       if (day !== 0 && day !== 6) {
//         // Exclude non-Saturdays and non-Sundays
//         totalDays--;
//       }
//     }

//     return totalDays;
//   }

//   // const handleInputChange = (event: any) => {
//   //   const { name, value } = event.target;
//   //   let totalDays = leaveDetails.balance;
//   //   let isValid = true;
//   //   const newErrors = { ...tryErrors };

//   //   switch (name) {
//   //     case "startDate":
//   //       const selectedStartDate = new Date(value);

//   //       // if (selectedStartDate > endDate) {
//   //       //   newErrors.startDate = "Start date cannot be greater than end date.";
//   //       //   isValid = false;
//   //       // } else {
//   //       //   newErrors.requestComments = "";
//   //       // }

//   //       // totalDays = getDateDifference(selectedStartDate, endDate);
//   //       break;

//   //     case "endDate":
//   //       const startDate = new Date(leaveDetails.workedDate);
//   //       const selectedEndDate = new Date(value);

//   //       if (selectedEndDate < startDate) {
//   //         // newErrors.endDate = "End date cannot be less than start date.";
//   //         return;
//   //       }

//   //       totalDays = getDateDifference(startDate, selectedEndDate);
//   //       break;

//   //     default:
//   //       break;
//   //   }

//   //   setLeaveDetails((prevState: any) => ({
//   //     ...prevState,
//   //     [name]: value,
//   //     totalDays: totalDays,
//   //     errors: newErrors,
//   //     empManager: { managerId },
//   //   }));
//   // };

//   const handleInputChange = (event: any) => {
//     const { name, value } = event.target;
//     let totalDays = leaveDetails.balance;
//     let isValid = true;
//     const newErrors = { ...tryErrors };

//     switch (name) {
//       case "startDate":
//         const selectedStartDate = new Date(value);
//         const selectedEndDate = new Date(leaveDetails.endDate);

//         // Calculate the total days
//         totalDays = getDateDifference(selectedStartDate, selectedEndDate);
//         break;

//       case "endDate":
//         const startDate = new Date(leaveDetails.workedDate);
//         const selectedEndDate = new Date(value);

//         if (selectedEndDate < startDate) {
//           // newErrors.endDate = "End date cannot be less than start date.";
//           return;
//         }

//         // Calculate the total days
//         totalDays = getDateDifference(startDate, selectedEndDate);
//         break;

//       default:
//         break;
//     }

//     setLeaveDetails((prevState: any) => ({
//       ...prevState,
//       [name]: value,
//       balance: totalDays, // Update the balance field
//       errors: newErrors,
//       empManager: { managerId },
//     }));
//   };

//   return (
//     <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
//       <Container>
//         <Typography variant="h4" align="left">
//           Add Compensatory Off
//         </Typography>

//         <Divider />

//         <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
//           <br />
//           <br />
//           <FormControl fullWidth>
//             <InputLabel id="demo-simple-select-label">Employee</InputLabel>
//             <Select
//               name="employeeId"
//               label="Employee Id/Name"
//               id="demo-simple-select"
//               fullWidth
//               sx={{ mb: 2 }}
//               value={employeeName.employeeId}
//               onChange={handleInputChange}
//               error={!isEmployeeSelected && Boolean(leaveDetails.errors.userId)}
//             >
//               {employeeName?.map((option: any) => (
//                 <MenuItem value={option.id}>
//                   {`${option.firstName} ${option.lastName}`}
//                 </MenuItem>
//               ))}
//             </Select>
//             {!isEmployeeSelected && (
//               <Typography variant="caption" color="error">
//                 {leaveDetails.errors.userId}
//               </Typography>
//             )}
//           </FormControl>
//           {/* <TextField
//             name="startDate"
//             type="date"
//             autoComplete="off"
//             value={leaveDetails.workedDate}
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: new Date().toISOString().slice(0, 10),
//             }}
//             error={Boolean(leaveDetails.errors.workedDate)}
//             helperText={leaveDetails.errors.workedDate}
//           /> */}
//           <TextField
//             name="startDate"
//             type="date"
//             autoComplete="off"
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: new Date().toISOString().slice(0, 10),
//               max: new Date(
//                 new Date().setFullYear(new Date().getFullYear() + 1)
//               )
//                 .toISOString()
//                 .slice(0, 10),
//             }}
//             error={Boolean(leaveDetails.errors.workedDate)}
//             helperText={leaveDetails.errors.workedDate}
//           />
//           {/*
//           <TextField
//             name="endDate"
//             type="date"
//             autoComplete="off"
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: leaveDetails.workedDate,
//               max: new Date(
//                 new Date().setFullYear(new Date().getFullYear() + 1)
//               )
//                 .toISOString()
//                 .slice(0, 10),
//             }}
//           /> */}
//           <TextField
//             name="endDate"
//             type="date"
//             autoComplete="off"
//             value={leaveDetails.endDate}
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: leaveDetails.workedDate,
//               max: new Date(
//                 new Date().setFullYear(new Date().getFullYear() + 1)
//               )
//                 .toISOString()
//                 .slice(0, 10),
//             }}
//             error={Boolean(leaveDetails.errors.endDate)}
//             helperText={leaveDetails.errors.endDate}
//           />
//           <br></br>
//           <br></br>
//           Total Days: {leaveDetails.balance}
//           <br></br>
//           <br></br>
//           <TextField
//             name="requestComments"
//             type="text"
//             autoComplete="off"
//             label="Reason for leave"
//             multiline
//             maxRows={4}
//             fullWidth
//             value={leaveDetails.requestComments}
//             onChange={handleInputChange}
//             error={Boolean(leaveDetails.errors.requestComments)}
//             helperText={leaveDetails.errors.requestComments}
//           />
//           <br></br>
//           <br></br>
//           <Button type="submit" variant="contained" sx={{ mr: 2 }}>
//             Submit
//           </Button>
//           <Button variant="contained" onClick={() => navigate("/leavedetails")}>
//             Cancel
//           </Button>
//         </Box>
//       </Container>
//     </Box>
//   );
// };

// export default CompOff;

// import React, { useEffect, useState } from "react";
// import { useNavigate } from "react-router-dom";
// import {
//   TextField,
//   Button,
//   Typography,
//   Box,
//   Container,
//   Select,
//   MenuItem,
//   InputLabel,
//   FormControl,
//   Divider,
// } from "@mui/material";
// import useReadLocalStorage from "./useReadLocalStorage";
// import useHttp from "../config/https";
// import swal from "sweetalert";

// const CompOff = () => {
//   const navigate = useNavigate();

//   const managerId = useReadLocalStorage("id");
//   console.log(managerId);

//   const { axiosInstance, loading } = useHttp();

//   const [leaveDetails, setLeaveDetails] = useState({
//     reason: "",
//     workedDate: "",
//     balance: 0,
//     userId: undefined,
//     endDate: "",
//     errors: {
//       reason: "",
//       workedDate: "",
//       balance: "",
//       userId: "",
//       endDate: "",
//     },
//   });

//   const [tryErrors, setTryErrors] = useState({
//     requestComments: "",
//     workedDate: "",
//     balance: "",
//     userId: "",
//   });

//   const [isEmployeeSelected, setEmployeeSelected] = useState(false);

//   const [employeeName, setEmployeeName] = useState<any>([]);

//   const [username, setUsername] = useState<string>();

//   const [unAuthorized, setUnAuthorized] = useState(false);

//   const handleSubmit = async (event: any) => {
//     event.preventDefault();

//     let errors = { ...leaveDetails.errors };
//     let hasErrors = false;

//     if (!leaveDetails.reason) {
//       errors.reason = "Request comments is required";
//       hasErrors = true;
//     }

//     if (!leaveDetails.workedDate) {
//       errors.workedDate = "Start Date is required";
//       hasErrors = true;
//     }

//     setLeaveDetails((prevState: any) => ({
//       ...prevState,
//       errors: errors,
//     }));

//     if (hasErrors) {
//       return;
//     }

//     const newLeaveTypeDetails = {
//       userId: leaveDetails.userId,
//       balance: leaveDetails.balance,
//       workedDate: leaveDetails.workedDate,
//       reason: leaveDetails.reason,
//     };

//     try {
//       axiosInstance
//         .put("/api/CompOff", newLeaveTypeDetails)
//         .then((response) => {
//           swal("Comp-Off Leave Added Succesfully");

//           navigate("/viewemployees");
//         });
//     } catch (error: any) {
//       swal(error.response.data.message);

//       const existingData = {
//         reason: "",
//         workedDate: "",
//         balance: 0,
//         endDate: "",
//         userId: undefined,
//         errors: {
//           reason: "",
//           workedDate: "",
//           endDate: "",
//           balance: "",
//           userId: "",
//         },
//       };

//       setLeaveDetails(existingData);
//       setUnAuthorized(true);

//       setLeaveDetails({
//         reason: "",
//         workedDate: "",
//         balance: 0,
//         //select empid from dropdown menu
//         userId: undefined,
//         endDate: "",
//         errors: {
//           reason: "",
//           workedDate: "",
//           balance: "",
//           endDate: "",
//           userId: "",
//         },
//       });
//     }
//   };

//   useEffect(() => {
//     const mId = localStorage.getItem("id");
//     const fetchEmployees = async () => {
//       try {
//         const response = await axiosInstance.get(`/api/User/employees/${mId}`);
//         setEmployeeName(response.data);
//         console.log(response.data);
//       } catch (error) {}
//     };

//     fetchEmployees();
//   }, []);

//   useEffect(() => {
//     if (!managerId || username) {
//       return;
//     }
//   }, [managerId, username]);

//   function getDateDifference(startDate: any, endDate: any) {
//     const oneDay = 24 * 60 * 60 * 1000;
//     let totalDays = Math.round(Math.abs((startDate - endDate) / oneDay)) + 1;

//     const start = new Date(startDate);
//     const end = new Date(endDate);

//     // Iterate through each day between start and end dates
//     for (let date = start; date <= end; date.setDate(date.getDate() + 1)) {
//       const day = date.getDay();
//       if (day !== 0 && day !== 6) {
//         // Exclude non-Saturdays and non-Sundays
//         totalDays--;
//       }
//     }

//     return totalDays;
//   }

//   const handleInputChange = (event: any) => {
//     const { name, value } = event.target;
//     let totalDays = leaveDetails.balance;
//     let isValid = true;
//     const newErrors = { ...tryErrors };

//     switch (name) {
//       case "workedDate":
//         const selectedStartDate = new Date(value);
//         const startDateEndDate = new Date(leaveDetails.endDate);

//         // Calculate the total days
//         totalDays = getDateDifference(selectedStartDate, startDateEndDate);
//         break;

//       case "endDate":
//         const startDate = new Date(leaveDetails.workedDate);
//         const selectedEndDate = new Date(value);

//         // Calculate the total days
//         totalDays = getDateDifference(startDate, selectedEndDate);
//         break;

//       default:
//         break;
//     }

//     setLeaveDetails((prevState: any) => ({
//       ...prevState,
//       [name]: value,
//       balance: totalDays, // Update the balance field
//       errors: newErrors,
//     }));
//   };

//   return (
//     <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
//       <Container>
//         <Typography variant="h4" align="left">
//           Add Compensatory Off
//         </Typography>

//         <Divider />

//         <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
//           <br />
//           <br />
//           <FormControl fullWidth>
//             <InputLabel id="demo-simple-select-label">Employee</InputLabel>
//             <Select
//               name="employeeId"
//               label="Employee Id/Name"
//               id="demo-simple-select"
//               fullWidth
//               sx={{ mb: 2 }}
//               value={leaveDetails.userId}
//               onChange={handleInputChange}
//               error={!isEmployeeSelected && Boolean(leaveDetails.errors.userId)}
//             >
//               {employeeName?.map((option: any) => (
//                 <MenuItem key={option.id} value={option.id}>
//                   {`${option.firstName} ${option.lastName}`}
//                 </MenuItem>
//               ))}
//             </Select>
//             {!isEmployeeSelected && (
//               <Typography variant="caption" color="error">
//                 {leaveDetails.errors.userId}
//               </Typography>
//             )}
//           </FormControl>
//           <TextField
//             name="workedDate"
//             type="date"
//             autoComplete="off"
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: new Date().toISOString().slice(0, 10),
//               max: new Date(
//                 new Date().setFullYear(new Date().getFullYear() + 1)
//               )
//                 .toISOString()
//                 .slice(0, 10),
//             }}
//             error={Boolean(leaveDetails.errors.workedDate)}
//             helperText={leaveDetails.errors.workedDate}
//           />
//           <TextField
//             name="endDate"
//             type="date"
//             autoComplete="off"
//             value={leaveDetails.endDate}
//             onChange={handleInputChange}
//             sx={{ mb: 2, mr: 2 }}
//             inputProps={{
//               min: leaveDetails.workedDate,
//               max: new Date(
//                 new Date().setFullYear(new Date().getFullYear() + 1)
//               )
//                 .toISOString()
//                 .slice(0, 10),
//             }}
//             error={Boolean(leaveDetails.errors.endDate)}
//             helperText={leaveDetails.errors.endDate}
//           />
//           <br></br>
//           <br></br>
//           Total Days: {leaveDetails.balance}
//           <br></br>
//           <br></br>
//           <TextField
//             name="reason"
//             type="text"
//             autoComplete="off"
//             label="Reason for leave"
//             multiline
//             maxRows={4}
//             fullWidth
//             value={leaveDetails.reason}
//             onChange={handleInputChange}
//             error={Boolean(leaveDetails.errors.reason)}
//             helperText={leaveDetails.errors.reason}
//           />
//           <br></br>
//           <br></br>
//           <Button type="submit" variant="contained" sx={{ mr: 2 }}>
//             Submit
//           </Button>
//           <Button variant="contained" onClick={() => navigate("/leavedetails")}>
//             Cancel
//           </Button>
//         </Box>
//       </Container>
//     </Box>
//   );
// };

// export default CompOff;
import React, { useEffect, useState } from "react";
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
import useReadLocalStorage from "./useReadLocalStorage";
import useHttp from "../config/https";
import swal from "sweetalert";

const CompOff = () => {
  const navigate = useNavigate();

  const managerId = useReadLocalStorage("id");
  console.log(managerId);

  const { axiosInstance, loading } = useHttp();

  const [leaveDetails, setLeaveDetails] = useState({
    reason: "",
    workedDate: "",
    balance: 0,
    userId: undefined,
    endDate: "",
    errors: {
      reason: "",
      workedDate: "",
      balance: "",
      userId: "",
      endDate: "",
    },
  });

  const [isEmployeeSelected, setEmployeeSelected] = useState(false);

  const [employeeName, setEmployeeName] = useState([]);

  const handleSubmit = async (event:any) => {
    event.preventDefault();

    let errors = { ...leaveDetails.errors };
    let hasErrors = false;

    if (!leaveDetails.reason) {
      errors.reason = "Request comments is required";
      hasErrors = true;
    }

    if (!leaveDetails.workedDate) {
      errors.workedDate = "Start Date is required";
      hasErrors = true;
    }

    setLeaveDetails((prevState) => ({
      ...prevState,
      errors: errors,
    }));

    if (hasErrors) {
      return;
    }

    const newLeaveTypeDetails = {
      UserId: leaveDetails.userId,
      Balance: leaveDetails.balance,
      WorkedDate: leaveDetails.workedDate,
      Reason: leaveDetails.reason,
    };

    try {
      const response = await axiosInstance.put("/api/CompOff", newLeaveTypeDetails);
      const updatedCompOff = response.data;
      swal("Comp-Off Leave Added Successfully");

      navigate("/viewemployees");
    } catch (error) {
      swal("Error Occurred");
    }
  };

  useEffect(() => {
    const mId = localStorage.getItem("id");
    const fetchEmployees = async () => {
      try {
        const response = await axiosInstance.get(`/api/User/employees/${mId}`);
        setEmployeeName(response.data);
        console.log(response.data);
      } catch (error) {
        console.log(error);
      }
    };

    fetchEmployees();
  }, []);

  const handleInputChange = (event:any) => {
    const { name, value } = event.target;
    let totalDays = leaveDetails.balance;
    let isValid = true;
    const newErrors = { ...leaveDetails.errors };

    switch (name) {
      case "workedDate":
        const selectedStartDate = new Date(value);
        const startDateEndDate = new Date(leaveDetails.endDate);

        // Calculate the total days
        totalDays = getDateDifference(selectedStartDate, startDateEndDate);
        break;

      case "endDate":
        const startDate = new Date(leaveDetails.workedDate);
        const selectedEndDate = new Date(value);

        // Calculate the total days
        totalDays = getDateDifference(startDate, selectedEndDate);
        break;

      default:
        break;
    }

    setLeaveDetails((prevState) => ({
      ...prevState,
      [name]: value,
      balance: totalDays, // Update the balance field
      errors: newErrors,
    }));
  };

  function getDateDifference(startDate:any, endDate:any) {
    const oneDay = 24 * 60 * 60 * 1000;
    let totalDays = Math.round(Math.abs((startDate - endDate) / oneDay)) + 1;

    const start = new Date(startDate);
    const end = new Date(endDate);

    // Iterate through each day between start and end dates
    for (let date = start; date <= end; date.setDate(date.getDate() + 1)) {
      const day = date.getDay();
      if (day !== 0 && day !== 6) {
        // Exclude non-Saturdays and non-Sundays
        totalDays--;
      }
    }

    return totalDays;
  }

  return (
    <Box sx={{ display: "flex", justifyContent: "left", mt: 4 }}>
      <Container>
        <Typography variant="h4" align="left">
          Add Compensatory Off
        </Typography>

        <Divider />

        <Box component="form" sx={{ mt: 2 }} onSubmit={handleSubmit}>
          <br />
          <br />
          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Employee</InputLabel>
            <Select
              name="userId"
              label="Employee Id/Name"
              id="demo-simple-select"
              fullWidth
              sx={{ mb: 2 }}
              value={leaveDetails.userId}
              onChange={handleInputChange}
              error={!isEmployeeSelected && Boolean(leaveDetails.errors.userId)}
            >
              {employeeName.map((option:any) => (
                <MenuItem key={option.id} value={option.id}>
                  {`${option.firstName} ${option.lastName}`}
                </MenuItem>
              ))}
            </Select>
            {!isEmployeeSelected && (
              <Typography variant="caption" color="error">
                {leaveDetails.errors.userId}
              </Typography>
            )}
          </FormControl>
          <br></br>
          <br></br>

          Start Date : 
          <TextField
            name="workedDate"
            type="date"
            autoComplete="off"
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2, ml:2 }}
            inputProps={{
              min: new Date().toISOString().slice(0, 10),
              max: new Date(
                new Date().setFullYear(new Date().getFullYear() + 1)
              )
                .toISOString()
                .slice(0, 10),
            }}
            error={Boolean(leaveDetails.errors.workedDate)}
            helperText={leaveDetails.errors.workedDate}
          />

          End Date : 
          <TextField
            name="endDate"
            type="date"
            autoComplete="off"
            value={leaveDetails.endDate}
            onChange={handleInputChange}
            sx={{ mb: 2, mr: 2, ml:2 }}
            inputProps={{
              min: leaveDetails.workedDate,
              max: new Date(
                new Date().setFullYear(new Date().getFullYear() + 1)
              )
                .toISOString()
                .slice(0, 10),
            }}
            error={Boolean(leaveDetails.errors.endDate)}
            helperText={leaveDetails.errors.endDate}
          />
          <br></br>
          <br></br>
          Total Days: {leaveDetails.balance}
          <br></br>
          <br></br>
          <br></br>

          <TextField
            name="reason"
            type="text"
            autoComplete="off"
            label="Reason for leave"
            multiline
            maxRows={4}
            fullWidth
            value={leaveDetails.reason}
            onChange={handleInputChange}
            error={Boolean(leaveDetails.errors.reason)}
            helperText={leaveDetails.errors.reason}
          />
          <br></br>
          <br></br>
          <br></br>

          <Button type="submit" variant="contained" sx={{ mr: 2 }}>
            Submit
          </Button>
          <Button variant="contained" onClick={() => navigate("/viewemployees")}>
            Cancel
          </Button>
        </Box>
      </Container>
    </Box>
  );
};

export default CompOff;
