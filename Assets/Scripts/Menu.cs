
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string[] text;
    [SerializeField] private Vector2[] waitTime;
    [SerializeField] private float timeBetweenCharacters;
    [SerializeField] private TMP_Text textBox;
    private string currentText;
    private float timeElapsed;
    private int charIndex;
    private int textIndex;
    private bool forward;
    private bool paused;
    private bool finished;
    [SerializeField] private bool autoStart;
    [SerializeField] private bool endGame;
    void Awake()
    {
        forward = true;
    }
    public void Initiate()
    {
        Time.timeScale = 1f;
        Debug.Log("AHAHAHSHS");
        autoStart = true;
        GetComponent<Animation>().Play("menu_end");
        GameManager.Instance.WreckIt();
        GetComponent<RawImage>().color = new Color(0, 0, 0, 255);
    }
    void Update()
    {
        if (!autoStart) return;
        if (finished)
        {
            if (endGame)
            {
                Application.Quit();
            }
            return;
        }
        timeElapsed += Time.deltaTime;
        if (paused)
        {
            float waitSecs;
            if (!forward) waitSecs = waitTime[textIndex].x;
            else waitSecs = waitTime[textIndex].y;
            if (timeElapsed >= waitSecs)
            {
                paused = false;
                timeElapsed = 0f;
            }
            return;
        }
        if (timeElapsed >= timeBetweenCharacters)
        {
            timeElapsed = 0f;
            if (forward) Forward();
            else Backward();
        }
    }
    private void Forward()
    {
        currentText += text[textIndex][charIndex];
        charIndex += 1;
        if (charIndex >= text[textIndex].Length)
        {
            paused = true;
            forward = false;
        }
        textBox.SetText(currentText);
    }
    private void Backward()
    {
        currentText = currentText.Substring(0, charIndex);
        charIndex -= 1;
        if (currentText.Length == 0)
        {
            charIndex = 0;
            paused = true;
            forward = true;
            textIndex += 1;
            if (textIndex >= text.Length)
            {
                finished = true;
                GetComponent<Animation>().Play("menu_fade");
            }
        }
        textBox.SetText(currentText);
    }
}
