const products = [
  {
    id: 1,
    name: "HAVIT HV-G92 Gamepad",
    price: 160,
    disscountPercentage: 40,
    category: "flash-sales",
    quantity: 1,
  },
  {
    id: 2,
    name: "Ak-900 Wired Keyboard",
    price: 1160,
    disscountPercentage: 35,
    category: "flash-sales",
    quantity: 1,
  },
  {
    id: 3,
    name: "IPS LCD Gaming Monitor",
    price: 400,
    disscountPercentage: 30,
    category: "flash-sales",
    quantity: 1,
  },
  {
    id: 4,
    name: "S-Series Comfort Chair",
    price: 400,
    disscountPercentage: 25,
    category: "flash-sales",
    quantity: 1,
  },
  {
    id: 20,
    name: "The north coat",
    price: 360,
    disscountPercentage: 10,
    category: "best-sales",
    quantity: 1,
  },
  {
    id: 21,
    name: "Gucci Duffle bag",
    price: 1160,
    disscountPercentage: 10,
    category: "best-sales",
    quantity: 1,
  },
  {
    id: 22,
    name: "RBG liquid CPU Cooler",
    price: 170,
    disscountPercentage: 5,
    category: "best-sales",
    quantity: 1,
  },
  {
    id: 23,
    name: "Small bookself",
    price: 360,
    disscountPercentage: 5,
    category: "best-sales",
    quantity: 1,
  },
  {
    id: 41,
    name: "Bree Dry Dog Food",
    price: 100,
    disscountPercentage: 0,
    category: "our-products",
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 42,
    name: "CANON EOS DSLR Camera",
    price: 360,
    disscountPercentage: 0,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 43,
    name: "ASUS FHD Gaming Laptop",
    price: 700,
    disscountPercentage: 0,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 44,
    name: "S-Series Comfort Chair",
    price: 400,
    disscountPercentage: 25,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 45,
    name: "The north coat",
    price: 360,
    disscountPercentage: 10,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 46,
    name: "Gucci Duffle bag",
    price: 1160,
    disscountPercentage: 10,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 47,
    name: "RBG liquid CPU Cooler",
    price: 170,
    disscountPercentage: 5,
    category: "our-sales",
    quantity: 1,
  },
  {
    id: 48,
    name: "Small bookself",
    price: 360,
    disscountPercentage: 5,
    category: "our-sales",
    quantity: 1,
  },
];
const category = [
  {
    id: "Phones",
  },
  {
    id: "Computers",
  },
  {
    id: "SmartWatch",
  },
  {
    id: "Camera",
  },
  {
    id: "Headphones",
  },
  {
    id: "Gaming",
  },
];
function showFlashSales() {
  const flashSales = products.filter(
    (product) => product.category === "flash-sales"
    );
    document.getElementById("section-flashSales").innerHTML = flashSales
    .map((product) => {
      let priceWithDiscount = Math.floor(
        product.price - (product.price * product.disscountPercentage) / 100
      );
      return `<div class="product">
            <div class="image-product-container" onmouseenter="showAddToCart(this)" onmouseleave="hideAddToCart(this)">
                <a href="/Home/ProductDetails"><img class="product-image" src="../img/product-${product.id}.png" title="Producto de nuestra tienda ${product.name}" alt="Imagen ${product.name}"></a>
                <div class="disscount-container">
                    <p class="disscount-text">-${product.disscountPercentage}%</p>
                </div>
                <div class="add-to-cart-container" onclick="addToCart(${product.id})">
                    Add to cart
                </div>
            </div>
            <div class="info">
                <h3 class="product-name">${product.name}</h3>
                <div class="price-container">
                    <h6 class="product-price">$${priceWithDiscount}.00 MX</h6>
                     <h6 class="disscount-span">$${product.price}.00 MX</h6>
                </div>
            </div>
        </div>`;
    })
    .join("")
}
console.log(showFlashSales());
function showAddToCart(element) {
  element.querySelector(".add-to-cart-container").style.display = "flex";
}
function hideAddToCart(element) {
  element.querySelector(".add-to-cart-container").style.display = "none";
}
function broseByCategory() {
  document.getElementById("section-browseByCategory").innerHTML = category
    .map((category) => {
      return `
            <div class="category">
                <img class="category-image" src="./img/category-${category.id}.svg" title="Categoria ${category.id}" alt="Imagen ${category.id}">
                <h3 class="category-name">${category.id}</h3>
            </div>`;
    })
    .join("");
}
console.log(broseByCategory());

function showBestSelling() {
  const bestSelling = products.filter(
    (product) => product.category === "best-sales"
  );
  document.getElementById("section-bestSelling").innerHTML = bestSelling
    .map((product) => {
      let priceWithDisscount = Math.floor(
        product.price - (product.price * product.disscountPercentage) / 100
      );
      return `<div class="product">
                <div class="image-product-container" onmouseenter="showAddToCart(this)" onmouseleave="hideAddToCart(this)">
                    <img class="product-image" src="./img/product-${product.id}.png" title="Producto de nuestra tienda ${product.name}" alt="Imagen ${product.name}">
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
    })
    .join("");
}
console.log(showBestSelling());

function showOurProducts() {
  const ourProducts = products.filter(
    (product) => product.category === "our-sales"
  );
  document.getElementById("section-ourProducts").innerHTML = ourProducts
    .map((product) => {
      let priceWithDisscount = Math.floor(
        product.price - (product.price * product.disscountPercentage) / 100
      );
      return `<div class="product">
                <div class="image-product-container" onmouseenter="showAddToCart(this)" onmouseleave="hideAddToCart(this)">
                    <img class:"product-image" src="./img/product-${product.id}.png" title="Producto de nuestra tienda ${product.name}" alt="Imagen ${product.name}">
                    <div class="add-to-cart-container" onclick="addToCart(${product.id})">
                        Add to cart
                    </div>
                </div>
                <div class="info">
                    <h3 class="product-name">${product.name}</h3>
                    <h6 class="product-price">$${priceWithDisscount}.00 MX</h6>
                </div>
            </div>`;
    })
    .join("");
}
console.log(showOurProducts());
function addToCart(productId) {
    const product = products.find((item) => item.id === productId);
    const cartItems = document.getElementById("cart-items");
    const existingProduct = cartItems.querySelector(
        `.cart-container[data-id="${productId}"]`
    );
    const priceWithDiscount = Math.floor(
        product.price - (product.price * product.disscountPercentage) / 100
    );
    if (existingProduct) {
        const quantityElement = existingProduct.querySelector(".quantity");
        let quantity = parseInt(quantityElement.textContent);
        quantity++;
        quantityElement.textContent = quantity;
    } else {
        const totalProduct = priceWithDiscount * product.quantity;
        cartItems.innerHTML += `
        <div class="cart-container" data-id="${productId}">
            <div class="cart-product-image">
                <img src="../img/product-${product.id}.png" class="cart-image"/>
                ${product.name}
                <div class="delete-button" onclick="removeItem(${productId})">
                   <button>
                    <img src="../img/delete.svg"/>
                   </button>
                </div>
            </div>
            <div>
                $${priceWithDiscount}.00 MX
            </div>
            <div class="cart-quantity">
                <h1 class="quantity">${product.quantity}</h1>
                <div class="quantity-buttons">
                    <img src="../img/arrow-up.svg" class="quantity-button" onclick="addQuantity(${product.id})"/>
                    <img src="../img/arrow-down.svg" class="quantity-button" onclick="removeQuantity(${product.id})"/>
                </div>
            </div>
            <div>
                <h1 class="price">$${totalProduct}.00</h1>
            </div>
        </div>
        `;
        updateTotal()
    }
    console.log(product);
}

function addQuantity(productId) {
  const product = products.find((item) => item.id === productId);
  if (product) {
    product.quantity++;
    const quantityElement = document.querySelector(
      `.cart-container[data-id="${productId}"] .quantity`
    );
    quantityElement.textContent = product.quantity;
    let priceWithDiscount = Math.floor(
      product.price - (product.price * product.disscountPercentage) / 100
    );
    const totalProduct = priceWithDiscount * product.quantity;
    document.querySelector(
      `.cart-container[data-id="${productId}"] .price`
    ).textContent = `$${totalProduct}.00`;
    updateTotal();
  }
}
function removeQuantity(productId) {
  const product = products.find((item) => item.id === productId);
  if (product.quantity > 1) {
    product.quantity--;
    const quantityElement = document.querySelector(
      `.cart-container[data-id="${productId}"] .quantity`
    );
    quantityElement.textContent = product.quantity;
    let priceWithDiscount = Math.floor(
      product.price - (product.price * product.disscountPercentage) / 100
    );
    const totalProduct = priceWithDiscount * product.quantity;
    document.querySelector(
      `.cart-container[data-id="${productId}"] .price`
    ).textContent = `$${totalProduct}.00`;
    updateTotal();
  }
}
function updateTotal() {
  const cartItems = document.getElementById("cart-items");
  const cartProducts = cartItems.querySelectorAll(".cart-container");
  let total = 0;
  cartProducts.forEach((cartProduct) => {
    const priceElement = cartProduct.querySelector(".price");
    const priceText = priceElement.textContent;
    const price = parseFloat(priceText.replace("$", ""));
      total += price;
      const subtotalElement = document.querySelector("#subtotal");
      subtotalElement.textContent = `$${total}.00`;
      const totalElement = document.querySelector("#total");
      totalElement.textContent = `$${total}.00`;
  });
}
function removeItem(productId) {
  const product = products.find((item) => item.id === productId);
  if (product) {
    const cartItems = document.getElementById("cart-items");
    const existingProduct = cartItems.querySelector(
      `.cart-container[data-id="${productId}"]`
    );
    if (existingProduct) {
      existingProduct.remove();
      updateTotal();
    }
  }
}
console.log(upateTotal());
function deleteCart () {
  const cartItems = document.getElementById("cart-items");
  cartItems.innerHTML = '';
  updateTotal();
}