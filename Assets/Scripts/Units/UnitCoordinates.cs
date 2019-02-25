using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCoordinates : MonoBehaviour {

    public int x;
    public int y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUnitCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
