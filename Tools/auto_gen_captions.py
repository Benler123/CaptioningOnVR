# SRT (sentence level) generation
# https://cloud.google.com/speech-to-text#translate-audio-into-text
# https://github.com/alphacep/vosk-api/tree/master

# Word level generation
# https://cloud.google.com/speech-to-text/docs/async-time-offsets
# https://github.com/linto-ai/whisper-timestamped

# whisper-timestamped is a fork of OpenAI's whisper general purpuse speech recognition model
# https://github.com/openai/whisper
# https://github.com/ggerganov/whisper.cpp

# here is a package that wraps all of whisper and whisper-timestamped, might be easier to use
# https://github.com/openai/whisper/discussions/1093

import whisper_timestamped as whisper
import json
import pathlib
import sys
import os

def auto_gen_captions(video_path: str) -> None:
    audio = whisper.load_audio(video_path)

    model_name = "tiny"
    # model_name = "NbAiLab/whisper-large-v2-nob"
    model = whisper.load_model(model_name, device="cpu")
    result = whisper.transcribe(model, audio, language="en")

    video_name = os.path.basename(video_path)
    json_name = video_name + ".json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    with open(json_path, "w+") as f:
        json.dump(result, f)

if __name__ == "__main__":
    video_path = sys.argv[1]
    auto_gen_captions(video_path)
