using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ThinCode.Models.Tools.SoftTools
{
    public static class QuestionTypeUtils
    {
       
      /// <summary>
      /// 
      /// </summary>
      /// <param name="htmlHelper"></param>
      /// <param name="_name"></param>
      /// <param name="optionalLabel"></param>
      /// <param name="htmlAttributr"></param>
      /// <returns></returns>
        public static MvcHtmlString AddQuestionType(this HtmlHelper htmlHelper,string _name,string optionalLabel,object htmlAttributr)
        {
            //call : @Html.AddQuestionType("Type");

            var lstType = SqlHelper.dbContext.QuestionTypes;
            var list = (from item in lstType
                        select new SelectListItem()
                        {
                            Text = item.Type.ToString(),
                            Value = item.Id.ToString()
                        }).ToList();


            return htmlHelper.DropDownList(_name, list,optionalLabel,htmlAttributr);
        }
        /// <summary>
        /// Get Question Types
        /// </summary>
        /// <returns>List[SelectListItem]</returns>
        public static List<SelectListItem> GetQuestionType()
        {
            //Call : @Html.DropDownListFor(model => model.Type, QuestionTypeUtils.GetQuestionType())

            var lstType = SqlHelper.dbContext.QuestionTypes;
            var list = (from item in lstType
                        select new SelectListItem()
                        {
                            Text = item.Type.ToString(),
                            Value = item.Type.ToString()
                        }).ToList();


            return list;
        }
    }
}