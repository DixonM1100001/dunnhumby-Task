import { IProduct } from "../models/product";
import { AgGridReact } from 'ag-grid-react';
import "ag-grid-community/styles/ag-grid.css";
import "ag-grid-community/styles/ag-theme-quartz.css"
import { IProductDisplay } from "../models/productDisplay";
import './DataTable.css';
import React, { useMemo } from "react";
import { format } from "date-fns/format";
import { DATETIME_STRING_FORMAT } from '../constants/constants';

interface IDataTableProps {
    products: IProduct[]
}

const colDefs: any[] = [
    {headerName: 'Category', field: 'category'},
    {headerName: 'Name', field: 'name'},
    {headerName: 'Product Code', field: 'productCode'},
    {headerName: 'Price', field: 'priceSterling'},
    {headerName: 'SKU', field: 'sku'},
    {headerName: 'Stock Quantity', field: 'stockQuantity'},
    {headerName: 'Date Added', field: 'dateAdded', width: 250},
];

const autoSizeStrategy = {
    type: 'fitGridWidth',
    defaultMinWidth: 80
} as any;

export const DataTable: React.FC<IDataTableProps> = ({products}) => {
    const defaultColDef = useMemo(() => { 
        return {
            resizable: true,
            cellStyle: {'text-align': 'left'}
        };
    }, []);

    return (
        <React.Fragment>
            <h3>Your Product Data</h3>
            <div className="data-table ag-theme-quartz">
            <AgGridReact
                defaultColDef={defaultColDef}
                columnDefs={colDefs}
                rowData={GetProductDisplayProperties(products)}
                pagination
                paginationAutoPageSize
                autoSizeStrategy={autoSizeStrategy}
            />
            </div>
        </React.Fragment>
       )
}

function GetProductDisplayProperties(products: IProduct[]) : IProductDisplay[] {
    const productDisplayProperties = [] as IProductDisplay[];
    
    products.forEach(p => productDisplayProperties.push({
        category: p.category,
        name: p.name,
        productCode: p.productCode,
        priceSterling: p.priceSterling,
        sku: p.sku,
        stockQuantity: p.stockQuantity,
        dateAdded: format(p.dateAdded, DATETIME_STRING_FORMAT),
    }));

    return productDisplayProperties;
}