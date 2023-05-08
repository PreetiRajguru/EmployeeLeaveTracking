import React, { useState, createContext, useContext } from "react";
import axios from "axios";

const AppContext = createContext("");

const userId = localStorage.getItem("id");

const NameProvider = ({ children }: any) => {
    const [name,setName] = useState("")
  const getEmpName = async() => {
    try {
      const name = await axios.get(`/User/currentuser/${userId}`);
      setName(name.data)
      console.log("APp Provider==", name);
    } catch (error) {
      console.log(error);
    }
  };
  getEmpName();

  return <AppContext.Provider value={name}>{children}</AppContext.Provider>;
};

export const useNameContext = () => {
    return useContext(AppContext);
}

export default {AppContext, NameProvider};
