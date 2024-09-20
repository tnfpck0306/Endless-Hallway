using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AnomalyList", menuName = "Scriptable Object/Anomaly", order = int.MaxValue )]
public class Anomaly : ScriptableObject
{
    public List<int> anomalyList;
}
