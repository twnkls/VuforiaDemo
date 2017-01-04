using UnityEngine;
using System.Collections;
using Vuforia;

public class TrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject[] _objects;
    private TrackableBehaviour _trackableBehaviour;

    private void Awake()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();

        if (_trackableBehaviour)
            _trackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj)
                obj.SetActive(true);
        }
        
        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost()
    {
        foreach (GameObject obj in _objects)
        {
            if (obj)
                obj.SetActive(false);
        }

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
    }
}