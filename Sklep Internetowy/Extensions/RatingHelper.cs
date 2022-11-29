using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Extensions
{
    public static class RatingHelper
    {
        public static HtmlString RenderRating (this IHtmlHelper helper, double? current, int max = 5) 
        {

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

        public static HtmlString GetRatingRepresentativeStar(this IHtmlHelper helper, double? current, int max = 5)
        {
            HtmlString fullStar = new HtmlString("<i class='fas fa-star'></i>");
            HtmlString halfStar = new HtmlString("<i class='fas fa-star-half-alt'></i>");
            HtmlString emptyStar = new HtmlString("<i class='far fa-star'></i>");

            return (current == null) ? emptyStar : (current < 2) ? emptyStar : (current < 4) ? halfStar : fullStar;
        }

        public static HtmlString GenerateChartBar(this IHtmlHelper helper, int current, int max)
        {
            double percentage;
            if (max != 0)
                percentage = ((double)current / max) * 100;
            else
                percentage = 0;

            string bar = $"<div class=\"chart-bar\"> <div style=\"width:{percentage}%\" class=\"chart-bar-content\"></div> </div>";
            return new HtmlString(bar);
        }
    }
}
