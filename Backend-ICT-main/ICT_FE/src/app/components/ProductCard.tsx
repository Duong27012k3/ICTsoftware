import { Link } from 'react-router';
import { ArrowRight } from 'lucide-react';
import { motion } from 'framer-motion';

interface ProductCardProps {
  id: string;
  title: string;
  description: string;
  image: string;
  icon: React.ReactNode;
  index?: number;
}

export function ProductCard({ id, title, description, image, icon, index = 0 }: ProductCardProps) {
  // If the backend image isn't available, we use a sleek tech placeholder based on the product ID or generic
  const defaultImage = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?auto=format&fit=crop&q=80";
  const displayImage = image.includes("localhost") || image === '' ? defaultImage : image;

  return (
    <motion.div
      initial={{ opacity: 0, y: 30 }}
      whileInView={{ opacity: 1, y: 0 }}
      viewport={{ once: true, margin: "-50px" }}
      transition={{ duration: 0.6, delay: index * 0.1, ease: "easeOut" }}
      whileHover={{ y: -8 }}
      className="h-full"
    >
      <Link 
      to={`/product/${id}`}
      className="group block bg-slate-900/40 backdrop-blur-sm border border-slate-800 rounded-xl overflow-hidden hover:shadow-[0_0_30px_rgba(34,197,94,0.15)] hover:border-green-500/50 transition-all duration-500"
    >
      <div className="relative h-56 overflow-hidden bg-slate-950">
        <div className="absolute inset-0 bg-gradient-to-t from-slate-900 via-transparent to-transparent z-10"></div>
        <img 
          src={displayImage} 
          alt={title}
          className="w-full h-full object-cover group-hover:scale-110 group-hover:rotate-1 transition-transform duration-700 opacity-80 group-hover:opacity-100 mix-blend-luminosity hover:mix-blend-normal"
        />
        
        <div className="absolute bottom-4 left-6 z-20 w-12 h-12 bg-slate-900/80 backdrop-blur-md border border-green-500/30 rounded-lg flex items-center justify-center text-green-400 group-hover:bg-green-500/20 group-hover:border-green-400 transition-colors duration-300">
          {icon}
        </div>
      </div>
      
      <div className="p-6">
        <h3 className="text-xl font-bold text-white mb-3 group-hover:text-green-300 transition-colors">{title}</h3>
        
        <p className="text-sm text-slate-400 mb-6 leading-relaxed line-clamp-3">
          {description}
        </p>
        
        <div className="flex items-center gap-2 text-green-400 tracking-wide text-sm font-semibold group-hover:gap-4 transition-all uppercase">
          Xem chi tiết
          <ArrowRight className="w-4 h-4" />
        </div>
      </div>
    </Link>
    </motion.div>
  );
}
