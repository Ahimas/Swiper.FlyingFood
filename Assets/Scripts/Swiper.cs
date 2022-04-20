using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    private Camera cam;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        trail.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive) ClickAndSwipe();
       
    }

    void ClickAndSwipe()
    {
        Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        trail.transform.position = new Vector3(pos.x, pos.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            trail.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            trail.gameObject.SetActive(false);
        }
    }
}
