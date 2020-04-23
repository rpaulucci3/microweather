# MicroWeather

**A free, open-source and open-hardware weather monitoring platform.**

https://microweather-dev.azurewebsites.net

Originally made for CS 4365 (Introduction to Enterprise Computing) at [Georgia Tech](https://www.gatech.edu).

Licensed under BSD 3-clause where applicable. Some code inspired by Stack Overflow posts.

## Folder Structure

* `microbit` contains JavaScript files to be compiled into `.hex` using [the BBC microbit MakeCode editor](https://makecode.microbit.org/#editor) and uploaded onto a microbit.
* `microweather` contains an ASP.NET Core 3.1 Web App project, using a Visual Studio 2019 (16.5) solution.
* `raspberry` contains the Python files that run on a Raspberry Pi attached to a camera module and the microbit via a serial (USB) connection.

## Hardware Requirements

* SparkFun micro:climate kit (KIT-15301 or [16274](https://www.sparkfun.com/products/16274))
* [BBC microbit](https://www.sparkfun.com/products/14208)
* Raspberry Pi >=3 or Pi Zero W
* [Raspberry Pi Camera Module](https://www.sparkfun.com/products/14028) (with adapter cable for a Pi Zero, if needed)
* 5V power supply, micro-USB cables etc.

## Software Requirements for Development

* .NET Core 3.1 SDK
* Visual Studio 2019 >=16.5 (might work on VS Code too)
* Python >=3.7 (see `requirements.txt` in `raspberry`)
* A configured Azure Web App + SQL stack, or some other hosting and database solution
