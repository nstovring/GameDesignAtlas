using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclussionTest : MonoBehaviour {


    public GameObject[] Bookshelfs;
    public float DistanceLimit;

	// Use this for initialization
	void Start () {

        Bookshelfs = GameObject.FindGameObjectsWithTag("Bookshelf");
        print(Bookshelfs.Length);

    }

    // Update is called once per frame
    void Update () {


        foreach (GameObject Bookshelf in Bookshelfs) {

            float dist = Vector3.Distance(Bookshelf.transform.position, this.transform.position);

            if (dist <= DistanceLimit)
            {
                Bookshelf.SetActive(false);
                print("fas");
            }
            else {
                Bookshelf.SetActive(true);
            }
        }





    }
}
