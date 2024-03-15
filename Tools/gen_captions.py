#!/usr/bin/env python
"""
This script generates captions for the given video and outputs them
to a JSON file in the same directory as the script.

Usage: python gen_captions.py PATH
"""

# https://github.com/jianfch/stable-ts
import stable_whisper
import pathlib
import sys
import os

def gen_captions(video_path: str) -> str:
    """
    Generate captions for the video.
    Args:
        video_path: absolute path to the video.
    Return:
        str: absolute path to the JSON file.
    """
    # Load model
    model = stable_whisper.load_model("base")

    # Generate captions
    result = model.transcribe(video_path, word_timestamps=True)

    # Save captions to JSON file
    video_name = os.path.basename(video_path)
    json_name = video_name + ".auto.json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    result.save_as_json(json_path)

    return json_path

if __name__ == "__main__":
    video_path = sys.argv[1]
    gen_captions(video_path)
