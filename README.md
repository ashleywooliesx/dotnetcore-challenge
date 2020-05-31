# WooliesX Coding Challenge

## URLs for test

* Exercise 1: https://wooliesxchallenge-dev-appservice.azurewebsites.net/api/user
* Exercise 2: https://wooliesxchallenge-dev-appservice.azurewebsites.net/api/sort
* Exercise 3: https://wooliesxchallenge-dev-appservice.azurewebsites.net/api/trolleyTotal [POST]

## Using Postman

```
GET /api/user HTTP/1.1
Host: wooliesxchallenge-dev-appservice.azurewebsites.net
Cache-Control: no-cache
Postman-Token: 1bbcde07-bdee-4a40-5b2b-2d8be4c64532
```

```
GET /api/products/sort HTTP/1.1
Host: wooliesxchallenge-dev-appservice.azurewebsites.net
Cache-Control: no-cache
Postman-Token: e500a586-5acf-8bff-26f2-35c1ae81af9e
```

```
POST /api/trolleyTotal HTTP/1.1
Host: wooliesxchallenge-dev-appservice.azurewebsites.net
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 3bab859a-3db6-93a0-6a8f-08081fa12c24

{
  "products": [
    {
      "name": "Test Product B",
      "price": 10
    }
  ],
  "specials": [
    {
      "quantities": [
        {
          "name": "Test Product B",
          "quantity": 20
        }
      ],
      "total": 50
    }
  ],
  "quantities": [
    {
      "name": "Test Product B",
      "quantity": 30
    }
  ]
}
```
