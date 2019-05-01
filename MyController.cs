using UnityEngine;

public class MyController : MonoBehaviour {

    public float player_speed = 0.5f;
    public float gravity = 9.8f;
    public GameObject explosionPrefab;
    public GameObject player;
    public GameObject spawnLocation;

    private CharacterController mController;

    // Start is called before the first frame update
    private void Start() {
        this.mController = this.gameObject.GetComponent<CharacterController>();
        this.enabled = true;
    }

    // when collided with another gameObject
    private void OnCollisionEnter(Collision newCollision) {

        // only do stuff if hit by a projectile
        if (newCollision.gameObject.tag == "enemy") {
            if (this.explosionPrefab) {
                // Instantiate an explosion effect at the gameObjects position and rotation
                GameObject particle = Instantiate(this.explosionPrefab, this.transform.position, this.transform.rotation) as GameObject;
                Destroy(particle, 1f);
            }

            // Spawn Player self
            GameObject go = Instantiate(this.player, this.spawnLocation.transform.position, this.spawnLocation.transform.rotation) as GameObject;
            go.name = "Player";
            go.SetActive(true); 
            
            // destroy the Player
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    private void Update() {
        // moving player
        Vector3 moveX = Input.GetAxisRaw("Horizontal") * Vector3.right * this.player_speed;
        Vector3 moveZ = Input.GetAxisRaw("Vertical") * Vector3.forward * this.player_speed;
        Vector3 move = this.transform.TransformDirection(moveX + moveZ);

        move.y -= this.gravity * Time.deltaTime;
        this.mController.Move(move);
    }
}
