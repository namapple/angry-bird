using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private SpriteRenderer sr;
    private Camera cam;
    private Vector3 dragOffset;
    private Vector3 initialPosition;
    private bool birdWasLaunched;
    private Rigidbody2D rb;
    private float timeSittingAround;
    [SerializeField] private float launchForce = 250;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (birdWasLaunched && rb.velocity.magnitude <= 0.1f)
        {
            timeSittingAround += Time.deltaTime;
        }
        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            timeSittingAround > 2.5f)
        {
            RestartScene();
        }
    }

    private void OnMouseDown()
    {
        sr.color = Color.red;
        dragOffset = transform.position - GetMousePos();
    }
    private void OnMouseUp()
    {
        sr.color = Color.white;
        Vector2 directitonToInitialPosition = initialPosition - transform.position;
        rb.AddForce(directitonToInitialPosition * launchForce);
        rb.gravityScale = 1;
        birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void RestartScene()
    {
        string curentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(curentSceneName);
    }
}

