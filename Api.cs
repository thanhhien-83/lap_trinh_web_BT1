using System;
using System.Globalization;
using System.Web;
using MyLazyLib;

namespace LazyWebApp
{
    public class Api : IHttpHandler
    {
        static double P(string s)
        {
            if (s == null) return double.NaN;
            s = s.Trim().Replace(',', '.');
            double v;
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v) ? v : double.NaN;
        }

        static string Esc(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n");
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                double sleep = P(context.Request["s"]);
                double ent = P(context.Request["e"]);
                double study = P(context.Request["t"]);
                double work = P(context.Request["w"]); // thay cho g (game)

                LazyCalculator calc = new LazyCalculator();
                calc.Signature = "Web API — Thanh Hiền 83";
                calc.SleepHours = sleep;
                calc.EntertainmentHours = ent;
                calc.StudyHours = study;
                calc.WorkHours = work;

                LazyResult res;
                int code = calc.Process(out res);
                if (code < 0)
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("{\"ok\":false,\"msg\":\"" + Esc(calc.LastError) + "\"}");
                    return;
                }

                context.Response.Write("{"
                    + "\"ok\":true,"
                    + "\"li\":" + res.LazyIndex.ToString(CultureInfo.InvariantCulture) + ","
                    + "\"category\":\"" + Esc(res.Category) + "\","
                    + "\"emoji\":\"" + Esc(res.Emoji) + "\","
                    + "\"report\":\"" + Esc(calc.Report) + "\""
                    + "}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Write("{\"ok\":false,\"msg\":\"" + Esc(ex.Message) + "\"}");
            }
        }

        public bool IsReusable { get { return true; } }
    }
}