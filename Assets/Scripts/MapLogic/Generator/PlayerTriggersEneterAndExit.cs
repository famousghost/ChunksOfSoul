using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggersEneterAndExit : MonoBehaviour {

    #region Items From Usable Item
    [SerializeField]
    private Generator generator;

    [SerializeField]
    private ItemDisplay itemDisplay;

    [SerializeField]
    private PlayerControler playerControler;

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    private Canvas riddleCanvas;

    [SerializeField]
    private RiddleAnswer riddle;

    [SerializeField]
    private EmergencyCard emergencyCard;
    #endregion

    #region System Methods
    void Start()
    {
        playerControler = GetComponent<PlayerControler>();
        itemDisplay = GetComponentInChildren<ItemDisplay>();
        itemsPickUp = GetComponent<ItemsPickUp>();
        riddleCanvas = GameObject.FindGameObjectWithTag("Riddle").GetComponent<Canvas>();
        riddle = GameObject.Find("Riddles").GetComponent<RiddleAnswer>();
        emergencyCard = GameObject.Find("UsableItems").GetComponent<EmergencyCard>();
        generator = GameObject.Find("UsableItems").GetComponent<Generator>();
    }

    void Update()
    {
        if (emergencyCard.GetCorrectCode())
        {
            emergencyCard.NormalModeActivate();
            itemDisplay.enabled = true;
            playerControler.enabled = true;
            itemsPickUp.enabled = true;
        }
        if(!riddle.GetCanOpenRiddle())
        {
            riddleCanvas.enabled = false;
        }
    }
    #endregion

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            generator.SetUseGenerator(true);
        }
        if(other.tag == "Computer")
        {
            emergencyCard.SetUseCard(true);
            if (!emergencyCard.GetCorrectCode())
                riddle.RiddleEnable();
            else
                riddle.RiddleDisable();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Computer")
        {
            if(emergencyCard.GetCanAnswerToRiddle() && riddle.GetCanOpenRiddle())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    itemDisplay.enabled = false;
                    CursorEnable();
                    riddleCanvas.enabled = true;
                    playerControler.enabled = false;
                    itemsPickUp.enabled = false;
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    itemDisplay.enabled = true;
                    CursorDisable();
                    riddleCanvas.enabled = false;
                    playerControler.enabled = true;
                    itemsPickUp.enabled = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "GeneratorPlace")
        {
            generator.SetUseGenerator(false);
        }
        if (other.tag == "Computer")
        {
            emergencyCard.SetUseCard(false);
            riddle.RiddleDisable();
        }
    }
    #endregion

    private void CursorEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}