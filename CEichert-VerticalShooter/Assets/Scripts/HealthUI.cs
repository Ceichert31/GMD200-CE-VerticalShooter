using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image[] healthUI = new Image[5];

    [SerializeField] private RawImage background;

    private int index = 0;

    public delegate void RemoveHealthBar();
    public static RemoveHealthBar removeHealthUI;

    void RemoveHealth()
    {
        if (index > 4)
            return;

        healthUI[index].color = Color.white;
        index++;
    }
    private void OnEnable()
    {
        removeHealthUI += RemoveHealth;
    }
    private void OnDisable()
    {
        removeHealthUI -= RemoveHealth;
    }
}
