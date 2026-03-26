import { Link, useLocation } from 'react-router';
import { Menu, X } from 'lucide-react';
import { useState } from 'react';

export function Header() {
  const location = useLocation();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  
  const isActive = (path: string) => {
    return location.pathname === path;
  };

  const navLinks = [
    { path: '/', label: 'Trang chủ' },
    { path: '/fields', label: 'Lĩnh vực' },
    { path: '/products', label: 'Sản phẩm' },
    { path: '/about', label: 'Về chúng tôi' },
    { path: '/contact', label: 'Liên hệ' },
  ];

  return (
    <header className="fixed top-0 left-0 right-0 z-50 bg-slate-950/80 backdrop-blur-md border-b border-green-900/50">
      <div className="container mx-auto px-6">
        <div className="flex items-center justify-between h-20">
          <Link to="/" className="flex items-center gap-3 group">
            <div className="w-10 h-10 bg-green-500/10 border border-green-500/30 rounded-lg flex items-center justify-center group-hover:bg-green-500/20 transition-colors">
              <span className="text-xl font-bold text-green-400">IS</span>
            </div>
            <span className="text-2xl font-bold text-white tracking-wider">ICT <span className="text-green-400">Software</span></span>
          </Link>
          
          {/* Desktop Navigation */}
          <nav className="hidden md:flex items-center gap-8">
            {navLinks.map((link) => (
              <Link 
                key={link.path}
                to={link.path}
                className={`text-sm tracking-wide font-medium transition-colors ${
                  isActive(link.path) 
                    ? 'text-green-400' 
                    : 'text-slate-300 hover:text-white'
                }`}
              >
                {link.label}
              </Link>
            ))}
            <Link 
              to="/contact"
              className="px-6 py-2.5 bg-green-600/10 border border-green-500/50 text-green-400 text-sm font-semibold rounded-md hover:bg-green-500 hover:text-slate-950 transition-all duration-300 shadow-[0_0_15px_rgba(34,197,94,0.15)] hover:shadow-[0_0_20px_rgba(34,197,94,0.4)]"
            >
              Get Started
            </Link>
          </nav>

          {/* Mobile Menu Button */}
          <button 
            className="md:hidden"
            onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
          >
            {mobileMenuOpen ? (
              <X className="w-6 h-6 text-green-400" />
            ) : (
              <Menu className="w-6 h-6 text-green-400" />
            )}
          </button>
        </div>

        {/* Mobile Navigation */}
        {mobileMenuOpen && (
          <nav className="md:hidden py-4 border-t border-green-900/50 bg-slate-900 rounded-b-lg">
            {navLinks.map((link) => (
              <Link 
                key={link.path}
                to={link.path}
                className={`block px-4 py-3 text-sm ${
                  isActive(link.path) 
                    ? 'text-green-400 bg-slate-800' 
                    : 'text-slate-300 hover:bg-slate-800'
                }`}
                onClick={() => setMobileMenuOpen(false)}
              >
                {link.label}
              </Link>
            ))}
          </nav>
        )}
      </div>
    </header>
  );
}