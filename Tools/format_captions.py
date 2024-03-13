#!/usr/bin/env python
"""
This script modifies the delay values for the captions JSON file
given at PATH2 by retiming them by analyzing the video at PATH1.

Usage: format_captions.py PATH1 PATH2
"""

import json
import sys
from auto_gen_captions import auto_gen_captions

def format_captions(video_path: str, caption_path: str) -> None:
    """
    Modifies the file at caption_path with new delay values.
    Args:
        video_path: absolute path to the video.
        caption_path: absolute path to the prewritten captions for the video.
    Return:
        None.
    """
    auto_captions = []
    manual_captions = []

    # Get auto generated words
    video_json_path = auto_gen_captions(video_path)
    with open(video_json_path, "r") as f:
        video_data = json.load(f)
        segments = video_data["segments"]
        for segment in segments:
            words = segment["words"]
            for word in words:
                text, start, end = word["text"], float(word["start"]), float(word["end"])
                auto_captions.append((text, start, end))

    with open(caption_path, "r") as f:
        words = json.load(f)
        for word in words:
            text, delay = word["text"], float(word["delay"])
            manual_captions.append((text, delay))

    # Find differences between generated words and original words
    # TODO

    # Resolve differences
    # TODO

    # Fix original timestamps
    # TODO

    # Save
    # TODO

if __name__ == "__main__":
    video_path, caption_path = sys.argv[1], sys.argv[2]
    format_captions(video_path, caption_path)
