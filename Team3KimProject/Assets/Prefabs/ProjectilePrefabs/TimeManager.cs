
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowOnExplosion = 0.05f;
    public float slowdownLength = 2f;

    private void OnEnable()
    {
        Shooting.SlowMotion += SlowMotion;
        CheckTrigger.SlowOnExplosionMotion += SlowOnExplosion;
    }
    private void OnDisable()
    {
        Shooting.SlowMotion -= SlowMotion;
        CheckTrigger.SlowOnExplosionMotion -= SlowOnExplosion;
    }

    private void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    void SlowOnExplosion()
    {
        Time.timeScale = slowOnExplosion;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
