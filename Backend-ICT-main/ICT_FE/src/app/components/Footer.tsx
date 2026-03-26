import { Link } from 'react-router';
import { Mail, Phone, MapPin } from 'lucide-react';

export function Footer() {
  return (
    <footer className="bg-slate-950 border-t border-slate-800 text-slate-400">
      <div className="container mx-auto px-6 py-16">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-12 mb-12">
          <div>
            <h3 className="text-white text-2xl font-bold mb-6 flex items-center gap-3">
              <div className="w-8 h-8 bg-green-500/10 border border-green-500/30 rounded flex items-center justify-center">
                <span className="text-sm text-green-400">IS</span>
              </div>
              ICT <span className="text-green-400 font-light">Software</span>
            </h3>
            <p className="text-sm leading-relaxed text-slate-400">
              Nền tảng phòng chống rủi ro nâng cao được thiết kế cho hệ thống mạng doanh nghiệp toàn cầu.
            </p>
          </div>
          
          <div>
            <h4 className="text-white font-semibold mb-6 uppercase tracking-wider text-sm">Lĩnh vực</h4>
            <ul className="space-y-3 text-sm">
              <li><Link to="/field/bao-mat-du-lieu" className="hover:text-green-400 transition-colors">Bảo Mật Dữ Liệu</Link></li>
              <li><Link to="/field/danh-gia-an-ninh" className="hover:text-green-400 transition-colors">Đánh Giá An Ninh</Link></li>
              <li><Link to="/field/bao-mat-ung-dung" className="hover:text-green-400 transition-colors">Bảo Mật Ứng Dụng</Link></li>
              <li><Link to="/field/quan-ly-truy-cap" className="hover:text-green-400 transition-colors">Quản Lý Truy Cập</Link></li>
              <li><Link to="/field/giam-sat-su-co" className="hover:text-green-400 transition-colors">Giám Sát Sự Cố</Link></li>
              <li><Link to="/field/bao-mat-endpoint" className="hover:text-green-400 transition-colors">Bảo Mật Endpoint</Link></li>
            </ul>
          </div>
          
          <div>
            <h4 className="text-white font-semibold mb-6 uppercase tracking-wider text-sm">Sản phẩm</h4>
            <ul className="space-y-3 text-sm">
              <li><Link to="/product/dlp-network" className="hover:text-green-400 transition-colors">ICT DLP</Link></li>
              <li><Link to="/product/ias" className="hover:text-green-400 transition-colors">Hệ Thống IAS</Link></li>
              <li><Link to="/product/waf" className="hover:text-green-400 transition-colors">ICT WAF</Link></li>
              <li><Link to="/product/ict-pam" className="hover:text-green-400 transition-colors">ICT PAM</Link></li>
              <li><Link to="/product/ict-siem" className="hover:text-green-400 transition-colors">ICT SIEM</Link></li>
              <li><Link to="/product/ict-edr" className="hover:text-green-400 transition-colors">ICT EDR</Link></li>
            </ul>
          </div>
          
          <div>
            <h4 className="text-white font-semibold mb-6 uppercase tracking-wider text-sm">Liên hệ</h4>
            <ul className="space-y-4 text-sm">
              <li className="flex items-start gap-3">
                <div className="w-8 h-8 rounded bg-slate-900 border border-slate-800 flex items-center justify-center flex-shrink-0">
                  <Mail className="w-4 h-4 text-green-400" />
                </div>
                <span className="mt-1.5">info@ictsoftware.net</span>
              </li>
              <li className="flex items-start gap-3">
                <div className="w-8 h-8 rounded bg-slate-900 border border-slate-800 flex items-center justify-center flex-shrink-0">
                  <Phone className="w-4 h-4 text-green-400" />
                </div>
                <span className="mt-1.5">+84 848 200 000</span>
              </li>
              <li className="flex items-start gap-3">
                <div className="w-8 h-8 rounded bg-slate-900 border border-slate-800 flex items-center justify-center flex-shrink-0">
                  <MapPin className="w-4 h-4 text-green-400" />
                </div>
                <span className="mt-1.5 leading-relaxed">33 An Trai, Vân Canh, Hoài Đức, Hà Nội, Việt Nam</span>
              </li>
            </ul>
          </div>
        </div>
        
        <div className="pt-8 border-t border-slate-900 flex flex-col md:flex-row justify-between items-center text-sm gap-4">
          <p>© 2026 ICT Software Company Limited. ID: 0109907269</p>
          <div className="flex gap-6">
            <Link to="#" className="hover:text-green-400 transition-colors">Chính sách bảo mật</Link>
            <Link to="#" className="hover:text-green-400 transition-colors">Điều khoản dịch vụ</Link>
          </div>
        </div>
      </div>
    </footer>
  );
}