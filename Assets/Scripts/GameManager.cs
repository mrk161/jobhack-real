using System;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float appsPerSec;
    [SerializeField] private float yield;
    [SerializeField] private string maxStage;
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
    private void Initialize()
    {
        appsPerSecText.SetText(appsPerSec.ToString());
        yieldText.SetText((yield * 10).ToString() + "%");
        processingTimeText.SetText(processingTime.ToString() + "s");
        maxStageText.SetText(maxStage);
        creditsText.SetText(credits.ToString());
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
