-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2023-05-31 00:04:03.177

-- tables
-- Table: Bus
CREATE TABLE Bus (
    idBus int  NOT NULL,
    Capacity int  NOT NULL,
    Registation varchar(15)  NOT NULL,
    idBusType int  NOT NULL,
    idBusSchema int  NOT NULL,
    CONSTRAINT Bus_pk PRIMARY KEY  (idBus)
);

-- Table: BusSchema
CREATE TABLE BusSchema (
    idBusSchema int  NOT NULL,
    Filename varchar(100)  NOT NULL,
    CONSTRAINT BusSchema_pk PRIMARY KEY  (idBusSchema)
);

-- Table: BusType
CREATE TABLE BusType (
    idBusType int  NOT NULL,
    Made varchar(50)  NOT NULL,
    Version varchar(20)  NOT NULL,
    Engine varchar(20)  NOT NULL,
    Year int  NOT NULL,
    CONSTRAINT BusType_pk PRIMARY KEY  (idBusType)
);

-- Table: Discount
CREATE TABLE Discount (
    idDiscount int  NOT NULL,
    Discount decimal(5,2)  NOT NULL,
    Name varchar(150)  NOT NULL,
    CONSTRAINT Discount_pk PRIMARY KEY  (idDiscount)
);

-- Table: Passenger
CREATE TABLE Passenger (
    idPassenger int  NOT NULL,
    FirstName varchar(35)  NOT NULL,
    LastName varchar(35)  NOT NULL,
    Age int  NOT NULL,
    PhoneNumber varchar(25)  NOT NULL,
    Email varchar(100)  NOT NULL,
    CONSTRAINT Passenger_pk PRIMARY KEY  (idPassenger)
);

-- Table: Route
CREATE TABLE Route (
    idRoute int  NOT NULL,
    RouteName varchar(50)  NOT NULL,
    Distance decimal(6,2)  NOT NULL,
    Cost decimal(6,2)  NOT NULL,
    CONSTRAINT Route_pk PRIMARY KEY  (idRoute)
);

-- Table: RouteStop
CREATE TABLE RouteStop (
    idRouteStop int  NOT NULL,
    ArriveTime datetime  NOT NULL,
    DepartueTime datetime  NOT NULL,
    idStop int  NOT NULL,
    idRoute int  NOT NULL,
    CONSTRAINT RouteStop_pk PRIMARY KEY  (idRouteStop)
);

-- Table: Stop
CREATE TABLE Stop (
    idStop int  NOT NULL,
    StopName varchar(50)  NOT NULL,
    CONSTRAINT Stop_pk PRIMARY KEY  (idStop)
);

-- Table: Ticket
CREATE TABLE Ticket (
    idTicket int  NOT NULL,
    ValidFrom datetime  NOT NULL,
    ValidTo datetime  NOT NULL,
    Validated bit  NOT NULL,
    SeatNumber varchar(5)  NOT NULL,
    idBus int  NOT NULL,
    idRoute int  NOT NULL,
    idPassenger int  NOT NULL,
    idDiscount int  NOT NULL,
    CONSTRAINT Ticket_pk PRIMARY KEY  (idTicket)
);

-- foreign keys
-- Reference: Bus_BusSchema (table: Bus)
ALTER TABLE Bus ADD CONSTRAINT Bus_BusSchema
    FOREIGN KEY (idBusSchema)
    REFERENCES BusSchema (idBusSchema);

-- Reference: Bus_Bus_Type (table: Bus)
ALTER TABLE Bus ADD CONSTRAINT Bus_Bus_Type
    FOREIGN KEY (idBusType)
    REFERENCES BusType (idBusType);

-- Reference: RouteStop_Route (table: RouteStop)
ALTER TABLE RouteStop ADD CONSTRAINT RouteStop_Route
    FOREIGN KEY (idRoute)
    REFERENCES Route (idRoute);

-- Reference: RouteStop_Stop (table: RouteStop)
ALTER TABLE RouteStop ADD CONSTRAINT RouteStop_Stop
    FOREIGN KEY (idStop)
    REFERENCES Stop (idStop);

-- Reference: Ticket_Bus (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Bus
    FOREIGN KEY (idBus)
    REFERENCES Bus (idBus);

-- Reference: Ticket_Discount (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Discount
    FOREIGN KEY (idDiscount)
    REFERENCES Discount (idDiscount);

-- Reference: Ticket_Passenger (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Passenger
    FOREIGN KEY (idPassenger)
    REFERENCES Passenger (idPassenger);

-- Reference: Ticket_Route (table: Ticket)
ALTER TABLE Ticket ADD CONSTRAINT Ticket_Route
    FOREIGN KEY (idRoute)
    REFERENCES Route (idRoute);

-- End of file.

