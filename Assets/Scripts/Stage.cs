using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    [SerializeField] private int nextStageIndex;
    [SerializeField] private TMP_Text failCount;
    [SerializeField] private TMP_Text passCount;
    [SerializeField] private int maxStage;
    private int fails;
    private int passes;
    public bool final;
    [SerializeField] private UnityEvent finalEvent;
    public void Trigger(float yield)
    {
        float threshold = Random.Range(0f, 1f);
        if (threshold <= yield)
        {
            if (GameManager.Instance.GetMaxStage() >= maxStage)
            {
                Success();
            }
            else Fail();
        }
        else
        {
            Fail();
        }
    }
    private void Success()
    {
        if (final)
        {
            finalEvent.Invoke();
            return;
        }
        GameManager.Instance.Progress(nextStageIndex);
        passes += 1;
        DisplayCounts();
        GameManager.Instance.AddCredits(1);
    }
    private void Fail()
    {
        if (final)
        {
            return;
        }
        fails += 1;
        DisplayCounts();
    }
    private void DisplayCounts()
    {
        failCount.SetText(fails.ToString());
        passCount.SetText(passes.ToString());
    }
}
