import { useEffect, useState } from 'react';
import { api, BackendField } from '../services/api';
import { FieldCard } from '../components/FieldCard';
import { Footer } from '../components/Footer';
import { Shield, Eye, Globe, Key, Activity, Monitor, Brain } from 'lucide-react';
import React from 'react';
import { motion } from 'framer-motion';

const fieldIcons: Record<string, React.ReactNode> = {
  'bao-mat-du-lieu': <Shield className="w-6 h-6" />,
  'danh-gia-an-ninh': <Eye className="w-6 h-6" />,
  'bao-mat-ung-dung': <Globe className="w-6 h-6" />,
  'quan-ly-truy-cap': <Key className="w-6 h-6" />,
  'giam-sat-su-co': <Activity className="w-6 h-6" />,
  'bao-mat-endpoint': <Monitor className="w-6 h-6" />
};

export function Fields() {
  const [fields, setFields] = useState<BackendField[]>([]);

  useEffect(() => {
    async function fetchFields() {
      try {
        const data = await api.getFields();
        setFields(data);
      } catch (err) {
        console.error("Failed to fetch fields", err);
      }
    }
    fetchFields();
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
              Hệ sinh thái <span className="text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-green-400">Giải pháp</span>
            </h1>
            <p className="text-xl text-slate-400 max-w-2xl mx-auto font-light leading-relaxed">
              Các lĩnh vực An ninh mạng chiến lược, được tích hợp sâu trí tuệ nhân tạo và mật mã học tiên tiến.
            </p>
          </motion.div>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
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
      
      <Footer />
    </div>
  );
}
