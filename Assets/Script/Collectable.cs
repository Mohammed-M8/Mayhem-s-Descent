using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float XRotate , YRotate , ZRotate;
    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        gameObject.transform.Rotate(new Vector3(XRotate , YRotate , ZRotate));
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            scoreManager.CollectCollectable();
        }
    } */

     private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            scoreManager.CollectCollectable();
        }
    }
}
