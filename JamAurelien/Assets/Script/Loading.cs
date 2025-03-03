using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI percentText;

    [SerializeField] private GameObject Ring;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.value = 0;
        percentText.text = (int)(slider.value) + " %";
        StartCoroutine(AddPercent());
    }

    IEnumerator AddPercent()
    {
        while (slider.value < 100)
        {
            slider.value += Random.Range(1, 20);
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
        Ring.gameObject.SetActive(true);
    }
}
