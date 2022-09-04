using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_Controller : MonoBehaviour
{
    private TMPro.TextMeshProUGUI gameScoreText;
    // Start is called before the first frame update
    void Start()
    {
        gameScoreText = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        gameScoreText.text = "Score: " + gameControllerVariables.gameScore;
    }
}
