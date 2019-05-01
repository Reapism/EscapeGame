using UnityEngine;

public class ChangeScene : MonoBehaviour {

    public GameObject NewSpawnLocation;
    public GameObject OldSpawnLocation;
    public Camera camera;
    public GameObject player;

    private int count;
    private static int SLOW_FACTOR;
    private int y_val;
    private bool begin;

    // Start is called before the first frame update
    private void Start() {
        this.count = 0;
        SLOW_FACTOR = 2;
        this.y_val = 0;
        this.begin = false;
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Portal") {
            this.begin = true;
        }
    }

    // Update is called once per frame
    private void Update() {
        if (this.begin) {
            if (++this.count % SLOW_FACTOR == 0) {
                this.camera.transform.Rotate(new Vector3(0, 1, 0));
                ++this.y_val;
            }

            if (this.y_val == 80) {
                this.camera.transform.Rotate(new Vector3(25, 0, -23));
                player.transform.position = NewSpawnLocation.transform.position;
                OldSpawnLocation.transform.position = NewSpawnLocation.transform.position;
                begin = false;
            }
        }

    }
}
