using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CrackedSlabData", menuName = "CrackedSlabData")]
public class CrackedSlabData : ScriptableObject
{
    public List<Mesh> statesMeshes = new List<Mesh>();
}
