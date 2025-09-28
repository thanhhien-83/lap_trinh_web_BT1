using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MyLazyLib;

namespace LazyWinFormApp
{
    public class Form1 : Form
    {
        TextBox txtSleep, txtEnt, txtStudy, txtWork;
        Label lblOut;
        Button btn;

        public Form1()
        {
            this.Text = "Lazy Index (WinForms) — Thanh Hiền 83";
            this.ClientSize = new Size(620, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label l1 = new Label(); l1.Text = "Ngủ (h/ngày):"; l1.Location = new Point(24, 24); l1.AutoSize = true;
            Label l2 = new Label(); l2.Text = "Giải trí (h/ngày):"; l2.Location = new Point(24, 56); l2.AutoSize = true;
            Label l3 = new Label(); l3.Text = "Học tập (h/ngày):"; l3.Location = new Point(24, 88); l3.AutoSize = true;
            Label l4 = new Label(); l4.Text = "Làm việc (h/ngày):"; l4.Location = new Point(24, 120); l4.AutoSize = true;

            txtSleep = new TextBox(); txtSleep.Location = new Point(180, 20); txtSleep.Width = 120; txtSleep.Text = "8";
            txtEnt = new TextBox(); txtEnt.Location = new Point(180, 52); txtEnt.Width = 120; txtEnt.Text = "1";
            txtStudy = new TextBox(); txtStudy.Location = new Point(180, 84); txtStudy.Width = 120; txtStudy.Text = "2";
            txtWork = new TextBox(); txtWork.Location = new Point(180, 116); txtWork.Width = 120; txtWork.Text = "4";

            btn = new Button(); btn.Text = "Tính"; btn.Location = new Point(180, 152); btn.Width = 120;
            btn.Click += new EventHandler(OnCalc);

            lblOut = new Label(); lblOut.Location = new Point(24, 188); lblOut.AutoSize = false; lblOut.Width = 560; lblOut.Height = 90;

            this.Controls.Add(l1); this.Controls.Add(l2); this.Controls.Add(l3); this.Controls.Add(l4);
            this.Controls.Add(txtSleep); this.Controls.Add(txtEnt); this.Controls.Add(txtStudy); this.Controls.Add(txtWork);
            this.Controls.Add(btn); this.Controls.Add(lblOut);
        }

        static double P(string s)
        {
            if (s == null) return double.NaN;
            s = s.Trim().Replace(',', '.');
            double v;
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v) ? v : double.NaN;
        }

        void OnCalc(object sender, EventArgs e)
        {
            double sleep = P(txtSleep.Text);
            double ent = P(txtEnt.Text);
            double study = P(txtStudy.Text);
            double work = P(txtWork.Text);

            LazyCalculator calc = new LazyCalculator();
            calc.Signature = "WinForms by Thanh Hiền 83";
            calc.SleepHours = sleep;
            calc.EntertainmentHours = ent;
            calc.StudyHours = study;
            calc.WorkHours = work;

            LazyResult res;
            int code = calc.Process(out res);
            if (code < 0)
            {
                MessageBox.Show(calc.LastError, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblOut.Text = string.Format("LI = {0} | {1} {2}\n{3}",
                res.LazyIndex.ToString(CultureInfo.InvariantCulture), res.Category, res.Emoji, calc.Report);
        }
    }
}