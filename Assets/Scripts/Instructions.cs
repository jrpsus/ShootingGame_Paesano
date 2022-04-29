using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject a;
    void Start()
    {
        a.SetActive(false);
    }
    public void OpenInstructions()
    {
        a.SetActive(true);
    }
    public void CloseInstructions()
    {
        a.SetActive(false);
    }
}
