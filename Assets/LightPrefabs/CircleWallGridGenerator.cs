using UnityEngine;
using System.Collections;



[ExecuteInEditMode] 
public class CircleWallGridGenerator : MonoBehaviour
{
    public int numObjects;
    public int radius;
    public GameObject prefab;

    void Start()
    {
        Vector3 center = transform.position;
        float tmp = 0;
        float tmp2 = 360 / numObjects;

        for (int i = 0; i < numObjects; i++)
        {
       

            Vector3 pos = RandomCircle(center, radius, tmp);
            // make the object face the center
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

            Instantiate(prefab, pos, rot);
            tmp += tmp2;

        }



    }

   Vector3 RandomCircle (Vector3 center, float radius, float ang) { // create random angle between 0 to 360 degrees 
        //var ang = Random.value * 360;

        Vector3 pos; 
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
