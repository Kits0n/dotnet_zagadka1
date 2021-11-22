using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace test6.Tools
{
    public class ThemesViewLocationExpander : IViewLocationExpander
    {
        public string Theme { get; set; }

        public ThemesViewLocationExpander(string theme)
        {
            this.Theme = theme;
        }
        
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["theme"] = Theme;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var theme = context.Values["theme"];
            return viewLocations.Select(x => x.Replace("/Views/", "/Views/" + theme + "/")).Concat(viewLocations);
        }
    }
}