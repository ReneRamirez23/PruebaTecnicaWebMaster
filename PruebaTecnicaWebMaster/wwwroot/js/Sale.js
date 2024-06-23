let products = [];
let saleProducts = [];
let finalCost = 0;

    axios.get('/Sales/InfoData')
        .then(function (response) {
            let data = response.data;

            data.forEach(item => {
                let product = {
                    idProducts: item.idProducts,
                    nameProducts: item.nameProducts,
                    unitPrice: item.unitPrice,
                    quantity: item.quantity
                };

                products.push(product);
            })

            CreateTable(products);
        })
        .catch(function (error) {
            console.log("Something was wrong", error);
        });
}

function CreateTable(data) {
    const tbody = document.querySelector('#bodyProduct');
    tbody.innerHTML = '';
    var files = "";
    data.forEach(item => {
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

    if (action == "add") {
        if (existence) {

            existence.quantity += 1;
            existence.totalPrice = existence.quantity * existence.unitPrice;
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
    } else {
        if (existence) {
            existence.quantity -= 1;
            existence.totalPrice = existence.quantity * existence.unitPrice;
            
        }
    }

    AddPrdSale();
}

function Stock(id, action) {
    products.forEach((item, index) => {

        if (item.idProducts == id) {
            if (action == "add") {
                products[index].quantity -= 1;
            } else {
                products[index].quantity += 1;
            }
        }

    })

    CreateTable(products)

}

function AddPrdSale() {
    const salesBody = document.getElementById('saleBody');
    salesBody.innerHTML = '';
    var files = "";

    finalCost = 0;

    saleProducts.forEach(item => {
        if (item.quantity > 0) {
            var file =
                `<tr>
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

    let pruductList = [];
    saleProducts.forEach(item => {

        var infoProduct = { productsId: item.idProducts, quantity: item.quantity }
        pruductList.push(infoProduct);
    });

    const data = {
        totalPrice: finalCost,
        client: nombre,
        descripcion: descripcion,
        mailClient: mail,
        salesProducts: pruductList,
    };
    
    axios.post('/Sales/Create', data)
        .then(function (response) {
            console.log('sale successfully completed', response.data);
            alert('sale successfully completed');
            window.location.reload();
        })
        .catch(function (error) {
            console.error('Something was wrong', error);
            alert('Something was wrong');
        });
}

document.addEventListener('DOMContentLoaded', function () {
    getProducts();
});

});