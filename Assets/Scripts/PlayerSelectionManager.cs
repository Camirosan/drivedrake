using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager instance;

    //this script is in charge of the character selection data,
    //this object pass trought the scenes to keep the data of the character selected
    private int dracSelection;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDracSelection(int drac)
    {
        dracSelection = drac;
    }
    public int GetDracSelection()
    {
        return dracSelection;
    }
}
