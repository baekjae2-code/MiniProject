using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitsData", menuName = "Scriptable Objects/UnitsData")]
public class UnitsData : ScriptableObject
{
    public List<UnitData> list;
}
