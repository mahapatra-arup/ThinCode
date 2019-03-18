using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ThinCode
{
    public static  class QuestionsUtils
    {
        public static List<SelectListItem> GetQuestions()
        {
            //Call : @Html.DropDownListFor(model => model.Type, QuestionTypeUtils.GetQuestionType())

            var lstType = SqlHelper.dbContext.QuestionTables.ToList();
            List<SelectListItem> lSelectListItem = new List<SelectListItem>();
            if (lstType.IsValidList())
            {
                foreach (var item in lstType)
                {
                    string question = WebUtility.HtmlDecode(item.Question.ToString());
                    lSelectListItem.Add(new SelectListItem
                    {
                        Text = question,
                        Value = item.QusID.ToString()
                    });
                } 
            }
            return lSelectListItem;
        }
    }
}