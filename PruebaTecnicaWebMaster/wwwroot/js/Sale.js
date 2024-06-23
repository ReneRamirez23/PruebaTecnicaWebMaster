let products = [];
let saleProducts = [];
let finalCost = 0;

function getProducts() {
    axios.get('/Sales/InfoData')
        .then(function (response) {
            let data = response.data;

            data.forEach(item => {
                if (item.active == true) {
                    let product = {
                        idProducts: item.idProducts,
                        nameProducts: item.nameProducts,
                        unitPrice: item.unitPrice,
                        quantity: item.quantity,
                        originalQuantity: item.quantity
                    };

                    products.push(product);
                }
            });

            CreateTable(products);
        })
        .catch(function (error) {
            console.log("Something went wrong", error);
        });
}

function CreateTable(data) {
    const tbody = document.querySelector('#bodyProduct');
    tbody.innerHTML = '';
    var files = "";
    data.forEach(item => {
        if (item.quantity > 0) {
            var row = `<tr>
                <td class="text-center">${item.nameProducts}</td>
                <td class="text-center">$${item.unitPrice}</td>
                <td class="text-center">${item.quantity}</td>
                <td class="text-center">
                    <div class="btn-group prdBtns" role="group">
                        <a class="btn btn-danger" onclick="ProductToSale(${item.idProducts},'${item.nameProducts}','${item.unitPrice}','cancel')">Cancel</a>
                        <a class="btn btn-success" onclick="ProductToSale(${item.idProducts},'${item.nameProducts}','${item.unitPrice}','add')">Add</a>
                    </div>
                </td>
            </tr>`;
            files += row;
        } else {
            var row = `<tr>
                <td class="text-center">${item.nameProducts}</td>
                <td class="text-center">$${item.unitPrice}</td>
                <td class="text-center">${item.quantity}</td>
                <td class="text-center">
                    <div class="btn-group prdBtns" role="group">
                        <a class="btn btn-danger" onclick="ProductToSale(${item.idProducts},'${item.nameProducts}','${item.unitPrice}','cancel')">Cancel</a>
                    </div>
                </td>
            </tr>`;
            files += row;
        }
    });

    tbody.innerHTML = files;
}

function ProductToSale(id, name, price, action) {
    const existence = saleProducts.find(x => x.idProducts === id);

    if (action === "add") {
        const product = products.find(p => p.idProducts === id);
        if (product && product.quantity > 0) {
            if (existence) {
                if (existence.quantity < product.originalQuantity) { // Validar no exceder la cantidad original
                    existence.quantity += 1;
                    existence.totalPrice = existence.quantity * existence.unitPrice;
                } else {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Stock limit reached',
                        text: `Cannot add more than ${product.originalQuantity} units of this product.`,
                        confirmButtonColor: '#3085d6',
                    });
                }
            } else {
                const newSale = {
                    idProducts: id,
                    nameProducts: name,
                    quantity: 1,
                    unitPrice: price,
                    totalPrice: price
                };
                saleProducts.push(newSale);
            }
            Stock(id, action);
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'Out of stock',
                text: 'This product is out of stock.',
                confirmButtonColor: '#3085d6',
            });
        }
    } else {
        if (existence && existence.quantity > 0) {
            existence.quantity -= 1;
            existence.totalPrice = existence.quantity * existence.unitPrice;
            if (existence.quantity === 0) {
                saleProducts = saleProducts.filter(x => x.idProducts !== id);
            }
            Stock(id, action);
        }
    }

    AddPrdSale();
}

function Stock(id, action) {
    products.forEach((item, index) => {
        if (item.idProducts === id) {
            if (action === "add") {
                if (products[index].quantity > 0) {
                    products[index].quantity -= 1;
                }
            } else {
                if (products[index].quantity < products[index].originalQuantity) {
                    products[index].quantity += 1;
                }
            }
        }
    });

    CreateTable(products);
}

function AddPrdSale() {
    const salesBody = document.getElementById('saleBody');
    salesBody.innerHTML = '';
    var files = "";

    finalCost = 0;

    saleProducts.forEach(item => {
        if (item.quantity > 0) {
            var file = `<tr>
                <td>${item.nameProducts}</td>
                <td>${item.quantity}</td>
                <td>$${item.totalPrice}</td>
            </tr>`;
            files += file;
        }
        finalCost += parseFloat(item.totalPrice);
    });
    salesBody.innerHTML = files;

    let cf = document.getElementById('finalCost');
    cf.value = finalCost;
}

function saveSale() {
    const finalCost = parseFloat(document.getElementById('finalCost').value);
    const nombre = document.getElementById('name').value;
    const descripcion = document.getElementById('descripcion').value;
    const mail = document.getElementById('mail').value;

    if (finalCost === '' || nombre === '' || descripcion === '' || mail === '') {
        if (finalCost === 0) {
            Swal.fire({
                icon: 'warning',
                title: 'Required Product',
                text: 'To make the sale you must add at least one product.',
                confirmButtonColor: '#3085d6',
            });
        } else {
            Swal.fire({
                icon: 'warning',
                title: 'Required fields',
                text: 'Please fill out all fields.',
                confirmButtonColor: '#3085d6',
            });
        }
    } else {
        let pruductList = [];
        saleProducts.forEach(item => {
            var infoProduct = { productsId: item.idProducts, quantity: item.quantity };
            pruductList.push(infoProduct);
        });

        const data = {
            totalPrice: finalCost,
            client: nombre,
            descripcion: descripcion,
            mailClient: mail,
            salesProducts: pruductList,
        };

        Swal.fire({
            title: "You're sure?",
            text: "Do you want to continue with the sale?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#18CA07',
            cancelButtonColor: '#CC241E',
            confirmButtonText: 'Yes continue',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                axios.post('/Sales/Create', data)
                    .then(function (response) {
                        Swal.fire(
                            'Sale completed',
                            'The sale has been completed successfully.',
                            'success'
                        ).then(() => {
                            window.location.reload();
                        });
                    })
                    .catch(function (error) {
                        Swal.fire(
                            'Error!',
                            'There was a problem completing the sale.',
                            'error'
                        );
                        console.error('Algo salió mal', error);
                    });
            }
        });
    }
}

document.addEventListener('DOMContentLoaded', function () {
    getProducts();
});