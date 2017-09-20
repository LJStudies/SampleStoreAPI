import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../models/product.interface';

import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
import { ReactiveFormsModule } from "@angular/forms";

@Component({
    selector: 'product',
    templateUrl: './product.component.html'
})

export class ProductComponent implements OnInit {

    public products: IProduct[] = [];
    public product: IProduct = <IProduct>{};

    formLabel: string;
    isEditMode: boolean;
    form: FormGroup;

    constructor(private productService: ProductService,
        @Inject('BASE_URL') baseUrl: string,
        private formBuilder: FormBuilder) {
        this.form = formBuilder.group({
            "id": [""],
            "name": ["", Validators.required],
            "description": [""],
            "price": ["", Validators.required],
            "imageUrl": ["", Validators.required]
        });

        this.setEditMode(false);
    }

    ngOnInit() {
        this.getProducts();
    }

    private getProducts() {
        this.productService.getProducts().subscribe(
            data => this.products = data,
            error => alert(error),
            () => console.log(this.products)
        );
    }

    deleteProduct(product: IProduct) {
        if (confirm("Are you sure delete this product?")) {
            this.productService.deleteProduct(product.id)
                .subscribe(
                response => {
                    this.getProducts();
                    this.form.reset();
                });
        }
    }

    editProduct(product: IProduct) {
        this.setEditMode(true);
        this.product = product;

        //Carrega valores para o formulário
        this.form.controls['id'].setValue(this.product.id);
        this.form.controls['name'].setValue(this.product.name);
        this.form.controls['description'].setValue(this.product.description);
        this.form.controls['price'].setValue(this.product.price);
        this.form.controls['imageUrl'].setValue(this.product.imageUrl);
    }

    onSubmit() {
        this.product.name = this.form.controls["name"].value;
        this.product.description = this.form.controls["description"].value;
        this.product.price = this.form.controls["price"].value;
        this.product.imageUrl = this.form.controls["imageUrl"].value;

        if (this.isEditMode) {
            this.product.id = this.form.controls["id"].value;
            this.productService.editProduct(this.product)
                .subscribe(response => {
                    this.getProducts();
                });
        } else {
            this.product.id = 0;
            this.productService.addProduct(this.product)
                .subscribe(response => {
                    this.getProducts();
                });
        }
        this.setEditMode(false);
        this.form.reset();
    }

    cancel() {
        this.form.reset();
        this.setEditMode(false);
        this.product = <IProduct>{};

    }

    setEditMode(isEditMode: boolean) {
        if (isEditMode) {
            this.formLabel = "Atualizar";
            this.isEditMode = true;
        } else {
            this.formLabel = "Novo";
            this.isEditMode = false;
        }
    }

}