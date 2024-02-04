using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [SerializeField] private AnimationCurve slowCurve;
    [SerializeField] private float slowTime;

    public delegate void TimeControlDelegate();
    public static TimeControlDelegate timeControl;

    // Update is called once per frame
    void StartSlowdown()
    {
        StartCoroutine(SlowTime(slowTime));
    }
    IEnumerator SlowTime(float time)
    {
        UIManager.enable?.Invoke();
        while (Time.timeScale >= 0) 
        {
            time -= Time.unscaledDeltaTime;
            Time.timeScale = slowCurve.Evaluate(time);
            yield return null;
        }
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        timeControl += StartSlowdown;
    }
    private void OnDisable()
    {
        timeControl -= StartSlowdown;
    }
}
