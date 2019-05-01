using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // make game manager public static so can access this from other scripts
    public static GameManager gm;

    // public variables
    public int score = 0;

    public bool canBeatLevel = false;
    public int beatLevelScore = 0;

    public float startTime = 5.0f;

    public Text mainScoreDisplay;
    public Text mainTimerDisplay;

    public GameObject gameOverScoreOutline;

    public AudioSource musicAudioSource;

    public bool gameIsOver = false;

    public GameObject playAgainButtons;
    public string playAgainLevelToLoad;

    public GameObject nextLevelButtons;
    public string nextLevelToLoad;

    private float currentTime;

    // setup the game
    private void Start() {

        // set the current time to the startTime specified
        this.currentTime = this.startTime;

        // get a reference to the GameManager component for use by other scripts
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        // init scoreboard to 0
        this.mainScoreDisplay.text = "0";

        // inactivate the gameOverScoreOutline gameObject, if it is set
        if (this.gameOverScoreOutline)
            this.gameOverScoreOutline.SetActive(false);

        // inactivate the playAgainButtons gameObject, if it is set
        if (this.playAgainButtons)
            this.playAgainButtons.SetActive(false);

        // inactivate the nextLevelButtons gameObject, if it is set
        if (this.nextLevelButtons)
            this.nextLevelButtons.SetActive(false);
    }

    // this is the main game event loop
    private void Update() {
        if (!this.gameIsOver) {
            if (this.canBeatLevel && (this.score >= this.beatLevelScore)) {  // check to see if beat game
                BeatLevel();
            } else if (this.currentTime < 0) { // check to see if timer has run out
                EndGame();
            } else { // game playing state, so update the timer
                this.currentTime -= Time.deltaTime;
                this.mainTimerDisplay.text = this.currentTime.ToString("0.00");
            }
        }
    }

    private void EndGame() {
        // game is over
        this.gameIsOver = true;

        // repurpose the timer to display a message to the player
        this.mainTimerDisplay.text = "";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (this.gameOverScoreOutline)
            this.gameOverScoreOutline.SetActive(true);

        // activate the playAgainButtons gameObject, if it is set 
        if (this.playAgainButtons)
            this.playAgainButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (this.musicAudioSource)
            this.musicAudioSource.pitch = 0.5f; // slow down the music
    }

    private void BeatLevel() {
        // game is over
        this.gameIsOver = true;

        // repurpose the timer to display a message to the player
        this.mainTimerDisplay.text = "LEVEL COMPLETE";

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (this.gameOverScoreOutline)
            this.gameOverScoreOutline.SetActive(true);

        // activate the nextLevelButtons gameObject, if it is set 
        if (this.nextLevelButtons)
            this.nextLevelButtons.SetActive(true);

        // reduce the pitch of the background music, if it is set 
        if (this.musicAudioSource)
            this.musicAudioSource.pitch = 0.5f; // slow down the music
    }

    // public function that can be called to update the score or time
    public void targetHit(int scoreAmount, float timeAmount) {
        // increase the score by the scoreAmount and update the text UI
        this.score += scoreAmount;
        this.mainScoreDisplay.text = this.score.ToString();

        // increase the time by the timeAmount
        this.currentTime += timeAmount;

        // don't let it go negative
        if (this.currentTime < 0)
            this.currentTime = 0.0f;

        // update the text UI
        this.mainTimerDisplay.text = this.currentTime.ToString("0.00");
    }

    // public function that can be called to restart the game
    public void RestartGame() {
        // we are just loading a scene (or reloading this scene)
        // which is an easy way to restart the level
        SceneManager.LoadScene(this.playAgainLevelToLoad);
    }

    // public function that can be called to go to the next level of the game
    public void NextLevel() {
        // we are just loading the specified next level (scene)
        SceneManager.LoadScene(this.nextLevelToLoad);
    }


}
