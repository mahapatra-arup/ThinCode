using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinCode.Models.Tools
{
    public class WebMessageBox
    {
        #region Messege Alart
        public enum _messegeType { danger, info, warning, success }
        public enum _alartTopIndex { Simple, Topindex }
        public static string Show(string msg, string msgHeader, _messegeType type, _alartTopIndex ltop)
        {
            //------------Icon Set--------
            string ico = IcoSet(type);
            string msgHtmlString = "";

            //------------Generate Message String--------
            string strmsgValue = Enum.GetName(typeof(_messegeType), type);
            switch (ltop)
            {
                #region ========tOP iNDEX==============
                case _alartTopIndex.Topindex:
                    msgHtmlString
 =
                       //=============Messege box=============-
                       "<div id=\"msgdialog\" title=\"Basic dialog\"> " +
"  <p>This is the default dialog which is useful for displaying information. The dialog window can be moved, resized and closed with the 'x' icon.</p> " +
"</div> " +
"  <script> " +
"  $( function() { " +
"    $( \"#dialog\" ).dialog(); " +
"  } ); " +
"  </script> ";

                    
                    break;
                #endregion

                #region --------sIMPLE-----------
                case _alartTopIndex.Simple:
                     msgHtmlString 
 =
                        //=============Messege box=============-
                        "<div class = \"alert alert-" + strmsgValue + "  fade in w3-card-4\">" +
                "<a href =\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a>" +
                 "<strong>" + ico + " " + msgHeader + "</strong>  " + msg + "." +
                 "</div>";
                    //=============Messege box=============-

                    break;
                    #endregion
            }
            return  msgHtmlString = "";
            
        }

        private static string IcoSet(_messegeType type)
        {
            switch (type)
            {
                case _messegeType.danger:
                    return "&#10007;";
                case _messegeType.info:
                    return "&#x1F603;";
                case _messegeType.warning:
                    return "&#x26A0;";
                case _messegeType.success:
                    return "&#10004;";
            }
            return string.Empty;
        }
        #endregion
    }
}