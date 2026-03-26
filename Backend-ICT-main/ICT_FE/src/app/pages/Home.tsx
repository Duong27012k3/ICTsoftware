import { useEffect, useState } from 'react';
import { api, BackendField, BackendProject } from '../services/api';
import { Hero } from '../components/Hero';
import { FieldCard } from '../components/FieldCard';
import { ProductCard } from '../components/ProductCard';
import { Footer } from '../components/Footer';
import { Shield as ShieldIcon, Brain, CheckCircle, Users, Award, HeadphonesIcon, Lock, Eye, Database, Key, Activity, Monitor, Globe, ArrowRight } from 'lucide-react';
import { Link } from 'react-router';
import React from 'react';
import { motion } from 'framer-motion';

const fieldIcons: Record<string, React.ReactNode> = {
  'bao-mat-du-lieu': <ShieldIcon className="w-6 h-6" />,
  'danh-gia-an-ninh': <Eye className="w-6 h-6" />,
  'bao-mat-ung-dung': <Globe className="w-6 h-6" />,
  'quan-ly-truy-cap': <Key className="w-6 h-6" />,
  'giam-sat-su-co': <Activity className="w-6 h-6" />,
  'bao-mat-endpoint': <Monitor className="w-6 h-6" />
};

const productIcons: Record<string, React.ReactNode> = {
  'dlp-network': <ShieldIcon className="w-6 h-6" />,
  'waf': <Lock className="w-6 h-6" />,
  'ias': <Eye className="w-6 h-6" />,
  'ict-pam': <Key className="w-6 h-6" />,
  'ict-siem': <Database className="w-6 h-6" />,
  'ict-edr': <Monitor className="w-6 h-6" />
};

export function Home() {
  const [fields, setFields] = useState<BackendField[]>([]);
  const [products, setProducts] = useState<BackendProject[]>([]);

  useEffect(() => {
    async function loadData() {
      try {
        const fieldsData = await api.getFields();
        const productsData = await api.getProjects();
        setFields(fieldsData);
        setProducts(productsData);
      } catch (err) {
        console.error("Failed to load data", err);
      }
    }
    loadData();
  }, []);

  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      <Hero />
      
      {/* Why Choose Us Section */}
      <section className="relative py-24 px-6 overflow-hidden">
        <div className="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[800px] h-[300px] bg-green-900/10 blur-[100px] rounded-full pointer-events-none"></div>
        <div className="container mx-auto max-w-6xl relative z-10">
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <h2 className="text-3xl md:text-5xl font-bold text-white mb-6 tracking-tight">
              Tại sao chọn <span className="text-green-400">ICT Software?</span>
            </h2>
            <p className="text-lg text-slate-400 max-w-2xl mx-auto leading-relaxed">
              Kiến trúc bảo mật cấp doanh nghiệp, kết hợp trí tuệ nhân tạo để phát hiện và ngăn chặn mọi nguy cơ zero-day.
            </p>
          </motion.div>
          
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            <motion.div 
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5, delay: 0.1 }}
              className="p-8 bg-slate-900/50 border border-slate-800 rounded-xl text-center hover:border-green-500/50 hover:bg-slate-800/80 transition-all duration-300 group"
            >
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mx-auto mb-6 group-hover:border-green-400/50 group-hover:shadow-[0_0_20px_rgba(34,197,94,0.2)] transition-all">
                <CheckCircle className="w-8 h-8 text-green-400" />
              </div>
              <h3 className="text-lg font-bold text-white mb-3 tracking-wide">Bảo mật Tối đa</h3>
              <p className="text-sm text-slate-400 leading-relaxed">Mã hóa end-to-end, tuân thủ các chuẩn an ninh khắt khe nhất.</p>
            </motion.div>
            
            <motion.div 
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5, delay: 0.2 }}
              className="p-8 bg-slate-900/50 border border-slate-800 rounded-xl text-center hover:border-green-500/50 hover:bg-slate-800/80 transition-all duration-300 group"
            >
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mx-auto mb-6 group-hover:border-green-400/50 group-hover:shadow-[0_0_20px_rgba(34,197,94,0.2)] transition-all">
                <Users className="w-8 h-8 text-green-400" />
              </div>
              <h3 className="text-lg font-bold text-white mb-3 tracking-wide">Đội ngũ Chuyên gia</h3>
              <p className="text-sm text-slate-400 leading-relaxed">Hơn 10 năm kinh nghiệm xử lý các sự cố mạng quy mô lớn.</p>
            </motion.div>
            
            <motion.div 
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5, delay: 0.3 }}
              className="p-8 bg-slate-900/50 border border-slate-800 rounded-xl text-center hover:border-green-500/50 hover:bg-slate-800/80 transition-all duration-300 group"
            >
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mx-auto mb-6 group-hover:border-green-400/50 group-hover:shadow-[0_0_20px_rgba(34,197,94,0.2)] transition-all">
                <HeadphonesIcon className="w-8 h-8 text-green-400" />
              </div>
              <h3 className="text-lg font-bold text-white mb-3 tracking-wide">Giám sát 24/7</h3>
              <p className="text-sm text-slate-400 leading-relaxed">SOC (Security Operations Center) trực chiến ngày đêm.</p>
            </motion.div>
            
            <motion.div 
              initial={{ opacity: 0, y: 20 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.5, delay: 0.4 }}
              className="p-8 bg-slate-900/50 border border-slate-800 rounded-xl text-center hover:border-green-500/50 hover:bg-slate-800/80 transition-all duration-300 group"
            >
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mx-auto mb-6 group-hover:border-green-400/50 group-hover:shadow-[0_0_20px_rgba(34,197,94,0.2)] transition-all">
                <Award className="w-8 h-8 text-green-400" />
              </div>
              <h3 className="text-lg font-bold text-white mb-3 tracking-wide">Giải thưởng Uy tín</h3>
              <p className="text-sm text-slate-400 leading-relaxed">Công nghệ đã được chứng thực bởi các tổ chức bảo mật quốc tế.</p>
            </motion.div>
          </div>
        </div>
      </section>
      
      {/* Fields Section Placeholder (Would update FieldCard.tsx to dark theme in real scenario, here assuming transparent backgrounds) */}
      <section className="py-24 px-6 bg-slate-900/30 border-t border-b border-slate-800/50">
        <div className="container mx-auto max-w-6xl">
          <motion.div 
            initial={{ opacity: 0, scale: 0.95 }}
            whileInView={{ opacity: 1, scale: 1 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <h2 className="text-3xl md:text-5xl font-bold text-white mb-6 tracking-tight">
              Hệ sinh thái <span className="text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-green-400">Giải pháp</span>
            </h2>
            <p className="text-lg text-slate-400 max-w-2xl mx-auto leading-relaxed">
              Các mảng hoạt động chiến lược tích hợp sâu trí tuệ nhân tạo và mật mã học tiên tiến.
            </p>
          </motion.div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            {fields.map((field, index) => (
              <FieldCard
                key={field.fieldId}
                id={field.fieldId.toString()}
                title={field.fieldTrans?.[0]?.name || field.nameField || field.uid || 'N/A'}
                description={field.fieldTrans?.[0]?.description || ''}
                image={api.getImageUrl(field.image) || "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?auto=format&fit=crop&q=80"}
                icon={fieldIcons[field.uid] || <Brain className="w-6 h-6" />}
                index={index}
              />
            ))}
          </div>
        </div>
      </section>
      
      {/* Products Section */}
      <section className="relative py-24 px-6 overflow-hidden">
        {/* Abstract Background Elements */}
        <div className="absolute top-0 right-0 w-[500px] h-[500px] bg-green-600/10 blur-[150px] rounded-full pointer-events-none"></div>

        <div className="container mx-auto max-w-6xl relative z-10">
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.6 }}
            className="text-center mb-16"
          >
            <h2 className="text-3xl md:text-5xl font-bold text-white mb-6 tracking-tight">
              Sản phẩm <span className="text-green-400">Nổi bật</span>
            </h2>
            <p className="text-lg text-slate-400 max-w-2xl mx-auto leading-relaxed">
              Bảo vệ toàn diện mạng lưới, dữ liệu, và danh tính số của bạn với các công cụ giám sát theo thời gian thực.
            </p>
          </motion.div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {products.slice(0, 3).map((product, index) => (
              <ProductCard
                key={product.projectId}
                id={product.projectId.toString()}
                title={product.projectTrans?.[0]?.name || product.name || 'N/A'}
                description={product.projectTrans?.[0]?.shortDescription || product.description || ''}
                image={api.getImageUrl(product.image) || "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?auto=format&fit=crop&q=80"}
                icon={productIcons[String(product.projectId)] || <ShieldIcon className="w-6 h-6" />}
                index={index}
              />
            ))}
          </div>
          
          <div className="text-center mt-16">
            <Link 
              to="/products"
              className="inline-flex items-center justify-center gap-2 px-8 py-4 border border-green-500/50 text-green-400 font-semibold rounded-md hover:border-green-400 hover:bg-green-900/20 transition-all shadow-[0_0_15px_rgba(34,197,94,0.1)] hover:shadow-[0_0_25px_rgba(34,197,94,0.3)]"
            >
              Xem toàn bộ sản phẩm
              <ArrowRight className="w-5 h-5" />
            </Link>
          </div>
        </div>
      </section>
      
      {/* CTA Section */}
      <section className="py-24 px-6 bg-gradient-to-br from-slate-900 via-slate-950 to-green-950 border-t border-slate-800">
        <div className="container mx-auto max-w-4xl text-center relative z-10">
          <div className="inline-block px-4 py-1.5 bg-green-500/10 border border-green-500/30 text-green-400 text-sm rounded-full mb-8 font-medium">
             Sẵn sàng trải nghiệm mức độ bảo mật mới?
          </div>
          <h2 className="text-4xl md:text-5xl font-extrabold text-white mb-8 tracking-tight">
            Xây dựng pháo đài kỹ thuật số ngay hôm nay
          </h2>
          <p className="text-xl mb-12 text-slate-300 font-light leading-relaxed">
            Liên hệ với chuyên gia an ninh mạng của chúng tôi để được audit hệ thống và đề xuất lộ trình bảo vệ toàn diện.
          </p>
          <Link 
            to="/contact"
            className="inline-block px-10 py-5 bg-green-500 text-slate-950 font-bold rounded-md hover:bg-green-400 transition-all shadow-[0_0_30px_rgba(34,197,94,0.3)] hover:shadow-[0_0_40px_rgba(34,197,94,0.5)] scale-100 hover:scale-[1.02]"
          >
            Yêu Cầu Demo & Tư Vấn
          </Link>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
