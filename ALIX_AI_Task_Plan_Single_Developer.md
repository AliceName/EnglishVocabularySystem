# ALIX — Kế hoạch giao nhiệm vụ cho AI khi một người phát triển

## 0. Cách sử dụng tài liệu này

- Đặt `AGENTS.md` và tài liệu mô tả hệ thống ở thư mục gốc dự án.
- Mỗi task bên dưới tương ứng với **một lượt giao việc cho AI**. Không gửi tất cả task trong một prompt.
- Chỉ chuyển task khi tiêu chí nghiệm thu của task hiện tại đạt.
- Nếu repository đã có code, AI phải đọc code hiện tại và sửa tiếp, không tạo lại từ đầu.
- Stack phải theo quyết định mới nhất và repository thực tế. Tài liệu đặc tả ghi React + Tailwind CSS, ASP.NET Core Web API, SQL Server, EF Core; nếu đã chốt Next.js + Tailwind CSS thì tiếp tục dùng Next.js.
- Không yêu cầu AI làm các chức năng chưa chốt nghiệp vụ. Ghi câu hỏi vào `docs/OPEN_QUESTIONS.md`.

## 1. Quy trình dùng cho mọi task

Với mỗi task:
1. Yêu cầu AI đọc `AGENTS.md`, tài liệu liên quan và kiểm tra repository.
2. Yêu cầu AI tóm tắt hiện trạng, file sẽ sửa và kế hoạch trước khi code.
3. Chỉ rõ phạm vi task; không cho phép sửa lan sang module khác.
4. Yêu cầu AI triển khai theo convention hiện có.
5. Yêu cầu chạy build, lint và test phù hợp; không được nói test thành công nếu chưa chạy.
6. Yêu cầu báo cáo file đã tạo/sửa, migration/API/UI đã có, lệnh đã chạy, kết quả và lỗi còn lại.
7. Tự kiểm tra luồng trên máy; commit khi task đạt.

### Prompt khởi đầu dùng cho mọi task

```text
Đọc AGENTS.md và tài liệu đặc tả có liên quan trước khi làm.

Task: [TÊN TASK]
Mục tiêu: [KẾT QUẢ CỤ THỂ]
Hiện trạng: [NẾU BIẾT]
Phạm vi: [MODULE/FILE ĐƯỢC PHÉP THAY ĐỔI]
Không làm: [CÁC VIỆC NGOÀI PHẠM VI]
Phụ thuộc: [TASK/ENDPOINT/ENTITY CẦN CÓ TRƯỚC]

Trước khi code:
1. Kiểm tra repository, framework/version, cấu trúc thư mục và code hiện có.
2. Nêu ngắn gọn cách triển khai và các file dự kiến sửa.
3. Nêu các điểm thiếu hoặc mâu thuẫn trong yêu cầu; không tự đoán quy tắc nghiệp vụ.

Sau khi được yêu cầu triển khai:
1. Code theo convention hiện tại, không viết lại module không liên quan.
2. Kiểm tra validation, lỗi, quyền truy cập và các trường hợp biên.
3. Chạy build/lint/test liên quan và báo kết quả thực tế.
4. Báo cáo file thay đổi, cách kiểm thử thủ công, việc còn thiếu.
Không giả lập kết quả test và không đánh dấu hoàn thành nếu chưa kiểm chứng.
```

---

# Giai đoạn A — Kiểm tra và chuẩn bị

## TASK 01 — Kiểm tra hiện trạng repository

**Prompt bổ sung**
```text
Chỉ kiểm tra, chưa sửa code. Đọc AGENTS.md và tài liệu yêu cầu.
Kiểm tra cấu trúc thư mục, framework/version, solution/project, package,
database/migration, API hiện có, frontend route/page, test và lỗi build.
Phân loại: đã có, đang làm, chưa có, đang lỗi. Đề xuất thứ tự thực hiện.
Không tạo file hoặc thay đổi cấu hình ở task này.
```

**Nghiệm thu**
- Có báo cáo hiện trạng rõ ràng.
- Xác định được lệnh chạy frontend/backend.
- Không có file nào bị sửa ngoài ý muốn.

## TASK 02 — Chốt câu hỏi nghiệp vụ còn thiếu

```text
Đối chiếu tài liệu yêu cầu và AGENTS.md. Liệt kê các quy tắc mâu thuẫn/chưa rõ
ảnh hưởng đến code, ví dụ stack frontend, công thức XP/Level, luật Arena,
giới hạn lượt chơi, quy tắc thưởng và xử lý AI moderation.
Tạo/cập nhật docs/OPEN_QUESTIONS.md nếu phù hợp. Không tự chọn đáp án thay người dùng.
Tách câu hỏi bắt buộc chốt sớm khỏi câu hỏi có thể hoãn.
```

**Nghiệm thu:** không còn quy tắc quan trọng bị AI âm thầm tự quyết định.

## TASK 03 — Chuẩn hóa cấu trúc và hướng dẫn chạy

```text
Dựa trên repository hiện có, chỉ chuẩn hóa cấu trúc cần thiết để chạy dự án.
Không tạo lại ứng dụng nếu đã tồn tại. Cập nhật README với yêu cầu môi trường,
lệnh chạy frontend/backend, cấu hình DB và cách chạy test.
Tạo .env.example nếu cần, tuyệt đối không ghi secret thật.
```

**Nghiệm thu:** có thể chạy dự án theo README; không làm mất code cũ.

## TASK 04 — Kiểm tra database và migration nền tảng

```text
Đối chiếu ERD/tài liệu DB với entity và migration hiện có.
Chỉ ra bảng/quan hệ/constraint thiếu hoặc thừa. Không tự tạo lại toàn bộ DB.
Đề xuất thay đổi nhỏ nhất, giải thích ảnh hưởng dữ liệu, rồi chờ xác nhận trước
khi thực hiện thay đổi phá hủy hoặc migration có thể mất dữ liệu.
```

**Nghiệm thu:** sơ đồ và code DB nhất quán; migration không xóa dữ liệu ngoài ý muốn.

## TASK 05 — Chuẩn hóa API contract và xử lý lỗi

```text
Kiểm tra quy ước API hiện có. Chuẩn hóa response thành công, lỗi validation,
401/403/404/409/500, phân trang và cách cấu hình base URL. Viết ví dụ request/
response trong docs/API_CONTRACT.md. Không đổi API đã dùng ở nơi khác nếu chưa
liệt kê tác động.
```

**Nghiệm thu:** có contract đủ rõ để frontend và backend triển khai độc lập.

---

# Giai đoạn B — Đăng ký, đăng nhập và tài khoản

## TASK 06 — Thiết kế luồng Authentication

```text
Thiết kế luồng đăng ký, đăng nhập, đăng xuất, khôi phục/thay đổi mật khẩu
(nếu nằm trong phạm vi), xác thực phiên/token và phân quyền User/Admin.
Đề xuất entity/field cần dùng, endpoint, request/response, validation và lỗi.
Chưa code cho đến khi các quyết định ảnh hưởng bảo mật được chốt.
```

**Nghiệm thu:** thống nhất cách xác thực, vai trò, trạng thái tài khoản và hợp đồng API.

## TASK 07 — Backend đăng ký tài khoản

```text
Chỉ triển khai API đăng ký User theo contract đã chốt. Kiểm tra dữ liệu đầu vào,
email/tên đăng nhập trùng, chính sách mật khẩu và trạng thái tài khoản.
Dùng cơ chế hash mật khẩu an toàn của framework/Identity; không lưu password
dạng rõ. Không cho người dùng tự đăng ký với role Admin.
Viết test thành công, dữ liệu sai và tài khoản trùng.
```

**Nghiệm thu:** tài khoản được lưu đúng; mật khẩu không phải plain text; lỗi trùng rõ ràng.

## TASK 08 — Frontend trang đăng ký

```text
Chỉ làm giao diện đăng ký và kết nối API đăng ký đã có. Có validation phía UI
để hỗ trợ người dùng nhưng Backend vẫn là nguồn xác thực cuối cùng. Hiển thị
trạng thái đang gửi, thành công, lỗi validation, lỗi mạng và email/tài khoản trùng.
Không tự lưu password hoặc token vào nơi không an toàn.
```

**Nghiệm thu:** tạo tài khoản từ UI và thấy kết quả từ API thật.

## TASK 09 — Backend đăng nhập

```text
Triển khai API đăng nhập theo contract. Xử lý thông tin đăng nhập sai, tài khoản
bị khóa/vô hiệu hóa, cơ chế token/session đã thống nhất và giới hạn thông tin
nhạy cảm trong response. Không tiết lộ password hash.
Viết test cho đăng nhập đúng/sai và tài khoản bị khóa.
```

**Nghiệm thu:** đăng nhập đúng được xác thực; đăng nhập sai không tạo phiên hợp lệ.

## TASK 10 — Frontend trang đăng nhập

```text
Làm trang đăng nhập và tích hợp API thật. Xử lý loading, lỗi, điều hướng sau
đăng nhập và lưu trạng thái xác thực theo phương án bảo mật đã chốt.
Không tự dựng một cơ chế auth khác với Backend.
```

**Nghiệm thu:** người dùng có thể đăng nhập từ UI và truy cập trang cần đăng nhập.

## TASK 11 — Đăng xuất và bảo vệ route

```text
Triển khai đăng xuất và bảo vệ route/page theo cơ chế auth hiện tại.
Xử lý khi hết hạn phiên/token, truy cập route riêng tư khi chưa đăng nhập
và điều hướng về trang phù hợp. Bảo vệ API ở Backend, không chỉ ẩn menu ở UI.
```

**Nghiệm thu:** đăng xuất làm mất quyền truy cập; API riêng tư vẫn được bảo vệ server-side.

## TASK 12 — Hồ sơ cá nhân

```text
Triển khai xem và cập nhật hồ sơ của chính người dùng đang đăng nhập.
Chỉ cho sửa các trường được phép. Không lấy UserId từ client làm căn cứ quyền sở hữu;
xác định người dùng từ danh tính đã xác thực. Thêm validation và test quyền truy cập.
```

**Nghiệm thu:** xem/sửa hồ sơ đúng người; không thể đổi ID để sửa tài khoản khác.

## TASK 13 — Đổi mật khẩu và trạng thái tài khoản

```text
Triển khai đổi mật khẩu theo chính sách đã thống nhất, xác minh mật khẩu hiện tại
nếu yêu cầu, kiểm tra mật khẩu mới và xử lý phiên đăng nhập theo thiết kế.
Kiểm tra tài khoản bị khóa không thể sử dụng endpoint cần đăng nhập.
```

**Nghiệm thu:** đổi mật khẩu an toàn; mật khẩu cũ/mới hoạt động đúng; tài khoản khóa bị chặn.

## TASK 14 — Phân quyền User/Admin

```text
Hoàn thiện role/policy và bảo vệ endpoint Admin. Tạo test chứng minh User không
thể gọi API Admin dù tự nhập URL hoặc gọi API trực tiếp. Kiểm tra dữ liệu thuộc
quyền sở hữu của User. Không dựa vào việc ẩn nút trên frontend làm biện pháp bảo mật.
```

**Nghiệm thu:** test 401/403 và quyền sở hữu dữ liệu đạt.

---

# Giai đoạn C — Nội dung học tập

## TASK 15 — Admin quản lý Topic

```text
Triển khai CRUD Topic cho Admin và API đọc Topic cho User theo đặc tả.
Kiểm tra tên, mô tả, trạng thái, dữ liệu trùng và xử lý Topic đang được tham chiếu.
Làm Backend/API trước theo contract, sau đó làm UI trong cùng task hoặc task kế tiếp.
```

**Nghiệm thu:** Admin tạo/sửa/ẩn/xem được Topic; User chỉ thấy Topic được phép hiển thị.

## TASK 16 — Frontend danh sách và chi tiết Topic

```text
Xây danh sách Topic và trang chi tiết Topic dùng API thật. Có loading, empty,
error, responsive và điều hướng rõ ràng. Không dùng mock data trong luồng nghiệm thu.
```

## TASK 17 — Admin quản lý Vocabulary

```text
Triển khai CRUD Vocabulary theo schema đã thống nhất, liên kết đúng Topic.
Kiểm tra từ tiếng Anh, nghĩa, phiên âm, loại từ, trạng thái và trường bắt buộc
theo tài liệu thực tế. Không tự thêm field nếu chưa cần.
```

## TASK 18 — Example và nội dung từ vựng

```text
Triển khai quản lý/xem Example gắn với Vocabulary. Kiểm tra dữ liệu ví dụ,
bản dịch và quan hệ; xử lý khi xóa/sửa từ đang được tham chiếu.
```

## TASK 19 — Question và QuestionType

```text
Triển khai quản lý Question và QuestionType theo loại câu hỏi được đặc tả.
Thiết kế validation theo từng loại, xác định cách lưu đáp án và chấm điểm.
Không tự thêm loại câu hỏi mới ngoài phạm vi đã thống nhất.
```

## TASK 20 — Frontend học từ vựng

```text
Xây luồng học: chọn Topic → xem danh sách từ → xem chi tiết từ và Example.
Dùng API thật, hỗ trợ loading/error/empty, responsive và điều hướng quay lại.
```

## TASK 21 — Làm bài tập và chấm kết quả

```text
Xây luồng lấy câu hỏi, gửi câu trả lời, nhận kết quả và hiển thị giải thích
nếu dữ liệu có. Backend phải tự chấm theo đáp án đã lưu; không tin điểm số do
client gửi lên. Kiểm tra câu trả lời thiếu, sai định dạng và gửi lặp.
```

## TASK 22 — Lưu tiến độ và lịch sử học

```text
Triển khai lưu tiến độ học/lịch sử trả lời của người dùng đang xác thực.
Xác định rõ cách tính từ đã học, câu đúng/sai và tiến độ theo tài liệu.
Đảm bảo tải lại trang vẫn thấy dữ liệu đã lưu; không tự tạo công thức chưa chốt.
```

## TASK 23 — Trang tiến độ học tập

```text
Xây trang tiến độ từ API thật. Hiển thị các chỉ số đã được định nghĩa trong
đặc tả; xử lý người dùng mới chưa có dữ liệu. Không tự bịa số liệu hoặc biểu đồ.
```

---

# Giai đoạn D — OCR và AI tạo câu hỏi

## TASK 24 — Khảo sát pipeline OCR/AI

```text
Chỉ khảo sát trước khi tích hợp: model/dịch vụ có sẵn, input/output, dependency,
tài nguyên, thời gian xử lý, giới hạn file và bộ dữ liệu kiểm thử. Đề xuất adapter
để Backend gọi dịch vụ mà không gắn logic model vào controller. Không tuyên bố
model hoạt động nếu chưa chạy thử.
```

## TASK 25 — Upload tài liệu an toàn

```text
Triển khai endpoint upload theo định dạng/kích thước được chốt. Kiểm tra MIME
và nội dung file, đặt tên lưu trữ an toàn, tránh path traversal, giới hạn dung lượng
và kiểm tra quyền sở hữu. Không cho thực thi file tải lên.
```

## TASK 26 — Trích xuất văn bản OCR

```text
Tích hợp OCR vào pipeline và chuẩn hóa output. Lưu trạng thái xử lý và thông báo
lỗi rõ ràng. Kiểm thử ảnh rõ, ảnh mờ, file không hỗ trợ và nội dung rỗng.
```

## TASK 27 — Sinh câu hỏi và người dùng xác nhận

```text
Tích hợp bước sinh câu hỏi theo schema Question/QuestionType hiện có.
Cho người dùng xem, chỉnh sửa hoặc loại bỏ câu hỏi trước khi lưu nếu đặc tả yêu cầu.
Xác thực output AI, không lưu output sai schema như dữ liệu hợp lệ.
```

## TASK 28 — Quản lý bộ câu hỏi do người dùng tạo

```text
Triển khai lưu, xem và quản lý bộ câu hỏi theo quyền sở hữu. Kiểm tra User A
không thể xem/sửa bộ câu hỏi riêng của User B. Xử lý bộ câu hỏi trống và lỗi lưu.
```

---

# Giai đoạn E — Community và kiểm duyệt

## TASK 29 — Bài viết Community

```text
Triển khai tạo, xem, sửa, xóa bài viết theo quyền và trạng thái kiểm duyệt.
Xử lý nội dung rỗng/quá dài, phân trang và bài bị ẩn. Không cho User sửa bài của người khác.
```

## TASK 30 — Bình luận

```text
Triển khai tạo, xem, sửa/xóa bình luận theo quy tắc đã chốt. Kiểm tra bài viết
đã bị ẩn/xóa, bình luận rỗng và quyền chủ sở hữu.
```

## TASK 31 — Report nội dung

```text
Triển khai báo cáo bài viết/bình luận với lý do và trạng thái xử lý.
Chống gửi lặp theo quy tắc được chốt; lưu người báo cáo, nội dung được báo cáo
và thời gian. User không được tự sửa trạng thái xử lý của report.
```

## TASK 32 — Admin kiểm duyệt thủ công

```text
Xây API và UI Admin xem danh sách report, xem chi tiết nội dung, xử lý report
và thay đổi trạng thái kiểm duyệt. Ghi lại người xử lý và thời điểm xử lý nếu schema hỗ trợ.
```

## TASK 33 — AI Moderation

```text
Tích hợp model kiểm duyệt vào luồng Post/Comment. Lưu Classification, Confidence,
ModerationStatus và kết quả review theo schema đã chốt. Xác định ngưỡng tự động/
chờ Admin theo quyết định được xác nhận; không tự đặt ngưỡng nghiệp vụ.
Kiểm thử model lỗi và trường hợp confidence thấp.
```

---

# Giai đoạn F — Friend & Chat

## TASK 34 — Tìm kiếm người dùng

```text
Triển khai tìm kiếm người dùng theo trường được phép công khai. Không trả password,
token hoặc dữ liệu riêng tư. Hỗ trợ phân trang/giới hạn kết quả và loại trừ chính mình
nếu quy tắc sản phẩm yêu cầu.
```

## TASK 35 — Lời mời kết bạn

```text
Triển khai gửi, chấp nhận, từ chối và hủy lời mời kết bạn theo trạng thái đã chốt.
Chống tạo quan hệ trùng, xử lý lời mời gửi ngược chiều và không cho người dùng
thao tác lời mời của người khác.
```

## TASK 36 — Danh sách bạn bè

```text
Triển khai API và UI danh sách bạn bè, xem hồ sơ công khai và hủy kết bạn nếu
được hỗ trợ. Kiểm tra trạng thái quan hệ nhất quán ở hai phía.
```

## TASK 37 — Lưu và đọc tin nhắn

```text
Xây API lưu/đọc lịch sử tin nhắn trước khi làm realtime. Chỉ thành viên cuộc trò
chuyện mới được đọc/gửi tin. Kiểm tra phân trang, tin nhắn rỗng và quyền truy cập.
```

## TASK 38 — Chat realtime

```text
Tích hợp giải pháp realtime đã chọn với API/lưu trữ tin nhắn. Xử lý reconnect,
mất mạng, gửi lỗi và tải lịch sử sau khi kết nối lại. Không chỉ hiển thị tin nhắn
trên client mà không lưu ở Backend.
```

---

# Giai đoạn G — Arena và hệ thống game

## TASK 39 — Chốt luật Arena

```text
Trước khi code, lập tài liệu luật trận đấu: mode, số người, số câu, thời lượng,
cách tính điểm, điều kiện kết thúc, xử lý người rời trận, giới hạn lượt và cách
tính kết quả. Liệt kê điểm chưa rõ và chờ xác nhận. Không tự quyết luật chơi.
```

## TASK 40 — Tạo và tham gia Match

```text
Triển khai backend tạo/tham gia trận và trạng thái trận theo luật đã xác nhận.
Kiểm tra số người tối đa, trạng thái trận, người tham gia trùng và yêu cầu gửi lặp.
```

## TASK 41 — Matchmaking

```text
Triển khai ghép người chơi theo quy tắc đã chốt. Xử lý nhiều yêu cầu đồng thời,
hủy tìm trận, timeout và người chơi rời hàng chờ. Nếu chưa làm realtime, mô tả
rõ phương án polling tạm thời và giới hạn.
```

## TASK 42 — Vòng chơi và trả lời câu hỏi

```text
Triển khai cấp câu hỏi, nhận câu trả lời và thời gian theo luật đã chốt.
Backend là nguồn xác định đáp án, thời gian và điểm; không tin điểm do client gửi.
Chống gửi câu trả lời lặp hoặc sau khi trận kết thúc.
```

## TASK 43 — Kết quả trận và lịch sử

```text
Tính và lưu kết quả trận ở Backend; xử lý kết thúc bình thường và trường hợp
người chơi rời trận. Đảm bảo một trận chỉ chốt kết quả một lần. Xây UI kết quả
và lịch sử từ dữ liệu đã lưu.
```

## TASK 44 — XP và Level

```text
Chỉ triển khai sau khi công thức XP/Level đã được xác nhận. Tách logic tính toán
ra khỏi controller; kiểm tra biên Level, cộng XP lặp, dữ liệu cũ và giao dịch DB.
Viết unit test cho các trường hợp lên Level và không lên Level.
```

## TASK 45 — Reward/Achievement

```text
Triển khai điều kiện nhận thưởng/danh hiệu theo quy tắc đã chốt. Đảm bảo cùng
một sự kiện không cấp trùng thưởng; lưu lịch sử trao thưởng để kiểm tra.
```

## TASK 46 — Ranking

```text
Triển khai API và UI Ranking theo công thức, chu kỳ và tiêu chí phá hòa đã chốt.
Xử lý người chưa có điểm, đồng điểm và dữ liệu lớn. Không tự suy diễn công thức rank.
```

---

# Giai đoạn H — Admin và hoàn thiện

## TASK 47 — Admin quản lý tài khoản

```text
Xây trang/API Admin xem danh sách, chi tiết, trạng thái khóa/mở khóa và lịch sử
vi phạm theo đặc tả. Chỉ Admin được gọi API; ghi nhận lỗi và thao tác nhạy cảm phù hợp.
```

## TASK 48 — Admin quản lý nội dung học tập

```text
Hoàn thiện UI Admin quản lý Topic, Vocabulary, Example, Question và QuestionType
bằng API thật. Có validation, trạng thái loading/error và xác nhận trước thao tác nguy hiểm.
```

## TASK 49 — Kiểm thử tổng thể và bảo mật

```text
Chạy regression test các luồng: đăng ký/đăng nhập, học, lưu tiến độ, OCR/AI,
Community/Report, Friend/Chat, Arena và Admin. Ưu tiên IDOR, phân quyền sai,
gửi request lặp, mất kết nối, lỗi dữ liệu và rò rỉ thông tin nhạy cảm.
Báo lỗi theo mức độ nghiêm trọng và không sửa lan module không liên quan.
```

## TASK 50 — Responsive, lỗi UX và tài liệu bàn giao

```text
Rà soát desktop/tablet/mobile cho các trang chính; sửa lỗi giao diện và trạng thái
loading/empty/error. Cập nhật README, biến môi trường mẫu, cách migrate/seed DB,
cách chạy test, chức năng đã hoàn thành và known issues. Không đưa secret vào tài liệu.
```

---

# 2. Cách nghiệm thu một chức năng

Ví dụ task Đăng ký chỉ được xem là hoàn thành khi:
- [ ] Backend build thành công.
- [ ] API tạo được user trong DB.
- [ ] Dữ liệu trùng và sai định dạng bị xử lý đúng.
- [ ] Password không lưu plain text.
- [ ] Frontend gọi API thật và hiển thị kết quả.
- [ ] Có test hoặc bước kiểm tra tái hiện được.
- [ ] README/contract được cập nhật nếu có thay đổi.
- [ ] Không làm hỏng đăng nhập hoặc module khác.

Không chuyển task kế tiếp chỉ vì AI trả lời “đã hoàn thành”. Tự chạy và kiểm tra kết quả trên máy.

# 3. Thứ tự thực hiện ngắn gọn

1. TASK 01–05: hiểu và chuẩn bị dự án.
2. TASK 06–14: Authentication và phân quyền.
3. TASK 15–23: nội dung học tập và tiến độ.
4. TASK 24–28: OCR/AI tạo câu hỏi.
5. TASK 29–33: Community và Moderation.
6. TASK 34–38: Friend & Chat.
7. TASK 39–46: Arena và game progression.
8. TASK 47–50: Admin, bảo mật, responsive, bàn giao.

**Nếu thời gian hạn chế:** ưu tiên TASK 01–23 để có sản phẩm học từ vựng chạy được; sau đó hoàn thiện các module khác theo yêu cầu bắt buộc của dự án.
