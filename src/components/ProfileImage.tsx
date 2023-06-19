import { ChangeEvent, useRef, useState } from "react";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import useHttp from "../config/https";
import swal from 'sweetalert';

export default function ProfileImage() {
  const { axiosInstance, loading } = useHttp();
  const [file, setFile] = useState<string>("");
  const userId = localStorage.getItem("id");
  const inputRef = useRef<HTMLInputElement | null>(null);

  const handleUploadClick = () => {
    inputRef.current?.click();
  };

  const handleFileChange = async (e: ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) {
      return;
    }

    const selectedFile = e.target.files[0];
    const imageUrl = URL.createObjectURL(selectedFile);

    setFile(imageUrl);

    try {
      const formData = new FormData();
      formData.append("UserId", userId!);
      formData.append("Image", selectedFile);

      const response = await axiosInstance.post("/api/ProfileImage", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      console.log(response);
      swal("Image Added Successfully.");
    } catch (error: any) {
      swal(error.response?.data?.message || "An error occurred.");
    }
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      
      <div style={{ fontSize: "30px", marginTop: "50px" }}>Upload Profile Image:</div>

      <Stack spacing={2} direction="row">
        <Button
          variant="contained"
          onClick={handleUploadClick}
          style={{ marginTop: "50px" }}
        >
          Upload 
        </Button>
      </Stack>

      <input
        type="file"
        ref={inputRef}
        onChange={handleFileChange}
        style={{ display: "none" }}
      />

    <div style={{ fontSize: "25px", marginTop: "10px" }}>Preview :</div>

      {file && (
        <img
          src={file}
          alt="Uploaded Image"
          style={{ height: "400px", width: "700px" }}
        />
      )}
    </div>
  );
}