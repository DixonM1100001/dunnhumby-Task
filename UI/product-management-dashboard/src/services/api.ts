import { IProduct } from "../models/interfaces/product";

const API_BASE_URL: string = 'http://localhost:7030/api';

export async function fetchProducts(): Promise<IProduct[]> {
    const productPath = GetProductApiPath();
    const response = await fetch(productPath);
    const data = await response.json();
    return data;
}

function GetProductApiPath(): string {
    return API_BASE_URL + '/product';
}
