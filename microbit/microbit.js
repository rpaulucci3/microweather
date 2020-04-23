// https://makecode.microbit.org/_43F1p3iyDAYY
let outp = ""
basic.pause(2000)
weatherbit.startWeatherMonitoring()
weatherbit.startRainMonitoring()
weatherbit.startWindMonitoring()
basic.forever(function () {
    outp = ("\"altitude\":" + weatherbit.altitude() + ",") + ("\"temperature\":" + Math.idiv(weatherbit.temperature(), 100) + ",") + ("\"humidity\":" + Math.idiv(weatherbit.humidity(), 1024) + ",") + ("\"pressure\":" + Math.idiv(weatherbit.pressure(), 25600) + ",") + ("\"wind_speed\":" + weatherbit.windSpeed() + ",") + ("\"wind_direction\":\"" + weatherbit.windDirection() + "\",") + ("\"soil_moisture\":" + weatherbit.soilMoisture() + ",") + ("\"soil_temp\":" + Math.idiv(weatherbit.soilTemperature(), 100) + ",") + ("\"rain_rate\":" + weatherbit.rain())
    basic.showIcon(IconNames.Yes)
    serial.writeLine("{" + outp + "}")
    basic.showLeds(`
        . . . . .
        . . . . .
        . . . . .
        . . . . .
        . . . . .
        `)
    basic.pause(5000)
})
