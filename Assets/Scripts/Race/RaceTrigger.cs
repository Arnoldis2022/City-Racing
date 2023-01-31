using System;
using UnityEngine;

public class RaceTrigger : RotatableObject
{
    public Action<RaceTrack> Entered;
    public Action Exit;

    [SerializeField] private string _triggerTag;
    [SerializeField] private RaceTrack _raceTrack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _triggerTag)
        {
            Entered.Invoke(_raceTrack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == _triggerTag)
        {
            Exit.Invoke();
        }
    }
}
