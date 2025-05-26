using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class TextWriter : MonoBehaviour
{
    public float interval = 0.01f;
    public float afterFinishDelay = 0.5f; // Delay after finishing the text
    public TextMeshProUGUI dialogueText;
    private float timer = 0;
    private string textBuffer = null;
    private char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    private int i = 0;
    private int length;
    private bool isFinished = false;

    public void Init(string line)
    {
        chars = line.ToCharArray();
        length = chars.Length;
    }

    void FixedUpdate()
    {
        if (isFinished)
        {
            if (timer > afterFinishDelay)
            {
                Destroy(gameObject); // Destroy the TextWriter object after the delay
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (timer > interval)
            {
                textBuffer += chars[i];
                dialogueText.text = textBuffer;
                timer += interval;
                i++;
                if (i >= length)
                {
                    isFinished = true;
                    timer = 0; // Reset timer to avoid further updates
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}