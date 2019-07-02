# farma API

This document prepared to describe farma API

> Use this UI to consume and explore endpoints.

## Register

Post user details to `/api/User/Register`

Post example payload:

```json
{
  "name": "Cemal Erdemir"
  "username": "cerdemir",
  "password": "very-secure-and-secret-password",
}
```

## Authentication

Post credentials to `/api/User/Authentication`

Post example payload:

```json
{
  "username": "cerdemir",
  "password": "very-secure-and-secret-password",
}
```

> Note the corresponded *token* to **Authorize**

Example response content of `/api/User/Authentication`

```json
{
  ...
  "token": "string",  
  ...
}
```

## Authorization

Click `Authorize` button at up right of the documentation.

* Write `Bearer {token}` to the *Authorization* section and click `Authorize` button in section.
