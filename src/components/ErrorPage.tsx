import Container from '@material-ui/core/Container';
import Typography from '@material-ui/core/Typography';

const NotFound = () => {
  
    return (
      <Container maxWidth="md" sx={{pt: 4, pb: 4}} style={{display:'flex', flexDirection:'column', alignItems:'center', justifyContent:'center'}}>
        <Typography variant="h3" align="center">
          404 Error: Page Not Found
        </Typography>
        <Typography
          variant="body1"
          align="center"
          sx={{mt: 2}}
        >
          The page you are looking for does not exist or has been removed.
        </Typography>
        <img style={{width: '50%', height:'70%'}} src='https://static.vecteezy.com/system/resources/previews/008/568/882/original/website-page-not-found-error-404-robot-character-broken-chatbot-mascot-disabled-site-on-technical-work-web-design-template-cartoon-online-bot-crash-accident-robotic-assistance-failure-eps-vector.jpg' />
      </Container>
    );
  };
  
  export default NotFound;