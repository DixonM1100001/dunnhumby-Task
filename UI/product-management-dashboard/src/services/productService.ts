import { IProduct } from "../models/interfaces/product";

export function getProductCategories(products: IProduct[]): string[] {
    const categories: string[] = [];
    products.forEach(p => {
        if (!categories.includes(p.category)) {
            categories.push(p.category)
        }
    })

    return categories;
}
