using System.Diagnostics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private bool triggerCardInfo;
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
    [SerializeField] private int maxStage;
    [SerializeField] private float processingTime;
    private bool purchased;
    private Color orangeColor;
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF7300", out orangeColor);
        GetComponent<Image>().color = Color.white;
        foreach(TMP_Text text in GetComponentsInChildren<TMP_Text>())
        {
            text.color = Color.black;
        }
        GetComponent<Button>().interactable = false;
        statText.color = orangeColor;
        costText.SetText(cost.ToString() + "C");
        titleText.SetText(title);
        bodyText.SetText(body);
        if (timeBetweenFreeApp != 0) statText.SetText("apps/sec: +" + timeBetweenFreeApp);
        if (yield != 0) statText.SetText("yield: +" + yield * 100f + "%");
        string maxStageString = "";
        if (maxStage == 0) maxStageString = "considered";
        else if (maxStage == 1) maxStageString = "interview 1";
        else if (maxStage == 2) maxStageString = "interview 2";
        else if (maxStage == 3) maxStageString = "interview 3";
        else if (maxStage == 4) maxStageString = "accepted";
        if (maxStage != 0) statText.SetText("max stage -> " + maxStageString);
        if (processingTime != 0) statText.SetText("process time: -" + processingTime);
    }
    void Update()
    {
        if (purchased)
        {
            GetComponent<Button>().interactable = false;
            return;
        }
        if (GameManager.Instance.GetCredits() >= cost)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
    public void Purchase()
    {
        purchased = true;
        ColorBlock colors = GetComponent<Button>().colors;
        colors.disabledColor = new Color(GetComponent<Button>().colors.disabledColor.r, GetComponent<Button>().colors.disabledColor.g, GetComponent<Button>().colors.disabledColor.b, 255);
        GetComponent<Button>().colors = colors;
        if (GameManager.Instance.GetCredits() >= cost)
        {
            GameManager.Instance.AddCredits(-cost);
        }
        else return;
        if (timeBetweenFreeApp != 0) GameManager.Instance.AddAppsPerSec(timeBetweenFreeApp);
        if (yield != 0) GameManager.Instance.AddYield(yield);
        if (maxStage != 0) GameManager.Instance.IncreaseMaxStage();
        if (processingTime != 0) GameManager.Instance.SubtractProcessingTime(processingTime);
        GetComponent<Button>().interactable = false;
        GetComponent<Image>().color = orangeColor;
        foreach(TMP_Text text in GetComponentsInChildren<TMP_Text>())
        {
            text.color = Color.white;
        }
        if (triggerCardInfo)
        {
            GameManager.Instance.TriggerCardInfo();
        }
    }
}
