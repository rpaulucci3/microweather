#!/usr/bin/env python3

import base64
import json
import logging
from time import sleep

import requests

import picamera
import serial

camera = picamera.PiCamera(resolution=(640, 480))


def conv_to_json(my_string: str):
    """
    Converts a valid string to a JSON object.
    """
    try:
        json_obj = json.loads(my_string)
        return json_obj
    except ValueError:
        return None


def take_pic():
    """
    Takes a JPEG picture with the Pi Camera and writes it to a file.
    """
    camera.start_preview()
    sleep(2)
    camera.capture('image.jpg', thumbnail=None)


def post_observation(json_obs, dest_url):
    """
    Performs an HTTP POST of an observation in JSON object format.
    """
    try:
        obs_response = requests.post(dest_url, json=json_obs)
        if not obs_response.ok:
            print("Observation POST failed")
    except Exception as e:
        logging.exception(e)


def main():
    # Initialize serial (USB) connection with microbit
    ser = serial.Serial(
        port="/dev/ttyACM0",
        baudrate=115200,
        timeout=1
    )

    while True:
        line = ser.readline()
        to_print = line.decode("utf-8").strip()
        # Read JSON-like weather info output by microbit (see microbit.js)
        if not to_print:
            continue

        # Convert the above string to a JSON object
        json_obs = conv_to_json(to_print)
        if json_obs is None:
            print("Invalid JSON received from microbit")
            continue

        # Take a picture, encode it in Base64 and insert it into the JSON
        take_pic()
        handle = open('image.jpg', 'rb')
        jpeg = handle.read()
        jpeg64 = base64.b64encode(jpeg)
        handle.close()
        json_obs["image"] = jpeg64

        # Observation POST steps
        # print(json_obs)
        obs_dest_url = "https://microweather-dev.azurewebsites.net/Home/AddObservation"
        post_observation(json_obs, obs_dest_url)


if __name__ == "__main__":
    main()
