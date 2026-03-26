import { useEffect, useState } from 'react';
import { api, BackendProject } from '../services/api';
import { ProductCard } from '../components/ProductCard';
import { Footer } from '../components/Footer';
import { Shield, Lock, Eye, Database, Key, Monitor } from 'lucide-react';
import React from 'react';
import { motion } from 'framer-motion';

const productIcons: Record<string, React.ReactNode> = {
  'dlp-network': <Shield className="w-6 h-6" />,
  'waf': <Lock className="w-6 h-6" />,
  'ias': <Eye className="w-6 h-6" />,
  'ict-pam': <Key className="w-6 h-6" />,
  'ict-siem': <Database className="w-6 h-6" />,
  'ict-edr': <Monitor className="w-6 h-6" />
};

export function Products() {
  const [products, setProducts] = useState<BackendProject[]>([]);

  useEffect(() => {
    async function fetchProducts() {
      try {
        const data = await api.getProjects();
        setProducts(data);
      } catch (err) {
        console.error("Failed to fetch products", err);
      }
    }
    fetchProducts();
  }, []);

  return (
    <div className="bg-slate-950 min-h-screen text-slate-300">
      <section className="relative pt-32 pb-20 px-6 overflow-hidden">
        {/* Abstract Background Elements */}
        <div className="absolute top-0 left-0 w-full h-full overflow-hidden z-0 pointer-events-none">
           <div className="absolute top-[-20%] right-[-10%] w-[50%] h-[50%] bg-green-500/10 blur-[150px] rounded-full"></div>
           <div className="absolute bottom-[20%] left-[-10%] w-[30%] h-[30%] bg-green-500/15 blur-[120px] rounded-full"></div>
        </div>

        <div className="container mx-auto max-w-6xl relative z-10">
          <motion.div 
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6 }}
            className="text-center mb-20"
          >
            <h1 className="text-4xl md:text-5xl font-extrabold text-white mb-6 tracking-tight">
              Sản phẩm của <span className="text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-green-400">chúng tôi</span>
            </h1>
            <p className="text-xl text-slate-400 max-w-2xl mx-auto font-light leading-relaxed">
              Khám phá các nền tảng bảo mật tiên tiến, được thiết kế với kiến trúc Zero Trust để bảo vệ doanh nghiệp của bạn khỏi mọi mối đe dọa.
            </p>
          </motion.div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
            {products.map((product, index) => (
              <ProductCard
                key={product.projectId}
                id={product.projectId.toString()}
                title={product.projectTrans?.[0]?.name || product.name || 'N/A'}
                description={product.projectTrans?.[0]?.shortDescription || product.description || ''}
                image={api.getImageUrl(product.image) || "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?auto=format&fit=crop&q=80"}
                icon={productIcons[String(product.projectId)] || <Shield className="w-6 h-6" />}
                index={index}
              />
            ))}
          </div>
        </div>
      </section>
      
      <Footer />
    </div>
  );
}
