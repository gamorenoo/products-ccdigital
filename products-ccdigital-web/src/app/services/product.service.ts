import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  urlBase = 'https://localhost:7156/api';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    let Url = `${this.urlBase}/product`
    return this.http.get<Product[]>(Url);
  }

  createProducts( product: Product): Observable<Product> {
    let Url = `${this.urlBase}/product`
    return this.http.post<Product>(Url, product);
  }
  
  updateProducts( product: Product): Observable<Product> {
    let Url = `${this.urlBase}/product`
    return this.http.put<Product>(Url, product);
  }
  
  deleteProducts( productId: string): Observable<Product> {
    let Url = `${this.urlBase}/product/${productId}`
    return this.http.delete<Product>(Url);
  }

  // Agrega métodos para crear y actualizar productos si es necesario
}

// Interfaz para el producto
interface Product {
  id: string;
  code: string;
  name: string;
  description: string;
  price: number;
  stock: number;
}
