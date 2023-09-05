# PKS-ASP.NET-Core-DF

It is an application that enables service of PKS lines (Car Transport Company) . It is a tool designed to effectively manage various aspects of the transport business, including vehicles, seating patterns, passengers, seat tickets, coach routes and stations in between.


## Features

- Vehicle management - The application allows you to register and monitor all vehicles in PKS fleets, including information about the make, model, registration number and availability.
- Passenger Management - The database stores information about passengers, including their contact details, travel history and preferences.
- Ticket Reservations - Passengers can book tickets for specific seats on the coach, and the system automatically controls the availability of seats.
- Coach Courses - The application allows you to create and plan various bus courses, including setting timetables, stops and routes.
- Stop Stations - The database contains information about stop stations, including their location, facilities and available services.


## API Reference

### Bus

#### GET
```http
  GET /api/bus
```

```http
  GET /api/bus/{idBus}
```

| Parameter | Type     |
| :------- | :------ | 
| `idBus` | `int` |

###
#### POST
```http
  POST /api/bus
```
| Parameter | Type     |
| :------- | :------ | 
| `Bus` | `json` |

###### JSON
```json
{
  "capacity": 0,
  "registration": "string",
  "schema": {
    "filename": "string"
  },
  "type": {
    "made": "string",
    "version": "string",
    "engine": "string",
    "year": 0
  }
}
```

###
#### PUT
```http
  PUT /api/bus
```
| Parameter | Type     |
| :------- | :------ | 
| `Bus` | `json` |

###### JSON
```json
{
  "capacity": 0,
  "registration": "string",
  "schema": {
    "filename": "string"
  },
  "type": {
    "made": "string",
    "version": "string",
    "engine": "string",
    "year": 0
  }
}
```

###
#### DELETE
```http
  DELETE /api/bus
```
| Parameter | Type     |
| :------- | :------ | 
| `registration` | `string` |


### Bus Schema
#### GET
```http
  GET /api/busschema
```
```http
  GET /api/busschema/{idBusSchema}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idBusSchema` | `int` |

### 
#### POST
```http
  POST /api/busschema/{idBusSchema}
```

| Parameter | Type     | 
| :-------- | :------- |
| `BusSchema` | `json` |

###### JSON
```json
{
  "filename": "string"
}
```
### 
#### PUT
```http
  PUT /api/busschema/{idBusSchema}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idBusSchema` | `int` |
| `BusSchema` | `json` |

###### JSON
```json
{
  "filename": "string"
}
```
### 
#### DELETE

```http
  DELETE /api/busschema/{idBusSchema}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idBusSchema` | `int` | 

### Bus Type
#### GET
```http
  GET /api/bustype
```
```http
  GET /api/bustype/{idBusType}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idBusType` | `int` | 

### 
#### POST

```http
  POST /api/bustype
```
| Parameter | Type     |
| :-------- | :------- | 
| `BusType` | `json` | 

###### JSON
```json
{
  "made": "string",
  "version": "string",
  "engine": "string",
  "year": 0
}
```

### 
#### PUT

```http
  PUT /api/bustype/{idBusType}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idBusType` | `int` | 
| `BusType` | `json` | 

###### JSON
```json
{
  "made": "string",
  "version": "string",
  "engine": "string",
  "year": 0
}
```

### 
#### DELETE

```http
  DELETE /api/bustype/{idBusType}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idBusType` | `int` | 

### Discount
#### GET
```http
  GET /api/discount
```
```http
  GET /api/discount/{idDiscount}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idDiscount` | `int` | 

### 
#### POST
```http
  POST /api/discount
```
| Parameter | Type     |
| :-------- | :------- | 
| `Discount` | `json` | 

###### JSON
```json
{
  "name": "string",
  "discountValue": 0
}
```
### 
#### PUT
```http
  PUT /api/discount/{idDiscount}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idDiscount` | `int` | 
| `Discount` | `json` | 

###### JSON
```json
{
  "name": "string",
  "discountValue": 0
}
```

### 
#### DELETE

```http
  DELETE /api/discount/{idDiscount}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idDiscount` | `int` | 

### Passenger
#### GET
```http
  GET /api/passenger
```
```http
  GET /api/passenger/{idPassenger}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idPassenger` | `int` | 

### 
#### POST
```http
  POST /api/passenger
```
| Parameter | Type     |
| :-------- | :------- | 
| `Passenger` | `json` | 

###### JSON
```json
{
  "firstname": "string",
  "lastName": "string",
  "age": 0,
  "phoneNumber": "string",
  "email": "string"
}
```
### 
#### PUT
```http
  PUT /api/passenger/{idPassenger}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idPassenger` | `int` | 
| `Passenger` | `json` | 

###### JSON
```json
{
  "firstname": "string",
  "lastName": "string",
  "age": 0,
  "phoneNumber": "string",
  "email": "string"
}
```

### 
#### DELETE

```http
  DELETE /api/passenger/{idPassenger}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idPassenger` | `int` | 

### Route
#### GET
```http
  GET /api/route
```
```http
  GET /api/route/{idRoute}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idRoute` | `int` | 

### 
#### POST
```http
  POST /api/route
```
| Parameter | Type     |
| :-------- | :------- | 
| `Route` | `json` | 

###### JSON
```json
{
  "routeName": "string",
  "distance": 0,
  "cost": 0
}
```
```http
  POST /api/route/{idRoute}/{idStop}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idRoute` | `int` | 
| `idStop` | `int` | 
| `RouteStop` | `json` | 

###### JSON
```json
{
  "arriveTime": "yyyy-MM-ddTHH-mm-ssZ",
  "departueTime": "yyyy-MM-ddTHH-mm-ssZ"
}
```

### 
#### PUT
```http
  PUT /api/route/{idRoute}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idRoute` | `int` | 
| `Route` | `json` | 

###### JSON
```json
{
  "routeName": "string",
  "distance": 0,
  "cost": 0
}
```

### 
#### DELETE

```http
  DELETE /api/route/{idRoute}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idRoute` | `int` | 

### Stop
#### GET
```http
  GET /api/stop
```
```http
  GET /api/stop/{idStop}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idStop` | `int` | 

### 
#### POST
```http
  POST /api/stop
```
| Parameter | Type     |
| :-------- | :------- | 
| `Stop` | `json` | 

###### JSON
```json
{
  "stopName": "string"
}
```
### 
#### PUT
```http
  PUT /api/stop/{idStop}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idStop` | `int` | 
| `Stop` | `json` | 

###### JSON
```json
{
  "stopName": "string"
}
```

### 
#### DELETE

```http
  DELETE /api/stop/{idStop}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idStop` | `int` | 

### Ticket
#### GET
```http
  GET /api/ticket
```
```http
  GET /api/ticket/{idTicket}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idTicket` | `int` | 

### 
#### POST
```http
  POST /api/ticket
```
| Parameter | Type     |
| :-------- | :------- | 
| `Ticket` | `json` | 

###### JSON
```json
{
  "cost": 0,
  "validFrom": "2023-09-05T22:38:40.396Z",
  "validTo": "2023-09-05T22:38:40.396Z",
  "validated": true,
  "seatNumber": "string",
  "routeName": "string",
  "distance": 0,
  "discountName": "string",
  "idBus": 0,
  "idRoute": 0,
  "idPassenger": 0,
  "idDiscount": 0
}
```
### 
#### PUT
```http
  PUT /api/ticket/{idTicket}
```
| Parameter | Type     |
| :-------- | :------- | 
| `idTicket` | `int` | 
| `Ticket` | `json` | 

###### JSON
```json
{
  "cost": 0,
  "validFrom": "2023-09-05T22:38:40.396Z",
  "validTo": "2023-09-05T22:38:40.396Z",
  "validated": true,
  "seatNumber": "string",
  "routeName": "string",
  "distance": 0,
  "discountName": "string",
  "idBus": 0,
  "idRoute": 0,
  "idPassenger": 0,
  "idDiscount": 0
}
```

### 
#### DELETE

```http
  DELETE /api/ticket/{idTicket}
```

| Parameter | Type     |
| :-------- | :------- | 
| `idTicket` | `int` | 
