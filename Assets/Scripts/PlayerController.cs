using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;

    private int count;
    public TextMeshProUGUI countText;

    public GameObject winTextObject;

    // Start is called before the first frame update
    void Start()
    {
        winTextObject.gameObject.SetActive(false);

        rb = GetComponent<Rigidbody>();

        count = 0;
        SetCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText(); 
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        countText.color = Color.red;

        if (count >= 5)
        {
            winTextObject.SetActive(true);
        }
    }
}
