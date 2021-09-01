using System.Web;
using System.Web.Optimization;

namespace Molotok34
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/query.min.js",
                      "~/Scripts/tether.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/jquery.easing.js",
                      "~/Scripts/jquery-validate.js",
                      "~/Scripts/numinate.min.js",
                      "~/Scripts/jquery.magnific-popup.min.js",
                      "~/Scripts/price_range_script.js",
                      "~/Scripts/easyzoom.js",
                      "~/Scripts/main.js",
                      "~/Scripts/jquery.themepunch.tools.min.js",
                      "~/Scripts/jquery.themepunch.revolution.min.js",
                      "~/Scripts/slider.js",
                      "~/Scripts/revolution.extension.actions.min.js",
                      "~/Scripts/revolution.extension.carousel.min.js",
                      "~/Scripts/revolution.extension.kenburn.min.js",
                      "~/Scripts/revolution.extension.layeranimation.min.js",
                      "~/Scripts/revolution.extension.migration.min.js",
                      "~/Scripts/revolution.extension.navigation.min.js",
                      "~/Scripts/revolution.extension.parallax.min.js",
                      "~/Scripts/revolution.extension.slideanims.min.js",
                      "~/Scripts/revolution.extension.video.min.js",
                      "~/Scripts/mdb.min.js",
                      "~/vendor/bootstrap/js/bootstrap.bundle.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/themify-icons.css",
                      "~/Content/layers.css",
                      "~/Content/settings.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/megamenu.css",
                      "~/Content/shortcodes.css",
                      "~/Content/main.css",
                      "~/Content/responsive.css",
                      "~/Content/site.css"));
        }
    }
}
