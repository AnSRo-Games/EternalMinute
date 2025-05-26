using UnityEngine;
using System.Collections;

public class DelayEnable : MonoBehaviour
{
    public GameObject target;
    public float delay = 0.05f;

    IEnumerator Start()
    {
        target.SetActive(false);
        yield return new WaitForSeconds(delay);
        target.SetActive(true);
    }
}
