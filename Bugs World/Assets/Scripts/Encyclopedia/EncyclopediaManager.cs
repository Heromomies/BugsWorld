using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public static EncyclopediaManager Instance;
    public GameObject panel;
    public TextMeshProUGUI feedbackEncyclopedia;
    public int nmbInTheCollection = 10;
    private int _collectionFull;

    #region Singleton

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    public void AddObjectsToEncyclopedia(Insect obj)
    {
        //si l'objet n'est pas trouvé, on le trouve et on l'affiche dans l'encyclopédie
        if (!obj.found)
        {
            feedbackEncyclopedia.text = $"You find a {obj.name}";
            StartCoroutine(WaitForText());
            Debug.Log("found");
            obj.found = true;
            _collectionFull++;
        }
        else
        {
            feedbackEncyclopedia.text = "You have already found this insect";
            StartCoroutine(WaitForText());
        }
    }
    public void Update()
    {
        if (_collectionFull >= nmbInTheCollection)
        {
           //TODO fin du jeu
        }
    }
    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(3);
        feedbackEncyclopedia.text = "";
    }
}
