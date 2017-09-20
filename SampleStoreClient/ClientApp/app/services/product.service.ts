import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';
import { IProduct } from '../models/product.interface';

@Injectable()
export class ProductService {

    constructor(private http: Http) { }

    //Get
    getProducts() {
        return this.http.get("http://ljasmimsamplestore.azurewebsites.net/api/products")
            .map(data => <IProduct[]>data.json());
    }

    //GetById
    getProduct(productId: number) {
        return this.http.get(`http://ljasmimsamplestore.azurewebsites.net/api/products/${productId}`)
            .map(data => <IProduct>data.json());
    }

    //Post
    addProduct(product: IProduct) {
        return this.http.post("http://ljasmimsamplestore.azurewebsites.net/api/products", product);
    }

    //Delete
    deleteProduct(productId: number) {
        return this.http.delete(`http://ljasmimsamplestore.azurewebsites.net/api/products/${productId}`);
    }

    //Put
    editProduct(product: IProduct) {
        return this.http.put(`http://ljasmimsamplestore.azurewebsites.net/api/products/${product.id}`, product);
    }
}
