export interface Field {
  id: string;
  title: string;
  description: string;
  fullDescription: string;
  image: string;
  features: string[];
  benefits: string[];
  stats?: {
    label: string;
    value: string;
  }[];
}

export const fields: Field[] = [
  {
    id: 'bao-mat-du-lieu',
    title: 'Bảo Mật & Phòng Chống Thất Thoát Dữ Liệu',
    description: 'Bảo vệ tài sản số quan trọng nhất của doanh nghiệp. Đảm bảo dữ liệu không bị rò rỉ, đánh cắp hay lạm dụng.',
    fullDescription: 'Lĩnh vực bảo mật dữ liệu tập trung vào việc quản lý, giám sát và bảo vệ các thông tin nhạy cảm của tổ chức. Hệ thống tự động phân loại dữ liệu, phát hiện các hành vi bất thường và ngăn chặn rủi ro thất thoát dữ liệu qua mạng, thiết bị ngoại vi hoặc các ứng dụng đám mây (Cloud).',
    image: 'https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop',
    features: [
      'Phân loại và định danh dữ liệu nhạy cảm',
      'Kiểm soát thiết bị ngoại vi và đường truyền',
      'Mã hóa dữ liệu đầu cuối',
      'Giám sát hành vi người dùng (UBA)'
    ],
    benefits: [
      'Ngăn chặn gian lận và đánh cắp dữ liệu',
      'Tuân thủ các tiêu chuẩn bảo mật quốc tế',
      'Giảm thiểu rủi ro từ các cuộc tấn công nội bộ'
    ]
  },
  {
    id: 'danh-gia-an-ninh',
    title: 'Đánh Giá An Ninh & Dò Quét Lỗ Hổng',
    description: 'Chủ động phát hiện và khắc phục các điểm yếu bảo mật trong hệ thống trước khi tin tặc kịp lợi dụng.',
    fullDescription: 'Giải pháp cung cấp bức tranh toàn cảnh về tình trạng an ninh của tổ chức nhờ vào khả năng dò quét diện rộng với hiệu suất cao. Hệ thống tự động phân tích lỗ hổng trên hạ tầng mạng, website, và ứng dụng bằng cách kết hợp sức mạnh phân tích của Trí Tuệ Nhân Tạo (AI), đưa ra các cảnh báo theo thời gian thực.',
    image: 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&auto=format&fit=crop',
    features: [
      'Dò quét lỗ hổng tự động và liên tục',
      'Phân tích dựa trên Trí Tuệ Nhân Tạo (AI)',
      'Kiểm tra cấu hình an toàn hệ thống',
      'Giám sát an ninh mạng diện rộng'
    ],
    benefits: [
      'Chủ động phát hiện các rủi ro zero-day',
      'Xây dựng tuyến phòng thủ vững chắc từ sớm',
      'Tiết kiệm nguồn lực giám sát an ninh thủ công'
    ]
  },
  {
    id: 'bao-mat-ung-dung',
    title: 'Bảo Mật Ứng Dụng Web & API',
    description: 'Bảo vệ các website và ứng dụng trực tuyến khỏi hacker, botnet và các hành vi khai thác lỗ hổng.',
    fullDescription: 'Lĩnh vực bảo mật ứng dụng cung cấp lớp phòng thủ vững chắc cho các máy chủ Web và API. Hệ thống tường lửa ứng dụng giúp phân tích chi tiết các truy cập HTTP/HTTPS, loại bỏ các mối đe dọa như SQL Injection, Cross-Site Scripting (XSS), DDoS và rò rỉ dữ liệu thông qua ứng dụng số.',
    image: 'https://images.unsplash.com/photo-1555949963-aa79dcee981c?w=800&auto=format&fit=crop',
    features: [
      'Ngăn chặn tấn công ứng dụng Web phổ biến',
      'Bảo vệ API và Microservices',
      'Lọc lưu lượng truy cập ảo (Bot Management)',
      'Phát hiện xâm nhập lớp ứng dụng'
    ],
    benefits: [
      'Đảm bảo ứng dụng trực tuyến luôn hoạt động ổn định',
      'Loại bỏ các nguy cơ gián đoạn dịch vụ',
      'Duy trì uy tín thương hiệu đối với khách hàng'
    ]
  },
  {
    id: 'quan-ly-truy-cap',
    title: 'Quản Lý Định Danh & Truy Cập Đặc Quyền',
    description: 'Giám sát gắt gao các tài khoản quản trị mạng, đảm bảo đúng người, đúng thời điểm và không bị lạm dụng quyền hạn.',
    fullDescription: 'Các tài khoản đặc quyền là mục tiêu đánh cắp hàng đầu của hacker. Lĩnh vực này chuyên cung cấp các giải pháp cô lập, quản lý mật khẩu tự động và theo dõi chi tiết từng phiên làm việc của quản trị viên (ghi hình, ghi log). Đảm bảo kiểm soát toàn diện mọi truy cập đến các máy chủ và thiết bị mạng trọng yếu.',
    image: 'https://images.unsplash.com/photo-1518770660439-4636190af475?w=800&auto=format&fit=crop',
    features: [
      'Quản lý kho mật khẩu đặc quyền',
      'Xác thực đa yếu tố (MFA)',
      'Ghi hình và giám sát phiên làm việc',
      'Nguyên tắc cấp quyền tối thiểu (Least Privilege)'
    ],
    benefits: [
      'Kiểm soát chặt chẽ đối tác và nhân viên nội bộ',
      'Rút ngắn thời gian điều tra các sự cố',
      'Trực quan hóa truy cập hệ thống'
    ]
  },
  {
    id: 'giam-sat-su-co',
    title: 'Giám Sát & Phản Ứng Sự Cố An Ninh',
    description: 'Trung tâm điều hành an ninh mạng — thu thập, phân tích và xử lý sự cố bảo mật theo thời gian thực.',
    fullDescription: 'Lĩnh vực giám sát và phản ứng sự cố tập trung vào việc xây dựng khả năng phát hiện và xử lý mối đe dọa trong thời gian ngắn nhất. Kết hợp giải pháp SIEM, SOAR cùng đội ngũ SOC chuyên nghiệp, tổ chức có thể giám sát toàn bộ hạ tầng CNTT 24/7, phát hiện tấn công ở giai đoạn sớm và tự động kích hoạt quy trình phản ứng trước khi thiệt hại lan rộng.',
    image: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800&auto=format&fit=crop',
    features: [
      'Thu thập và tương quan log từ đa nguồn',
      'Tự động phản ứng sự cố (SOAR)',
      'Threat Intelligence tích hợp',
      'Báo cáo tuân thủ tự động'
    ],
    benefits: [
      'Giảm thời gian phát hiện và xử lý sự cố',
      'Tự động hóa quy trình, tiết kiệm nhân lực SOC',
      'Đáp ứng yêu cầu tuân thủ pháp lý'
    ]
  },
  {
    id: 'bao-mat-endpoint',
    title: 'Bảo Mật Thiết Bị Đầu Cuối',
    description: 'Phát hiện và ngăn chặn mối đe dọa trên workstation, laptop và server — vượt xa antivirus truyền thống.',
    fullDescription: 'Lĩnh vực bảo mật thiết bị đầu cuối (Endpoint Security) là lớp phòng thủ quan trọng nhất trước các cuộc tấn công nhắm vào người dùng. Sử dụng AI và phân tích hành vi để phát hiện malware, ransomware, fileless attack mà không cần dựa vào signature. Kết hợp khả năng phản ứng tự động để cách ly thiết bị bị nhiễm chỉ trong vài giây.',
    image: 'https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop',
    features: [
      'Phát hiện mối đe dọa bằng AI/ML',
      'Cách ly endpoint bị nhiễm tự động',
      'Threat Hunting chủ động',
      'Forensics và điều tra sự cố'
    ],
    benefits: [
      'Bảo vệ trước ransomware và APT',
      'Giảm bề mặt tấn công trên toàn fleet',
      'Cung cấp dữ liệu chi tiết cho điều tra'
    ]
  }
];

export const getFieldById = (id: string): Field | undefined => {
  return fields.find(field => field.id === id);
};
