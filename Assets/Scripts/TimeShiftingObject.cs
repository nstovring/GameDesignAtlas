using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShiftingObject : MonoBehaviour {
    float delay;
    public enum TimePeriod{
        Past, Future, Always, NonShifting
    }
    public TimePeriod TimeState = TimePeriod.Always;
    bool isShifting = false;
    int currentTime;
    List<Material> myMaterials;
    public List<Renderer> myRenderer;

    public bool disableObject = false;
    public bool invisibleObject = false;
	// Use this for initialization
	IEnumerator Start () {
        myMaterials = new List<Material>();
        if (myRenderer.Count == 0)
        {
            myMaterials.Add(GetComponent<Renderer>().material);
        }else
        {
            foreach (var item in myRenderer)
            {
                myMaterials.Add(item.GetComponent<Renderer>().material);
            }
        }



		if(LevelManager.lm != null)
        {
            LevelManager.lm.TimeShift += new LevelManager.TimeShiftHandler(CallTimeShift);
            Debug.Log("i am subscribing");
        }
        else
        {
            yield return new WaitForSeconds(delay);
            if (LevelManager.lm != null)
            {
                LevelManager.lm.TimeShift += new LevelManager.TimeShiftHandler(CallTimeShift);
                Debug.Log("Trying again");
            }
        }
    }
    void CallTimeShift(bool time)
    {
        if (!isShifting)
        {
            StartCoroutine(TimeShift(time));
        }
    }
    IEnumerator TimeShift(bool time) 
    {
        isShifting = true;

            switch (TimeState)
            {
                case TimePeriod.Past:
                    {
                    foreach (var myMaterial in myMaterials)
                    {
                        if (time)
                        {
                            if (disableObject)
                            {
                                SetObjectState(false);
                                if (invisibleObject)
                                {
                                    yield break;
                                }
                            }
                            while (myMaterial.GetFloat("_Blend") < 0.95)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 1, 0.1f));
                                myMaterial.SetFloat("_TimeShift", Mathf.Lerp(myMaterial.GetFloat("_TimeShift"), 0, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 1);
                            myMaterial.SetFloat("_TimeShift", 0);
                            if(transform.GetComponent<Collider>() != null)
                            transform.GetComponent<Collider>().enabled = false;
                            
                        }
                        else
                        {
                            if (disableObject)
                                SetObjectState(true);
                            if (invisibleObject)
                            {
                                yield break;
                            }
                            while (myMaterial.GetFloat("_Blend") > 0.05)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 0, 0.1f));
                                myMaterial.SetFloat("_TimeShift", Mathf.Lerp(myMaterial.GetFloat("_TimeShift"), 1, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 0);
                            myMaterial.SetFloat("_TimeShift", 1);
                            if (transform.GetComponent<Collider>()!= null)
                                transform.GetComponent<Collider>().enabled = true;
                          
                        }
                    }
                    break;
                    }
                case TimePeriod.Future:
                    {
                    foreach (var myMaterial in myMaterials)
                    {
                        if (!time)
                        {
                            if (disableObject)
                                SetObjectState(false);
                            if (invisibleObject)
                            {
                                yield break;
                            }
                            while (myMaterial.GetFloat("_Blend") > 0.05)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 0, 0.1f));
                                myMaterial.SetFloat("_TimeShift", Mathf.Lerp(myMaterial.GetFloat("_TimeShift"), 0, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 0);
                            myMaterial.SetFloat("_TimeShift", 0);
                            if (transform.GetComponent<Collider>() != null)
                                transform.GetComponent<Collider>().enabled = false;
                           
                        }
                        else
                        {
                            if (disableObject)
                                SetObjectState(true);
                            if (invisibleObject)
                            {
                                yield break;
                            } 
                            while (myMaterial.GetFloat("_Blend") < 0.95)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 1, 0.1f));
                                myMaterial.SetFloat("_TimeShift", Mathf.Lerp(myMaterial.GetFloat("_TimeShift"), 1, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 1);
                            myMaterial.SetFloat("_TimeShift", 1);
                            if (transform.GetComponent<Collider>() != null)
                                transform.GetComponent<Collider>().enabled = true;
                            
                        }
                    }
                    break;
                    }
                case TimePeriod.Always:
                    {
                    foreach (var myMaterial in myMaterials)
                    {
                        if (time)
                        {
                            while (myMaterial.GetFloat("_Blend") < 0.95)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 1, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 1);
                        }
                        else
                        {
                            while (myMaterial.GetFloat("_Blend") > 0.05)
                            {
                                myMaterial.SetFloat("_Blend", Mathf.Lerp(myMaterial.GetFloat("_Blend"), 0, 0.1f));
                                yield return new WaitForSeconds(delay);
                            }
                            myMaterial.SetFloat("_Blend", 0);
                        }
                    }
                    break;
                    }
            case TimePeriod.NonShifting:
                {
                    break;
                }
            
        }
        yield return new WaitForSeconds(delay);
        isShifting = false;
    }


    void SetObjectState(bool boolean)
    {
        if (transform.GetComponent<Animator>() != null)
        {
            transform.GetComponent<Animator>().enabled = boolean;
        }

        if (transform.GetComponent<Renderer>() != null)
            transform.GetComponent<Renderer>().enabled = boolean;

        if (transform.GetComponent<Collider>() != null)
            transform.GetComponent<Collider>().enabled = boolean;

        if (transform.childCount > 0)
            transform.GetChild(0).gameObject.SetActive(boolean);
    }
}
