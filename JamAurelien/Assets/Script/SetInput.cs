using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class SetInput : MonoBehaviour
{
    public PlayerMap playerMap;
    public InputAction MoveH;
    public InputAction MoveV;
    public InputAction Jump;
    public InputAction Look;
    public InputAction Back;
    public InputAction _Start;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        // Set input actions
        playerMap = new PlayerMap();
        playerMap.Enable();
        MoveH = playerMap.PLAYER.MOVEH;
        MoveV = playerMap.PLAYER.MOVEV;
        Jump = playerMap.PLAYER.JUMP;
        Look = playerMap.PLAYER.LOOK;
        Back = playerMap.PLAYER.BACK;
        _Start = playerMap.PLAYER.START;
    }
}
