using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThinCode.Models
{
    [MetadataType(typeof(QuestionTableMetaData))]
    public partial class QuestionTable
    {

    }
    public class QuestionTableMetaData
    {
        public long QusID { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date Is Required")]
        public Nullable<System.DateTime> Date { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Question Is Required")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Question Is Required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Question Is Required")]
        public string Subject { get; set; }

        public byte[] Image { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Question Is Required")]
        public string Tag { get; set; }

    }
}