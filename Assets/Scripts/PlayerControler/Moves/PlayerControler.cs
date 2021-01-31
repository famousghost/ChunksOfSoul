using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

#region PlayerState Enumerator
public enum PlayerStates
{
    PlayerIdle = 0,
    PlayerWalk,
    PlayerRun,
    PlayerCrouch,
    PlayerClimbing
}
#endregion

#region Character Enumerator
public enum PlayerCharacter
{
    Human = 0,
    Ghost
}
#endregion

public class PlayerControler : MonoBehaviour, IPunObservable
{
    #region PlayerState
    [Header("Player States Enum")]
    [SerializeField]
    PlayerStates playerStates;
    #endregion

    public PhotonView photonView;

    public SpawnSpiritChunks SpawnSpiritChunks;

    public Score score;

    public int rnd;
    public bool taken;
    public bool playerDied;
    public bool ladderBoardUpdated;
    public int scorePlayer1;
    public int scorePlayer2;
    public TMP_Text sensitivityValue;
    public Canvas grabImage;
    public Canvas gameOver;
    public TMP_Text playerNick;
    public string nickName;
    public FlashLightState flashLightState;

    public float throwStrength;

    #region PlayerCharacter
    [Header("Player Character Enum")]
    public PlayerCharacter playerCharacter;
    #endregion
    //If we want to make a gun
    #region Guns
    [SerializeField]
    private GunPickUp gunPickUp;
    #endregion
    //Player RigidBody use to gravity and walking
    #region Player
    [Header("Player")]
    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private Camera cam;
    #endregion
    //Keys input walking,runing, jumping, use
    #region LiftObject

    [SerializeField]
    private Rigidbody gameObj;

    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField]
    private MovmentSound movementSound;

    [SerializeField]
    private AudioSource playerSource;

    [SerializeField]
    private AudioClip humanAmbient;

    [SerializeField]
    private AudioClip monsterAmbient;
    #endregion

    #region Vector3
    [Header("Vector3")]
    [SerializeField]
    private Vector3 mainCamPosition;
    #endregion

    #region Bool Variables

    [SerializeField]
    private bool canRun = true;

    [SerializeField]
    private bool canLift = true;

    [SerializeField]
    private bool playerCanStand = true;

    [SerializeField]
    private bool liftItem = false;

    #endregion

    #region KeyClass
    [Header("KeyClass")]
    [SerializeField]
    private KeysInput keyInput;

    //Variables like walkSpeed, runSpeed
    #endregion

    #region Const Float

    private const float MINIMALROTATIONEY = -80.0f;

    private const float MAXIMUMROTATIONEY = 80.0f;

    private const float MAXSTAMINA = 300.0f;

    private const float MINSTAMINA = 0.0f;

    private const float MAXDISTANCEOFRAY = 1.4f;

    private const float MAXDISTANCEOFGROUNDEDRAY = 1.7f;

    private const float CROUCHSPEED = 2.5f;

    private const float CLIMBINGSPEED = 3.0f;
    #endregion

    #region Characters Stats

    private float m_walkSpeed;

    private float m_runSpeed;

    public bool cameraSet;

    public float stunTime = 0.0f;

    #endregion

    #region Bools
    public bool isClimbing { get; set; }
    #endregion

    #region Player Float Variables
    [Header("Float Variables")]
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private float stamina;

    [SerializeField]
    private float sensivity = 300.0f;

    [SerializeField]
    private float rotationeY;

    [SerializeField]
    private float rotateX;

    [SerializeField]
    private float rotateZ;
    #endregion

    public int spiritChunkCounter;

    #region System Methods
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        scorePlayer1 = 0;
        scorePlayer2 = 0;
    }

    // Use this for initialization
    void Start()
    {
        throwStrength = 10.0f;
        ladderBoardUpdated = false;
        playerDied = false;
        rnd = Random.Range(0, 201);
        photonView.RPC("RPC_randomSpiritChunkPositions", RpcTarget.All, new object[] { rnd });
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        flashLightState = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<FlashLightState>();
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().targetDisplay=2;
            return;
        }

        if(PhotonNetwork.IsMasterClient)
        {
            nickName = "Player1";
        }
        else
        {
            nickName = "Player2";
        }

        if (playerCharacter == PlayerCharacter.Ghost)
        {
            playerSource = GameObject.FindGameObjectWithTag("MonsterAmbient").GetComponent<AudioSource>();
        }
        if(playerCharacter == PlayerCharacter.Human)
        {
            playerSource = GameObject.FindGameObjectWithTag("HumanAmbient").GetComponent<AudioSource>();
        }

        gameOver = GameObject.Find("GameOverMenu").GetComponent<Canvas>();

        playerSource.Play();

        sensitivityValue = GameObject.Find("Sensivity").GetComponent<TMP_Text>();
        SpawnSpiritChunks = GameObject.Find("RoomManager").GetComponent<SpawnSpiritChunks>();
        score = GameObject.Find("RoomManager").GetComponent<Score>();
        taken = false;
        spiritChunkCounter = 0;
        score.spiritChunkCounter = 0;
        sensivity = float.Parse(sensitivityValue.text);
        if (playerCharacter == PlayerCharacter.Human)
        {
            m_walkSpeed = 5.0f;
            m_runSpeed = 8.5f;
        }
        else
        {
            m_walkSpeed = 6.0f;
            m_runSpeed = 8.0f;
        }
        isClimbing = false;
        stamina = MAXSTAMINA;
        rotateX = 0.0f;
        rotateZ = 0.0f;
        rigidBody = GetComponent<Rigidbody>();
        keyInput = GetComponent<KeysInput>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamPosition = cam.transform.localPosition;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ItemToPickUp" && playerCharacter == PlayerCharacter.Human)
        {
            col.gameObject.SetActive(false);
            spiritChunkCounter++;
            photonView.RPC("RPC_updateSpiritChunk", RpcTarget.AllBufferedViaServer, new object[] { spiritChunkCounter});
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster" || col.gameObject.tag == "Player")
        {
            Debug.Log("Monster Catch human");
            taken = true;
            if(playerCharacter == PlayerCharacter.Human)
            {
                playerDied = true;
            }
            photonView.RPC("RPC_setPLayerHasBeenTaken", RpcTarget.AllBufferedViaServer, new object[] { taken });
        }
    }

    [PunRPC]
    public void RPC_setPLayerHasBeenTaken(bool isTaken)
    {
        taken = isTaken;
    }

    [PunRPC]
    public void RPC_randomSpiritChunkPositions(int newRnd)
    {
        rnd = newRnd;
    }

    public void resetGame()
    {
        Player player = PhotonNetwork.PlayerListOthers[0];
        Debug.Log("Gracz: " + player.NickName);
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        if (!player.IsLocal)
        {
            if (taken)
            {
                if (player.NickName == "Human")
                {
                    Debug.Log("pierwszy gracz byl potworem Taken = " + taken);
                    scorePlayer1 = 100;
                }
                else
                {
                    Debug.Log("drugi gracz byl potworem Taken = " + taken);
                    scorePlayer2 = 100;
                }
            }
            else
            {
                if (player.NickName == "Human")
                {
                    Debug.Log("pierwszy gracz byl potworem Taken = " + taken);
                    scorePlayer2 = 200;
                }
                else
                {
                    Debug.Log("drugi gracz byl potworem Taken = " + taken);
                    scorePlayer1 = 200;
                }
            }
        }
        photonView.RPC("RPC_updateLadderBoard", RpcTarget.AllBufferedViaServer, new object[] { score.scorePlayer1 + scorePlayer1, score.scorePlayer2 + scorePlayer2 });
        photonView.RPC("RPC_Gameover", RpcTarget.All);
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if(score.scorePlayer1 >= 500 || score.scorePlayer2 >= 500)
        {
            gameOver.enabled = true;
            playerNick.text = nickName;
        }

        grabImage.enabled = false;

        if (score.spiritChunkCounter >= 7 || taken)
        {
            resetGame();
        }

        sensivity = float.Parse(sensitivityValue.text);
        if (sensivity != float.Parse(sensitivityValue.text))
        {
            sensivity = float.Parse(sensitivityValue.text);
        }

        keyInput.Inputs(playerCharacter);
        ChangePlayerStates();
        CheckPlayerState();
        if (stunTime > 0.0f)
        {
            stunTime -= Time.deltaTime;
        }
        if (stunTime <= 0)
        {
            Walking();
        }
        Rotatione();
        if (playerCharacter != PlayerCharacter.Ghost)
        {
            LiftObject();
            Exhaustion();
            LookSides();
            CheckIfPlayerIsUnderTheColider();
        }
    }
    #endregion

    [PunRPC]
    void RPC_Gameover()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        SpawnSpiritChunks.spawnChunks(rnd);
        if (taken)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.NickName == "Human")
                {
                    player.NickName = "Monster";
                }
                else
                {
                    player.NickName = "Human";
                }
            }
        }
        playerManager.gameover();
    }

    #region WalkMethods
    private void Walking()
    {
         Vector3 walkingX, walkingY;
         Vector3 Walks;
         walkingX = Vector3.right * walkSpeed * keyInput.GetWalkX() * Time.deltaTime;
         walkingY = Vector3.forward * walkSpeed * keyInput.GetWalkY() * Time.deltaTime;
         Walks = walkingX + walkingY;
         rigidBody.transform.Translate(Walks);
    }
    #endregion

    #region PlayerClimb
    public void PlayerClimb(bool lookUp)
    {
        Vector3 walkingUp;
        if(lookUp)
            walkingUp = Vector3.up * walkSpeed * keyInput.GetWalkY() * Time.deltaTime;
        else
            walkingUp = Vector3.up * walkSpeed * -keyInput.GetWalkY() * Time.deltaTime;
        rigidBody.transform.Translate(walkingUp);
    }
    #endregion

    #region RotationeMethod
    private void Rotatione()
    {
        Vector3 rotationeX;
        if (keyInput.GetRightSide() || keyInput.GetLeftSide())
        {
            rotateX += keyInput.GetMouseX() * sensivity  * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, MINIMALROTATIONEY, MAXIMUMROTATIONEY);
        }
        else
        {
            rotateX = Mathf.Lerp(rotateX, 0.0f, Time.deltaTime * 5.0f);
            rotationeX = Vector3.up * sensivity * keyInput.GetMouseX() * Time.deltaTime;
            rigidBody.transform.rotation *= Quaternion.Euler(rotationeX);
        }
        rotationeY -= keyInput.GetMouseY() * sensivity * Time.deltaTime;
        rotationeY = Mathf.Clamp(rotationeY, MINIMALROTATIONEY, MAXIMUMROTATIONEY);
        cam.transform.localRotation = Quaternion.Euler(rotationeY, rotateX, rotateZ);
    }
    #endregion


    #region CheckPlayerStates
    private void CheckPlayerState()
    {
        switch(playerStates)
        {
            case PlayerStates.PlayerIdle:
                StandUpAnimation();
                break;
            case PlayerStates.PlayerWalk:
                StandUpAnimation();
                walkSpeed = m_walkSpeed;
                break;
            case PlayerStates.PlayerRun:
                StandUpAnimation();
                walkSpeed = m_runSpeed;
                break;
            case PlayerStates.PlayerCrouch:
                CrouchAnimation();
                walkSpeed = CROUCHSPEED;
                break;
            case PlayerStates.PlayerClimbing:
                StandUpAnimation();
                walkSpeed = CLIMBINGSPEED;
                break;
            default:
                walkSpeed = 0.0f;
                break;
        }
    }
    #endregion

    #region Crouch And StandUp Animation
    private void CrouchAnimation()
    {
        Vector3 stand = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y, rigidBody.transform.localScale.z);
        Vector3 crouch = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y / 2.0f, rigidBody.transform.localScale.z);
        if (rigidBody.transform.localScale.y >= 0.5f)
            rigidBody.transform.localScale = Vector3.Lerp(stand, crouch, Time.deltaTime * 2.0f);
    }

    private void StandUpAnimation()
    {
        Vector3 crouch = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y, rigidBody.transform.localScale.z);
        Vector3 stand = new Vector3(rigidBody.transform.localScale.x, rigidBody.transform.localScale.y * 2.0f, rigidBody.transform.localScale.z);
        if (rigidBody.transform.localScale.y <= 1.0f && playerCanStand)
            rigidBody.transform.localScale = Vector3.Lerp(crouch,stand, Time.deltaTime * 2.0f);
    }
    #endregion

    #region Check If Player Is unders something
    private void CheckIfPlayerIsUnderTheColider()
    {
        Ray ray = new Ray(rigidBody.transform.position, rigidBody.transform.up);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,2.0f))
        {
           playerStates = PlayerStates.PlayerCrouch;
           playerCanStand = false;
        }
        else
        {
            playerCanStand = true;
        }
    }
    #endregion

    #region LookSides
    private void LookSides()
    {

        var leftLook = keyInput.GetLeftSide() && (cam.transform.localPosition.x >= -0.5f);
        var rightLook = keyInput.GetRightSide() && (cam.transform.localPosition.x <= 0.5f);
        //Left Side
        if (leftLook)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x - 0.5f, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 2.0f);
            rotateZ = Mathf.Lerp(rotateZ, 12.0f, Time.deltaTime * 5.0f);
        }
        if (rightLook)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(cam.transform.localPosition.x + 0.5f, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 2.0f);
            rotateZ = Mathf.Lerp(rotateZ, -12.0f, Time.deltaTime * 5.0f);
        }
        else
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(mainCamPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z), Time.deltaTime * 3.0f);
            rotateZ = Mathf.Lerp(rotateZ, 0.0f, Time.deltaTime * 5.0f);
        }
    }
    #endregion

    #region ChangePlayerStates
    private void ChangePlayerStates()
    {
        var playerIsWalking = (keyInput.GetWalkX() != 0 || keyInput.GetWalkY() != 0) && playerStates != PlayerStates.PlayerCrouch;
        var playerCanRun = (keyInput.GetIsRunning() && canRun) && playerStates != PlayerStates.PlayerCrouch;
        var playerCanCrouch = (keyInput.GetIsCrouching()) && playerStates != PlayerStates.PlayerRun;
        if (playerCanRun)
        {
            playerStates = PlayerStates.PlayerRun;
        }
        else if (playerCanCrouch && playerCharacter != PlayerCharacter.Ghost)
        {
            playerStates = PlayerStates.PlayerCrouch;
        }
        else if (playerIsWalking)
        {
            playerStates = PlayerStates.PlayerWalk;
        }
        else if(isClimbing && playerCharacter != PlayerCharacter.Ghost)
        {
            playerStates = PlayerStates.PlayerClimbing;
        }
        else
        {
            playerStates = PlayerStates.PlayerIdle;
        }
    }
    #endregion

    #region Jumping

    private void PlayerIsOnGrounded()
    {
        if (!keyInput.GetOnGrounded())
        {
            Ray rayToGround = new Ray(rigidBody.transform.position, -rigidBody.transform.up);
            RaycastHit hitCollider;
            Debug.DrawRay(rigidBody.transform.position, -rigidBody.transform.up, Color.green);
            if (Physics.Raycast(rayToGround, out hitCollider, MAXDISTANCEOFGROUNDEDRAY))
            {
                keyInput.SetOnGrounded(true);
            }
        }
    }

    #endregion

    #region exhaustion

    private void Exhaustion()
    {
        if (playerStates == PlayerStates.PlayerRun && stamina >= MINSTAMINA)
        {
            stamina -= 0.5f;
            if (stamina <= MINSTAMINA)
            {
                stamina = 0.0f;
                canRun = false;
            }
        }
        else if (stamina <= MAXSTAMINA)
        {
            stamina += 0.5f;
            if (stamina >= MAXSTAMINA)
            {
                stamina = MAXSTAMINA;
                canRun = true;
            }

        }
    }

    #endregion

    #region Lift GameObjects

    private void LiftObject()
    {
        RaycastHit hit;

        Vector3 forward = cam.transform.forward;

        Ray ray = new Ray(cam.transform.position, forward);


        if (liftItem && gameObj != null)
        {
            gameObj.transform.position = cam.transform.position + cam.transform.forward;
            gameObj.transform.rotation = cam.transform.rotation;
            Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), true);


            gameObj.useGravity = false;
            gameObj.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Lifted"))
            {
                grabImage.enabled = true;
            }
            if (keyInput.GetIsLifted())
            {

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Lifted") && canLift == true && !liftItem)
                {
                    gameObj = hit.rigidbody;
                    liftItem = true;
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    if (gameObj != null)
                    {
                        keyInput.SetLiftetd(false);
                        gameObj.useGravity = true;
                        gameObj.constraints = RigidbodyConstraints.None;

                        canLift = false;

                        gameObj.AddForce(cam.transform.forward * throwStrength, ForceMode.Impulse);
                        Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                        gameObj = null;
                        liftItem = false;
                    }
                }
            }
            else if(liftItem != false && !keyInput.GetIsLifted() && gameObj != null)
            {
                gameObj.useGravity = true;
                gameObj.constraints = RigidbodyConstraints.None;
                Physics.IgnoreCollision(gameObj.transform.GetComponent<Collider>(), rigidBody.transform.GetComponent<Collider>(), false);
                keyInput.SetLiftetd(false);
                gameObj = null;
                liftItem = false;
            }
        }

        else
        {
            keyInput.SetLiftetd(false);
            canLift = true;
            liftItem = false;
        }
    }
    #endregion

    #region Setters

    public void SetGameObj(Rigidbody gameObj)
    {
        this.gameObj = gameObj;
    }
    #endregion

    #region Getters
    public float GetRotationeY()
    {
        return rotationeY;
    }
    #endregion

    #region Gravity Enable And Diable
    public void GravityDisable()
    {
        rigidBody.useGravity = false;
    }

    public void GravityEnable()
    {
        rigidBody.useGravity = true;
    }
    #endregion


    [PunRPC]
    public void RPC_updateSpiritChunk(int spiritChunk)
    {
        score.spiritChunkCounter = spiritChunk;
    }

    [PunRPC]
    void RPC_updateLadderBoard(int scP1, int scP2)
    {
        score.scorePlayer1 = scP1;
        score.scorePlayer2 = scP2;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rnd);
            stream.SendNext(taken);
            stream.SendNext(score.spiritChunkCounter);
            stream.SendNext(score.scorePlayer1);
            stream.SendNext(score.scorePlayer2);
        }
        else
        {
            score = GameObject.Find("RoomManager").GetComponent<Score>();
            rnd = (int)stream.ReceiveNext();
            taken = (bool)stream.ReceiveNext();
            score.spiritChunkCounter = (int)stream.ReceiveNext();
            score.scorePlayer1 = (int)stream.ReceiveNext();
            score.scorePlayer2 = (int)stream.ReceiveNext();
            updateIfNecessary(spiritChunkCounter, score.spiritChunkCounter);
            updateIfNecessary(scorePlayer1, score.scorePlayer1);
            updateIfNecessary(scorePlayer2, score.scorePlayer2);
        }
    }

    void updateIfNecessary(int value1, int value2)
    {
        if(value1 != value2)
        {
            value1 = value2;
        }
    }

}
