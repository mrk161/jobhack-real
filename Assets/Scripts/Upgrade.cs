using System.Diagnostics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private TMP_Text statText;
    [Header("Stats")]
    [SerializeField] private int cost;
    [SerializeField] private string title;
    [SerializeField] private string body;
    [Header("Delta Stats")]
    [SerializeField] private float timeBetweenFreeApp;
    [SerializeField] private float yield;
    [SerializeField] private string maxStage;
    [SerializeField] private float processingTime;
    private Color orangeColor;
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF7300", out orangeColor);
        costText.SetText(cost.ToString() + "C");
        titleText.SetText(title);
        bodyText.SetText(body);
        if (timeBetweenFreeApp != 0) statText.SetText("apps/sec: +" + 1f / timeBetweenFreeApp);
        if (yield != 0) statText.SetText("yield: +" + timeBetweenFreeApp);
        if (maxStage != "") statText.SetText("max stage = " + maxStage);
        if (processingTime != 0) statText.SetText("process time: +" + processingTime);
    }
    public void Purchase()
    {
        if (timeBetweenFreeApp != 0) GameManager.Instance.AddAppsPerSec(timeBetweenFreeApp);
        if (yield != 0) GameManager.Instance.AddYield(yield);
        if (maxStage != "") GameManager.Instance.IncreaseMaxStage();
        if (processingTime != 0) GameManager.Instance.SubtractProcessingTime(processingTime);
        GetComponent<Image>().color = orangeColor;
        foreach(TMP_Text text in GetComponentsInChildren<TMP_Text>())
        {
            text.color = Color.white;
        }
    }
}
