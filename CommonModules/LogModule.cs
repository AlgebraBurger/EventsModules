using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonModules
{
    public class LogModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.LogRequest += HandleEvent;
        }
        public void Dispose()
        {
            // nothing to do
        }
        protected void HandleEvent(object src, EventArgs args)
        {
            HttpApplication app = src as HttpApplication;
            System.Diagnostics.Debug.WriteLine(
            string.Format("Request for {0} - code {1}",
            app.Request.RawUrl, app.Response.StatusCode));
        }
    }
}
