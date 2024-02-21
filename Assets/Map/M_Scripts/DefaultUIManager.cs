using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUIManager : MonoBehaviour
{
    public static DefaultUIManager instance;
    public GameObject DefaultUI;
    public GameObject InventoryUI;

    private void Awake()
    {
        instance = this;
    }

    public GameObject[] equment;
    // Start is called before the first frame update
    void Start()
    {
        DefaultUI.SetActive(true);
        InventoryUI.SetActive(false);
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            DefaultUI.SetActive(false);
            InventoryUI.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            DefaultUI.SetActive(true);
            InventoryUI.SetActive(false);
        }
    }
}
