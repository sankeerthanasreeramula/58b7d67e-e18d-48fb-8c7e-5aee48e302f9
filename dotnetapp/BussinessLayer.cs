using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp
{
    public class BusinessLayer
    {
         DataAccessLayer dataAccessLayer = new DataAccessLayer();
        public string saveAdmin(UserModel user)
        {
            return dataAccessLayer.saveAdmin(user);
        }

        public Boolean isAdminPresent(LoginModel data)
        {
            return (dataAccessLayer.isAdminPresent(data));
        }

        public string saveUser(UserModel user)
        {
            return dataAccessLayer.saveUser(user);
        }
        
        public Boolean isUserPresent(LoginModel data)
        {
            return dataAccessLayer.isUserPresent(data);
        }


        //servicecentercontroller
        /*by creating an object for the data access layer, we are accessing all the methods */
        public string availableSlots(AppointmentModel m)
        {
            return dataAccessLayer.availableSlots(m);
        }
        public string addServiceCenter([FromBody] JsonElement jsonData)
        {
            return dataAccessLayer.addServiceCenter(jsonData);
        }
    }
}