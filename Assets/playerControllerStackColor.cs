using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerStackColor : MonoBehaviour
{
    [SerializeField] Color myColor;
    [SerializeField] Renderer[] myRends;

    [SerializeField] bool isPlaying;
    [SerializeField] float forwardSpeed;
    Rigidbody rb;

    [SerializeField] float sideLerpSpeed;

    Transform parentPickup;
    [SerializeField] Transform stackPosition;
    // Start is called before the first frame update
    void Start()
    {
        SetColor(myColor);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            MoveForward();
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            if (isPlaying == false)
            {
                isPlaying = true;
            }
            MoveSIdeways();
        }
    }

    void SetColor(Color colorOn)
    {
        myColor = colorOn;
        for (int i = 0; i < myRends.Length; i++)
        {
            myRends[i].material.SetColor("color", myColor);
        }
    }

    void MoveForward()
    {
        rb.velocity = Vector3.forward * forwardSpeed;
    }

    void MoveSIdeways()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(hit.point.x, transform.position.y, transform.position.z), sideLerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "PickUp")
        {
            Transform otherTransform = other.transform.parent;

            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            other.enabled = false;
            if (parentPickup == null)
            {
                parentPickup = otherTransform;
                parentPickup.position = stackPosition.position;
                parentPickup.parent = stackPosition;
            } else
            {
                parentPickup.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPosition.position;
                otherTransform.parent = stackPosition;
            }
        }
    }
}
