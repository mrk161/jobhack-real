using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressMeter : MonoBehaviour
{
    private Slider slider;
    private float speed;
    private bool canProgress;
    private Stage targetStage;
    private float yield;
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Begin(float time, float yield, Stage targetStage)
    {
        speed = 1f / time;
        canProgress = true;
        this.targetStage = targetStage;
        this.yield = yield;
    }

    void Update()
    {
        if (!canProgress) return;
        slider.value += speed * Time.deltaTime;
        if (slider.value >= 1f) End();
    }

    private void End()
    {
        canProgress = false;
        targetStage.Trigger(yield);
        Destroy(gameObject);
    }
}
