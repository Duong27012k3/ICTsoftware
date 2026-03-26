import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router';
import { ArrowLeft, CheckCircle, Network } from 'lucide-react';
import { api, BackendProject } from '../services/api';
import { Footer } from '../components/Footer';

export function ProductDetail() {
  const { id } = useParams();
  const [product, setProduct] = useState<BackendProject | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchProduct() {
      if (!id) return;
      try {
        const data = await api.getProjectById(id);
        setProduct(data);
      } catch (err) {
        console.error("Failed to load product", err);
      } finally {
        setLoading(false);
      }
    }
    fetchProduct();
  }, [id]);

  if (loading) return <div className="min-h-screen bg-slate-950 flex items-center justify-center text-green-400 font-mono tracking-widest text-xl">ĐANG TẢI DỮ LIỆU...</div>;
  if (!product) {
    return (
      <div className="min-h-screen bg-slate-950 flex items-center justify-center pt-20">
        <div className="text-center">
          <h1 className="text-4xl text-red-400 mb-4 font-bold">LỖI: 404 Không tìm thấy sản phẩm</h1>
          <Link to="/products" className="text-green-400 hover:text-green-300 underline underline-offset-4">
            Quay lại Trung tâm Sản phẩm
          </Link>
        </div>
      </div>
    );
  }
  
  const defaultImage = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?auto=format&fit=crop&q=80";
  const displayImage = api.getImageUrl(product.image)?.includes("localhost") || !api.getImageUrl(product.image) ? defaultImage : api.getImageUrl(product.image);

  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      {/* Hero Section */}
      <section className="relative pt-32 pb-16 px-6 overflow-hidden border-b border-slate-800">
        <div className="absolute top-[-20%] right-[-10%] w-[50%] h-[50%] bg-green-500/10 blur-[150px] rounded-full pointer-events-none"></div>

        <div className="container mx-auto max-w-6xl relative z-10">
          <Link 
            to="/products" 
            className="inline-flex items-center gap-2 text-green-500 hover:text-green-300 mb-8 group font-medium uppercase tracking-wider text-sm"
          >
            <ArrowLeft className="w-5 h-5 group-hover:-translate-x-1 transition-transform" />
            Trở Về Danh Mục Sản Phẩm
          </Link>
          
          <div className="grid md:grid-cols-2 gap-12 items-center">
            <div>
              <div className="inline-flex items-center justify-center px-3 py-1 bg-slate-900 border border-slate-700 text-xs font-mono text-slate-400 rounded mb-6 tracking-widest">
                ID: {product.projectId} | TRẠNG THÁI: <span className="text-green-400 ml-2">ĐANG HOẠT ĐỘNG</span>
              </div>
              <h1 className="text-5xl md:text-6xl font-extrabold text-white mb-6 tracking-tight">
                {product.projectTrans?.[0]?.name || product.name || 'N/A'}
              </h1>
              
              <p className="text-xl text-slate-400 leading-relaxed mb-8 font-light">
                {product.projectTrans?.[0]?.shortDescription || product.description || ''}
              </p>
              
              {(product as any).technicalSpecs && (
                <div className="grid grid-cols-3 gap-4 mb-8">
                  {(product as any).technicalSpecs.map((spec: any, index: number) => (
                    <div key={index} className="text-center p-4 bg-slate-900/50 border border-slate-800 rounded-lg backdrop-blur-sm hover:border-green-500/50 transition-colors">
                      <div className="text-2xl font-bold text-green-400 mb-1">{spec.value}</div>
                      <div className="text-xs text-slate-500 uppercase tracking-wider">{spec.label}</div>
                    </div>
                  ))}
                </div>
              )}
              
              <div className="flex flex-col sm:flex-row gap-4">
                <Link 
                  to="/contact"
                  className="px-8 py-4 bg-green-500 text-slate-950 font-bold text-center rounded-md hover:bg-green-400 transition-all shadow-[0_0_20px_rgba(34,197,94,0.3)]"
                >
                  Triển Khai Ngay
                </Link>
                <button className="px-8 py-4 border border-slate-700 text-white rounded-md hover:border-green-500/50 hover:bg-slate-900 transition-colors">
                  Tải Tài Liệu Kỹ Thuật
                </button>
              </div>
            </div>
            
            <div className="relative group">
              <div className="absolute -inset-1 bg-gradient-to-r from-green-500 to-green-500 rounded-xl blur opacity-25 group-hover:opacity-50 transition duration-1000 group-hover:duration-200"></div>
              <img 
                src={displayImage}
                alt={product.projectTrans?.[0]?.name || product.name || ''}
                className="relative w-full h-96 object-cover rounded-xl border border-slate-700 shadow-2xl mix-blend-luminosity hover:mix-blend-normal transition-all duration-500"
              />
            </div>
          </div>
        </div>
      </section>
      
      {/* Features Section */}
      <section className="py-24 px-6 bg-slate-950 relative overflow-hidden">
        <div className="container mx-auto max-w-6xl relative z-10">
          <div className="text-center mb-16">
            <h2 className="text-3xl md:text-4xl font-bold text-white mb-4 tracking-tight">Tính năng Cốt lõi</h2>
            <p className="text-slate-400 max-w-2xl mx-auto text-lg leading-relaxed">
              Các tính năng vượt trội được thiết kế đặc thù cho kiến trúc an ninh của tổ chức ngân hàng và chính phủ.
            </p>
          </div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {((product as any).features || []).map((feature: string, index: number) => (
              <div key={index} className="bg-slate-900/60 border border-slate-800 rounded-xl p-6 hover:shadow-[0_0_20px_rgba(34,197,94,0.1)] hover:border-green-900 transition-all group">
                <div className="w-12 h-12 bg-slate-950 border border-slate-800 rounded-lg flex items-center justify-center mb-5 group-hover:border-green-500/50 transition-colors">
                  <CheckCircle className="w-6 h-6 text-green-400" />
                </div>
                <h3 className="text-lg font-medium text-slate-200 leading-snug">{feature}</h3>
              </div>
            ))}
          </div>
        </div>
      </section>
      
      {/* Description Section */}
      <section className="py-24 px-6 bg-slate-900/40 border-t border-b border-slate-800/50">
        <div className="container mx-auto max-w-6xl">
          <div className="bg-slate-950 border border-slate-800 rounded-2xl p-8 md:p-14 shadow-2xl relative overflow-hidden mb-12">
            <div className="absolute top-0 left-0 w-2 h-full bg-gradient-to-b from-green-400 to-green-400"></div>
            <h2 className="text-3xl font-bold text-white mb-8 tracking-tight">Tổng Quan Hệ Thống</h2>
            <div className="prose prose-lg prose-invert max-w-none">
              <p className="text-slate-300 leading-relaxed font-light text-lg mb-8">
                {product.projectTrans?.[0]?.description || ''}
              </p>
              
              {(product as any).benefits && (product as any).benefits.length > 0 && (
                <div className="mt-12">
                  <h3 className="text-2xl font-bold text-white mb-6 border-b border-slate-800 pb-4">Lợi Ích Doanh Nghiệp</h3>
                  <ul className="space-y-4">
                    {(product as any).benefits.map((benefit: string, index: number) => (
                      <li key={index} className="flex items-start gap-4">
                        <div className="mt-1 w-6 h-6 rounded bg-green-500/10 flex items-center justify-center flex-shrink-0 border border-green-500/30">
                          <CheckCircle className="w-4 h-4 text-green-400" />
                        </div>
                        <span className="text-slate-300 text-lg leading-relaxed">{benefit}</span>
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </div>
          </div>
          
          {/* Detailed Sections with alternating layout */}
          {(product as any).detailedSections && (product as any).detailedSections.length > 0 && (
            <div className="space-y-12">
              {(product as any).detailedSections.map((section: any, index: number) => (
                <div 
                  key={index} 
                  className="bg-slate-950 border border-slate-800 rounded-2xl overflow-hidden hover:border-green-900/50 transition-colors"
                >
                  {index % 2 === 0 ? (
                    // Text left, Image right
                    <div className="grid md:grid-cols-2 gap-0 items-stretch">
                      <div className="p-8 md:p-12 flex flex-col justify-center">
                        <h3 className="text-2xl font-bold text-white mb-4 tracking-tight">
                          {section.title}
                        </h3>
                        <p className="text-slate-400 leading-relaxed">
                          {section.content}
                        </p>
                      </div>
                      
                      {section.imageUrl && (
                        <div className="min-h-[300px] md:min-h-[400px] border-l border-slate-800 relative group">
                          <div className="absolute inset-0 bg-green-500/10 opacity-0 group-hover:opacity-100 transition-opacity duration-300 z-10"></div>
                          <img 
                            src={section.imageUrl}
                            alt={section.title}
                            className="w-full h-full object-cover mix-blend-luminosity group-hover:mix-blend-normal transition-all duration-500"
                          />
                        </div>
                      )}
                    </div>
                  ) : (
                    // Image left, Text right
                    <div className="grid md:grid-cols-2 gap-0 items-stretch">
                      {section.imageUrl && (
                        <div className="min-h-[300px] md:min-h-[400px] border-r border-slate-800 order-2 md:order-1 relative group">
                           <div className="absolute inset-0 bg-green-500/10 opacity-0 group-hover:opacity-100 transition-opacity duration-300 z-10"></div>
                          <img 
                            src={section.imageUrl}
                            alt={section.title}
                            className="w-full h-full object-cover mix-blend-luminosity group-hover:mix-blend-normal transition-all duration-500"
                          />
                        </div>
                      )}
                      
                      <div className="p-8 md:p-12 flex flex-col justify-center order-1 md:order-2">
                        <h3 className="text-2xl font-bold text-white mb-4 tracking-tight">
                          {section.title}
                        </h3>
                        <p className="text-slate-400 leading-relaxed">
                          {section.content}
                        </p>
                      </div>
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>
      </section>
      
      {/* Deployment Model Section */}
      <section className="py-24 px-6 bg-slate-950">
        <div className="container mx-auto max-w-6xl">
          <div className="text-center mb-16">
            <h2 className="text-3xl md:text-4xl font-bold text-white mb-4 tracking-tight">Mô Hình Triển Khai</h2>
            <p className="text-slate-400 max-w-2xl mx-auto text-lg leading-relaxed">
              Linh hoạt thích ứng với mọi hạ tầng kiến trúc từ biên đám mây (Cloud Edge) đến máy chủ vật lý (On-Prem).
            </p>
          </div>
          
          <div className="grid md:grid-cols-3 gap-8">
            <div className="bg-slate-900 border border-slate-800 rounded-xl p-8 text-center hover:border-green-500/30 transition-all">
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-xl flex items-center justify-center mx-auto mb-6">
                <Network className="w-8 h-8 text-slate-400" />
              </div>
              <h3 className="text-xl font-bold text-white mb-3 tracking-wide">On-Premise</h3>
              <p className="text-sm text-slate-400 mb-6 leading-relaxed">
                Máy chủ độc lập nội khu. Giao thức Air-gapped an toàn tuyệt đối.
              </p>
              <ul className="space-y-3 text-sm text-slate-400 text-left">
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-green-500 flex-shrink-0" />
                  <span>Dữ liệu lưu trữ nội bộ</span>
                </li>
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-green-500 flex-shrink-0" />
                  <span>Tùy biến kernel chuyên sâu</span>
                </li>
              </ul>
            </div>
            
            <div className="bg-slate-900 border-2 border-green-500/50 rounded-xl p-8 text-center relative shadow-[0_0_30px_rgba(34,197,94,0.1)] hover:shadow-[0_0_40px_rgba(34,197,94,0.2)] transition-all transform hover:-translate-y-1">
              <div className="absolute -top-4 left-1/2 -translate-x-1/2 px-4 py-1 bg-green-500 text-slate-950 font-bold text-xs uppercase tracking-widest rounded-full">
                Khuyên Dùng
              </div>
              <div className="w-16 h-16 bg-green-950 border border-green-500/30 rounded-xl flex items-center justify-center mx-auto mb-6">
                <Network className="w-8 h-8 text-green-400" />
              </div>
              <h3 className="text-xl font-bold text-white mb-3 tracking-wide">Hybrid Cloud</h3>
              <p className="text-sm text-slate-300 mb-6 leading-relaxed">
                Đồng bộ hóa luồng dữ liệu giữa đám mây và máy chủ vật lý.
              </p>
              <ul className="space-y-3 text-sm text-slate-300 text-left">
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-green-400 flex-shrink-0" />
                  <span>Chuyển đổi dự phòng phân tán</span>
                </li>
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-green-400 flex-shrink-0" />
                  <span>Tối ưu độ trễ băng thông</span>
                </li>
              </ul>
            </div>
            
            <div className="bg-slate-900 border border-slate-800 rounded-xl p-8 text-center hover:border-green-500/30 transition-all">
              <div className="w-16 h-16 bg-slate-950 border border-slate-800 rounded-xl flex items-center justify-center mx-auto mb-6">
                <Network className="w-8 h-8 text-slate-400" />
              </div>
              <h3 className="text-xl font-bold text-white mb-3 tracking-wide">SaaS/Cloud</h3>
              <p className="text-sm text-slate-400 mb-6 leading-relaxed">
                Triển khai qua nền tảng Cloud native. Tự động scaling.
              </p>
              <ul className="space-y-3 text-sm text-slate-400 text-left">
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-purple-400 flex-shrink-0" />
                  <span>Dễ dàng mở rộng</span>
                </li>
                <li className="flex items-center gap-3">
                  <CheckCircle className="w-4 h-4 text-purple-400 flex-shrink-0" />
                  <span>0-day patch automation</span>
                </li>
              </ul>
            </div>
          </div>
        </div>
      </section>
      
      {/* CTA Section */}
      <section className="py-20 px-6 bg-green-950/50 border-t border-slate-800 relative overflow-hidden">
        <div className="absolute bottom-0 right-0 w-[400px] h-[400px] bg-green-500/10 blur-[100px] rounded-full pointer-events-none"></div>
        <div className="container mx-auto max-w-4xl text-center relative z-10">
          <h2 className="text-3xl font-bold text-white mb-4 tracking-tight">
            Xây dựng Tuyến phòng thủ {product.projectTrans?.[0]?.name || product.name}
          </h2>
          <p className="text-lg mb-8 text-slate-400">
            Chuyên gia Kiến trúc An ninh của chúng tôi luôn trực tuyến 24/7 để phân tích cấu hình hệ thống hiện tại của bạn.
          </p>
          <div className="flex flex-col sm:flex-row gap-4 justify-center">
            <Link 
              to="/contact"
              className="inline-block px-8 py-4 bg-green-500 text-slate-950 font-bold tracking-wide rounded hover:bg-green-400 transition-colors shadow-[0_0_15px_rgba(34,197,94,0.3)]"
            >
              Yêu Cầu Tham Vấn Kỹ Thuật
            </Link>
          </div>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}