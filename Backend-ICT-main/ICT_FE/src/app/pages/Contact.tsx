import { useState } from 'react';
import { Footer } from '../components/Footer';
import { Mail, Phone, MapPin, Clock, ShieldCheck, Zap } from 'lucide-react';
import { api } from '../services/api';

export function Contact() {
  const [formData, setFormData] = useState({ name: '', email: '', phone: '', message: '' });
  const [status, setStatus] = useState<'idle' | 'submitting' | 'success' | 'error'>('idle');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setStatus('submitting');
    try {
      await api.submitContact(formData);
      setStatus('success');
      setFormData({ name: '', email: '', phone: '', message: '' });
    } catch (err) {
      console.error(err);
      setStatus('error');
    }
  };

  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      <section className="relative pt-32 pb-20 px-6 overflow-hidden">
        {/* Abstract Background Elements */}
        <div className="absolute top-0 right-0 w-full h-full overflow-hidden z-0 pointer-events-none">
           <div className="absolute top-[-10%] right-[-10%] w-[40%] h-[40%] bg-green-500/10 blur-[150px] rounded-full"></div>
           <div className="absolute top-[40%] left-[-20%] w-[30%] h-[30%] bg-green-500/10 blur-[120px] rounded-full"></div>
        </div>

        <div className="container mx-auto max-w-6xl relative z-10">
          <div className="text-center mb-16">
            <h1 className="text-4xl md:text-6xl font-extrabold text-white mb-6 tracking-tight">
              Trung tâm <span className="text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-green-400">Chỉ huy & Hỗ trợ</span>
            </h1>
            <p className="text-xl text-slate-400 max-w-2xl mx-auto font-light leading-relaxed">
              Các chuyên gia bảo mật của chúng tôi luôn trực chiến 24/7 để xử lý yêu cầu phản ứng sự cố hoặc lên phương án kiến trúc an ninh.
            </p>
          </div>
          
          <div className="grid md:grid-cols-2 gap-12 lg:gap-20">
            {/* Contact Form */}
            <div className="order-2 md:order-1">
              <div className="bg-slate-900/60 backdrop-blur-md border border-slate-800 rounded-2xl p-8 shadow-2xl relative overflow-hidden h-full">
                <div className="absolute top-0 left-0 w-full h-1 bg-gradient-to-r from-green-400 to-green-400"></div>
                <h2 className="text-2xl font-bold text-white mb-8 flex items-center gap-3">
                  <span className="w-8 h-8 rounded bg-green-500/10 flex items-center justify-center border border-green-500/30">
                    <Mail className="w-4 h-4 text-green-400" />
                  </span>
                  Gửi Thông điệp Mã hóa
                </h2>
                
                <form className="space-y-6" onSubmit={handleSubmit}>
                  {status === 'success' && (
                    <div className="bg-green-500/10 border border-green-500/50 text-green-400 p-4 rounded-lg flex items-center gap-3">
                      <ShieldCheck className="w-5 h-5 flex-shrink-0" />
                      Thông điệp đã được mã hóa và gửi tới trung tâm SOC. Chúng tôi sẽ liên hệ trong ít phút.
                    </div>
                  )}
                  {status === 'error' && (
                    <div className="bg-red-500/10 border border-red-500/50 text-red-400 p-4 rounded-lg flex items-center gap-3">
                      <Zap className="w-5 h-5 flex-shrink-0" />
                      Lỗi đường truyền mã hóa. Vui lòng thử lại sau.
                    </div>
                  )}
                  
                  <div className="group">
                    <label className="block text-sm font-medium text-slate-400 mb-2 uppercase tracking-wider group-focus-within:text-green-400 transition-colors">
                      Tên định danh (Họ và tên) <span className="text-green-400">*</span>
                    </label>
                    <input 
                      type="text"
                      className="w-full px-4 py-3 bg-slate-950 border border-slate-700 rounded-lg focus:outline-none focus:border-green-400 focus:ring-1 focus:ring-green-400/50 text-white transition-all"
                      placeholder="Nhập tên của bạn"
                      value={formData.name}
                      onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                      required
                    />
                  </div>
                  
                  <div className="group">
                    <label className="block text-sm font-medium text-slate-400 mb-2 uppercase tracking-wider group-focus-within:text-green-400 transition-colors">
                      Địa chỉ liên lạc số (Email) <span className="text-green-400">*</span>
                    </label>
                    <input 
                      type="email"
                      className="w-full px-4 py-3 bg-slate-950 border border-slate-700 rounded-lg focus:outline-none focus:border-green-400 focus:ring-1 focus:ring-green-400/50 text-white transition-all"
                      placeholder="email@organization.com"
                      value={formData.email}
                      onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                      required
                    />
                  </div>
                  
                  <div className="group">
                    <label className="block text-sm font-medium text-slate-400 mb-2 uppercase tracking-wider group-focus-within:text-green-400 transition-colors">
                      Đường dây bảo mật (Số điện thoại)
                    </label>
                    <input 
                      type="tel"
                      className="w-full px-4 py-3 bg-slate-950 border border-slate-700 rounded-lg focus:outline-none focus:border-green-400 focus:ring-1 focus:ring-green-400/50 text-white transition-all"
                      placeholder="+84 ..."
                      value={formData.phone}
                      onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
                    />
                  </div>
                  
                  <div className="group">
                    <label className="block text-sm font-medium text-slate-400 mb-2 uppercase tracking-wider group-focus-within:text-green-400 transition-colors">
                      Chi tiết sự kiện báo cáo <span className="text-green-400">*</span>
                    </label>
                    <textarea 
                      rows={4}
                      className="w-full px-4 py-3 bg-slate-950 border border-slate-700 rounded-lg focus:outline-none focus:border-green-400 focus:ring-1 focus:ring-green-400/50 text-white transition-all resize-none"
                      placeholder="Mô tả sự cố hoặc yêu cầu tư vấn..."
                      value={formData.message}
                      onChange={(e) => setFormData({ ...formData, message: e.target.value })}
                      required
                    />
                  </div>
                  
                  <button 
                    type="submit"
                    disabled={status === 'submitting'}
                    className="w-full px-6 py-4 bg-green-500 text-slate-950 font-bold rounded-lg hover:bg-green-400 transition-all shadow-[0_0_20px_rgba(34,197,94,0.3)] disabled:opacity-50 disabled:cursor-not-allowed group flex justify-center items-center gap-2"
                  >
                    {status === 'submitting' ? 'ĐANG kẾT NỐI TỚI SOC...' : 'THIẾT LẬP KẾT NỐI AN TOÀN'}
                    {status !== 'submitting' && <Zap className="w-5 h-5 group-hover:scale-110 transition-transform" />}
                  </button>
                </form>
              </div>
            </div>
            
            {/* Contact Information */}
            <div className="space-y-8 order-1 md:order-2">
              <div className="bg-slate-900/60 backdrop-blur-md border border-slate-800 rounded-2xl p-8 relative overflow-hidden">
                <div className="absolute top-0 left-0 w-2 h-full bg-slate-700"></div>
                <h2 className="text-2xl font-bold text-white mb-8 tracking-tight">Kênh Liên Lạc Chính Thức</h2>
                
                <div className="space-y-8">
                  <div className="flex items-start gap-4 group">
                    <div className="w-12 h-12 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center flex-shrink-0 group-hover:border-green-500/50 transition-colors">
                      <Mail className="w-6 h-6 text-green-400" />
                    </div>
                    <div>
                      <div className="text-sm font-medium text-slate-500 mb-1 uppercase tracking-wider">Hòm thư chuyên gia</div>
                      <div className="text-white text-lg font-mono">info@ictsoftware.net</div>
                    </div>
                  </div>
                  
                  <div className="flex items-start gap-4 group">
                    <div className="w-12 h-12 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center flex-shrink-0 group-hover:border-green-500/50 transition-colors">
                      <Phone className="w-6 h-6 text-green-400" />
                    </div>
                    <div>
                      <div className="text-sm font-medium text-slate-500 mb-1 uppercase tracking-wider">Đường dây nóng</div>
                      <div className="text-white text-lg font-mono">+84 848 200 000</div>
                    </div>
                  </div>
                  
                  <div className="flex items-start gap-4 group">
                    <div className="w-12 h-12 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center flex-shrink-0 group-hover:border-green-500/50 transition-colors">
                      <MapPin className="w-6 h-6 text-green-400" />
                    </div>
                    <div>
                      <div className="text-sm font-medium text-slate-500 mb-1 uppercase tracking-wider">Trụ sở chỉ huy</div>
                      <div className="text-white leading-relaxed font-light">
                        33 An Trai, Vân Canh, <br />Hoài Đức, Hà Nội,<br /> Việt Nam
                      </div>
                    </div>
                  </div>
                  
                  <div className="flex items-start gap-4 group">
                    <div className="w-12 h-12 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center flex-shrink-0 group-hover:border-green-500/50 transition-colors">
                      <Clock className="w-6 h-6 text-green-400" />
                    </div>
                    <div>
                      <div className="text-sm font-medium text-slate-500 mb-1 uppercase tracking-wider">Thời gian trực trạm SOC</div>
                      <div className="text-white leading-relaxed font-light">
                        Thứ 2 - Thứ 6: 08:00 - 18:00<br />
                        Thứ 7: 09:00 - 12:00<br />
                        Khẩn cấp 24/7 đối với Khách hàng TGB
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              
              <div className="bg-green-500/10 border border-green-500/30 rounded-2xl p-8 relative overflow-hidden">
                <div className="absolute top-0 right-0 w-32 h-32 bg-green-500/20 blur-[50px] rounded-full"></div>
                <div className="relative z-10 text-center md:text-left">
                  <h3 className="text-xl font-bold text-white mb-2 flex items-center gap-2 justify-center md:justify-start">
                    <ShieldCheck className="w-6 h-6 text-green-400" />
                    Cổng Phản Ứng Nhanh Kỹ Thuật
                  </h3>
                  <p className="mb-6 text-slate-400 font-light text-sm leading-relaxed">
                    Dành riêng cho các trường hợp rò rỉ dữ liệu hoặc tấn công D-DOS đang tiếp diễn. Các thông báo sẽ được định tuyến tự động vào hệ thống còi báo động SOC.
                  </p>
                  <div className="text-2xl font-mono font-bold text-green-400 tracking-wider">
                    HOT-LINE: <br className="md:hidden" /> +84 848 200 000
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
