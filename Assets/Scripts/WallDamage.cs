using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamage : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem=GetComponent<ParticleSystem>();
        _particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(_particleSystem.isStopped)Destroy(gameObject);
    }
}
