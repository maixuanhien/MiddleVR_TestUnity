using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        Vector3 up = player.up;
        Vector3 forward = player.forward;
        Vector3 vectorScale = Vector3.Cross(up, forward);
        Vector3 toCube = transform.position - player.position;
        if (Vector3.Dot(vectorScale, toCube) < 0f)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
