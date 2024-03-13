# scrape auto_gen_captions timestamps, insert into our prewritten captions

import json
import sys
from auto_gen_captions import auto_gen_captions

def format_captions(video_path: str, caption_path: str) -> None:
    result = auto_gen_captions(video_path)

    # Get word dictionary
    file = json.dumps(result)
    # TODO

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
