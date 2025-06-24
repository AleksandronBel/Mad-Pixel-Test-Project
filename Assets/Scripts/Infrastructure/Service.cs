using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using R3;
using UnityEngine.UI;

public class Service : MonoBehaviour
{
    Button button;

    [Inject]
    void Construct()
    {
    }
}
