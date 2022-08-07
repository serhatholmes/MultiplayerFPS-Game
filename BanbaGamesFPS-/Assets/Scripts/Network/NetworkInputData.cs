using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movementInput;

    public Vector3 aimForwardVector;
    public float rotationInput;
    public NetworkBool isJumpedPressed;
    public NetworkBool isFireButtonPressed;


}
