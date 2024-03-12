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

def main():
    video_name = "main.1.mp4"

    video_path = str(pathlib.Path(__file__).parent.parent.absolute()) + "/Assets/StreamingAssets/Videos/" + video_name
    audio = whisper.load_audio(video_path)

    model = whisper.load_model("tiny", device="cpu")
    result = whisper.transcribe(model, audio, language="en")

    json_name = video_name + ".json"
    json_path = str(pathlib.Path(__file__).parent.absolute()) + "/" + json_name
    print(json_name)
    print(json_path)
    with open(json_path, "w+") as f:
        json.dump(result, f)

if __name__ == "__main__":
    main()
