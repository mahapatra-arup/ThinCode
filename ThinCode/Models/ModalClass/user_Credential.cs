using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThinCode.Models.ModalClass
{
    [MetadataType(typeof(user_CredentialMetaData))]
    public partial class user_Credential
    { 
    }

    public class user_CredentialMetaData
    {
        public long Id { get; set; }
        public string user_Name { get; set; }
        public string user_passWord { get; set; }
        public string user_FirstName { get; set; }
        public string User_LastName { get; set; }
        public string user_email { get; set; }
        public string user_registered_Date { get; set; }
        public Nullable<bool> Activity { get; set; }
        public string display_name { get; set; }
        public byte[] User_Image { get; set; }
        public string User_Address { get; set; }
        public string User_Country { get; set; }
        public Nullable<int> User_PostalCode { get; set; }
        public string User_Abouts { get; set; }

       
    }
}