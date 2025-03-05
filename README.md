<h1 align="center">Clothing store API - .NET Backend</h1>
<p align="center">
  <b>The E-Commerce API is a robust and scalable backend solution built with .NET to support an online shopping platform. This API facilitates essential e-commerce functionalities</b>
</p>


![preview.png](https://github.com/Mostafa-Abuhemaid/E-Commerce-API/blob/main/Home.jpg)
## 🌟 Features

- **Secure Authentication** with JWT & Refresh Tokens
- **Authorization**(Admin,User)
- **Notification** using FireBase
- **Asp. Net Cor Identity**
- **Unit of Work & Generic Repository Pattern**
- **Password reset and OTP verification.**
- **MailKit Email Service Integration**
- **Email validation to prevent duplicate accounts.**
- **Auto-Mapping** with Auto-Mapper
  
## 🛠 Tech Stack

- **Framework**: .NET 8
- **Database**: SQL Server + Entity Framework Core
- **Notification**: FireBase
- **Mapping**:  Auto-Mapper
- **Architecture**: Onion Architecture

## 🧩 Design Patterns

- **Onion Architecture** - Separation of concerns
- **Repository Pattern** - Abstracted data access
- **Unit of Work** - Transaction management
  

## 📚 API Documentation

Explore endpoints interactively via Swagger UI:
```
http://ethiqclothes.runasp.net/swagger/index.html
```



## 📡 API Endpoints

### Authentication

| Method | Endpoint                       | Description                   |
|--------|--------------------------------|-------------------------------|
| POST   | `/api/Account/Register`        | User registration             |
| POST   | `/api/Account/Login`           | JWT authentication            |
| POST   | `/api/Account/ForgetPassword`  | Forgot password               |
| POST   | `/api/Account/VerifyOTP`       |Check if the CodeOTP is correct|
| PUT    | `/api/Account/ResetPassword`  | Reset password                 |
| GET    | `/api/Account/CheckEmailExists`| Check if email exists         |


### User

| Method | Endpoint                       | Description            |
|--------|--------------------------------|------------------------|
| GET    | `/api/User/GetUserDetails`     | Get User profile       |
| PUT    | `/api/User/EditUser`           | Update User info       |
| POST   | `/api/User/lockByEmail`        | Lock User Account      |
| POST   | `/api/User/UnlockByEmail`      | UnLock User Account    |
| Delete | `/api/User/DeleteByEmail`      | Delete User Account    |
| GET    | `/api/User/GetUserDetails`     | Get All Users At System|


### Product

| Method | Endpoint                       | Description            |
|--------|--------------------------------|------------------------|
| GET    | `/api/Product/GetAllProducts`  | Get All Products       |
| GET    | `/api/Product/{id}`            |Get the Product by Id   |
| POST   | `/api/Product`                 | Add New Product        |
| PUT   | `/api/Product/{id}`             | Edit Product details   |
| Delete | `/api/Product/{id}`            |Delete A Product By Id  |
| GET    | `/api/Product/SearchByName`    | Search Product By Name |


### Category

| Method | Endpoint                       | Description              |
|--------|--------------------------------|--------------------------|
| GET    | `/api/Category/GetAllCategory`  | Get All Categories      |
| GET    | `/api/Category/{id}`            |Get the Category by Id   |
| POST   | `/api/Category`                 | Add New Category        |
| PUT   | `/api/Category/{id}`             | Edit Category details   |
| Delete | `/api/Category/{id}`            |Delete A Category By Id  |

### Cart

| Method | Endpoint                           | Description                         |
|--------|------------------------------------|-------------------------------------|
| GET    | `/api/Cart/GetallProductFromCart`  |Get all Products From Cart           |
| POST   | `/api/Cart/add`                    | Add New Product to Cart             |
| GET   | `/api/Cart/increment/{productId}`   |increment the quantity o product by 1|
| GET   | `/api/Cart/decrement/{productId}`   |decrement the quantity o product by 1|
| Delete | `/api/Cart/remove/{productId}`     |Delete A Product From Cart           |


### Notification

| Method | Endpoint                           | Description                          |
|--------|------------------------------------|--------------------------------------|
| POST   | `/api/Notification/Send`           |Send Notification To User by FCMToken |
| POST   | `/api/Notification/SendAll`        | Send Notification To All Users       |


### Order

| Method | Endpoint                       | Description             |
|--------|--------------------------------|-------------------------|
| GET    | `/api/Order/GetAllOrders`      | Get All Orders          |
| GET    | `/api/Order/GetOrder/{id}`     |Get the Order by Id      |
| POST   | `/api/Order/MakeOrder`         | Make a new Order        |
| Delete | `/api/Order/DeleteOrder/{id}`  |Delete A Order By Id     |


### Favorite

| Method | Endpoint                       | Description                              |
|--------|--------------------------------|------------------------------------------|
| GET    | `/api/Favorite/GetAllFavorite` | Get All product from Favorite list       |
| POST   | `/api/Favorite/{productId}`    | Add a new product to Favorite list       |     
| POST   | `/api/Favorite/IsFavorite`     |Check if the product in the Favorite list |
| Delete | `/api/Favorite/{productId}`    |Delete product From Favorite list         | 

