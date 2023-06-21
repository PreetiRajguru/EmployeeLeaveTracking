import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import { CardActionArea } from "@mui/material";

export default function ActionAreaCard() {
  return (
    <div
      style={{ display: "flex", flexDirection: "column", alignItems: "center" }}
    >
      <Card sx={{ maxWidth: 700, mt: 3 }}>
        <CardActionArea>
          <CardMedia
            sx={{ height: 400 }}
            component="img"
            height="140"
            image="https://i0.wp.com/www.lovelycoding.org/wp-content/uploads/2022/09/Leave-Management-System.webp?fit=640%2C427&ssl=1"
            alt="leave"
          />
          <CardContent>
            <Typography gutterBottom variant="h5" component="div">
              LMS
              {/* <CardMedia
                sx={{ height: 400 }}
                component="img"
                height="140"
                image="https://img.freepik.com/free-vector/quitting-time-concept-illustration_114360-1657.jpg?w=900&t=st=1684495868~exp=1684496468~hmac=512077ad8392c558524c9a8ff98041e5ce3536f71b79f368c5b8a235f1b534f2"
                alt="leave"
              /> */}
              {/* <a href="https://www.flaticon.com/free-icons/finish" title="finish icons">Finish icons created by Leremy - Flaticon</a> */}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Leave management system (LMS) is a software-based solution that
              automates and streamlines the process of employee time off
              requests and approvals. The system allows employees to submit
              leave requests online, and managers can review and approve or deny
              requests based on the availability of staff and business needs.
              LMS helps organizations to manage employee absence efficiently,
              avoid conflicts, and ensure that the workload is distributed
              appropriately. The system can track different types of leaves,
              such as vacation, sick leave, and personal leave, and keep records
              of employee attendance and absenteeism. By using LMS,
              organizations can improve their productivity, reduce
              administrative burden, and ensure compliance with labor laws and
              company policies.
            </Typography>
          </CardContent>
        </CardActionArea>
      </Card>
    </div>
  );
}