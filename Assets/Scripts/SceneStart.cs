using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStart : MonoBehaviour
{
    public Material mat;
    public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in gameObjects)
        {
            g.GetComponent<Renderer>().material = mat;
        }
    }
}
