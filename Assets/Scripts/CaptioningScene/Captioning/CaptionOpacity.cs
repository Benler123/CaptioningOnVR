using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaptionOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters parameters;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        float alpha = parameters.alpha;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        Color color = textMeshPro.color;
        textMeshPro.color = new Color(color.r, color.g, color.b, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
