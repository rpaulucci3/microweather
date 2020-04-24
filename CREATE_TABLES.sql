BEGIN TRAN
CREATE TABLE [MicroWeather] (
    [timestamp] DateTime2 primary key NOT NULL,
    [altitude] int not null,
    [temperature] float not null,
    [humidity] int not null,
    [pressure] int not null,
    [wind_speed] float not null,
    [wind_direction] varchar(3) not null,
    [soil_moisture] int not null,
    [soil_temp] float not null,
    [rain_rate] float not null,
    [image] varchar(max)
);
COMMIT
