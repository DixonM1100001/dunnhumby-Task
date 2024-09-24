import { useEffect, useState } from 'react';
import './App.css';
import { DataTable } from './components/DataTable';
import { IProduct } from './models/product';
import { fetchProducts } from './services/api';

function App() {
  const [products, setProducts] = useState<IProduct[]>([]);

  useEffect(() => {
    fetchProducts().then(products => setProducts(products));
  }, [])

  return (
    <div className="app">
      <header className="app-header">
        <h1>Welcome to the Product Management Dashboard!</h1>
      </header>
      <h2>Your Products</h2>
      <div className="product-data-table">
        <DataTable products={products} />
      </div>
    </div>
  );
}

export default App;
