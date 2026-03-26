import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router';
import { ArrowLeft, CheckCircle, TrendingUp } from 'lucide-react';
import { api, BackendField } from '../services/api';
import { Footer } from '../components/Footer';

export function FieldDetail() {
  const { id } = useParams();
  const [field, setField] = useState<BackendField | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchField() {
      if (!id) return;
      try {
        const data = await api.getFieldById(id);
        setField(data);
      } catch (err) {
        console.error("Failed to load field", err);
      } finally {
        setLoading(false);
      }
    }
    fetchField();
  }, [id]);
  
  if (loading) return <div className="min-h-screen bg-slate-950 flex items-center justify-center text-green-400 font-mono tracking-widest text-xl">ĐANG TẢI DỮ LIỆU...</div>;
  if (!field) {
    return (
      <div className="min-h-screen bg-slate-950 flex items-center justify-center pt-20">
        <div className="text-center">
          <h1 className="text-4xl text-red-400 mb-4 font-bold">LỖI: 404 Không tìm thấy lĩnh vực</h1>
          <Link to="/fields" className="text-green-400 hover:text-green-300 underline underline-offset-4">
            Quay lại danh sách lĩnh vực
          </Link>
        </div>
      </div>
    );
  }
  
  const defaultImage = "https://images.unsplash.com/photo-1544197150-b99a580bb7a8?auto=format&fit=crop&q=80";
  const displayImage = api.getImageUrl(field.image)?.includes("localhost") || !api.getImageUrl(field.image) ? defaultImage : api.getImageUrl(field.image);

  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      {/* Hero */}
      <section className="relative pt-32 pb-16 px-6 overflow-hidden border-b border-slate-800">
        <div className="absolute top-[-20%] right-[-10%] w-[50%] h-[50%] bg-green-500/10 blur-[150px] rounded-full pointer-events-none"></div>

        <div className="container mx-auto max-w-6xl relative z-10">
          <Link 
            to="/fields" 
            className="inline-flex items-center gap-2 text-green-500 hover:text-green-300 mb-8 group font-medium uppercase tracking-wider text-sm"
          >
            <ArrowLeft className="w-5 h-5 group-hover:-translate-x-1 transition-transform" />
            Trở Về Danh Sách Lĩnh Vực
          </Link>
          
          <div className="grid md:grid-cols-2 gap-12 items-center">
            <div>
              <h1 className="text-4xl md:text-5xl font-extrabold text-white mb-6 tracking-tight">
                {field.fieldTrans?.[0]?.name || field.nameField || field.uid}
              </h1>
              
              <p className="text-xl text-slate-400 leading-relaxed mb-8 font-light">
                {field.fieldTrans?.[0]?.description || ''}
              </p>
              
              <div className="flex flex-col sm:flex-row gap-4">
                <Link 
                  to="/contact"
                  className="px-8 py-4 bg-green-500 text-slate-950 font-bold text-center rounded-md hover:bg-green-400 transition-all shadow-[0_0_20px_rgba(34,197,94,0.3)]"
                >
                  Liên hệ tư vấn
                </Link>
                <button className="px-8 py-4 border border-slate-700 text-white rounded-md hover:border-green-500/50 hover:bg-slate-900 transition-colors">
                  Dùng thử miễn phí
                </button>
              </div>
            </div>
            
            <div className="relative group">
              <div className="absolute -inset-1 bg-gradient-to-r from-green-500 to-green-500 rounded-xl blur opacity-25 group-hover:opacity-50 transition duration-1000 group-hover:duration-200"></div>
              <img 
                src={displayImage}
                alt={field.fieldTrans?.[0]?.name || field.uid}
                className="relative w-full h-96 object-cover rounded-xl border border-slate-700 shadow-2xl mix-blend-luminosity hover:mix-blend-normal transition-all duration-500"
              />
            </div>
          </div>
        </div>
      </section>
      
      {/* Features & Benefits */}
      <section className="py-24 px-6 bg-slate-900/40 border-b border-slate-800/50">
        <div className="container mx-auto max-w-6xl">
          <div className="grid md:grid-cols-2 gap-12">
            <div className="bg-slate-950 border border-slate-800 rounded-xl p-8 hover:border-green-500/30 transition-colors">
              <div className="flex items-center gap-3 mb-6">
                <div className="w-10 h-10 bg-green-500/10 border border-green-500/30 rounded-lg flex items-center justify-center">
                  <CheckCircle className="w-6 h-6 text-green-400" />
                </div>
                <h2 className="text-2xl font-bold text-white">Tính năng chính</h2>
              </div>
              <ul className="space-y-4">
                {(field as any).features?.map((feature: string, index: number) => (
                  <li key={index} className="flex items-start gap-3">
                    <CheckCircle className="w-5 h-5 text-green-400 flex-shrink-0 mt-0.5" />
                    <span className="text-slate-300">{feature}</span>
                  </li>
                ))}
              </ul>
            </div>
            
            <div className="bg-slate-950 border border-slate-800 rounded-xl p-8 hover:border-green-500/30 transition-colors">
              <div className="flex items-center gap-3 mb-6">
                <div className="w-10 h-10 bg-green-500/10 border border-green-500/30 rounded-lg flex items-center justify-center">
                  <TrendingUp className="w-6 h-6 text-green-400" />
                </div>
                <h2 className="text-2xl font-bold text-white">Lợi ích</h2>
              </div>
              <ul className="space-y-4">
                {(field as any).benefits?.map((benefit: string, index: number) => (
                  <li key={index} className="flex items-start gap-3">
                    <CheckCircle className="w-5 h-5 text-green-400 flex-shrink-0 mt-0.5" />
                    <span className="text-slate-300">{benefit}</span>
                  </li>
                ))}
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
            Bạn quan tâm đến {field.fieldTrans?.[0]?.name || field.uid}?
          </h2>
          <p className="text-lg mb-8 text-slate-400">
            Liên hệ với chuyên gia an ninh mạng của chúng tôi để được tư vấn và trải nghiệm giải pháp
          </p>
          <Link 
            to="/contact"
            className="inline-block px-8 py-4 bg-green-500 text-slate-950 font-bold tracking-wide rounded hover:bg-green-400 transition-colors shadow-[0_0_15px_rgba(34,197,94,0.3)]"
          >
            Liên hệ ngay
          </Link>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
