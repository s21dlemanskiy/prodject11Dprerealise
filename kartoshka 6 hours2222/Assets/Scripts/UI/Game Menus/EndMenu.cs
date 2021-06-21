using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text place;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int num = GameObject.Find("Camera1").GetComponent<FollowPlayer>().num;
        score.text = "With score: " + GameObject.Find("Playernums2").GetComponent<ScoreScript>().scor[num].ToString();
        place.text = "You are placed: " + GameObject.Find("Playernums2").GetComponent<ScoreScript>().findPlace(num).ToString();
    }
}
