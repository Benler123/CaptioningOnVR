#!/usr/bin/env python
"""
This script generates captions for the given video and outputs them
to a JSON file in the same directory as the script.

Usage: auto_gen_captions.py PATH
"""

import whisper_timestamped as whisper
import json
import pathlib
import sys
import os

def auto_gen_captions(video_path: str) -> str:
    """
    Generate captions for the video.
    Args:
        video_path: absolute path to the video.
    Return:
        str: absolute path to the JSON file.
    """
    # Load video
    audio = whisper.load_audio(video_path)

    # Load model
    model_name = "tiny"
    # model_name = "NbAiLab/whisper-large-v2-nob"
    model = whisper.load_model(model_name, device="cpu")

    # Generate captions
    result = whisper.transcribe(model, audio, language="en")

    # Save captions to JSON file
    video_name = os.path.basename(video_path)
    json_name = video_name + ".auto.json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    with open(json_path, "w+") as f:
        json.dump(result, f)

    return json_path

if __name__ == "__main__":
    video_path = sys.argv[1]
    auto_gen_captions(video_path)
