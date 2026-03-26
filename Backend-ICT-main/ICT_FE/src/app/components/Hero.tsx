import { Link } from 'react-router';
import { ArrowRight } from 'lucide-react';
import { motion } from 'framer-motion';

export function Hero() {
  return (
    <section className="relative pt-32 pb-20 px-6 bg-slate-950 overflow-hidden min-h-screen flex text-center flex-col justify-center">
      {/* Background Decorators */}
      <div className="absolute top-0 left-0 w-full h-full overflow-hidden z-0">
        <div className="absolute top-[-10%] left-[-10%] w-[40%] h-[40%] bg-green-500/20 blur-[120px] rounded-full"></div>
        <div className="absolute bottom-[-10%] right-[-10%] w-[30%] h-[30%] bg-green-500/10 blur-[120px] rounded-full"></div>
        <div className="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-full h-full bg-[linear-gradient(rgba(255,255,255,0.03)_1px,transparent_1px),linear-gradient(90deg,rgba(255,255,255,0.03)_1px,transparent_1px)] bg-[size:50px_50px] [mask-image:radial-gradient(ellipse_60%_60%_at_50%_50%,#000_10%,transparent_100%)]"></div>
      </div>

      <div className="container mx-auto max-w-6xl relative z-10">
        <div className="text-center max-w-4xl mx-auto">
          <motion.div 
            initial={{ opacity: 0, scale: 0.9 }}
            animate={{ opacity: 1, scale: 1 }}
            transition={{ duration: 0.5 }}
            className="inline-flex items-center gap-2 px-4 py-1.5 bg-green-950/50 border border-green-500/30 text-green-400 text-sm rounded-full mb-8 shadow-[0_0_15px_rgba(34,197,94,0.2)]"
          >
            <span className="w-2 h-2 rounded-full bg-green-400 animate-pulse"></span>
            Giải pháp Bảo mật Không gian mạng Thế Hệ Mới
          </motion.div>
          
          <motion.h1 
            initial={{ opacity: 0, y: 30 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.1 }}
            className="text-5xl md:text-7xl font-extrabold text-white mb-8 leading-tight tracking-tight"
          >
            Chủ động rà soát mối đe dọa với <span className="text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-green-400">công nghệ AI</span>
          </motion.h1>
          
          <motion.p 
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.2 }}
            className="text-xl text-slate-300 mb-12 leading-relaxed font-light"
          >
            Chúng tôi xây dựng các nền tảng phòng chống mối đe dọa nâng cao, tường lửa ứng dụng trực tuyến và hệ thống trung tâm an ninh vận hành bằng AI. 
          </motion.p>
          
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.3 }}
            className="flex flex-col sm:flex-row gap-6 justify-center"
          >
            <Link 
              to="/products"
              className="inline-flex items-center justify-center gap-3 px-8 py-4 bg-green-500 text-slate-950 font-semibold rounded-md hover:bg-green-400 transition-all shadow-[0_0_20px_rgba(34,197,94,0.4)]"
            >
              Khám phá sản phẩm
              <ArrowRight className="w-5 h-5" />
            </Link>
            <Link 
              to="/contact"
              className="inline-flex items-center justify-center gap-2 px-8 py-4 bg-slate-900 border border-slate-700 text-white rounded-md hover:border-green-500/50 hover:bg-slate-800 transition-colors"
            >
              Liên hệ chuyên gia
            </Link>
          </motion.div>

          <motion.div 
            initial={{ opacity: 0, y: 40 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.8, delay: 0.5 }}
            className="mt-20 grid grid-cols-1 sm:grid-cols-3 gap-8 py-10 border-t border-slate-800 relative"
          >
            <div className="absolute top-0 left-1/4 w-1/2 h-px bg-gradient-to-r from-transparent via-green-500/50 to-transparent"></div>
            <div>
              <div className="text-5xl font-bold text-white mb-2 tracking-tighter">500<span className="text-green-400">+</span></div>
              <div className="text-sm text-slate-400 font-medium uppercase tracking-widest">Khách hàng Doanh Nghiệp</div>
            </div>
            <div>
              <div className="text-5xl font-bold text-white mb-2 tracking-tighter">99.9<span className="text-green-400">%</span></div>
              <div className="text-sm text-slate-400 font-medium uppercase tracking-widest">Uptime Đảm Bảo</div>
            </div>
            <div>
              <div className="text-5xl font-bold text-white mb-2 tracking-tighter">24/7</div>
              <div className="text-sm text-slate-400 font-medium uppercase tracking-widest">Phản hồi sự cố khẩn cấp</div>
            </div>
          </motion.div>
        </div>
      </div>
    </section>
  );
}
