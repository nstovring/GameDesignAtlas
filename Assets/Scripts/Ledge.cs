using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Ledge : MonoBehaviour {

    public List<Transform> Ledges;
	// Use this for initialization
	void Start () {
		
	}
	
    public Vector3 GetHangingPosition(Vector3 playerPos)
    {
        Transform[] orderedLedges = Ledges.OrderBy(x => Vector3.Distance(x.position, playerPos)).ToArray();
        return orderedLedges[0].position;
    } 

	// Update is called once per frame
	void Update () {
		
	}
}
