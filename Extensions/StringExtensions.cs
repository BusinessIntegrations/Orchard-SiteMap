#region Using
using System;
using System.Linq;
#endregion

namespace WebAdvanced.Sitemap.Extensions {
    public static class StringExtensions {
        #region Methods
        public static string SlugToTitle(this string slug) {
            if (string.IsNullOrWhiteSpace(slug)) {
                return string.Empty;
            }
            var parts = slug.Split('-');
            return string.Join(" ",
                parts.Select(p => {
                        var a = p.ToCharArray();
                        a[0] = char.ToUpper(a[0]);
                        return new string(a);
                    })
                    .ToArray());
        }

        public static string UrlToTitle(this string url) {
            var slug = url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();
            return slug.SlugToTitle();
        }
        #endregion
    }
}
