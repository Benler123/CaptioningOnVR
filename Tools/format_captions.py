#!/usr/bin/env python
"""
This script modifies the delay values for the captions JSON file
given at PATH2 by retiming them by analyzing the video at PATH1.

Usage: format_captions.py PATH1 PATH2
"""

import json
import sys
import os
import pathlib
from auto_gen_captions import auto_gen_captions

def format_captions(video_path: str, caption_path: str) -> str:
    """
    Modifies the file at caption_path with new delay values.
    Args:
        video_path: absolute path to the video.
        caption_path: absolute path to the prewritten captions for the video.
    Return:
        str: absolute path to the JSON file.
    """
    auto_captions = []
    manual_captions = []
    captions = None

    # Get auto generated captions
    video_json_path = auto_gen_captions(video_path)
    with open(video_json_path, "r") as f:
        video_data = json.load(f)
        segments = video_data["segments"]
        for segment in segments:
            words = segment["words"]
            for word in words:
                text, start, end = word["text"], float(word["start"]), float(word["end"])
                auto_captions.append((text, start, end))

    # Get manual captions
    with open(caption_path, "r") as f:
        words = json.load(f)
        captions = words
        for word in words:
            text, delay = word["text"], float(word["delay"])
            manual_captions.append((text, delay))

    # Resolve time stamps between auto and manual captions
    i = 0
    j = 0
    while i < len(auto_captions) and j < len(manual_captions):
        auto_word, auto_start, auto_end = auto_captions[i]
        manual_word, manual_delay = manual_captions[j]
        # TODO

    # Save captions to JSON file
    video_name = os.path.basename(video_path)
    json_name = video_name + ".manual.json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    with open(json_path, "w+") as f:
        json.dump(captions, f)

    return json_path

if __name__ == "__main__":
    video_path, caption_path = sys.argv[1], sys.argv[2]
    format_captions(video_path, caption_path)
