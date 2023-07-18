using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Supreme_Mobile.Models
{
 
    public class MyUserResultModel
    {
        public string Status { get; set; }
        public Int32 UserID { get; set; }
        public string UserName { get; set; }
    }

    public class ValidTokenResultModel
    {
        public Int32 UserID { get; set; }
        public string UserName { get; set; }
        public string TokenDate { get; set; }
        public string SecurityStamp { get; set; }
    }

    public class MyTokenResultModel
    {
        public string tokenAuth { get; set; }
    }
    public class GenericResultModel
    {
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}