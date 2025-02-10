using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;

    public Vector3 this[int index] => _wayPoints[index].transform.position;
}
