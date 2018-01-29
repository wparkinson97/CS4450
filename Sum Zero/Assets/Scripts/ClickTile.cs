using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour {

    public Sprite tileGliph;
    public Sprite tileOpen;

    public bool isOpen;

    public int myId = 0;
    public int myNumber = 0;

    private bool mouseIsOver = false;

    static public GameObject frameRef;

    // Use this for initialization
    void Start () {
        myNumber = Random.Range(-9, 10);
        isOpen = true;
        CloseTile();
        FrameManager.tileArray[myId] = gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (!mouseIsOver)
                return;
            if (isOpen)
                CloseTile();
            else
                OpenTile();
        }

        if (Input.GetMouseButtonDown(1))

            CloseTile();
    }
    public void OpenTile()
    {
        
        GetComponent<SpriteRenderer>().sprite = tileOpen;
        GetComponentInChildren<TextMesh>().text = myNumber.ToString();
        isOpen = true;
        frameRef.GetComponent<FrameManager>().PlayTile(myId, myNumber);
    }
    public void CloseTile()
    {
        GetComponent<SpriteRenderer>().sprite = tileGliph;
        isOpen = false;
        GetComponentInChildren<TextMesh>().text = "";
    }
    void OnMouseEnter()
    {
        mouseIsOver = true;
    }

    void OnMouseExit()
    {
        mouseIsOver = false;
    }
}
