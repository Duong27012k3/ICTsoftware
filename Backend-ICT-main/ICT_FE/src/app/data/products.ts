export interface Product {
  id: string;
  fieldId: string;
  title: string;
  description: string;
  fullDescription: string;
  image: string;
  features: string[];
  benefits: string[];
  technicalSpecs?: {
    label: string;
    value: string;
  }[];
  detailedSections?: {
    title: string;
    content: string;
    imageUrl?: string;
  }[];
}

export const products: Product[] = [
  {
    id: 'dlp-network',
    fieldId: 'bao-mat-du-lieu',
    title: 'ICT DLP',
    description: 'Giải pháp phòng chống thất thoát dữ liệu toàn diện — kiểm soát thiết bị ngoại vi, giám sát mạng, phân loại dữ liệu tự động.',
    fullDescription: 'DATA LOSS PREVENTION (DLP) là giải pháp hàng đầu trong việc ngăn ngừa rò rỉ dữ liệu. Được trang bị các công cụ kiểm soát đường truyền mạng và thiết bị ngoại vi hiện đại, DLP tự động rà quét toàn hệ thống để định vị nơi lưu trữ dữ liệu mật, qua đó ngăn chặn nguy cơ thất thoát do vô tình hoặc cố ý từ bên trong tổ chức. Hệ thống báo cáo chi tiết hỗ trợ quản trị viên phục vụ việc điều tra nhanh chóng và tuân thủ các quy định bảo mật.',
    image: 'https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800&auto=format&fit=crop',
    features: [
      'Endpoint Security',
      'Network Control',
      'Centralized Management',
      'Data Discovery & Classification'
    ],
    benefits: [
      'Ngăn rò rỉ hình ảnh, thông tin thẻ tín dụng, CMND, hồ sơ thầu',
      'Chặn upload các tệp nhạy cảm lên Google Drive, Dropbox và các dịch vụ Cloud',
      'Quan sát, quản trị trực quan với cảnh báo tức thời qua Dashboard thời gian thực',
      'Tự động ghi hình màn hình khi phát hiện hành vi vi phạm chính sách'
    ],
    technicalSpecs: [
      { label: 'Endpoint Agents', value: '10K+' },
      { label: 'Policy Rules', value: '500+' },
      { label: 'Uptime SLA', value: '99.9%' }
    ],
    detailedSections: [
      {
        title: 'Endpoint Security',
        content: 'Kiểm soát chặt chẽ thiết bị ngoại vi (USB, máy in), chặn ứng dụng trái phép và tự động ghi hình màn hình khi phát hiện vi phạm. Hệ thống theo dõi mọi thao tác sao chép, in ấn, chụp màn hình đối với tài liệu mật để đảm bảo dữ liệu không bị rò rỉ qua các kênh vật lý.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/18155289.png'
      },
      {
        title: 'Network Control',
        content: 'Giám sát luồng dữ liệu qua Web/Email, chặn gửi file mật lên Cloud (Google Drive, Dropbox) và tự động lọc từ khóa nhạy cảm trong thư điện tử. Phân tích lưu lượng mạng theo thời gian thực để phát hiện các kênh truyền dữ liệu trái phép.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/5718761.png'
      },
      {
        title: 'Centralized Management',
        content: 'Dashboard trực quan hiển thị cảnh báo thời gian thực, hỗ trợ xuất báo cáo chi tiết phục vụ điều tra và triển khai linh hoạt (On-premise/Cloud). Quản trị viên có thể giám sát toàn bộ hệ thống từ một giao diện duy nhất.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/18536021.png'
      },
      {
        title: 'Data Discovery & Classification',
        content: 'Tự động rà quét toàn hệ thống để định vị nơi lưu trữ dữ liệu mật (CMND, thẻ tín dụng, hồ sơ thầu) và gán nhãn phân loại ngay lập tức. Hỗ trợ hơn 300 loại định dạng file và nhận dạng dữ liệu nhạy cảm bằng AI.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/9422856.png'
      }
    ]
  },
  {
    id: 'ias',
    fieldId: 'danh-gia-an-ninh',
    title: 'Hệ Thống IAS',
    description: 'Hệ thống phần mềm tự động dò quét thông minh — phát hiện lỗ hổng bảo mật diện rộng với hiệu suất cực cao.',
    fullDescription: 'Hệ thống dò quét IAS là giải pháp an toàn thông tin (ATTT) toàn diện, được thiết kế để tự động phát hiện các điểm yếu, lỗ hổng bảo mật trên website và hạ tầng công nghệ thông tin ở quy mô lớn. Đây là công cụ đắc lực giúp xây dựng bức tranh tổng thể về nguy cơ an ninh mạng và bảo vệ chủ quyền quốc gia trên không gian mạng.',
    image: 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&auto=format&fit=crop',
    features: [
      'Dò quét diện rộng',
      'Trí tuệ nhân tạo (AI)',
      'Quản lý tập trung',
      'Ẩn danh & kiểm thử'
    ],
    benefits: [
      'Đánh giá an toàn thông tin theo thời gian thực trên quy mô quốc gia',
      'Tiết kiệm tài nguyên thông qua AI và tự động hóa quy trình',
      'Xác định ngay những mục tiêu nhạy cảm để khắc phục sớm',
      'Đối chiếu với hơn 280.000 mã lỗi CVE phổ biến'
    ],
    technicalSpecs: [
      { label: 'Website/24h', value: '200K' },
      { label: 'Thiết bị/ngày', value: '1M' },
      { label: 'CVE Database', value: '280K+' }
    ],
    detailedSections: [
      {
        title: 'Khả năng Dò quét Diện rộng & Hiệu suất Cực cao',
        content: 'AutoScan sở hữu năng lực xử lý vượt trội: quét đồng thời 1.000 website, hoàn thành 100.000–200.000 website trong 24 giờ với độ chính xác trên 90%. Tự động nhận diện thiết bị (IPv4/IPv6), rà quét lên tới 1.000.000 thiết bị mạng mỗi ngày. Đối chiếu với hơn 280.000 mã lỗi CVE phổ biến để phát hiện nguy cơ.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/qr-scan.png'
      },
      {
        title: 'Công nghệ Trí tuệ Nhân tạo (AI) Tiên phong',
        content: 'Trợ lý ảo AI cùng báo cáo thông minh, đưa ra khuyến nghị khắc phục chuẩn xác. AI phân tích hành vi bất thường trên mạng để phát hiện các mối đe dọa chưa được biết đến (zero-day), giúp đội ngũ SecOps tiết kiệm thời gian xử lý sự cố.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/robot-talking.png'
      },
      {
        title: 'Quản lý tập trung & Dashboard trực quan',
        content: 'Dashboard trực quan với cảnh báo thời gian thực để đội ngũ DevOps/SecOps kịp thời phản ứng. Hỗ trợ xuất báo cáo chi tiết theo nhiều định dạng, tích hợp với các hệ thống SIEM hiện có.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/data-analysis.png'
      },
      {
        title: 'Ẩn danh & Kiểm thử bảo mật',
        content: 'Hỗ trợ chế độ ẩn danh hoàn toàn khi dò quét, giúp kiểm tra bảo mật mà không để lại dấu vết. Cung cấp các kịch bản kiểm thử xâm nhập tự động theo chuẩn OWASP Top 10.',
        imageUrl: 'https://ictsoftware.net/wp-content/uploads/2026/01/incognito.png'
      }
    ]
  },
  {
    id: 'waf',
    fieldId: 'bao-mat-ung-dung',
    title: 'ICT WAF',
    description: 'Tường lửa ứng dụng web chuyên biệt — ngăn chặn SQLi, XSS, DDoS cấp ứng dụng và các giao thức độc hại.',
    fullDescription: 'Web Application Firewall (WAF) đi sâu vào việc bảo vệ các ứng dụng Web và API trước những chuỗi tấn công phức tạp. Nó quét mọi lưu lượng HTTP/HTTPS kết nối đến hệ thống của doanh nghiệp và so sánh với kho dữ liệu các hình thức tấn công mới nhất để tự động lọc bỏ các Request nguy hiểm. WAF đảm bảo ứng dụng luôn sạch sẽ và ổn định, loại trừ các cuộc tấn công DDoS lớp ứng dụng, bot tự động và khai thác lỗ hổng zero-day.',
    image: 'https://images.unsplash.com/photo-1518770660439-4636190af475?w=800&auto=format&fit=crop',
    features: [
      'Bảo vệ phòng chống Web Exploits phổ biến',
      'Dynamic Bot Management',
      'Zero-Day Vulnerability Patching ảo',
      'API Security'
    ],
    benefits: [
      'Tránh rò rỉ dữ liệu thông qua ứng dụng Web và API',
      'Tuân thủ PCI DSS và các chuẩn bảo mật web quốc tế',
      'Đảm bảo thời gian Uptime cho dịch vụ khách hàng',
      'Chặn DDoS lớp ứng dụng mà không ảnh hưởng đến trải nghiệm người dùng'
    ],
    technicalSpecs: [
      { label: 'Throughput', value: '10Gbps' },
      { label: 'Rules Engine', value: '3K+' },
      { label: 'Response Time', value: '<1ms' }
    ],
    detailedSections: [
      {
        title: 'Bảo vệ Web Exploits & OWASP Top 10',
        content: 'Chặn hoàn toàn SQL Injection, Cross-Site Scripting (XSS), CSRF, Path Traversal, và các kỹ thuật khai thác nâng cao. Cập nhật liên tục bộ quy tắc dựa trên OWASP Top 10 mới nhất.',
        imageUrl: 'https://images.unsplash.com/photo-1555949963-aa79dcee981c?w=800'
      },
      {
        title: 'Dynamic Bot Management',
        content: 'Phân biệt chính xác bot hợp lệ (Google, Bing) với bot độc hại thông qua phân tích hành vi, fingerprinting trình duyệt và thử thách CAPTCHA thông minh. Ngăn chặn credential stuffing, web scraping và click fraud.',
        imageUrl: 'https://images.unsplash.com/photo-1674027444485-cec3da58eef4?w=800'
      },
      {
        title: 'Zero-Day Virtual Patching',
        content: 'Khi phát hiện lỗ hổng zero-day mới, hệ thống tự động triển khai bản vá ảo (virtual patch) để bảo vệ ứng dụng trước khi nhà phát triển kịp cập nhật mã nguồn. Giảm thiểu thời gian phơi nhiễm xuống dưới 1 giờ.',
        imageUrl: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800'
      },
      {
        title: 'API Security & Rate Limiting',
        content: 'Bảo vệ REST API, GraphQL và WebSocket với các chính sách kiểm soát truy cập chi tiết. Tích hợp rate limiting, JSON/XML validation và schema enforcement để ngăn chặn API abuse.',
        imageUrl: 'https://images.unsplash.com/photo-1454165804606-c3d57bc86b40?w=800'
      }
    ]
  },
  {
    id: 'ict-pam',
    fieldId: 'quan-ly-truy-cap',
    title: 'ICT PAM',
    description: 'Bộ giải pháp kiểm soát và quản lý truy cập đặc quyền — bảo vệ tài khoản quản trị, ghi hình phiên làm việc.',
    fullDescription: 'ICT Privileged Access Management (PAM) là chốt chặn cuối cùng bảo vệ hệ thống mạng trước rủi ro nội bộ hoặc bên thứ ba. PAM cung cấp khả năng khoá và quản lý mật khẩu của các tài khoản có quyền cao (như root, domain admin), buộc người dùng phải qua xác thực bằng Multi-Factor Authentication (MFA) trước khi truy cập. Mọi thao tác truy cập đều được ghi lại dưới dạng video để kiểm tra bất kỳ khi nào cần thiết.',
    image: 'https://images.unsplash.com/photo-1633265486064-086b219458ec?w=800&auto=format&fit=crop',
    features: [
      'Quản lý vòng đời mật khẩu đặc quyền tự động',
      'Session Recording (Ghi hình toàn bộ phiên làm việc RDP/SSH)',
      'Single Sign-On (SSO) và Multi-Factor Authentication (MFA)',
      'Nguyên tắc phân quyền truy cập "Least Privilege"'
    ],
    benefits: [
      'Cách ly hoàn toàn mật khẩu thực sự khỏi tổ chức (Zero-Knowledge)',
      'Phát hiện gian lận và sử dụng quyền trái phép lập tức',
      'Kiểm toán và báo cáo nhanh chóng với log rõ ràng',
      'Tuân thủ ISO 27001, PCI DSS, SOX'
    ],
    technicalSpecs: [
      { label: 'Managed Accounts', value: '50K+' },
      { label: 'Session Replay', value: 'HD' },
      { label: 'MFA Methods', value: '8+' }
    ],
    detailedSections: [
      {
        title: 'Quản lý Mật khẩu Đặc quyền',
        content: 'Tự động xoay vòng mật khẩu tài khoản root, admin, service account theo lịch trình hoặc sau mỗi phiên truy cập. Người dùng không bao giờ nhìn thấy mật khẩu thực sự — hệ thống tự động inject credentials khi tạo phiên.',
        imageUrl: 'https://images.unsplash.com/photo-1633265486064-086b219458ec?w=800'
      },
      {
        title: 'Session Recording & Monitoring',
        content: 'Ghi hình toàn bộ phiên làm việc RDP, SSH, VNC ở độ phân giải cao. Hỗ trợ tìm kiếm theo keyword, thời gian, lệnh đã thực thi. Quản trị viên có thể theo dõi phiên truy cập theo thời gian thực và can thiệp khi cần.',
        imageUrl: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800'
      }
    ]
  },
  {
    id: 'ict-siem',
    fieldId: 'giam-sat-su-co',
    title: 'ICT SIEM',
    description: 'Nền tảng quản lý sự kiện an ninh — thu thập, tương quan và phân tích log từ toàn bộ hạ tầng CNTT.',
    fullDescription: 'ICT SIEM (Security Information and Event Management) là hệ thống trung tâm điều hành an ninh mạng giúp tổ chức thu thập, lưu trữ, phân tích và tương quan log từ hàng ngàn nguồn dữ liệu khác nhau —  firewall, IDS/IPS, server, endpoint, ứng dụng — để phát hiện mối đe dọa một cách tức thời. Kết hợp với machine learning, SIEM tự động phân loại và ưu tiên cảnh báo, giảm thiểu false positive và tăng tốc phản ứng sự cố.',
    image: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800&auto=format&fit=crop',
    features: [
      'Thu thập và tương quan log tập trung từ mọi nguồn',
      'Threat Intelligence tích hợp',
      'Automated Incident Response (SOAR)',
      'Compliance Reporting (ISO 27001, PCI DSS, HIPAA)'
    ],
    benefits: [
      'Giảm thời gian phát hiện mối đe dọa (MTTD) xuống dưới 10 phút',
      'Tự động hóa quy trình phản ứng sự cố, tiết kiệm nhân lực',
      'Tổng hợp báo cáo tuân thủ một cách tự động',
      'Giảm false positive lên tới 90% nhờ tương quan thông minh'
    ],
    technicalSpecs: [
      { label: 'Events/Sec', value: '50K+' },
      { label: 'Log Sources', value: '500+' },
      { label: 'Retention', value: '5 Năm' }
    ],
    detailedSections: [
      {
        title: 'Thu thập & Tương quan Log',
        content: 'Thu thập log từ mọi thiết bị mạng, server, ứng dụng, endpoint và cloud service. Tương quan sự kiện từ hàng ngàn nguồn khác nhau để phát hiện các chuỗi tấn công phức tạp (kill chain) mà hệ thống đơn lẻ không thể nhận diện.',
        imageUrl: 'https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800'
      },
      {
        title: 'Threat Intelligence & ML',
        content: 'Tích hợp nhiều nguồn Threat Intelligence (MITRE ATT&CK, VirusTotal, OTX) để so khớp IoC (Indicator of Compromise). Machine Learning liên tục học hỏi hành vi mạng bình thường để phát hiện anomaly.',
        imageUrl: 'https://images.unsplash.com/photo-1674027444485-cec3da58eef4?w=800'
      },
      {
        title: 'SOAR — Tự động phản ứng sự cố',
        content: 'Khi phát hiện mối đe dọa, hệ thống tự động kích hoạt playbook phản ứng: cách ly endpoint, chặn IP, gửi cảnh báo đến nhóm SOC, tạo ticket trong hệ thống ITSM. Giảm MTTR xuống vài phút thay vì hàng giờ.',
        imageUrl: 'https://images.unsplash.com/photo-1555949963-aa79dcee981c?w=800'
      }
    ]
  },
  {
    id: 'ict-edr',
    fieldId: 'bao-mat-endpoint',
    title: 'ICT EDR',
    description: 'Giải pháp phát hiện và phản ứng mối đe dọa trên thiết bị đầu cuối — giám sát hành vi, hunting threat, cách ly tức thời.',
    fullDescription: 'ICT EDR (Endpoint Detection and Response) là giải pháp bảo mật thế hệ mới tập trung vào việc giám sát liên tục hành vi trên các thiết bị đầu cuối (workstation, laptop, server). Khác với antivirus truyền thống dựa trên signature, EDR sử dụng AI/ML để phát hiện các hành vi đáng ngờ chưa từng được biết đến, cho phép đội ngũ SecOps săn tìm mối đe dọa (threat hunting) chủ động và cách ly thiết bị nhiễm mã độc chỉ trong vài giây.',
    image: 'https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop',
    features: [
      'Behavioral Analysis & ML-based Detection',
      'Real-time Endpoint Visibility & Telemetry',
      'Automated Threat Containment & Isolation',
      'Proactive Threat Hunting'
    ],
    benefits: [
      'Phát hiện malware, ransomware, fileless attack trước khi gây thiệt hại',
      'Cách ly endpoint bị nhiễm trong vài giây mà không ảnh hưởng hệ thống',
      'Cung cấp dữ liệu forensics chi tiết cho điều tra sự cố',
      'Giảm bề mặt tấn công thông qua hardening tự động'
    ],
    technicalSpecs: [
      { label: 'Endpoints', value: '100K+' },
      { label: 'Detection Rate', value: '99.7%' },
      { label: 'Isolation Time', value: '<5s' }
    ],
    detailedSections: [
      {
        title: 'Behavioral Analysis & AI Detection',
        content: 'Agent nhẹ trên mỗi endpoint liên tục thu thập telemetry về process, file, registry, network connection. AI phân tích chuỗi hành vi để phát hiện fileless malware, lateral movement, privilege escalation mà antivirus truyền thống bỏ sót.',
        imageUrl: 'https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800'
      },
      {
        title: 'Automated Containment & Response',
        content: 'Khi phát hiện mối đe dọa, hệ thống tự động cách ly endpoint khỏi mạng (network isolation) nhưng vẫn giữ kết nối quản trị. Quản trị viên có thể remote shell vào endpoint bị cách ly để forensics và clean up.',
        imageUrl: 'https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800'
      },
      {
        title: 'Threat Hunting & Investigation',
        content: 'Giao diện threat hunting cho phép đội ngũ SOC tìm kiếm IoC trên toàn bộ fleet endpoint theo thời gian thực. Hỗ trợ query DSL mạnh mẽ, timeline visualization và MITRE ATT&CK mapping để hiểu rõ kill chain của cuộc tấn công.',
        imageUrl: 'https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800'
      }
    ]
  }
];

export const getProductById = (id: string): Product | undefined => {
  return products.find(product => product.id === id);
};