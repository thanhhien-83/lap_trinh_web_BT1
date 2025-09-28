# lap_trinh_web_BT1  
TẠO SOLUTION GỒM CÁC PROJECT SAU:  
1. DLL đa năng, keyword: c# window library -> Class Library (.NET Framework) bắt buộc sử dụng .NET Framework 2.0: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL.   DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app  dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).  
2. Console app, bắt buộc sử dụng .NET Framework 2.0, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => Console App (.NET Framework), biên   dịch ra EXE  
3. Windows Form Application, bắt buộc sử dụng .NET Framework 2.0**, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form,   phải có dấu án cá nhân; keyword: c# window Desktop => Windows Form Application (.NET Framework), biên dịch ra EXE  
4. Web đơn giản, bắt buộc sử dụng .NET Framework 2.0, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập   được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên.  kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => ASP.NET Web Application (.NET  Framework) + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.

# BÀI LÀM
# Giới thiệu đề tài
  Trong bối cảnh quản lý thời gian và tự đánh giá hiệu suất cá nhân ngày càng quan trọng, đề tài “Tính Chỉ Số Lười Biếng (Lazy Index)” được xây dựng nhằm chuyển hóa thói quen phân bổ thời gian thành một chỉ   số định lượng dễ hiểu. Hệ thống thu nhận bốn nhóm thời gian (ngủ, giải trí, học tập, làm việc), áp dụng một công thức có trọng số để phản ánh cân bằng giữa hoạt động thụ động và chủ động, sau đó chuẩn hóa   kết quả vào thang 0–100. Kết quả không chỉ là con số mà còn kèm phân loại, biểu tượng trực quan và thông điệp gợi ý cải thiện. Kiến trúc được thiết kế theo hướng “một logic – nhiều giao diện”, bảo đảm tính  tái sử dụng và dễ mở rộng.  

# 1. Thư viện DLL (Class Library – .NET Framework 2.0)  
Vai trò: Chứa toàn bộ logic tính “Lazy Index” (nhận các giờ đầu vào, tính ra LI 0–100, phân loại, icon, thông điệp).  
Không nhập/xuất trực tiếp (không Console, không UI, không phụ thuộc web).  
Expose: các thuộc tính input + phương thức tính + các thuộc tính kết quả.  
Dùng lại cho cả 3 ứng dụng còn lại.  

# 2. Ứng dụng Console (.NET Framework 2.0)  
Nhiệm vụ: Hỏi người dùng các giờ → gọi DLL → in ra LI, mức, icon, thông điệp.  
Mục đích chính: Test nhanh logic, minh họa tái sử dụng.  
Có “dấu ấn cá nhân” (ví dụ tên bạn, câu chào, lời khuyên).  

# 3. Ứng dụng Windows Forms (.NET Framework 2.0)  
Giao diện: TextBox nhập giờ, Button “Tính”, Label hiển thị kết quả (LI, icon, phân loại, thông điệp).  
Quy trình: Lấy dữ liệu form → gọi DLL → hiển thị lại trên form.  
Thêm chút trình bày (màu sắc / font / tiêu đề có tên bạn) để thể hiện tính cá nhân.  

# 4. Ứng dụng Web (ASP.NET 2.0 + IIS)  
Frontend: index.html (HTML/CSS/JS) nhập giờ, gửi yêu cầu.  
Backend: Endpoint (ví dụ api.ashx hoặc api.aspx) nhận POST → dùng DLL → trả JSON (LI, icon, phân loại, thông điệp).  
Chạy qua IIS (hoặc IIS Express khi dev), có thể thêm thanh màu thể hiện mức.  
JavaScript: kiểm tra dữ liệu trước khi gửi, nhận JSON rồi cập nhật giao diện.  
Giữ nét cá nhân (tiêu đề, footer, lời khuyên).  


























  
