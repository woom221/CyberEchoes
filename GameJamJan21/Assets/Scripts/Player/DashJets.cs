using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashJets : MonoBehaviour
{
    public ParticleSystem[] jets;

    public void Shoot()
    {
        foreach (var jet in jets)
        {
            jet.Play();
        }
    }

    public void Stop()
    {
        foreach (var jet in jets)
        {
            jet.Stop();
        }
    }

    public void SetStartSpeed(float speed=0)
    {
        foreach (var jet in jets)
        {
            var main = jet.main;
            main.startSpeed = speed;
        }
    }
}
