BEGIN TRAN
CREATE TABLE [MicroWeather] (
    [timestamp] DateTime2 primary key NOT NULL,
    [altitude] int not null,
    [temperature] int not null,
    [humidity] int not null,
    [pressure] int not null,
    [wind_speed] int not null,
    [wind_direction] varchar(3) not null,
    [soil_moisture] int not null,
    [soil_temp] int not null,
    [rain_rate] int not null,
    [image] varchar(max)
);
COMMIT
