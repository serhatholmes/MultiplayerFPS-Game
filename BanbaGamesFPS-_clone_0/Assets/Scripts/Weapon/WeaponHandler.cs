using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponHandler : NetworkBehaviour 
{
    [Networked(OnChanged =nameof(OnFireChanged))]
    public bool isFiring { get; set; }

    public ParticleSystem fireParticleSystem;
    public Transform aimPoint;
    public LayerMask collisionLayers;

    float lastTimeFired = 0;
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        //get the input from the network
        if(GetInput(out NetworkInputData networkInputData)){
            if (networkInputData.isFireButtonPressed){

                Fire(networkInputData.aimForwardVector);
            }
        }
    }

    void Fire(Vector3 aimForwardVector){

        // limit fire rate
        if(Time.time - lastTimeFired < 0.15f){
            return;
        }

        StartCoroutine(FireeEffectCO());

        Runner.LagCompensation.Raycast(aimPoint.position,aimForwardVector,100,Object.InputAuthority, out var hitInfo,collisionLayers, HitOptions.IncludePhysX);

        float hitDistance = 100;
        bool isHitOtherPlayer = false;

        if(hitInfo.Distance > 0){
            hitDistance = hitInfo.Distance;
        }
        if(hitInfo.Hitbox != null){

            Debug.Log($"{Time.time} hit hitbox {hitInfo.Hitbox.transform.root.name}");
            
            isHitOtherPlayer = true;
        }
        else if(hitInfo.Collider != null){

            Debug.Log($"{Time.time} hit PhysX collider {hitInfo.Collider.transform.name}");
        }


        // debug
        if(isHitOtherPlayer){
            Debug.DrawRay(aimPoint.position,aimForwardVector * hitDistance, Color.red,1);

        }
        else{
            Debug.DrawRay(aimPoint.position,aimForwardVector * hitDistance, Color.green,1);
        }


        lastTimeFired = Time.time;
    }

    IEnumerator FireeEffectCO(){

        isFiring = true;
        fireParticleSystem.Play();
        yield return new WaitForSeconds(0.09f);
        isFiring = false;
    }

    static void OnFireChanged(Changed<WeaponHandler> changed){

        Debug.Log($"{Time.time} OnFireChanged value {changed.Behaviour.isFiring}");

        bool isFiringCurrent = changed.Behaviour.isFiring;

        //load the old value
        changed.LoadOld();

        bool isFiringOld = changed.Behaviour.isFiring;

        if(isFiringCurrent && !isFiringOld){
            changed.Behaviour.OnFireRemote();
        }
    }

    void OnFireRemote(){

        if(!Object.HasInputAuthority){
            fireParticleSystem.Play();
        }

    }
}
