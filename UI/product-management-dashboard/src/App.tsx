import { useEffect, useState } from 'react';
import './App.css';
import { DataTable } from './components/DataTable';
import { IProduct } from './models/interfaces/product';
import { fetchProducts } from './services/api';
import { StockQuantityPieChart } from './components/StockQuantityPieChart';
import { ProductsAddedBarChart } from './components/ProductsAddedBarChart';

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
      <div className="product-data-table">
        <DataTable products={products} />
      </div>
      <div className="product-graphs">
        <StockQuantityPieChart products={products} />
        <ProductsAddedBarChart products={products} />
      </div>
    </div>
  );
}

export default App;
