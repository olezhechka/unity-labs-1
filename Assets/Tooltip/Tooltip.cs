using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textField;
    [SerializeField]
    private LayoutElement layoutElement;
   
    private List<string> lines = new List<string>();

    public void Update()
    {
        layoutElement.enabled = this.lines.Count > 1;
    }
    
    private List<string> SplitToLines(string stringToSplit, int[] maxLineLengthPerLine)
    {
        int absoluteMaxLineLength = maxLineLengthPerLine.Max();
        string[] words = stringToSplit.Split(' ');

        List<string> lines = new List<string>();
        int currentLineIndex = 0;
        string currentLine = words.First();
        foreach (string currentWord in words.Skip(1))
        {
            string assumedConcatenatedString = $"{currentLine} {currentWord}";
            // If all the assumed lines have been processed, use the max value instead
            int currentLineMaxLength = currentLineIndex < maxLineLengthPerLine.Length
                ? maxLineLengthPerLine[currentLineIndex]
                : absoluteMaxLineLength;
            if (assumedConcatenatedString.Length > currentLineMaxLength)
            {
                lines.Add(currentLine);
                currentLineIndex++;
                
                // Here we start the next line.
                // It doesn't take into account the maximum length,
                // because we want to overflow if there is such a long word
                currentLine = currentWord;
            }
            else
            {
                currentLine = assumedConcatenatedString;
            }
        }

        // Add leftovers, if necessary
        if (currentLine.Length > 0)
        {
            lines.Add(currentLine);
        }
        
        return lines;
    }

    public void ApplySettings(TooltipSettings newSettings)
    {
        this.lines = this.SplitToLines(newSettings.text, newSettings.maxCharsPerLine);
        this.textField.text = string.Join('\n', this.lines);

        Vector3 newPosition = newSettings.target.position;
        newPosition.x += newSettings.offsetX;
        newPosition.y += newSettings.offsetY;
        newPosition.z += newSettings.offsetZ;

        RectTransform rect = this.GetComponent<RectTransform>();
        rect.position = newPosition;
    }
}
