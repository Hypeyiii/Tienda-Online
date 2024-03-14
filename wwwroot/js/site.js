const products = [
    {
        id: 1,
        name: "HAVIT HV-G92 Gamepad",
        price: 160,
        disscountPercentage: 40,
    },
    {
        id: 2,
        name: "Ak-900 Wired Keyboard",
        price: 1160,
        disscountPercentage: 35,
    },
    {
        id: 3,
        name: "IPS LCD Gaming Monitor",
        price: 400,
        disscountPercentage: 30,
    },
    {
        id: 4,
        name: "S-Series Comfort Chair",
        price: 400,
        disscountPercentage:25,
    }
]
const category = [
    {
        id: "Phones"
    }, 
    {
        id: "Computers"
    },
    {
        id: "SmartWatch"
    },
    {
        id: "Camera"
    },
    {
        id: "Headphones"
    },
    {
        id: "Gaming"
    },
]
function getProducts() {
    document.getElementById("section-flashSales").innerHTML =
        products.map(product => {
            let priceWithDisscount = Math.floor(product.price - (product.price * product.disscountPercentage / 100));
            return `<div class="product">
                <div class="image-product-container" onmouseenter="showAddToCart(this)" onmouseleave="hideAddToCart(this)">
                    <img class:"product-image" src="./img/product-${product.id}.png" title="Producto de nuestra tienda ${product.name}" alt="Imagen ${product.name}">
                    <div class="disscount-container">
                        <p class="disscount-text">-${product.disscountPercentage}%</p>
                    </div>
                    <div class="add-to-cart-container" onclick="addToCart(${product.id})">
                        Add to cart
                    </div>
                </div>
                <div class="info">
                    <h3 class="product-name">${product.name}</h3>
                    <h6 class="product-price">$${priceWithDisscount}.00 MX   <span class="disscount-span">$${product.price}.00 MX</span></h6>
                </div>
            </div>`;
        }).join("");
}

function showAddToCart(element) {
    element.querySelector(".add-to-cart-container").style.display = "flex";;
}
function hideAddToCart(element) {
    element.querySelector(".add-to-cart-container").style.display = "none";
}
const allProducts = [];
const countProducts = 0;
const total = 0;
function addToCart(productId) {
    // Buscar el producto por su ID
    const product = products.find(item => item.id === productId);
    document.querySelector(".add-to-cart-modal").style.display = "flex";
    document.querySelector(".add-to-cart-modal").classList.add("slide-in-left");
    document.querySelector(".add-to-cart-modal").classList.remove("slide-out-left");
    setTimeout(function () {
        
        document.querySelector(".add-to-cart-modal").classList.add("slide-out-left");
        document.querySelector(".add-to-cart-modal").classList.remove("slide-in-left");
    }, 2000)
    console.log(product)
}
console.log(getProducts())

function broseByCategory() {
    document.getElementById("section-browseByCategory").innerHTML =
    category.map(category => {
        return `
            <div class="category">
                <img class="category-image" src="./img/category-${category.id}.svg" title="Categoria ${category.id}" alt="Imagen ${category.id}">
                <h3 class="category-name">${category.id}</h3>
            </div>`;
        }).join("");
}
console.log(broseByCategory());