import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ProductService } from '../../services/product.service';
import { IProduct } from '../../models/product.interface';

import { FormGroup, FormControl, FormBuilder, Validators } from "@angular/forms";
import { ReactiveFormsModule } from "@angular/forms";


@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})

export class SearchComponent implements OnInit {

    public searchId: number;

    public widthImage: number;
    public heightImage: number;

    public product: IProduct = <IProduct>{};
    formSearch: FormGroup;
    formProduct: FormGroup;

    ngOnInit(): void { }

    constructor(private productService: ProductService,
        @Inject('BASE_URL') baseUrl: string,
        private formBuilder: FormBuilder) {
        this.formSearch = formBuilder.group({
            "searchId": ["", Validators.required],
        });

        this.formProduct = formBuilder.group({
            "id": [""],
            "name": [""],
            "description": [""],
            "price": [""],
        });

        this.widthImage = 0;
        this.heightImage = 0;
    }

    onSubmit() {
        this.searchId = this.formSearch.controls['searchId'].value;
        this.productService.getProduct(this.searchId).subscribe(
            data => this.product = data,
            error => this.errorSearch(),
            () => this.loadProduct(this.product)
        );
    }

    loadProduct(product: IProduct) {
        this.formProduct.controls['id'].setValue(this.product.id);
        this.formProduct.controls['name'].setValue(this.product.name);
        this.formProduct.controls['description'].setValue(this.product.description);
        this.formProduct.controls['price'].setValue(this.product.price);

        this.widthImage = 400;
        this.heightImage = 300;
    }

    errorSearch() {
        this.product = <IProduct>{};
        this.formProduct.reset();
        this.formSearch.reset();

        this.widthImage = 0;
        this.heightImage = 0;

        alert("PRODUTO NÃO LOCALIZADO !");
    }








}