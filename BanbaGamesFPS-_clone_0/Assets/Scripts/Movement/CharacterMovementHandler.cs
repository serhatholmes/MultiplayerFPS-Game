using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    Vector2 viewInput;

    //rotation
    float cameraRotationX = 0;

    //other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;
    private void Awake() {
        
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90,90);

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX,0,0);
    }
    public override void FixedUpdateNetwork(){

        // get the input from network
        if(GetInput(out NetworkInputData networkInputData)){

            //rotate the view
            networkCharacterControllerPrototypeCustom.Rotate(networkInputData.rotationInput);


            //move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);
            
            //jump
            if(networkInputData.isJumpedPressed){
                networkCharacterControllerPrototypeCustom.Jump();

                
                
            }
            //check if we've fallen off the world
            CheckFallRespawn();

        }
    }

    void CheckFallRespawn(){
        if(transform.position.y < -10){
            transform.position = Utils.GetRandomSpawnPoint();
        }
    }

    public void SetViewInputVector(Vector2 viewInput){

        this.viewInput = viewInput;

    }
}
