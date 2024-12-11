using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnomaly : MonoBehaviour
{
    [SerializeField] private GameObject[] check;

    void Start()
    {
        for(int i = 1; i < check.Length; i++)
        {
            string anomalyKey = i.ToString();
            if(PlayerPrefs.GetInt(anomalyKey) == 1)
            {
                check[i].SetActive(true);
            }
        }
    }

}
