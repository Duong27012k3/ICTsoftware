import { Footer } from '../components/Footer';
import { Target, Eye, Award, Users, Shield, Zap } from 'lucide-react';
import { motion } from 'framer-motion';

export function About() {
  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      <section className="relative pt-32 pb-20 px-6 overflow-hidden">
        {/* Background Decorators */}
        <div className="absolute top-[-10%] left-[-10%] w-[40%] h-[40%] bg-green-500/10 blur-[120px] rounded-full pointer-events-none"></div>
        <div className="absolute bottom-[-10%] right-[-10%] w-[30%] h-[30%] bg-green-500/5 blur-[120px] rounded-full pointer-events-none"></div>

        <div className="container mx-auto max-w-5xl relative z-10">
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <div className="inline-flex items-center gap-2 px-4 py-1.5 bg-green-950/50 border border-green-500/30 text-green-400 text-sm rounded-full mb-8">
              <span className="w-2 h-2 rounded-full bg-green-400 animate-pulse"></span>
              Về chúng tôi
            </div>
            <h1 className="text-4xl md:text-6xl font-extrabold text-white mb-6 tracking-tight">
              ICT <span className="text-green-400">Software</span>
            </h1>
            <p className="text-xl text-slate-400 leading-relaxed max-w-3xl mx-auto font-light">
              Chúng tôi là đơn vị cung cấp giải pháp an ninh mạng hàng đầu, chuyên về phát triển các nền tảng phòng chống mối đe dọa với độ tin cậy cao nhất.
            </p>
          </motion.div>
          
          <motion.div 
            initial={{ opacity: 0, scale: 0.95 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.8, delay: 0.2 }}
            className="mb-16 relative group"
          >
            <div className="absolute -inset-1 bg-gradient-to-r from-green-500 to-green-500 rounded-xl blur opacity-25 group-hover:opacity-40 transition duration-1000"></div>
            <img 
              src="https://images.unsplash.com/photo-1769740333462-9a63bfa914bc?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxwcm9mZXNzaW9uYWwlMjBvZmZpY2UlMjB0ZWFtJTIwbWVldGluZ3xlbnwxfHx8fDE3NzIxMjE2NTN8MA&ixlib=rb-4.1.0&q=80&w=1080"
              alt="ICT Software Team"
              className="relative w-full h-96 object-cover rounded-xl border border-slate-700 shadow-2xl"
            />
          </motion.div>
          
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="mb-16 space-y-6"
          >
            <p className="text-slate-300 leading-relaxed text-lg font-light">
              ICT Software được thành lập với sứ mệnh mang đến những giải pháp an ninh mạng tiên tiến, giúp doanh nghiệp và tổ chức Việt Nam chủ động bảo vệ tài sản số trong kỷ nguyên chuyển đổi số. Với đội ngũ chuyên gia giàu kinh nghiệm và tâm huyết, chúng tôi không ngừng nghiên cứu và phát triển các sản phẩm bảo mật chất lượng cao.
            </p>
            <p className="text-slate-300 leading-relaxed text-lg font-light">
              Chúng tôi tự hào là đối tác tin cậy của hơn 500 doanh nghiệp tại Việt Nam, cung cấp các giải pháp từ DLP, WAF, SIEM, EDR đến PAM và IAS. Mỗi sản phẩm đều được thiết kế theo tiêu chí: bảo mật tối đa, hiệu suất cao và vận hành đơn giản.
            </p>
          </motion.div>
          
          {/* Mission & Vision */}
          <div className="grid md:grid-cols-2 gap-8 mb-16">
            <motion.div 
              initial={{ opacity: 0, x: -30 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6 }}
              className="bg-slate-900/50 border border-slate-800 rounded-xl p-8 hover:border-green-500/30 transition-colors"
            >
              <div className="w-14 h-14 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mb-6">
                <Target className="w-7 h-7 text-green-400" />
              </div>
              <h3 className="text-xl font-bold text-white mb-3">Sứ mệnh</h3>
              <p className="text-slate-400 leading-relaxed">
                Mang đến giải pháp an ninh mạng đáng tin cậy, giúp doanh nghiệp tối ưu hóa hệ thống phòng thủ và phát triển bền vững trong thời đại số hóa.
              </p>
            </motion.div>
            
            <motion.div 
              initial={{ opacity: 0, x: 30 }}
              whileInView={{ opacity: 1, x: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.6 }}
              className="bg-slate-900/50 border border-slate-800 rounded-xl p-8 hover:border-green-500/30 transition-colors"
            >
              <div className="w-14 h-14 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mb-6">
                <Eye className="w-7 h-7 text-green-400" />
              </div>
              <h3 className="text-xl font-bold text-white mb-3">Tầm nhìn</h3>
              <p className="text-slate-400 leading-relaxed">
                Trở thành đơn vị hàng đầu về giải pháp an ninh mạng tại Việt Nam, được khách hàng tin tưởng và đánh giá cao về chất lượng và công nghệ.
              </p>
            </motion.div>
          </div>
          
          {/* Core Values */}
          <motion.div 
            initial={{ opacity: 0, y: 30 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="bg-slate-900/30 border border-slate-800 rounded-xl p-10"
          >
            <h3 className="text-2xl font-bold text-white mb-8 text-center tracking-tight">Giá trị cốt lõi</h3>
            <div className="grid md:grid-cols-4 gap-8">
              <motion.div initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} transition={{ delay: 0.1 }} className="text-center group">
                <div className="w-14 h-14 bg-green-500/10 border border-green-500/30 rounded-xl flex items-center justify-center mx-auto mb-4 group-hover:bg-green-500/20 group-hover:shadow-[0_0_15px_rgba(34,197,94,0.2)] transition-all">
                  <Award className="w-7 h-7 text-green-400" />
                </div>
                <h4 className="text-white font-semibold mb-2">Chất lượng</h4>
                <p className="text-sm text-slate-400">Cam kết cung cấp sản phẩm bảo mật chất lượng cao nhất</p>
              </motion.div>
              
              <motion.div initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} transition={{ delay: 0.2 }} className="text-center group">
                <div className="w-14 h-14 bg-green-500/10 border border-green-500/30 rounded-xl flex items-center justify-center mx-auto mb-4 group-hover:bg-green-500/20 group-hover:shadow-[0_0_15px_rgba(34,197,94,0.2)] transition-all">
                  <Users className="w-7 h-7 text-green-400" />
                </div>
                <h4 className="text-white font-semibold mb-2">Tận tâm</h4>
                <p className="text-sm text-slate-400">Đặt an toàn khách hàng làm trung tâm</p>
              </motion.div>
              
              <motion.div initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} transition={{ delay: 0.3 }} className="text-center group">
                <div className="w-14 h-14 bg-green-500/10 border border-green-500/30 rounded-xl flex items-center justify-center mx-auto mb-4 group-hover:bg-green-500/20 group-hover:shadow-[0_0_15px_rgba(34,197,94,0.2)] transition-all">
                  <Zap className="w-7 h-7 text-green-400" />
                </div>
                <h4 className="text-white font-semibold mb-2">Đổi mới</h4>
                <p className="text-sm text-slate-400">Không ngừng sáng tạo công nghệ bảo mật tiên tiến</p>
              </motion.div>
              
              <motion.div initial={{ opacity: 0 }} whileInView={{ opacity: 1 }} transition={{ delay: 0.4 }} className="text-center group">
                <div className="w-14 h-14 bg-green-500/10 border border-green-500/30 rounded-xl flex items-center justify-center mx-auto mb-4 group-hover:bg-green-500/20 group-hover:shadow-[0_0_15px_rgba(34,197,94,0.2)] transition-all">
                  <Shield className="w-7 h-7 text-green-400" />
                </div>
                <h4 className="text-white font-semibold mb-2">Minh bạch</h4>
                <p className="text-sm text-slate-400">Luôn trung thực và minh bạch với khách hàng</p>
              </motion.div>
            </div>
          </motion.div>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
