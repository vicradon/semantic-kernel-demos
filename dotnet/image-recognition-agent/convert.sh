#!/bin/bash

if [ "$1" == "encode" ] && [ -f "$2" ]; then
  base64 "$2"
elif [ "$1" == "decode" ] && [ -n "$2" ]; then
  echo "$2" | sed 's/^data:image\/png;base64,//' | base64 -d > output.png
else
  echo "Usage:"
  echo "$0 encode <image_file>"
  echo "$0 decode <base64_string>"
fi
