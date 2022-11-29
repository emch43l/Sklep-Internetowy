using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sklep_Internetowy.Extensions
{
    public static class RatingHelper
    {
        public static HtmlString RenderRating (this IHtmlHelper helper, double? current, int max = 5) {

            string fullStar = "<i class='fas fa-star'></i>";
            string halfStar = "<i class='fas fa-star-half-alt'></i>";
            string emptyStar = "<i class='far fa-star'></i>";

            string output = "";

            if (current == null)
                return new HtmlString(String.Join("",Enumerable.Range(1, 5).Select(n => emptyStar)));

            for(int i = 1; i <= max; i++)
            {
                if(i <= current)
                {
                    output += fullStar;
                }
                else
                {
                    if(current >= (double)i - 0.5)
                        output += halfStar;
                    else
                        output += emptyStar;
                }
            }

            return new HtmlString(output);
        }
    }
}
