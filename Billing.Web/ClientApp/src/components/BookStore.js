import React, { Component } from 'react';

export class BookStore extends Component {
    displayName = BookStore.name

    constructor(props) {
        super(props);
        this.state = { books: [], loading: true, total: 0, discount: 0 };
        this.basket = [];

        fetch('api/billing/books')
            .then(response => response.json())
            .then(data => {
                this.setState({ books: data, loading: false });
            });
    }

    renderBooksTable(books, total, discount) {
        return (
            <div>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Price</th>
                            <th />
                        </tr>
                    </thead>
                    <tbody>
                        {books.map(book =>
                            <tr key={book.sku}>
                                <td>{book.name}</td>
                                <td>&pound;{book.price.toFixed(2)}</td>
                                <td><button onClick={() => this.addBookToBasket(book.sku)}>Add</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <span>Basket total: &pound;{total.toFixed(2)}</span>&nbsp;&nbsp;
                {discount > 0 && <span style={{ color: "red" }}>You have saved &pound;{discount.toFixed(2)}!</span>}
                <br />
                <button onClick={() => this.clearBasket()}>Clear basket ({this.basket.length})</button>
            </div>
        );
    }

    addBookToBasket(sku) {
        console.log(sku);
        this.basket.push(sku);

        let url = 'api/billing/basket?';
        this.basket.forEach(function (s) {
            url = url.concat('skus=' + s + '&');
        });
        fetch(url)
            .then(response => response.json())
            .then(data => {
                this.setState({ total: data.total, discount: data.totalDiscount });
            });
    }

    clearBasket() {
        this.basket = [];
        this.setState({ total: 0, discount: 0 });
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderBooksTable(this.state.books, this.state.total, this.state.discount);

        return (
            <div>
                <h1>Peach Bookstore</h1>
                <p>Add books to your basket: any discounts are automatically applied.</p>
                {contents}
            </div>
        );
    }
}
