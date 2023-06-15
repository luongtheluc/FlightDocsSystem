IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AircraftTypes] (
    [aircraft_type_id] int NOT NULL IDENTITY,
    [aircraft_type_name] varchar(100) NULL,
    [manufacturer] varchar(100) NULL,
    [seating_capacity] int NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK_AircraftTypes] PRIMARY KEY ([aircraft_type_id])
);
GO

CREATE TABLE [Airports] (
    [airport_id] int NOT NULL IDENTITY,
    [airport_code] varchar(10) NULL,
    [airport_name] varchar(100) NULL,
    [city] varchar(100) NULL,
    [country] varchar(100) NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Airports__C795D516759AC957] PRIMARY KEY ([airport_id])
);
GO

CREATE TABLE [FlightDocumentTypes] (
    [document_type_id] int NOT NULL IDENTITY,
    [document_type_name] varchar(100) NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK_FlightDocumentTypes] PRIMARY KEY ([document_type_id])
);
GO

CREATE TABLE [Passengers] (
    [passenger_id] int NOT NULL IDENTITY,
    [first_name] varchar(100) NULL,
    [last_name] varchar(100) NULL,
    [date_of_birth] date NULL,
    [gender] varchar(10) NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Passenge__03764586308DB9B2] PRIMARY KEY ([passenger_id])
);
GO

CREATE TABLE [Roles] (
    [role_id] int NOT NULL IDENTITY,
    [role_name] varchar(100) NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Roles__760965CC06F59FD5] PRIMARY KEY ([role_id])
);
GO

CREATE TABLE [Users] (
    [user_id] int NOT NULL IDENTITY,
    [username] varchar(100) NULL,
    [password] varchar(100) NULL,
    [address] nvarchar(100) NULL,
    [name] nvarchar(50) NULL,
    [email] varchar(50) NULL,
    [phone] varchar(11) NULL,
    [isActive] bit NULL,
    [userImage] varchar(50) NULL,
    [PasswordResetToken] nvarchar(max) NULL DEFAULT ((N'')),
    [ResetTokenExpries] datetime2 NULL,
    [VerificationToken] nvarchar(max) NULL,
    [VerifyAt] datetime2 NULL,
    [RefreshToken] nvarchar(max) NULL DEFAULT ((N'')),
    [RefreshTokenCreated] datetime2 NULL DEFAULT (('0001-01-01T00:00:00.0000000')),
    [RefreshTokenExpries] datetime2 NULL DEFAULT (('0001-01-01T00:00:00.0000000')),
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Users__B9BE370F2CF8CC80] PRIMARY KEY ([user_id])
);
GO

CREATE TABLE [Aircrafts] (
    [aircraft_id] int NOT NULL IDENTITY,
    [aircraft_number] varchar(20) NULL,
    [aircraft_type_id] int NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Aircraft__04015399FA3E6786] PRIMARY KEY ([aircraft_id]),
    CONSTRAINT [FK_Aircrafts_AircraftTypes] FOREIGN KEY ([aircraft_type_id]) REFERENCES [AircraftTypes] ([aircraft_type_id])
);
GO

CREATE TABLE [UserRoles] (
    [user_id] int NOT NULL,
    [role_id] int NOT NULL,
    CONSTRAINT [FK__UserRoles__role___5070F446] FOREIGN KEY ([role_id]) REFERENCES [Roles] ([role_id]),
    CONSTRAINT [FK__UserRoles__user___4F7CD00D] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id])
);
GO

CREATE TABLE [Flights] (
    [flight_id] int NOT NULL IDENTITY,
    [flight_number] varchar(10) NULL,
    [departure_airport_id] int NULL,
    [arrival_airport_id] int NULL,
    [departure_time] datetime NULL,
    [arrival_time] datetime NULL,
    [aircraft_id] int NULL,
    [user_id] int NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Flights__E3705765106FACBE] PRIMARY KEY ([flight_id]),
    CONSTRAINT [FK_Flights_Airports] FOREIGN KEY ([departure_airport_id]) REFERENCES [Airports] ([airport_id]),
    CONSTRAINT [FK_Flights_Airports1] FOREIGN KEY ([arrival_airport_id]) REFERENCES [Airports] ([airport_id]),
    CONSTRAINT [FK_Flights_Users] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]),
    CONSTRAINT [FK__Flights__aircraf__3C69FB99] FOREIGN KEY ([aircraft_id]) REFERENCES [Aircrafts] ([aircraft_id])
);
GO

CREATE TABLE [Documents] (
    [document_id] int NOT NULL IDENTITY,
    [flight_id] int NULL,
    [document_type] varchar(100) NULL,
    [document_number] varchar(100) NULL,
    [expiration_date] date NULL,
    [document_type_id] int NULL,
    [passenger_id] int NULL,
    [user_id] int NULL,
    [create_at] datetime NULL,
    [update_at] datetime NULL,
    CONSTRAINT [PK__Document__9666E8AC859AE755] PRIMARY KEY ([document_id]),
    CONSTRAINT [FK_Documents_FlightDocumentTypes] FOREIGN KEY ([document_type_id]) REFERENCES [FlightDocumentTypes] ([document_type_id]),
    CONSTRAINT [FK_Documents_Passengers] FOREIGN KEY ([passenger_id]) REFERENCES [Passengers] ([passenger_id]),
    CONSTRAINT [FK_Documents_Users] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]),
    CONSTRAINT [FK__Documents__fligh__44FF419A] FOREIGN KEY ([flight_id]) REFERENCES [Flights] ([flight_id])
);
GO

CREATE INDEX [IX_Aircrafts_aircraft_type_id] ON [Aircrafts] ([aircraft_type_id]);
GO

CREATE INDEX [IX_Documents_document_type_id] ON [Documents] ([document_type_id]);
GO

CREATE INDEX [IX_Documents_flight_id] ON [Documents] ([flight_id]);
GO

CREATE INDEX [IX_Documents_passenger_id] ON [Documents] ([passenger_id]);
GO

CREATE INDEX [IX_Documents_user_id] ON [Documents] ([user_id]);
GO

CREATE INDEX [IX_Flights_aircraft_id] ON [Flights] ([aircraft_id]);
GO

CREATE INDEX [IX_Flights_arrival_airport_id] ON [Flights] ([arrival_airport_id]);
GO

CREATE INDEX [IX_Flights_departure_airport_id] ON [Flights] ([departure_airport_id]);
GO

CREATE INDEX [IX_Flights_user_id] ON [Flights] ([user_id]);
GO

CREATE INDEX [IX_UserRoles_role_id] ON [UserRoles] ([role_id]);
GO

CREATE INDEX [IX_UserRoles_user_id] ON [UserRoles] ([user_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230615025724_Init', N'7.0.5');
GO

COMMIT;
GO

