using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public Image affectedImage;
    public Color highlightColor;
    public Color originalColor;
    public bool isHighlighted;

    // Start is called before the first frame update
    void Start()
    {
        if (!affectedImage)
            affectedImage = GetComponent<Image>();
       // originalColor = affectedImage.color;
    }

    public void OnHoverEnter()
    {
        affectedImage.color = highlightColor;
        isHighlighted = true;
    }

    public void OnHoverExit()
    {
        affectedImage.color = originalColor;
        isHighlighted = false;
    }
}
