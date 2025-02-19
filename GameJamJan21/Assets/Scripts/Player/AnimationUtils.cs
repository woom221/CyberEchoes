using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AnimationUtils : MonoBehaviour
{
    private DashJets _jets;

    private Controller _playerController;
    private Animator _animator;

    private bool _holdPosition = true;
    private bool _transition;

    private bool _landing;

    public bool Landing
    {
        get => _landing;
        set => _animator.SetBool(Animations.Landing, _landing = value);
    }

    public bool Dashing
    {
        set => _animator.SetBool(Attributes.Dashing, value);
    }

    public float XSpeed
    {
        set => _animator.SetFloat(Attributes.XSpeed, value);
    }

    public float YSpeed
    {
        set => _animator.SetFloat(Attributes.YSpeed, value);
    }

    private float _animationSpeed;

    public float AnimationSpeed
    {
        get => _animationSpeed;
        set => _animator.SetFloat(Attributes.AnimationMultiplier, _animationSpeed = value);
    }

    void Awake()
    {
        _jets = GetComponentInParent<DashJets>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<Controller>();
    }


    private void LateUpdate()
    {
        // catch the falling edge
        var t = _animator.IsInTransition(0);
        if (!t && _transition)
        {
            // in the case an animation was quit early
            // Debug.Log("switched state");
            _holdPosition = true;
            _jets.SetStartSpeed();
            Landing = false;
            _playerController.SpawnGun();
        }

        _transition = t;


        if (_holdPosition)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void JetLength(float length)
    {
        _jets.SetStartSpeed(length);
    }

    public void AllowAnimationMovement(int movement)
    {
        _holdPosition = movement == 0;
    }

    public void PulloutGun()
    {
        _playerController.SpawnGun();
    }

    public void PlayLanding()
    {
        Landing = true;
        AnimationSpeed = 1;
        Play(Animations.Landing);

        FMODUnity.RuntimeManager.PlayOneShot("event:/Actions/MechLanding", GetComponent<Transform>().position);
    }

    public void Play(string animationName)
    {
        _animator.Play(animationName);
    }
}