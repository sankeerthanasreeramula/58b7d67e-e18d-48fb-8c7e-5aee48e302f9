using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web.Http;
using dotnetapp.Models;
using System.Data;
using System.Text.Json.Serialization;
using System.Net.Mail;
using System.Net;


namespace dotnetapp
{
    public class DataAccessLayer
    {
        /*The DataAccessLayer class represents a data access layer in the application. 
     * It is responsible for handling database operations and interacting with the underlying database. */
        /*The DataAccessLayer class represents a data access layer in the application. 
     * It is responsible for handling database operations and interacting with the underlying database. */

        /*creates a new instance of the SqlConnection class, which establishes a connection to the database.
         the connection string indicates the server name (DESKTOP-IB4OOTB\\SQLEXPRESS), the database name (CameraServices), 
        and that Windows integrated security should be used for authentication*/
        /*creates a new instance of the SqlConnection class, which establishes a connection to the database.
         the connection string indicates the server name (DESKTOP-IB4OOTB\\SQLEXPRESS), the database name (CameraServices), 
        and that Windows integrated security should be used for authentication*/
        SqlConnection conn = new SqlConnection("User ID=sa;password=examlyMssql@123;Server=localhost;Database=master2;trusted_connection=false;Persist Security Info=False;Encrypt=False");

        /*SqlCommand declares a SqlCommand object that will be used to execute SQL commands 
        * against the database.*/

        /*SqlCommand declares a SqlCommand object that will be used to execute SQL commands 
        * against the database.*/
        SqlCommand cmd = null;

        /* SqlDataAdapter object that provides the ability 
      * to fill a DataSet or DataTable with data from the database. */

        /* SqlDataAdapter object that provides the ability 
      * to fill a DataSet or DataTable with data from the database. */
        SqlDataAdapter da = null;

        /* SqlDataReader object that allows forward-only, 
        * read-only access to the result of executing a SQL query against the database. */

        /* SqlDataReader object that allows forward-only, 
        * read-only access to the result of executing a SQL query against the database. */
        SqlDataReader dr = null;


        //Auth Controller

        /*This method helps to save the admin data in the database.  */

        //Auth Controller

        /*This method helps to save the admin data in the database.  */
        internal string saveAdmin(UserModel user)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("AddAdmin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@username", user.UserName);
                cmd.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                cmd.Parameters.AddWithValue("@userRole", user.UserRole);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                if (data >= 1)
                {
                    msg = "Admin Added";
                }
                else
                {
                    msg = "Email Id or Mobile Number already Exists!";
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
        /*This method helps to check whether the admin present or not and check the email and password ae correct and return the boolean value.  */
        /*This method helps to check whether the admin present or not and check the email and password ae correct and return the boolean value.  */
        internal Boolean isAdminPresent(LoginModel data)
        {
            Boolean msg = false;
            try
            {
                da = new SqlDataAdapter("CameraServiceAdminLogin", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@email", data.Email);
                da.SelectCommand.Parameters.AddWithValue("@password", data.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }
            catch (Exception e)
            {

            }
            return msg;
        }
        /*This method helps to save the user data in the database.  */
        /*This method helps to save the user data in the database.  */
        internal string saveUser(UserModel user)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@username", user.UserName );
                cmd.Parameters.AddWithValue("@mobileNumber", user.MobileNumber);
                cmd.Parameters.AddWithValue("@userRole", user.UserRole);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                if (data >= 1)
                {
                    MailMessage email = new MailMessage();
                    email.From = new MailAddress("kraftcamservices@gmail.com");
                    email.To.Add(user.Email);
                    email.Subject = "Thanks for Signing in with our services Kraft-Cam";
                    string emailBody = $"Hello {user.UserName},\n\n"
                    + "Thank you for signing up for our website! We are thrilled to have you as a new member of our community and are excited to provide you with our exceptional services.\n\n"
                    + "At Kraft-Cam, our mission is to deliver top-notch Camera Services that meet your needs and exceed your expectations. We have a team of dedicated professionals who are passionate about ensuring your satisfaction.\n\n"
                    + "As a valued member, you now have access to a wide range of features and benefits. Whether it's Lens Clean, Internal services, Battery services, etc., we have designed our platform to cater to your needs.\n\n"
                    + "We invite you to explore our website and take full advantage of the resources available to you. Should you have any questions, concerns, or feedback, our friendly support team is always here to assist you. We value your input and strive to continuously improve our services based on your valuable insights.\n\n"
                    + "Once again, thank you for choosing Kraft-Cam. We truly appreciate your trust and confidence in us. We look forward to serving you and ensuring that your experience with us is nothing short of exceptional.\n\n"
                    + "Best wishes,\n"
                    + "Team Kraft_Cam";

                    email.Body = emailBody;

                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("kraftcamservices@gmail.com", "nvutaqbuynvnqeia")
                    };

                    smtp.Send(email);
                    msg = "User Added";
                }
                else
                {
                    msg = "Email Id or Mobile Number already Exists!";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
         /* This method helps to check whether the user present or not and check the email and password are correct and return the boolean value */
         /* This method helps to check whether the user present or not and check the email and password are correct and return the boolean value */
        internal Boolean isUserPresent(LoginModel data)
        {
            Boolean msg = false;
            try
            {
                da = new SqlDataAdapter("CameraServiceUserLogin", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@email", data.Email);
                da.SelectCommand.Parameters.AddWithValue("@password", data.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    msg = true;
                }
                else
                {
                    msg = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return msg;
        }
        /*this method helps to  retrieve admin's data based on the provided email.*/
        internal UserModel getAdminByEmailId(string email)
        {
            UserModel m = new UserModel();

            try
            {
                SqlDataReader dr;
                cmd = new SqlCommand("getAdminByEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read() == true)
                {
                    m.UserName = dr["UserName"].ToString();
                    m.UserRole = dr["UserRole"].ToString();
                    m.UserId =int.Parse(dr["UserId"].ToString());
                }
            }
            catch (Exception e)
            {

            }
            conn.Close();
            return m;
        }
        
        /*this method helps to  retrieve user's data based on the provided email.*/
        internal UserModel getUserByEmailId(string email)
        {
            UserModel m = new UserModel();

            try
            {
                SqlDataReader dr;
                cmd = new SqlCommand("getUserByEmail", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);

                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read() == true)
                {
                    m.UserName = dr["UserName"].ToString();
                    m.UserRole = dr["UserRole"].ToString();
                    m.UserId = int.Parse(dr["UserId"].ToString());
                }
            }
            catch (Exception e)
            {

            }
            conn.Close();
            return m;
        }
        //AppointmentController

        internal List<ProductModel> getAllAppointments()
        {
            List<ProductModel> m = new List<ProductModel>();
            SqlDataReader dr;

            cmd = new SqlCommand("getAllAppointments", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read() == true)
            {
                ProductModel model = new ProductModel();
                model.ID =int.Parse( dr["ID"].ToString());
                model.customerName = dr["customerName"].ToString();
                model.email = dr["email"].ToString();
                model.productName = dr["productName"].ToString();
                model.dateOfAppointment = DateTime.Parse(dr["dateOfAppointment"].ToString());
                model.contactNumber = dr["contactNumber"].ToString();
                model.bookedSlots = dr["bookedSlots"].ToString();
                model.serviceCenterId = dr["serviceCenterId"].ToString();
                model.serviceCenterName = dr["serviceCenterName"].ToString();
                model.dateOfAppointmentBooking = DateTime.Parse(dr["dateOfAppointmentBooking"].ToString());
                model.serviceCost = (dr["serviceCost"].ToString());
                m.Add(model);
            }
            conn.Close();
            return m;
        }

       
        /*this method insert the availableSlots while adding the service center*/
        internal string availableSlots(AppointmentModel m)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("InsertAvailableSlots", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceCenterId", m.serviceCenterId);

                List<string> availableSlots = m.availableSlots;
                string slotsString = string.Join(",", availableSlots);
                cmd.Parameters.AddWithValue("@availableSlots", slotsString);

                conn.Open();
                int data = cmd.ExecuteNonQuery();
                conn.Close();

                msg = "Success";
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
           /*this method helps to get the service center details by their id*/
        internal ServiceCenterModel viewServiceCenterByID(string serivceCenterId)
        {
            SqlDataReader dr;
            ServiceCenterModel model = new ServiceCenterModel();
            cmd = new SqlCommand("getServiceCenterById", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@serviceCenterId", serivceCenterId);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read() == true)
            {

                model.serviceCenterId = dr["serviceCenterId"].ToString();
                model.serviceCenterName = dr["serviceCenterName"].ToString();
                model.serviceCenterPhone = dr["serviceCenterPhone"].ToString();
                model.serviceCenterAddress = dr["serviceCenterAddress"].ToString();
                model.serviceCenterImageUrl = dr["serviceCenterImageUrl"].ToString();
                model.serviceCenterMailId = dr["serviceCenterMailId"].ToString();
                model.serviceCost = (dr["serviceCost"].ToString());
                model.serviceCenterStartTime = TimeSpan.Parse(dr["serviceCenterStartTime"].ToString());
                model.serviceCenterEndTime = TimeSpan.Parse(dr["serviceCenterEndTime"].ToString());
                model.serviceCenterDescription = dr["serviceCenterDescription"].ToString();

            }
            conn.Close();
            return model;
        }
        /*this method update the slots while updating the service center*/
        internal string updateGetSlots(string serviceCenterId, AppointmentModel model)
        {
            string msg = string.Empty;

            try
            {
                cmd = new SqlCommand("editAvailableSlots", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceCenterId", serviceCenterId);
                List<string> availableSlots = model.availableSlots;
                string slotsString = string.Join(",", availableSlots);
                cmd.Parameters.AddWithValue("@availableSlots", slotsString);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                conn.Close();

                if (data >= 1)
                {
                    msg = "Service center updated";
                }
                else
                {
                    msg = "Failed";
                }


            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
        /*this method deletes the service center by the id*/
        internal string deleteServiceCenter(string serivceCenterId)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("deleteServiceCenterId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceCenterId", serivceCenterId);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                conn.Close();
                if (data >= 1)
                {
                    msg = "Service center deleted";
                }
                else
                {
                    msg = "Can't Delete Service Center Appointments booked for that service center";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return msg;
        }

        /*this method deletes the available slots while deleting the service center*/
        internal string deleteAvailableSlots(string serviceCenterId)
        {
            string msg = string.Empty;
            try
            {
                cmd = new SqlCommand("deleteAvailableSlots", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceCenterId", serviceCenterId);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                conn.Close();
                if (data >= 1)
                {
                    msg = "Service center deleted";
                }
                else
                {
                    msg = "Failed to Delete";
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return msg;
        }
        /*this method edit the service center details according to the id*/
        internal string editServiceCenter(string serviceCenterId, [FromBody] JsonElement jsonData)
        {
            string msg = string.Empty;

            try
            {

                var options = new JsonSerializerOptions
                {
                    Converters = { new TimeSpanConverter() }
                };

                var model = JsonSerializer.Deserialize<ServiceCenterModel>(jsonData.GetRawText(), options);

                cmd = new SqlCommand("updateAddCenters", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceCenterId", serviceCenterId);
                cmd.Parameters.AddWithValue("@serviceCenterName", model.serviceCenterName);
                cmd.Parameters.AddWithValue("@serviceCenterPhone", model.serviceCenterPhone);
                cmd.Parameters.AddWithValue("@serviceCenterAddress", model.serviceCenterAddress);
                cmd.Parameters.AddWithValue("@serviceCenterImageUrl", model.serviceCenterImageUrl);
                cmd.Parameters.AddWithValue("@serviceCenterMailId", model.serviceCenterMailId);
                cmd.Parameters.AddWithValue("@serviceCost", model.serviceCost);
                cmd.Parameters.AddWithValue("@serviceCenterStartTime", model.serviceCenterStartTime);
                cmd.Parameters.AddWithValue("@serviceCenterEndTime", model.serviceCenterEndTime);
                cmd.Parameters.AddWithValue("@serviceCenterDescription", model.serviceCenterDescription);
                conn.Open();
                int data = cmd.ExecuteNonQuery();
                conn.Close();

                if (data >= 1)
                {
                    msg = "Service center updated";
                }
                else
                {
                    msg = "Failed";
                }


            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;

        }
        
    }
}