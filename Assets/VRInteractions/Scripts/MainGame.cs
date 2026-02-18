using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    
    GameObject[] Aliens;
    float popupTimer = 1;
    

    static public bool LeftHandInUse = false;
    static public bool RightHandInUse = false;


    // Start is called before the first frame update
    void Start()
    {
        Aliens = GameObject.FindGameObjectsWithTag("Alien");
    }

    // Update is called once per frame
    void Update()
    {
        updateMoles();
    }

    void updateMoles()
    {
        //edit change times for difficulty. 
        popupTimer -= Time.deltaTime;

        if (popupTimer < 0)
        {
            int rnd = Random.Range(0, Aliens.Length);
            var script = Aliens[rnd].GetComponent<AlienJump>();
            script.pop();

            popupTimer = Random.Range(1, 3);
        }
    }
}
