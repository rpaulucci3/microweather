#!/usr/bin/env python3

import json
import logging
from io import BytesIO
from time import sleep

import requests

import picamera
import serial

camera = picamera.PiCamera()


def conv_to_json(my_string: str):
    """
    Converts a valid string to a JSON object.
    """
    try:
        json_obj = json.loads(my_string)
        return json_obj
    except ValueError:
        return None


def take_pic(stream: BytesIO):
    """
    Outputs a JPEG-encoded picture from the Pi Camera to a BytesIO stream.
    """
    camera.start_preview()
    sleep(2)
    camera.capture(stream, 'jpeg', resize=(640, 480))


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


def post_image(file, dest_url):
    """
    Performs an HTTP POST of a JPEG image in a form.
    """
    try:
        img_response = requests.post(
            dest_url, files=file, headers={"content-type": "image/jpeg"})
        if not img_response.ok:
            print("Image POST failed")
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

        # Observation POST steps
        print(json_obs)
        obs_dest_url = "https://microweather-dev.azurewebsites.net/Home/AddObservation"
        post_observation(json_obs, obs_dest_url)

        # Image POST steps
        image_stream = BytesIO()
        take_pic(image_stream)
        fake_file = {"file": image_stream.getvalue()}
        img_dest_url = "https://microweather-dev.azurewebsites.net/Home/AddImage"
        post_image(fake_file, img_dest_url)
        image_stream.close()


if __name__ == "__main__":
    main()
