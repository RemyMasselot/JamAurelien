using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;
    [SerializeField] private RingSpawner ringSpawner;

    

    void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.value = 0;
        percentText.text = (int)(slider.value) + " %";
        StartCoroutine(AddPercentStarting());
    }

    IEnumerator AddPercentStarting()
    {
        yield return new WaitForSeconds(2);
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
                ringSpawner.SpawnRing();
                //Debug.Log("FINISH");
            }
            else
            {
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}
