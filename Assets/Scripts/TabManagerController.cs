using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManagerController : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=bpB6_tDQqU0&ab_channel=Harrazmo
    [SerializeField] public GameObject[] tabs;
    public void onTabSwitch(GameObject tab)
    {
        tab.SetActive(true);

        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] != tab)
            {
                tabs[i].SetActive(false);
            }
        }
    }
}