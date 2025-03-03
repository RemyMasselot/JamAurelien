using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    [SerializeField] private GameObject ring;
    [SerializeField] private SplineSampler splineSampler;
    [SerializeField, Range(0f, 1f)] private float sliderPos = 0f;

    void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.value = 0;
        percentText.text = (int)(slider.value) + " %";
        StartCoroutine(AddPercentPart1());
    }

    IEnumerator AddPercentPart1()
    {
        while (slider.value < 60)
        {
            slider.value += Random.Range(10, 20);
            percentText.text = (int)(slider.value) + " %";
            float _delay = Random.Range(0.1f, 0.4f); ;
            
            if (slider.value >= 60)
            {
                StartCoroutine(AddPercentPart2());
            }
            else
            {
                yield return new WaitForSeconds(_delay);
            }
        }
    }
    IEnumerator AddPercentPart2()
    {
        while (slider.value < 100)
        {
            slider.value += Random.Range(5, 10);
            percentText.text = (int)(slider.value) + " %";
            float _delay = Random.Range(1, 3); ;

            if (slider.value >= 100)
            {
                SpawnRing();
            }
            else
            {
                yield return new WaitForSeconds(_delay);
            }
        }
    }

    private void SpawnRing()
    {
        //sliderPos = splineSampler.EvaluatePosition(splineSampler.m_time, out Vector3 position);
        ring.gameObject.SetActive(true);
    }
}
