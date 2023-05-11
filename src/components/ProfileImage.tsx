import { ChangeEvent, useRef, useState } from "react";
import Button from "@mui/material/Button";
import Stack from "@mui/material/Stack";
import axios from "axios";

const UserId = localStorage.getItem("id");

export default function ProfileImage() 
{
  const [file, setFile] = useState<any>({
    UserId : "",
    Image : ""
  });
  
  const inputRef = useRef<HTMLInputElement | null>(null);

  const handleUploadClick = () => {
    inputRef.current?.click();
  };

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
    if (!e.target.files) {
      return;
    }

    setFile({
      UserId: UserId,
      Image: URL.createObjectURL(e.target.files[0])
    });
console.log("file-----", file)

    try {
      axios.post("/api/ProfileImage", file).then((response) => {
        console.log(response);
        alert("Image Added Successfully.");
      });
    } catch (error: any) {
      alert(error.response.data.message);
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
      <div style={{ fontSize: "30px", marginTop: "50px" }}>Upload Image:</div>

      <Stack spacing={2} direction="row">
        <Button
          variant="contained"
          onClick={handleUploadClick}
          style={{ marginTop: "50px" }}
        >
          Upload Image
        </Button>
      </Stack>

      <input
        type="file"
        ref={inputRef}
        onChange={handleFileChange}
        style={{ display: "none" }}
      />

      <img
        src={file.Image}
        alt="sds"
        style={{ height: "500px", width: "900px" }}
      ></img>
    </div>
  );
}