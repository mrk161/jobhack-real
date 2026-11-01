using System;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float appsPerSec;
    [SerializeField] private float yield;
    [SerializeField] private int maxStage;
    [SerializeField] private float processingTime;
    [SerializeField] private int credits;
    [Header("Prefabs")]
    [SerializeField] private ProgressMeter progressMeter;
    [Header("MeterContainers")]
    [SerializeField] private MeterContainer[] meterContainers;
    [Header("Stages")]
    [SerializeField] private Stage[] stages;
    [Header("References")]
    [SerializeField] private Animation sendAppAnimation;
    [Header("UI References")]
    [SerializeField] private TMP_Text appsPerSecText;
    [SerializeField] private TMP_Text yieldText;
    [SerializeField] private TMP_Text processingTimeText;
    [SerializeField] private TMP_Text maxStageText;
    [SerializeField] private TMP_Text creditsText;
    [SerializeField] private Button sendAppButton;
    [SerializeField] private GameObject cardInfo;
    [SerializeField] private AudioClip cardInfoSound;
    [SerializeField] private AudioSource audioSource;
    public static GameManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Initialize();
    }
    public void TriggerCardInfo()
    {
        Time.timeScale = 0f;
        cardInfo.SetActive(true);
        audioSource.PlayOneShot(cardInfoSound);
    }
    public void EndCardInfo()
    {
        Time.timeScale = 1f;
    }
    private float timeElapsed;
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 1f / appsPerSec)
        {
            sendAppButton.onClick.Invoke();
            timeElapsed = 0f;
        }
    }
    public int GetMaxStage()
    {
        return maxStage;
    }
    public int GetCredits()
    {
        return credits;
    }
    private void Initialize()
    {
        appsPerSecText.SetText(appsPerSec.ToString());
        yieldText.SetText((yield * 100).ToString() + "%");
        processingTimeText.SetText(processingTime.ToString() + "s");
        string maxStageString = "";
        if (maxStage == 0) maxStageString = "considered";
        else if (maxStage == 1) maxStageString = "interview 1";
        else if (maxStage == 2) maxStageString = "interview 2";
        else if (maxStage == 3) maxStageString = "interview 3";
        else if (maxStage == 4) maxStageString = "accepted";
        maxStageText.SetText(maxStageString);
        creditsText.SetText(credits.ToString());
    }
    public void AddAppsPerSec(float amount)
    {
        appsPerSec += amount;
        appsPerSecText.SetText(appsPerSec.ToString());
    }
    public void AddYield(float amount)
    {
        yield += amount;
        yieldText.SetText((yield * 100).ToString() + "%");
    }
    public void IncreaseMaxStage()
    {
        maxStage += 1;
        string maxStageString = "";
        if (maxStage == 0) maxStageString = "considered";
        else if (maxStage == 1) maxStageString = "interview 1";
        else if (maxStage == 2) maxStageString = "interview 2";
        else if (maxStage == 3) maxStageString = "interview 3";
        else if (maxStage == 4) maxStageString = "accepted";
        maxStageText.SetText(maxStageString);
    }
    public void SubtractProcessingTime(float amount)
    {
        processingTime -= amount;
        processingTimeText.SetText(processingTime.ToString() + "s");
    }
    public void AddCredits(int amount)
    {
        credits += amount;
        creditsText.SetText(credits.ToString());
        creditsText.GetComponent<Animation>().Play();
    }
    public void SendApplication()
    {
        sendAppAnimation.Stop();
        sendAppAnimation.Play();
        Progress(0);
    }
    public void Progress(int stage)
    {
        Instantiate(progressMeter, meterContainers[stage].transform).Begin(processingTime, yield, stages[stage]);
    }
}
