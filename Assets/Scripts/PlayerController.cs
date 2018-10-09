using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Public breytur svo þær eru breytanlegar innan editorsins
    public float movementSpeed = 0.1f;
    public Rigidbody2D rb;
    public Text scoreCountText;
    public Text gameOverText;
    public Text gameOverSecText;
    public Text finalScoreText;
    public Button restartButton;
    public Button quitButton;
    public SpriteRenderer renderRef;

    public AudioClip pickupSound;
    public AudioSource source;

    // Private Breytur
    private int scoreCount = 0;
    private Vector3 mousePosition;

    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;


    private void Start()
    {
        setCountText();

        // Felur allt UI sem á ekki að vera til staðar
        gameOverText.enabled = false;
        gameOverSecText.enabled = false;
        finalScoreText.enabled = false;
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    private void FixedUpdate () {

        // Breytir punktnum sem bendillinn er á skjánum í stað í leiknum
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Debug.Log(mousePosition);

        // Færir Playerinn í áttina að bendlinum
        rb.position = Vector3.MoveTowards(rb.position, mousePosition, movementSpeed);
    }

    // Þegar playerinn snertir trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickupPoint")
        {
            source.PlayOneShot(pickupSound, 1);

            collision.gameObject.SetActive(false); // Felur punktinn sem playerinn var að fara yfir
            scoreCount++;                          // Bætir við stigi
            setCountText();
            
            rb.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f); // Stækkar playerinn  
            Camera.main.orthographicSize += 0.5f;                     // Víðkar myndavélina
            scoreCountText.fontSize += 3;                             // Stækkar score textann
        }else if (collision.gameObject.tag == "EvilPickupPoint")
        {
            gameOver();
        }
    }

    // Game Over fall
    private void gameOver()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;  // Frís stöðu playersins
        renderRef.color = new Color(1, 0, 0);                    // Breytir lit playersins í rauðann
        
        // Enablar allt í game over skjánum
        gameOverText.enabled = true;                            
        gameOverSecText.enabled = true;
        finalScoreText.text = "Final Score: " + scoreCount.ToString();
        finalScoreText.enabled = true;
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

    }

    private void setCountText()
    {
        scoreCountText.text = scoreCount.ToString();  // Breytir stiga textanum í stiga töluna
    }

}
