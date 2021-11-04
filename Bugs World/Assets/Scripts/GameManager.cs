using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject encyclopedia;

	private int input;
    // Update is called once per frame
    void Update()
    {
	    
	    if (Input.GetKeyDown(KeyCode.I) && input == 0)
	    {
		    encyclopedia.SetActive(true);
		    input++;
	    }
	    else if(Input.GetKeyDown(KeyCode.I) && input >= 0)
	    {
		    encyclopedia.SetActive(false);
		    input = 0;
	    }
    }
}
