using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MergeReader : MonoBehaviour
{
    public int M3Count = 0;
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Mmerge_1")
        {
            if(M3Count == 2)
            {
                SceneManager.LoadScene("M1Dialogue");
            }
        }
    }
}
