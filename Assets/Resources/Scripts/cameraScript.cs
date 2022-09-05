using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private float shakeTime = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControllerVariables.gameEnd == true && shakeTime > 0)
        {
            gameObject.transform.position = new Vector2(Mathf.Sin(9)*2.5f, transform.position.y);
            shakeTime -= 1 * Time.deltaTime;
        }
    }
}
