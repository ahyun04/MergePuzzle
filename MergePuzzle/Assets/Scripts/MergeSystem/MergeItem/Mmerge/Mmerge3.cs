using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mmerge3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MergeManager").GetComponent<MergeReader>().M3Count += 1;
    }
    void OnDestroy()
    {
        GameObject.Find("MergeManager").GetComponent<MergeReader>().M3Count -= 1;
    }
}
