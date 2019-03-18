using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThinCode.Models.Tools;

namespace ThinCode.Models
{
    public class LogInVM
    {
        private CodeliteEntities1 db = new CodeliteEntities1();

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool IsRememberMe { get; set; }
        public string ReturnURL { get; set; }

        #region ============== Comapare HASH Value==============
        public static bool CompareHashValue(string password, string username, string OldHASHValue, byte[] SALT)
        {
            try
            {
                string expectedHashString = GenerateHASHValue.GenerateHASH(password, username, SALT);

                return (OldHASHValue == expectedHashString);
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}