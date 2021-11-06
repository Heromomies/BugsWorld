using System;
using System.Collections;
using System.Collections.Generic;
using Pinwheel.Griffin.Wizard;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<GameObject> _allInteractableObject;

    [SerializeField] private float timerForSearchObject = 1f;

    private float _nexTimerForSearchObject;

    [SerializeField] private Transform closestObject;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SearchForInteractableObject();
    }

    void SearchForInteractableObject()
    {
        if (Time.time > _nexTimerForSearchObject)
        {
            if (_allInteractableObject.Count > 0)
            {
                closestObject = GetClosestObject(_allInteractableObject.ToArray());
                closestObject.gameObject.layer = LayerMask.NameToLayer("Outlines");
                _nexTimerForSearchObject = Time.time + timerForSearchObject;
            }
        }
    }

    Transform GetClosestObject(GameObject[] interactableObject)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in interactableObject)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }

            potentialTarget.layer = LayerMask.NameToLayer("Default");

        }

        return bestTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (!_allInteractableObject.Contains(other.gameObject))
            {
                _allInteractableObject.Add(other.gameObject);
            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (_allInteractableObject.Contains(other.gameObject))
            {
                GameObject distantObject = other.gameObject;
                
                distantObject.gameObject.layer = LayerMask.NameToLayer("Default");
                closestObject = null;
                _allInteractableObject.Remove(distantObject);
            }
            
        }
    }
}