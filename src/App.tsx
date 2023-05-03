import { useEffect } from 'react';
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import './App.css';
import Login from './components/Auth/Login';
import Register from './components/Auth/Register';
import Navigation from './components/Navigation';
import ViewEmployess from './components/Manager/ViewEmployees';
import ViewEmployeeDetails from './components/Manager/ViewEmployeeDetails';
import AddEmployee from './components/Manager/AddEmployee';
import ApplyForLeaves from './components/Employee/ApplyForLeaves';
import About from './components/About'
import ErrorPage from './components/ErrorPage'
import MyLeaveDetails from './components/Employee/MyLeaveDetails'
import axios from 'axios'

function App() {

  useEffect(() => {
    axios.defaults.baseURL = 'https://localhost:7033';
    })

  return (
    <div>
      <BrowserRouter>
      <Navigation />
      {/* <NAppBar /> */}
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/about" element={<About />} />
        <Route path="/addemployee" element={<AddEmployee />} />
        <Route path="/viewemployees" element={<ViewEmployess />} />
        <Route path="/viewempdetails/:empId" element={<ViewEmployeeDetails />} />
        <Route path="/applyforleaves" element={<ApplyForLeaves />} />
        <Route path="/leavedetails" element={<MyLeaveDetails />} />
        <Route path="/*" element={<ErrorPage />} />
        {/* <Route path="/leavebalance" element={<LeaveBalance/>}/> */}
      </Routes>
      
      </BrowserRouter>
    </div>
  );
}

export default App;