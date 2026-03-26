import { Star } from 'lucide-react';

interface TestimonialCardProps {
  name: string;
  role: string;
  company: string;
  content: string;
  rating: number;
}

export function TestimonialCard({ name, role, company, content, rating }: TestimonialCardProps) {
  return (
    <div className="bg-slate-900 border border-slate-800 rounded-xl p-8 relative hover:border-green-500/50 transition-colors group">
      <div className="absolute top-0 right-8 -translate-y-1/2 w-12 h-12 bg-slate-950 border border-slate-800 rounded-full flex items-center justify-center shadow-lg group-hover:border-green-500/50 transition-colors">
        <span className="text-3xl font-serif text-green-500/50 leading-none">"</span>
      </div>
      
      <div className="flex gap-1 mb-6">
        {[...Array(rating)].map((_, i) => (
          <Star key={i} className="w-5 h-5 fill-green-400 text-green-400" />
        ))}
      </div>
      
      <p className="text-lg text-slate-300 mb-8 leading-relaxed font-light italic">
        {content}
      </p>
      
      <div className="border-t border-slate-800 pt-6">
        <div className="text-white font-bold tracking-wide">{name}</div>
        <div className="text-sm text-green-400 uppercase tracking-wider font-semibold mt-1">
          {role} <span className="text-slate-500 font-normal">| {company}</span>
        </div>
      </div>
    </div>
  );
}
