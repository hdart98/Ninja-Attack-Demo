using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private int extraJump;
    [SerializeField] private Joystick joystick;

   private PlayerController playerCtrl;
    private float moveHorizontal;
    private int curExtraJump;

    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // player run
        if (joystick.Horizontal > 0) moveHorizontal = 1;
        else if (joystick.Horizontal < 0) moveHorizontal = -1;
        else moveHorizontal = 0;
        playerCtrl.run(moveHorizontal);

        // Player jump;
        if (joystick.Vertical > 0.5f && playerCtrl.getGrounded())
        {
            playerCtrl.jump();
        }
        // player attack
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerCtrl.attack(true);
        } else
        {
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                playerCtrl.attack(false);
            }
        }

        // player skill

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerCtrl.skill(true);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                playerCtrl.skill(false);
            }
        }
    }
}
