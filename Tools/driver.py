from format_captions import format_captions

videos = ["D:\Code\CaptioningOnVR\Assets\StreamingAssets\Videos\main.1.mp4", "D:\Code\CaptioningOnVR\Assets\StreamingAssets\Videos\main.2.mp4", "D:\Code\CaptioningOnVR\Assets\StreamingAssets\Videos\main.3.mp4", "D:\Code\CaptioningOnVR\Assets\StreamingAssets\Videos\main.4.mp4"]
captions = ["D:\Code\CaptioningOnVR\Assets\Resources\CaptionJsons\merged_captions.1.json", "D:\Code\CaptioningOnVR\Assets\Resources\CaptionJsons\merged_captions.2.json", "D:\Code\CaptioningOnVR\Assets\Resources\CaptionJsons\merged_captions.3.json", "D:\Code\CaptioningOnVR\Assets\Resources\CaptionJsons\merged_captions.4.json"]

for i in range(len(videos)):
    video = videos[i]
    caption = captions[i]
    format_captions(video, caption)
    