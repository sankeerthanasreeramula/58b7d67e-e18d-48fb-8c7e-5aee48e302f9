import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';  
import { Nav, Navbar, Container, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';



function AddCenter() {
  
  const navigate = useNavigate();
  const [email, setEmail] = useState("")


  useEffect(() => {
    const email = localStorage.getItem("email")
    setEmail(email)
  }, [])


  const Logout = () => {
    localStorage.removeItem("email");
    navigate("/")
  }

return(
<div>
<Navbar bg="primary" expand="md">  
  <Container>  
    <Navbar.Brand href="#AddCenter">Kraft Cam</Navbar.Brand>  
    <Navbar.Toggle aria-controls="basic-navbar-nav" />  
    <Navbar.Collapse id="basic-navbar-nav">  
      <Nav className="me-auto"> 
        <Link to="/admin/addServiceCenter" activeClassName='active' style={{color:'black', marginRight: '10px', textDecoration: 'none'}}>Add Center</Link>  
        <Link to="/admin/Centerprofile" activeClassName='active' style={{color:'black', marginRight: '10px', textDecoration: 'none'}}>Center Profile</Link>   
      </Nav>  
      <Nav class="collapse navbar-collapse justify-content-end">
      {email} &nbsp;
        <Button onClick={Logout} activeClassName='active' style={{color:'black', textDecoration: 'none'}}>Logout</Button>
      </Nav> 
    </Navbar.Collapse>  
  </Container>  
</Navbar>  
</div>
)
}

export default AddCenter;