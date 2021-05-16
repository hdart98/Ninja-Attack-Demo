using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolowPlayer : MonoBehaviour
{
    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player)
        gameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, gameObject.transform.position.z);
    }

    
}
