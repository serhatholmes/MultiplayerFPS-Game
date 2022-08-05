using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;

    CharacterMovementHandler characterMovementHandler;

    private void Awake() {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //view input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y")* -1; // invert mouse;

        characterMovementHandler.SetViewInputVector(viewInputVector);

        //move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        isJumpButtonPressed = Input.GetButtonDown("Jump");
    }

    public NetworkInputData GetNetworkInput(){
        NetworkInputData networkInputData = new NetworkInputData();

        //view data
        networkInputData.rotationInput = viewInputVector.x;

        networkInputData.movementInput = moveInputVector;

        //jump data
        networkInputData.isJumpedPressed = isJumpButtonPressed;

        return networkInputData;
    }
}
