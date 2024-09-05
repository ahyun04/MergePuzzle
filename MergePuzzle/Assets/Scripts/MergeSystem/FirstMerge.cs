using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMerge : MonoBehaviour
{
    float endtimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Dialogue2");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(endtimer);
     //if(Time.timeScale == 0)
     //   {
     //       endtimer += Time.unscaledDeltaTime;
     //   }
     //if (endtimer >= 4.0f)
     //   {
     //       SceneManager.LoadScene("Dialogue2");
     //   }
    }
}
