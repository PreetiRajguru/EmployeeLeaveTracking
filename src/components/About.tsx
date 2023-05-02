import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import { CardActionArea } from '@mui/material';

export default function ActionAreaCard() {
    return (
        <Card sx={{ maxWidth: 700, mt: 3, ml:42 }}>
            <CardActionArea>
                <CardMedia
                sx={{height: 400}}
                    component="img"
                    height="140"
                    image="https://i0.wp.com/www.lovelycoding.org/wp-content/uploads/2022/09/Leave-Management-System.webp?fit=640%2C427&ssl=1"
                    alt="leave"
                />
                <CardContent>
                    <Typography gutterBottom variant="h5" component="div">
                        LMS
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        Leave management system (LMS) is a software-based solution that automates and streamlines the process of employee time off requests and approvals.
                        The system allows employees to submit leave requests online, and managers can review and approve or deny requests based on the availability of staff and business needs.
                        LMS helps organizations to manage employee absence efficiently, avoid conflicts, and ensure that the workload is distributed appropriately.
                        The system can track different types of leaves, such as vacation, sick leave, and personal leave, and keep records of employee attendance and absenteeism.
                        By using LMS, organizations can improve their productivity, reduce administrative burden, and ensure compliance with labor laws and company policies.
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}