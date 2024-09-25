import React from "react";
import { IProduct } from "../models/interfaces/product";
import { Pie } from "react-chartjs-2";
import { ArcElement, Chart, ChartData, ChartDataset, Colors, Legend, Tooltip } from "chart.js";
import { IStockQuantity } from "../models/interfaces/stockQuantity";
import { getProductCategories } from "../services/productService";
import { generateColorScheme } from "../services/chartService";

Chart.register(ArcElement, Tooltip, Legend, Colors);

interface StockQuantityPieChartProps {
    products: IProduct[]
}

export const StockQuantityPieChart: React.FC<StockQuantityPieChartProps> = ({products}) => {
    const data: ChartData<'pie'> = createSeriesDisplay(products);

    return (
        <div className="stock-quantity-pie-chart-section">
            <h3>Your Product Stock Quantity by Category</h3>
            {data && <Pie
                id='stock-quantity'
                key='stock-quantity'
                data={data}
            />}
            {!data && <p>Data Loading...</p>}
        </div>
       )
}

function createSeriesDisplay(products: IProduct[]): ChartData<'pie'> {
    const categories = getProductCategories(products);
    const data: ChartData<'pie'> = {
        labels: categories,
        datasets: [
            {
                label: 'Stock Quantity',
                data: getTotalStockPerCategory(products).map(s => s.stockQuantity),
                borderWidth: 1,
                backgroundColor: generateColorScheme(categories.length)
            }
        ] as ChartDataset<'pie', number[]>[]
    };

    return data;
}

function getTotalStockPerCategory(products: IProduct[]): IStockQuantity[] {
    const categories = getProductCategories(products);
    const stockQuantities: IStockQuantity[] = [];

    categories.forEach(c => {
        stockQuantities.push({
            category: c,
            stockQuantity: 0
        })
    });

    products.forEach(p => {
        const stockQuantity = stockQuantities.filter(s => s.category === p.category)[0];
        stockQuantity.stockQuantity += p.stockQuantity
    })
    
    return stockQuantities;
}