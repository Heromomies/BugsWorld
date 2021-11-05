using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaManager : MonoBehaviour
{
    public static EncyclopediaManager Instance;
    public GameObject panel;
    public TextMeshProUGUI feedbackEncyclopedia;
    public int nmbInTheCollection = 10;

    public List<GameObject> placesForInsect;
    
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

            placesForInsect[obj.id].GetComponent<Image>().sprite = obj.image;
            placesForInsect[obj.id].GetComponentInChildren<TextMeshProUGUI>().text = obj.name;
            placesForInsect[obj.id].GetComponent<TooltipTrigger>().enabled = false;

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
           Debug.Log("J'ai trouvé tous les insectes");
        }
    }
    IEnumerator WaitForText()
    {
        yield return new WaitForSeconds(3);
        feedbackEncyclopedia.text = "";
    }
}
