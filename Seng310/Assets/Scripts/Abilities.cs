using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    private static GameObject Ab;
    public static bool active = true;
    private void Start()
    {
        Ab = GameObject.Find("DropDown");
    }
    public static void OnClick()
    {
        if (active)
        {
            Ab.SetActive(false);
            active = false;
        }
        else
        {
            Ab.SetActive(true);
            active = true;
        }
    }
}
