import { useEffect, useState } from 'react';
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
import MyProfile from './components/MyProfile'
import ProfileImage from './components/ProfileImage'
import MyLeaveDetails from './components/Employee/MyLeaveDetails'
import NewRequests from './components/Manager/NewRequests';
import React from 'react';
import { AppContext } from './config/https';
import Loader from './components/Loader/Loader';
import useHttp from './config/https'
import ChangePassword from './components/ChangePassword';
import UpdateUserProfile from './components/UpdateUserProfile';
import CompOff from './components/CompOff';
import OnDuty from './components/OnDuty';
import ManagerNotifications from './components/Manager/ManagerNotifications';

function App() {

  const [isLogin, setIsLogin] = React.useState(false);
  
  const getToken = () => {
    setIsLogin(
      localStorage.getItem("token") && localStorage.getItem("token") != ""
        ? true
        : false
    );
  };

  useEffect(() => {
    getToken()
    })

    const {axiosInstance, loading} = useHttp();

  return (

    <AppContext.Provider value={{ loading }}>
    <div>

    {loading ? <Loader /> : ""}
    
      <BrowserRouter>

      <Navigation />

      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/about" element={<About />} />
        <Route path="/register" element={<Register />} />
        <Route path="/addemployee" element={<AddEmployee />} />
        <Route path="/viewemployees" element={<ViewEmployess />} />
        <Route path="/viewempdetails/:empId" element={<ViewEmployeeDetails />} />
        <Route path="/applyforleaves" element={<ApplyForLeaves />} />
        <Route path="/profileimage" element={<ProfileImage />} />
        <Route path="/leavedetails" element={<MyLeaveDetails />} />
        <Route path="/newrequests" element={<NewRequests />} />
        <Route path="/myprofile" element={<MyProfile />} />
        <Route path="/changepassword" element={<ChangePassword />} />
        <Route path="/updateuserprofile" element={<UpdateUserProfile />} />
        <Route path="/compoff" element={<CompOff />} />
        <Route path="/onduty" element={<OnDuty />} />
        <Route path="/*" element={<ErrorPage />} />
        <Route path="/managernotifications" element={<ManagerNotifications />} />
      </Routes>
      
      </BrowserRouter>
    </div>
    </AppContext.Provider>
  );
}

export default App;