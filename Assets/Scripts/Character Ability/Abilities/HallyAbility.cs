using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallyAbility : MonoBehaviour
{
    [SerializeField] GameObject pigPrefab;
    [SerializeField] GameObject birdPrefab;
    UnitCoordinates unitCoordinate;

    private void Start()
    {
        unitCoordinate = GetComponent<UnitCoordinates>();
    }



}
