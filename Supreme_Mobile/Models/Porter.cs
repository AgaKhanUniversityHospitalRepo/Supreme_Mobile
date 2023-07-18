using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supreme_Mobile.Models
{
    public class Porter
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Othername { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
    }
    public class CreatedPorterResultModel
    {
        public Int32 ColumnID { get; set; }
        public Int32 Porter_ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Othername { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CreatedBy { get; set; }
        public string UserStatus { get; set; }
    }
    public class MyPortersModel
    {
        public Int32 Porter_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MyPorterResultModel
    {
        public string Status { get; set; }
        public Int32 Porter_ID { get; set; }
        public string UserName { get; set; }
    }
}