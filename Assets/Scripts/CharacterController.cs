using UnityEngine;



public class CharacterController : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidbody;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public float flapStrength;
    public GameObject titleScreen;

    public SpriteRenderer wingsRenderer;  // Reference to SpriteRenderer
    public Sprite flapUpSprite;  // Assign in Inspector (wing up)
    public Sprite flapDownSprite;  // Assign in Inspector (wing down)
    private bool gameStarted = false;

    private AudioSource audio;
    [SerializeField] private AudioClip jumpSound;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // Get the SpriteRenderer component from the Wings child object
        wingsRenderer = transform.Find("Wings").GetComponent<SpriteRenderer>();  

        myRigidbody.simulated = false;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive)
        {
            Jump();
            wingsRenderer.sprite = flapUpSprite;
        }
        if (Input.GetKeyUp(KeyCode.Space) && birdIsAlive)
        {
            wingsRenderer.sprite = flapDownSprite; // Change back when the key is released
        }

        //out of box
        if (myRigidbody.position.y > 17 && birdIsAlive)
        {
            logic.GameOver();
            birdIsAlive = false;
        }
        else if (myRigidbody.position.y < -17 && birdIsAlive)
        {
            logic.GameOver();
            birdIsAlive = false;
        }
        
    }

    void Jump(){
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * 10;
        // myRigidbody.linearVelocity = Vector2.up * flapStrength;
        audio.clip = jumpSound;
        audio.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.GameOver();
        birdIsAlive = false;
    }

    public void StartGame(){
        gameStarted = true;
        myRigidbody.simulated = true;
        titleScreen.SetActive(false);
    }
}
