using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int mazeNumber = Random.Range(0, 10);
        Debug.Log("Maze : " + mazeNumber);
        transform.GetChild(mazeNumber).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
