using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private float shakeTime = 0.5f;
    private float leftRight = 0;
    private float bounds = .1f;
    private float shakeSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftRight >= bounds || leftRight <= -bounds)
        {
            shakeSpeed *= -1;
        }
        if (gameControllerVariables.gameEnd == true && shakeTime > 0)
        {
            leftRight += shakeSpeed * Time.deltaTime;
            gameObject.transform.position = new Vector2(leftRight, transform.position.y);
            shakeTime -= 1 * Time.deltaTime;
        }
        if (shakeTime <= 0)
        {
            gameObject.transform.position = new Vector2(0, 0);

        }
    }
}
