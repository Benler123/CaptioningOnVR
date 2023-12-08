
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class CaptionWords : MonoBehaviour
{
    public CheckOverflowCaptions overFlowCaptions;
    private TextMeshProUGUI overflowTextMeshProText;
    private TextMeshProUGUI textMeshProText;
    int curr_word = 0;
    public string jsonBlob;
    private List<Dictionary<string, string>> deserialized_words;
    public Parameters Params;

    double timer = 0;

    double delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        // transform.GetChild(0);
        textMeshProText = GetComponent<TextMeshProUGUI>();
        overflowTextMeshProText = overFlowCaptions.textMeshProText;
        curr_word = 0;
        string jsonResourcePath = string.Format("CaptionJsons/Merged_captions.{0}", Params.video);
        TextAsset jsonTextFile = Resources.Load<TextAsset>(jsonResourcePath);
        deserialized_words = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonTextFile.text);
            if (textMeshProText == null) {
            Debug.LogError("TextMeshProUGUI component is null.");
        }
        if (overflowTextMeshProText == null) {
            Debug.LogError("overflowTextMeshProText is null.");
        }
        if (jsonTextFile == null) {
            Debug.LogError("Failed to load JSON TextAsset from " + jsonResourcePath);
        }
   }
    // Update is called once per frame
    void Update() {
    timer += Time.deltaTime;
        if (timer >= delay && curr_word < deserialized_words.Count) {
            Dictionary<string, string> currChunk = deserialized_words[curr_word];
            delay =  double.Parse(currChunk["delay"], System.Globalization.CultureInfo.InvariantCulture) / 1000;
            Params.setCurrentJuror(currChunk["speaker_id"]);
            if(addWordtoOverflow(currChunk)) {
                clearAndSet(textMeshProText, currChunk);
                clearAndSet(overflowTextMeshProText, currChunk);
                curr_word++;
            } else {
                addWord(currChunk);
            }
        }
    }

    void clearAndSet(TextMeshProUGUI tmpText, Dictionary<string, string> currChunk) {
        tmpText.text = currChunk["text"] + " ";
    }

    bool addWordtoOverflow(Dictionary<string, string> currChunk) {
        overflowTextMeshProText.text += currChunk["text"] + " ";
        overflowTextMeshProText.ForceMeshUpdate();
        return overflowTextMeshProText.isTextOverflowing;
    }

    void addWord(Dictionary<string, string> currChunk) {
        textMeshProText.text += currChunk["text"] + " ";
        curr_word++;

    }
    IEnumerator ClearText() {
        textMeshProText.text = "";
        yield return null; 
    }
}
