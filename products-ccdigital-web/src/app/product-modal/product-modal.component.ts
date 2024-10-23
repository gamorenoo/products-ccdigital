import { Component, EventEmitter, Input, OnChanges, OnInit, Output, output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-modal.component.html',
  styleUrls: ['./product-modal.component.css']
})

export class ProductModalComponent implements OnInit, OnChanges{
  productForm: FormGroup;
  @Output() createProductEvent = new EventEmitter<boolean>();
  @Input() product: any;
  constructor(private fb: FormBuilder, private productService: ProductService) {
    this.productForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      code: ['', Validators.required],
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      stock: ['', [Validators.required, Validators.min(1)]],
    });
  }

  ngOnInit(): void {
    this.setProductUpdate();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['product'] && changes['product'].currentValue) {
      setTimeout(() => {
        this.setProductUpdate();
      }, 500);
    }
  }

  setProductUpdate(){
    console.log(this.product);
    if(this.product != undefined && this.product != null) {
      this.f['id'].setValue(this.product.id);
      this.f['code'].setValue(this.product.code);
      this.f['name'].setValue(this.product.name);
      this.f['description'].setValue(this.product.description);
      this.f['price'].setValue(this.product.price);
      this.f['stock'].setValue(this.product.stock);
    }
  }

  get f() {
    return this.productForm.controls;
  }

  generateGUID(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      const r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      console.log(this.productForm.value);
      if(this.product == undefined || this.product == null) {
        this.createProduct();
      } else {
        this.updateProduct();
      }
    }
  }

  createProduct(){
    this.productService.createProducts(this.productForm.value).subscribe((data: any) => {
      this.productForm.reset();
      this.createProductEvent.emit(true);
    });
  }

  updateProduct(){
    let product: any = {
      id : this.product.id,
      code: this.f['code'].value,
      name: this.f['name'].value,
      description: this.f['description'].value,
      price: this.f['price'].value,
      stock: this.f['stock'].value
    }

    this.productService.updateProducts(product).subscribe((data: any) => {
      this.productForm.reset();
      this.createProductEvent.emit(true);
    });
  }
}
