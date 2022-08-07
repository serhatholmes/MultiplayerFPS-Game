using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponHandler : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring { get; set; }

    public ParticleSystem fireParticleSystem;

    float lastTimeFired = 0;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork(){

        // get the input from the network
        if(GetInput(out NetworkInputData networkInputData)){

            if(networkInputData.isFireButtonPressed){
                Fire(networkInputData.aimForwardVector);
            }
        }
    }

    void Fire(Vector3 aimForwardVector){

        // limit fire rate
        if(Time.time - lastTimeFired < 0.15f ){
            return;
        }
        StartCoroutine(FireEffectCO());

        lastTimeFired = Time.time;
    }

    IEnumerator FireEffectCO(){


        isFiring = true;

        fireParticleSystem.Play();

        yield return new WaitForSeconds(0.1f);

        isFiring = false;

    }
    static void OnFireChanged(Changed<WeaponHandler> changed){

        Debug.Log($"{Time.time} OnFireChanged value {changed.Behaviour.isFiring}");

        bool isFiringCurrent = changed.Behaviour.isFiring;

        //load the old value
        changed.LoadOld();

        bool IsFiringOld = changed.Behaviour.isFiring;

        if(isFiringCurrent && !IsFiringOld){
            changed.Behaviour.OnFireRemote();
        }


        
    }

    void OnFireRemote(){

        if(!Object.HasInputAuthority){
            fireParticleSystem.Play();
        }
    }
}
