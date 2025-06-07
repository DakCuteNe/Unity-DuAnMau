using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    void Start()
    {
        ActiveTab(0);
    }

    public void ActiveTab(int Tabno)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImages[i].color = Color.grey;
        }
        pages[Tabno].SetActive(true);
        tabImages[Tabno].color = Color.white;
    }
}
