using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeInsect : MonoBehaviour
{
	public Insect soInsect;
	void OnMouseDown() // Quand on clique sur l'insecte, il détecte si l'insecte à déjà été trouver
	{
		Debug.Log(soInsect.name);
		EncyclopediaManager.Instance.AddObjectsToEncyclopedia(soInsect);
		Debug.Log(soInsect.found);
	}

	private void OnApplicationQuit() // Quand on quitte l'application, les insectes ne sont plus trouvés
	{
		soInsect.found = false;
	}
}
