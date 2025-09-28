using System;
using System.Globalization;
using MyLazyLib;

namespace LazyConsoleApp
{
    class Program
    {
        static double Parse(string s)
        {
            if (s == null) return double.NaN;
            s = s.Trim().Replace(',', '.');
            double v;
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v)) return v;
            return double.NaN;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Lazy Index Console — Thanh Hiền 83 ===");
            Console.Write("Ngủ (h/ngày): ");
            double sleep = Parse(Console.ReadLine());
            Console.Write("Giải trí (h/ngày): ");
            double ent = Parse(Console.ReadLine());
            Console.Write("Học tập (h/ngày): ");
            double study = Parse(Console.ReadLine());
            Console.Write("Làm việc (h/ngày): ");
            double work = Parse(Console.ReadLine());

            LazyCalculator calc = new LazyCalculator();
            calc.Signature = "Console by Thanh Hiền 83";
            calc.SleepHours = sleep;
            calc.EntertainmentHours = ent;
            calc.StudyHours = study;
            calc.WorkHours = work;

            LazyResult res;
            int code = calc.Process(out res);
            if (code < 0)
            {
                Console.WriteLine("Lỗi: " + calc.LastError);
                return;
            }

            Console.WriteLine("LI = {0} | {1} {2}", res.LazyIndex.ToString(CultureInfo.InvariantCulture), res.Category, res.Emoji);
            Console.WriteLine("Report:\n" + calc.Report);
        }
    }
}