using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour{
    public Color statusColor, warningColor, dangerColor;
    [SerializeField] private Slider lifebarSlider;
    [SerializeField] private Image bgSlider, statusSlider;

    void Awake(){
        bgSlider.color = dangerColor;
        statusSlider.color = statusColor;
    }

    public void SetSliderEnergy(int energy){
        energy = Mathf.Clamp(energy, 0, 100);
        lifebarSlider.value = energy;
        statusSlider.color = Color.Lerp(warningColor, statusColor, Mathf.Clamp(energy - 10, 0, 100) / 100f);
    }

}
