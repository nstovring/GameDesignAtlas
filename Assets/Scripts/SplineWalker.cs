using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineWalker : MonoBehaviour
{

    public BezierSpline spline;

    public float duration;
    public bool lookForward;
    public bool stayLevel;
    public SplineWalkerMode mode;

    private bool goingForward = true;

    private float progress;

    private void Update()
    {
        if (goingForward)
        {

            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }

            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }
        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
       
        if(lookForward && stayLevel)
        {
            Debug.Log("should draw line");
            Vector3 tempRot = new Vector3(spline.GetDirection(progress).x, 0f, spline.GetDirection(progress).z);
            transform.LookAt(position + tempRot);
            // Quaternion newRotation = Quaternion.LookRotation(position + spline.GetDirection(progress), Vector3.up);

        }
        else if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }

}
