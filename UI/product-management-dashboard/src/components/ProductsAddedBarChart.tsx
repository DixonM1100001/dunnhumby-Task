import { BarElement, CategoryScale, Chart, ChartData, ChartDataset, Legend, LinearScale, Tooltip } from "chart.js";
import { IProduct } from "../models/interfaces/product";
import { useEffect, useState } from "react";
import { Bar } from "react-chartjs-2";
import { getProductCategories } from "../services/productService";
import { generateColorScheme } from "../services/chartService";
import { TimePeriod } from "../models/interfaces/timePeriodFilter";
import { IStockQuantity } from "../models/interfaces/stockQuantity";
import { getFilterTimeFrames } from "../services/dateService";
import { compareAsc } from "date-fns";
import React from "react";
import './ProductsAddedBarChart.css';

Chart.register(BarElement, Tooltip, Legend, LinearScale, CategoryScale);

interface ProductsAddedBarChartProps {
    products: IProduct[]
}

export const ProductsAddedBarChart: React.FC<ProductsAddedBarChartProps> = ({products}) => {
    const [timeFrame, setTimeFrame] = useState<TimePeriod>(TimePeriod.AllTime);
    const [data, setData] = useState<ChartData<'bar'>>(getDateInTimeFrame(products, timeFrame)); 


    useEffect(() => {
        setData(getDateInTimeFrame(products, timeFrame))
    }, [timeFrame, products])

    const onTimeFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setTimeFrame(parseInt(e.target.value) as TimePeriod)
    }

    return (
        <div className="stock-quantity-added-bar-chart-section">
            <h3>Products added per category in selected time frame</h3>
            {data && <div className="stock-quantity-added-bar-chart">
                <Bar
                    id='products-added'
                    key={`products-added_${Math.random().toString()}`}
                    data={data}
                />
                <div>
                    <h4>Select a time frame</h4>
                    {getFilterTimeFrames().map(f => (
                        <div className="stock-quantity-added-bar-chart-radio-buttons">
                            <input key={f.description} 
                                    type="radio"
                                    name="timeFrame"
                                    value={f.timePeriod}
                                    id={f.timePeriod.toString()}
                                    onChange={e => onTimeFilterChange(e)} 
                                    checked={f.timePeriod === timeFrame}/>
                            <label htmlFor={f.timePeriod.toString()}>{f.description}</label>
                        </div>
                    ))}
                </div>
            </div>}
            {!data && <p>Data Loading...</p>}
        </div>
       )
}

function getDateInTimeFrame(products: IProduct[], timeFrame: TimePeriod): ChartData<"bar"> {
    const categories = getProductCategories(products)
    const data = {
        labels: categories,
        datasets: [
            {
                label: 'Number of products added in selected timeframe',
                data: getProductsAddedInTimeFrame(products, timeFrame).map(s => s.stockQuantity),
                borderWidth: 1,
                backgroundColor: generateColorScheme(categories.length),
            }
        ] as ChartDataset<'bar', number[]>[]
    }

    return data;
}

function getProductsAddedInTimeFrame(products: IProduct[], timeFrame: TimePeriod): IStockQuantity[] {

    const categories = getProductCategories(products);
    const timeFrameFilters = getFilterTimeFrames();
    const timeFrameFilterToUse = timeFrameFilters.filter(t => t.timePeriod === timeFrame)[0];

    const stockQuantities: IStockQuantity[] = [];

    categories.forEach(c => {
        stockQuantities.push({
            category: c,
            stockQuantity: 0
        })
    });

    products.forEach(p => {
        const stockQuantity = stockQuantities.filter(s => s.category === p.category)[0];
        if (compareAsc(p.dateAdded, timeFrameFilterToUse.minimumDate) !== -1) {
            stockQuantity.stockQuantity += p.stockQuantity
        }
    })

    return stockQuantities;
}

