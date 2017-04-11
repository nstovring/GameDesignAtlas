using UnityEngine;
using System.Collections;



[ExecuteInEditMode] 
public class CircleWallGridGenerator : MonoBehaviour
{
    public float numObjects;
    public int radius;
    public int amount;
    public float heightOffset;

    public GameObject prefab;

    void Start()
    {
        Vector3 center = this.transform.position;
        float tmp = 0;
        float tmp2 = 360 / numObjects;

        for (int j = 0; j < amount; j++) {

            for (int i = 0; i < numObjects; i++)
            {


                Vector3 pos = RandomCircle(center, radius, tmp);
                // make the object face the center
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);

                print(pos.y);

                GameObject shelf = Instantiate(prefab, pos, rot);
                shelf.transform.parent = transform;
                tmp += tmp2;
                print(tmp);
            }
            print(tmp2);

            radius += 2;

            //center.y += heightOffset;

        }
     



    }

   Vector3 RandomCircle (Vector3 center, float radius, float ang) {
        Vector3 pos; 
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }
}
