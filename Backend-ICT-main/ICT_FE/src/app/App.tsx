import { BrowserRouter, Routes, Route, Navigate, useParams } from 'react-router';
import { Header } from './components/Header';
import { Home } from './pages/Home';
import { Fields } from './pages/Fields';
import { FieldDetail } from './pages/FieldDetail';
import { Products } from './pages/Products';
import { ProductDetail } from './pages/ProductDetail';
import { About } from './pages/About';
import { Contact } from './pages/Contact';

function ProductRedirect() {
  const { id } = useParams();
  return <Navigate to={`/field/${id}`} replace />;
}

export default function App() {
  return (
    <BrowserRouter>
      <div className="min-h-screen">
        <Header />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/fields" element={<Fields />} />
          <Route path="/field/:id" element={<FieldDetail />} />
          <Route path="/products" element={<Products />} />
          <Route path="/product/:id" element={<ProductDetail />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}