using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyGate : MonoBehaviour
{
    public GameObject gateObject;
    public GameObject barbarianObject;
    public bool didIt;

    private void Start()
    {
        if (gateObject != null)
        {
            gateObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (ControlPlayer.killCount == 8)
        {
            Destroy(gateObject);
            if(didIt == false)
            {
                ControlPlayer.health = 100;
                didIt = true;
            }

            if (barbarianObject != null)
            {
                Vector3 newPosition = new Vector3(-152.5f, 0.15f, 21.5f);
                barbarianObject.transform.position = newPosition;
            }
        }
    }
}