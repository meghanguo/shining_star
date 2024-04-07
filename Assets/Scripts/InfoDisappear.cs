using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisappear : MonoBehaviour
{
    public Canvas canvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
