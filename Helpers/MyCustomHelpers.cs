using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace LeagueManager.Helpers
{
    public static class MyCustomHelpers
    {
        public static string MyLabel(this HtmlHelper helper, string target, string text)
        {
            string str = "";
            if (target == "")
            {
                str = string.Format("<label>{0}</label>", text);
            }
            else {
                str = string.Format("<label for='{0}'>{1}</label>",target, text);
            }
            return str;            
        }


        public static MvcHtmlString MyCheckBoxList(this HtmlHelper helper, string name,
                                         IEnumerable<SelectListItem> items)
        {
            var output = new StringBuilder();
            output.Append(@"<div class=""checkboxList"">");

            foreach (var item in items)
            {
                output.Append(@"<input type=""checkbox"" name=""");
                output.Append(name);
                output.Append("\" value=\"");
                output.Append(item.Value);
                output.Append("\"");

                if (item.Selected)
                    output.Append(@" checked=""checked""");

                output.Append(" />");
                output.Append(item.Text);
                output.Append("<br />");
            }

            output.Append("</div>");

            return new MvcHtmlString(output.ToString());
        }

    }
}