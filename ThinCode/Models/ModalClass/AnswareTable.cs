using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThinCode.Models
{
    [MetadataType(typeof(AnswareTableMetaData))]
    public partial class AnswareTable
    {

    }
    public class AnswareTableMetaData
    {
        public long Id { get; set; }


        [Required(ErrorMessage = "Question Is Required")]
        [Display(Name ="Question")]
        public Nullable<long> QusID { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Asware Is Required")]
        public string Answer { get; set; }

        public Nullable<bool> CorrectAns { get; set; }
        public byte[] Image { get; set; }
        public string Remarks { get; set; }
        public string ProjectFilePath { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Is Required")]
        public Nullable<System.DateTime> Date { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        public string ContactNo { get; set; }
        
        public virtual QuestionTable QuestionTable { get; set; }
    }
}