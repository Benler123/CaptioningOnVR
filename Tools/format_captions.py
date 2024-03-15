#!/usr/bin/env python
"""
This script modifies the delay values for the captions JSON file
given at PATH2 by retiming them by analyzing the video at PATH1.

Usage: python format_captions.py PATH1 PATH2
"""

# https://github.com/jianfch/stable-ts
import stable_whisper
import pathlib
import sys
import os
import json
import string

def format_captions(video_path: str, caption_path: str) -> str:
    """
    Modifies the file at caption_path with new delay values.
    Args:
        video_path: absolute path to the video.
        caption_path: absolute path to the prewritten captions for the video.
    Return:
        str: absolute path to the JSON file.
    """
    original_captions = None

    # Load original captions
    captions_str = ""
    with open(caption_path, "r") as original_file:
        original_captions = json.load(original_file)
        for word in original_captions:
            text = word["text"]
            captions_str += " " + text

    # Filter original captions
    captions_str = captions_str.replace("a.m.", "am")
    captions_str = captions_str.replace("p.m.", "pm")

    # Load model
    model = stable_whisper.load_model("base")

    # Generate timestamps
    result = model.align(video_path, captions_str, language="en", )

    # Save captions to JSON file
    video_name = os.path.basename(video_path)
    json_name = video_name + ".manual.json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    result.save_as_json(json_path)

    # Format output
    i = 0
    with open(json_path, "r") as new_file:
        file = json.load(new_file)
        segments = file["segments"]
        for segment in segments:
            words = segment["words"]
            for word in words:

                # Do not get time for punctuation
                # Results splits up hypens: "boy-oh-boy" -> ("boy", "-oh", "-boy"). Only consider first instance
                # Results splits up time: 12:10 -> (12, :10). Only consider first instance
                gen_text = word["word"]
                if gen_text in string.punctuation or gen_text[0] == "-" or gen_text[0] == ":":
                    continue

                # Get start time in seconds
                start = float(word["start"])
                # Conver to milliseconds
                start = start * 1000

                # Replace delay in original captions
                original_captions[i]["delay"] = start
                i += 1

    # Resave output
    with open(json_path, "w+") as format_new_file:
        json.dump(original_captions, format_new_file)

    return json_path

if __name__ == "__main__":
    video_path, caption_path = sys.argv[1], sys.argv[2]
    format_captions(video_path, caption_path)
