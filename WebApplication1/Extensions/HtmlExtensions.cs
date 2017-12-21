using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Extensions
{
    public static class HtmlExtensions
    {

        public static HtmlString Img(this HtmlHelper helper, string src, int width=0, int height=0)
        {
            TagBuilder tag = new TagBuilder("img");
            tag.Attributes.Add("src", src);
            if (width != 0)
            {
                tag.Attributes.Add("width", width + "px");
            }

            if (height != 0)
            {
                tag.Attributes.Add("height", height + "px"); 
            }

            return new HtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static HtmlString Ul(this HtmlHelper helper, IEnumerable<MailAdress> elements)
        {
            StringBuilder stag = new StringBuilder("<ul>");

            foreach(var mail in elements)
            {
                stag.AppendLine(String.Format("<li>{0} <a href='{1}'>usuń</a></li>", mail.Adres, "MailAdress/Remove?ID="+mail.ID));
            }
            stag.AppendLine("</ul>");

            HtmlString s = new HtmlString(stag.ToString());
            return s;
        }

        public static HtmlString UlUniversal(this HtmlHelper helper, IEnumerable<object> elements)
        {
            StringBuilder stag = new StringBuilder("<ul>");

            foreach (var obj in elements)
            {
                stag.AppendLine(String.Format("<li>{0}</li>",obj.ToString()));
            }
            stag.AppendLine("</ul>");

            HtmlString s = new HtmlString(stag.ToString());
            return s;
        }


        public static HtmlString UlDictionary(this HtmlHelper helper, IDictionary<string,decimal> dict)
        {
            StringBuilder stag = new StringBuilder("<ul>");

            foreach (var obj in dict)
            {
                stag.AppendLine(String.Format("<li>{0}: {1}</li>", obj.Key,obj.Value));
            }
            stag.AppendLine("</ul>");

            HtmlString s = new HtmlString(stag.ToString());
            return s;
        }

    }
}