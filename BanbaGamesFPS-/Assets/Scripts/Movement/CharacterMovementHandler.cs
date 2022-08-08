using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
  
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

    public override void FixedUpdateNetwork(){

        // get the input from network
        if(GetInput(out NetworkInputData networkInputData)){

            //rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            //Cancel out rotation on X axis as we dont want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0,rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;


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

}
