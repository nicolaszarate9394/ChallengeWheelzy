-- Punto 1 Car information database

-- Tabla de marcas
CREATE TABLE CarMake (
    MakeId INT IDENTITY PRIMARY KEY,
    MakeName VARCHAR(50) NOT NULL
);

-- Tabla de modelos
CREATE TABLE CarModel (
    ModelId INT IDENTITY PRIMARY KEY,
    MakeId INT NOT NULL FOREIGN KEY REFERENCES CarMake(MakeId),
    ModelName VARCHAR(50) NOT NULL
);

-- Tabla de submodelos
CREATE TABLE CarSubModel (
    SubModelId INT IDENTITY PRIMARY KEY,
    ModelId INT NOT NULL FOREIGN KEY REFERENCES CarModel(ModelId),
    SubModelName VARCHAR(50) NOT NULL
);

-- Tabla de autos publicados por clientes
CREATE TABLE Car (
    CarId INT IDENTITY PRIMARY KEY,
    Year INT NOT NULL,
    SubModelId INT NOT NULL FOREIGN KEY REFERENCES CarSubModel(SubModelId),
    ZipCode VARCHAR(10) NOT NULL
);

-- Tabla de compradores
CREATE TABLE Buyer (
    BuyerId INT IDENTITY PRIMARY KEY,
    BuyerName VARCHAR(100) NOT NULL
);

-- Tabla relacion comprador/codigos postales 
CREATE TABLE BuyerZipCode (
    BuyerZipId INT IDENTITY PRIMARY KEY,
    BuyerId INT NOT NULL FOREIGN KEY REFERENCES Buyer(BuyerId),
    ZipCode VARCHAR(10) NOT NULL,
    Quote DECIMAL(18,2) NOT NULL
);

-- Cotizaciones para un auto
CREATE TABLE CarQuote (
    CarQuoteId INT IDENTITY PRIMARY KEY,
    CarId INT NOT NULL FOREIGN KEY REFERENCES Car(CarId),
    BuyerZipId INT NOT NULL FOREIGN KEY REFERENCES BuyerZipCode(BuyerZipId),
    Amount DECIMAL(18,2) NOT NULL,
    IsCurrent BIT NOT NULL DEFAULT 0
);

-- Estados posibles
CREATE TABLE Status (
    StatusId INT IDENTITY PRIMARY KEY,
    StatusName VARCHAR(50) NOT NULL
);

-- Historial de estados
CREATE TABLE CarStatusHistory (
    CarStatusHistoryId INT IDENTITY PRIMARY KEY,
    CarId INT NOT NULL FOREIGN KEY REFERENCES Car(CarId),
    StatusId INT NOT NULL FOREIGN KEY REFERENCES Status(StatusId),
    ChangedBy VARCHAR(50) NOT NULL,
    ChangedDate DATETIME NOT NULL DEFAULT GETDATE(),
    StatusDate DATETIME NOT NULL
);

-- Consulta de cotizacion mas estado actual

SELECT 
    c.CarId,
    c.Year,
    mk.MakeName,
    m.ModelName,
    sm.SubModelName,
    c.ZipCode,
    b.BuyerName,
    cq.Amount AS CurrentQuote,
    s.StatusName AS CurrentStatus,
    h.StatusDate
FROM Car c
JOIN CarSubModel sm ON c.SubModelId = sm.SubModelId
JOIN CarModel m ON sm.ModelId = m.ModelId
JOIN CarMake mk ON m.MakeId = mk.MakeId
JOIN CarQuote cq ON c.CarId = cq.CarId AND cq.IsCurrent = 1
JOIN BuyerZipCode bz ON cq.BuyerZipId = bz.BuyerZipId
JOIN Buyer b ON bz.BuyerId = b.BuyerId
JOIN (
    SELECT CarId, MAX(CarStatusHistoryId) AS LastStatusId
    FROM CarStatusHistory
    GROUP BY CarId
) latest ON c.CarId = latest.CarId
JOIN CarStatusHistory h ON latest.LastStatusId = h.CarStatusHistoryId
JOIN Status s ON h.StatusId = s.StatusId;
