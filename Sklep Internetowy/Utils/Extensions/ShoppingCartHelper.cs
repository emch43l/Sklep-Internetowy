using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Utils.Extensions
{
    public static class ShoppingCartHelper
    {
        public static HtmlString DisplayShoppingCartItemsNumber(this IHtmlHelper helper, Cart? cart)
        {
            if (cart == null)
                return new HtmlString(null);
            if (cart.Items.Count == 0)
                return new HtmlString(null);
            return new HtmlString($"<div class=\"cart-item-number\"><span>{cart.Items.Count()}</span></div>");
        }
    }
}
