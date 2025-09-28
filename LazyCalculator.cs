using System;
using System.Globalization;
using System.Text;

namespace MyLazyLib
{
    public class LazyResult
    {
        private double _lazyIndex;
        private string _category;
        private string _emoji;
        private string _advice;

        public double LazyIndex { get { return _lazyIndex; } set { _lazyIndex = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public string Emoji { get { return _emoji; } set { _emoji = value; } }
        public string Advice { get { return _advice; } set { _advice = value; } }
    }

    // C# 2.0 compatible (hỗ trợ .NET 2.0/3.5)
    public class LazyCalculator
    {
        private double _sleepHours;         // S
        private double _entertainmentHours; // E - Giải trí (đã gồm chơi game)
        private double _studyHours;         // T - Học tập
        private double _workHours;          // W - Làm việc

        private string _signature;
        private string _lastError;
        private string _report;

        public double SleepHours { get { return _sleepHours; } set { _sleepHours = value; } }
        public double EntertainmentHours { get { return _entertainmentHours; } set { _entertainmentHours = value; } }
        public double StudyHours { get { return _studyHours; } set { _studyHours = value; } }
        public double WorkHours { get { return _workHours; } set { _workHours = value; } }

        public string Signature { get { return _signature; } set { _signature = value; } }
        public string LastError { get { return _lastError; } }
        public string Report { get { return _report; } }

        public LazyCalculator()
        {
            _signature = "Thanh Hiền 83 • MyLazyLib";
            _lastError = null;
            _report = "";
        }

        public int Process(out LazyResult result)
        {
            result = new LazyResult();

            if (!Validate())
            {
                return -1;
            }

            // Phạt khi ngủ > 8h
            double sleepPenalty = 0.0;
            if (_sleepHours > 8.0)
            {
                sleepPenalty = ((_sleepHours - 8.0) / 8.0) * 35.0;
                if (sleepPenalty > 35.0) sleepPenalty = 35.0;
            }

            double entScore = (_entertainmentHours <= 8.0 ? _entertainmentHours : 8.0) / 8.0 * 50.0; // tối đa 50
            double studyBonus = (_studyHours <= 8.0 ? _studyHours : 8.0) / 8.0 * 45.0;                  // tối đa 45 (trừ)
            double workBonus = (_workHours <= 8.0 ? _workHours : 8.0) / 8.0 * 50.0;                  // tối đa 50 (trừ)

            double index = 30.0 + sleepPenalty + entScore - studyBonus - workBonus;
            if (index < 0.0) index = 0.0;
            if (index > 100.0) index = 100.0;
            index = Math.Round(index, 1);

            string category = "Bình thường";
            string emoji = "😎";
            if (index < 30.0) { category = "Chăm chỉ"; emoji = "🐝"; }
            else if (index >= 60.0) { category = "Lười"; emoji = "🐻💤"; }

            string advice = BuildAdviceDetailed(index);

            result.LazyIndex = index;
            result.Category = category;
            result.Emoji = emoji;
            result.Advice = advice;

            // Report nhiều dòng
            StringBuilder sb = new StringBuilder();
            sb.Append("Lazy Index (LI) = ").Append(index.ToString(CultureInfo.InvariantCulture))
              .Append(" (").Append(category).Append(") ").Append(emoji).Append(".\n");
            sb.Append("Chi tiết ngày: ")
              .Append("Ngủ ").Append(_sleepHours.ToString("0.##", CultureInfo.InvariantCulture)).Append("h, ")
              .Append("Giải trí ").Append(_entertainmentHours.ToString("0.##", CultureInfo.InvariantCulture)).Append("h, ")
              .Append("Học tập ").Append(_studyHours.ToString("0.##", CultureInfo.InvariantCulture)).Append("h, ")
              .Append("Làm việc ").Append(_workHours.ToString("0.##", CultureInfo.InvariantCulture)).Append("h.\n");
            sb.Append(advice);
            if (!string.IsNullOrEmpty(_signature)) sb.Append("\n| ").Append(_signature);
            _report = sb.ToString();

            return 0;
        }

        private bool Validate()
        {
            if (_sleepHours < 0 || _sleepHours > 24) { _lastError = "Giờ ngủ phải trong khoảng 0..24."; return false; }
            if (_entertainmentHours < 0 || _entertainmentHours > 24) { _lastError = "Giờ giải trí phải trong khoảng 0..24."; return false; }
            if (_studyHours < 0 || _studyHours > 24) { _lastError = "Giờ học tập phải trong khoảng 0..24."; return false; }
            if (_workHours < 0 || _workHours > 24) { _lastError = "Giờ làm việc phải trong khoảng 0..24."; return false; }

            double sum = _sleepHours + _entertainmentHours + _studyHours + _workHours;
            if (sum > 24.0) { _lastError = "Tổng giờ trong ngày vượt quá 24h."; return false; }
            _lastError = null;
            return true;
        }

        private string BuildAdviceDetailed(double index)
        {
            StringBuilder sb = new StringBuilder();

            // Mức LI
            if (index < 30.0)
                sb.Append("• Mức độ: Chăm chỉ. Duy trì cân bằng; nghỉ ngơi đủ 7–9h.\n");
            else if (index < 45.0)
                sb.Append("• Mức độ: Hơi lười. Điều chỉnh nhẹ 0.5–1h/ngày (giảm giải trí, tăng học/làm việc).\n");
            else if (index < 60.0)
                sb.Append("• Mức độ: Trung bình. Tái phân bổ 1–2h/ngày: cắt bớt giải trí, thêm học hoặc làm việc.\n");
            else if (index < 75.0)
                sb.Append("• Mức độ: Lười. Giảm 2–3h giải trí; đặt lịch học/làm việc ≥ 60–90 phút/ngày.\n");
            else
                sb.Append("• Mức độ: Rất lười. Kế hoạch 7 ngày mạnh: giới hạn nghiêm ngặt giải trí, học/làm việc đều.\n");

            // Ngủ
            if (_sleepHours < 6.0)
                sb.Append("• Giấc ngủ: <6h → thiếu ngủ. Thêm 15–30′ mỗi đêm tới 7–9h; tránh màn hình 60′ trước ngủ.\n");
            else if (_sleepHours <= 9.0)
                sb.Append("• Giấc ngủ: 7–9h lành mạnh. Giữ giờ ngủ/thức cố định, hạn chế cafein sau 15:00.\n");
            else if (_sleepHours < 10.0)
                sb.Append("• Giấc ngủ: >9h hơi dư. Giảm ~30–60′, thay bằng vận động nhẹ buổi sáng.\n");
            else
                sb.Append("• Giấc ngủ: ≥10h quá nhiều. Rút 1–2h, chia cho học/làm việc; theo dõi buồn ngủ ban ngày.\n");

            // Giải trí
            if (_entertainmentHours > 4.0)
                sb.Append("• Giải trí: >4h/ngày nhiều. Đặt giới hạn 1–2h; ưu tiên hoạt động ngoài trời/đọc 20–30′.\n");
            else if (_entertainmentHours >= 2.0)
                sb.Append("• Giải trí: 2–4h vừa. Dùng hẹn giờ để không lấn sang giờ học/làm việc.\n");
            else
                sb.Append("• Giải trí: <2h tốt. Giữ cân bằng; xen kẽ vận động nhẹ.\n");

            // Học tập
            if (_studyHours < 1.0)
                sb.Append("• Học tập: <1h. Bắt đầu 30–60′/ngày (Pomodoro 25/5).\n");
            else if (_studyHours < 2.0)
                sb.Append("• Học tập: 1–2h ổn. Nâng thêm 30′ cho môn khó; ôn tập cuối ngày.\n");
            else if (_studyHours <= 4.0)
                sb.Append("• Học tập: 2–4h tốt. Nghỉ 5–10′ mỗi 50–60′ học.\n");
            else
                sb.Append("• Học tập: >4h. Tránh quá tải; thêm vận động nhẹ.\n");

            // Làm việc
            if (_workHours < 2.0)
                sb.Append("• Làm việc: <2h. Nếu là sinh viên/part‑time, cân nhắc tăng 1–2h công việc hữu ích (project nhỏ/việc nhà).\n");
            else if (_workHours < 4.0)
                sb.Append("• Làm việc: 2–4h ổn với lịch học. Xác định mục tiêu rõ ràng theo ngày.\n");
            else if (_workHours <= 8.0)
                sb.Append("• Làm việc: 4–8h tốt. Chia khối 50/10 để giữ tập trung; review nhiệm vụ cuối ngày 5′.\n");
            else
                sb.Append("• Làm việc: >8–10h. Cân bằng để tránh kiệt sức; đảm bảo ngủ ≥7h và có 30′ vận động.\n");

            // Quỹ thời gian
            double sum = _sleepHours + _entertainmentHours + _studyHours + _workHours;
            if (sum >= 22.0)
                sb.Append("• Quỹ thời gian: ngày gần kín (≥22h). Rà soát ước lượng thời gian, cắt nội dung ít giá trị.\n");
            else if (sum <= 16.0)
                sb.Append("• Quỹ thời gian: còn dư ~").Append((24.0 - sum).ToString("0.#", CultureInfo.InvariantCulture)).Append("h. Thêm 30–60′ học hoặc việc nhà.\n");

            // Kế hoạch 7 ngày
            if (index < 45.0)
                sb.Append("• Kế hoạch 7 ngày: giữ giờ ngủ cố định; +30′ học mỗi ngày; giải trí ≤2h; thử thêm 1 nhiệm vụ nhỏ/ngày.\n");
            else if (index < 60.0)
                sb.Append("• Kế hoạch 7 ngày: cắt 1h giải trí, +1h học/làm việc; chốt danh sách 3 việc quan trọng mỗi sáng.\n");
            else
                sb.Append("• Kế hoạch 7 ngày: cắt 2h giải trí, +90′ học/làm việc (3×30′); ngủ 7–9h; review tiến độ 5′ mỗi tối.\n");

            return sb.ToString();
        }
    }
}