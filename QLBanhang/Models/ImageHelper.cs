using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QLBanhang.Models
{
    public static class ImageHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src,
            string altText, string height)
        {
            var buider = new TagBuilder("img");
            buider.MergeAttribute("src ", src);
            buider.MergeAttribute("alt", altText);
            buider.MergeAttribute("height", height);
            return MvcHtmlString.Create(buider.ToString(TagRenderMode.SelfClosing));
        }
    }
}