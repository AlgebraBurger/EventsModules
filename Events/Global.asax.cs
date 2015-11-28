using System;
using System.Web;
namespace Events
{
    public class Global : System.Web.HttpApplication
    {
        private DateTime startTime;
        protected void Application_Start(object sender, EventArgs e)
        {
            EventCollection.Add(EventSource.Application, "Start");
            Application["message"] = "Application Events";
        }
        protected void Application_End(object sender, EventArgs e)
        {
            EventCollection.Add(EventSource.Application, "End");
        }

        //public override void Init()
        //{
        //    IHttpModule mod = Modules["AverageTime"];
        //    if (mod is AverageTimeModule)
        //    {
        //        ((AverageTimeModule)mod).NewAverage += (src, args) => {
        //            Response.Write(string.Format("<h3>Ave time: {0:F2}ms</h3>",
        //            args.AverageTime));
        //        };
        //    }
        //}

        public void AverageTime_NewAverage(object src, AverageTimeEventArgs args)
        {
            Response.Write(string.Format("<h3>Ave time: {0:F2}ms</h3>",
            args.AverageTime));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            startTime = Context.Timestamp;
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            double elapsed = DateTime.Now.Subtract(startTime).TotalMilliseconds;
            System.Diagnostics.Debug.WriteLine(
            string.Format("Duration: {0} {1}ms", Request.RawUrl, elapsed));
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.Url.LocalPath == "/Params.aspx" &&
            !User.Identity.IsAuthenticated)
            {
                Context.AddError(new UnauthorizedAccessException());
            }
        }
        protected void Application_LogRequest(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(
            string.Format("Request for {0} - code {1}",
            Request.RawUrl, Response.StatusCode));
        }
    }
}