import { Component, OnInit } from '@angular/core';
import { NgxPaginationModule } from 'ngx-pagination';
import { ProductService } from '../services/product.service'; // Suponiendo que tengas un servicio para manejar los productos
import { ProductModalComponent } from '../product-modal/product-modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, NgxPaginationModule, ProductModalComponent],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  pagedProducts: Product[] = [];
  itemsPerPage = 10;
  currentPage = 1;
  title: string = 'Listado de Productos';
  productUpdate?: Product = undefined; 
  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  // Cargar los productos del servicio
  loadProducts(): void {
    this.productService.getProducts().subscribe((data: Product[]) => {
      this.products = data;
      this.onPageChange(1); // Inicializar la primera página
    });
  }

  // Manejar el cambio de página
  onPageChange(page: number): void {
    this.currentPage = page;
    const startIndex = (page - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.pagedProducts = this.products.slice(startIndex, endIndex);
  }

  editProduct(product: Product): void {
    // Lógica para editar el producto
    this.productUpdate = product;
  }

  deleteProduct(id: string): void {
    if (confirm('¿Estás seguro de que quieres eliminar este producto?')) {
      this.productService.deleteProducts(id).subscribe((data: any) => {
        this.products = data;
        this.loadProducts();
      });
    }
  }

  createProduct(){
    this.productUpdate = undefined;
  }

  createProductEvent(e: boolean){
    this.loadProducts();
  }
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
