using System;
using System.Collections;
using System.Collections.Generic;
using Pinwheel.Griffin.Wizard;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class PlayerInteraction : MonoBehaviour
{
    public List<GameObject> _allInteractableObject;

    [SerializeField] private float timerForSearchObject = 1f;

    private float _nexTimerForSearchObject;

    [SerializeField] private Transform closestObject;

    [SerializeField] private float radiusDetectionObject = 1.5f;

    public bool canInteract;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SearchForInteractableObject();
        if (canInteract && Player.instance.isInteractionPressed)
        {
           StartCoroutine(SearchForInsects());
           
        }
    }

    IEnumerator SearchForInsects()
    {
        canInteract = false;
        float percentage = Random.value;
        Debug.Log(percentage);
        if (percentage > 0.3)
        {
            Debug.Log("You discover a new insect !");
            ResetInteractionObject();
            yield return null;
        }
        else
        {
            Debug.Log("You fail ! and insect is running away");
            ResetInteractionObject();
            yield return null;
        }
    }

    void ResetInteractionObject()
    {
        if (closestObject.transform.childCount > 0)
        {
            Transform[] allChildren = closestObject.GetComponentsInChildren<Transform>();
            foreach (var obj in allChildren)
            {
                obj.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
        _allInteractableObject.Remove(closestObject.gameObject);
        closestObject.gameObject.layer = LayerMask.NameToLayer("Default");
        closestObject = null;
        
    }

    void SearchForInteractableObject()
    {
        //On check tout les x temps si on a un object dans notre list proche du player
        if (Time.time > _nexTimerForSearchObject)
        {
            if (_allInteractableObject.Count > 0)
            {
                closestObject = GetClosestObject(_allInteractableObject.ToArray());
                float dist = Vector3.Distance(transform.position, closestObject.position);
                canInteract = CheckInteractableObjectDistance(dist);
            }

            _nexTimerForSearchObject = Time.time + timerForSearchObject;
        }
    }

    bool CheckInteractableObjectDistance(float distance)
    {
        //Permet de return un bool si le joueur est dans la "range" pour intéragir avec un objet
        bool canPlayerInteract = false;
        if (distance < radiusDetectionObject)
        {
            if (closestObject.transform.childCount > 0)
            {
                Transform[] allChildren = closestObject.GetComponentsInChildren<Transform>();
                foreach (var obj in allChildren)
                {
                    obj.gameObject.layer = LayerMask.NameToLayer("Outlines");
                }
            }

            closestObject.gameObject.layer = LayerMask.NameToLayer("Outlines");
            canPlayerInteract = true;
        }
        else if (distance > radiusDetectionObject)
        {
            if (closestObject.transform.childCount > 0)
            {
                Transform[] allChildren = closestObject.GetComponentsInChildren<Transform>();
                foreach (var obj in allChildren)
                {
                    obj.gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }

            closestObject.gameObject.layer = LayerMask.NameToLayer("Default");
            canPlayerInteract = false;
        }

        return canPlayerInteract;
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
                closestObject = null;
                _allInteractableObject.Remove(distantObject);
            }
        }
    }
}