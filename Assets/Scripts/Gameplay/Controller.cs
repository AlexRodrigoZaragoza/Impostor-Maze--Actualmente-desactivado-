using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using System.Globalization;

public class Controller : MonoBehaviour
{
    //Refs
    Transform player;
    Rigidbody rb;
    float xRotation;
    public Transform transformCamera;

    [Header("Movement")]
    public Transform groundCheck;
    public float groundRadius = 0.1f;
    public float walkSpeed, crouchSpeed, sensitivity, jumpForce;
    bool isGrounded, isCrouching;
    float speed, walkHeight;

    [Header("Messages - Prefab & UI")]
    public GameObject signPrefab;
    public GameObject readingWindow, writingWindow, ePrompt;
    public Button leftButton, rightButton;
    Text readingText, writingText;
    int currentMessage;

    [Header("Messages - Script variables")]
    public bool canWrite;
    public bool isReading, isWriting;

    private GameObject _messageParent;

    bool isDead;

    void Start()
    {
        #region References

        readingWindow = ServiceLocator.Instance.GetService<ICanvasReference>().GetRead();
        writingWindow = ServiceLocator.Instance.GetService<ICanvasReference>().GetWrite();
        ePrompt = ServiceLocator.Instance.GetService<ICanvasReference>().GetEPrompt();
        leftButton = ServiceLocator.Instance.GetService<ICanvasReference>().GetPrevious();
        rightButton = ServiceLocator.Instance.GetService<ICanvasReference>().GetNext();

        player = transform;
        rb = player.gameObject.GetComponent<Rigidbody>();

        speed = walkSpeed;
        walkHeight = player.GetComponent<CapsuleCollider>().height;

        readingText = readingWindow.GetComponentInChildren<Text>();
        writingText = writingWindow.transform.Find("InputField/Text").gameObject.GetComponent<Text>();

        writingWindow.SetActive(false);
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        readingWindow.SetActive(false);

        #endregion References

        transformCamera.localPosition = new Vector3(0, player.localScale.y / 2, 0);

        leftButton.onClick.AddListener(PreviousMessage);
        rightButton.onClick.AddListener(NextMessage);
        isDead = false;
        print("Entro en el Controlador jeje");
        StartCoroutine(ServiceLocator.Instance.GetService<GameManager>().UpdateGame(false));
        print("Controlador jeje");
        StartCoroutine("PlayerAlive");
        _messageParent = GameObject.FindGameObjectWithTag("MessageParent");
    }
    IEnumerator PlayerAlive()
    {
        yield return new WaitForSeconds(2.0f);
        isDead = false;
    }
    private void Update()
    {
        if (!isReading && !isWriting)
        {
            Movement();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Messages();
    }

    #region Character Controller
    void Movement()
    {
        if (!isDead)
        {
            Move();
            Jump();
            Crouch();
            Rotation();
        }
    }

    void Move()
    {
        float right = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        Vector3 horizontalMovement = player.right * right + player.forward * forward;
        rb.velocity = horizontalMovement * speed + new Vector3(0, rb.velocity.y, 0);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, 1 << 8);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C)) && isGrounded)
        {
            isCrouching = !isCrouching;
            CapsuleCollider col = player.GetComponent<CapsuleCollider>();

            if (isCrouching)
            {
                col.height *= 0.5f;
                col.center = new Vector3(0, -player.localScale.y / 2, 0);
                transformCamera.localPosition = new Vector3(0, transformCamera.position.y - player.localScale.y / 2, 0);
                speed = crouchSpeed;
            }
            else
            {
                col.height = walkHeight;
                col.center = Vector3.zero;
                transformCamera.localPosition = new Vector3(0, transformCamera.position.y + player.localScale.y / 2, 0);
                speed = walkSpeed;
            }
        }
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transformCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Y rotation
        player.Rotate(Vector3.up * mouseX); //X rotation
    }

    #endregion Character Controller

    void Messages()
    {
        int layerMask = 1 << 6 | 1 << 7 | 1 << 8;
        RaycastHit hit;

        if (Physics.Raycast(transformCamera.position, transformCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Floor"))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Wall")) // If the player sees a message, allow reading
                {
                    if (!isReading) ePrompt.SetActive(true);
                    Read(hit);
                }
                else // If the player sees a wall, prevent reading and allow message creation
                {
                    ePrompt.SetActive(false);
                    Write();
                }
            }
        }
    }

    #region Create Messages

    void Write()
    {
        #region Input Field

        if (Input.GetKeyDown(KeyCode.E) && canWrite)
        {
            WritingUI(true);
            ShowCursor();
            canWrite = false;
        }

        #endregion Input Field

        #region Instantiate Sign

        if (Input.GetKeyDown(KeyCode.Return) && isWriting)
        {
            GameObject mySign = Instantiate(signPrefab);
            mySign.transform.SetParent(_messageParent.transform);
            Orientation(mySign);
            mySign.GetComponentInChildren<Message>().message = writingText.text;
            WritingUI(false);
            canWrite = true;

            string signPos = mySign.transform.position.x.ToString(CultureInfo.InvariantCulture);
            signPos += "," + mySign.transform.position.y.ToString(CultureInfo.InvariantCulture);
            signPos += "," + mySign.transform.position.z.ToString(CultureInfo.InvariantCulture);
            signPos += "," + mySign.transform.rotation.eulerAngles.y.ToString();

            Consumer.Message newMessage = new Consumer.Message(writingText.text, ServiceLocator.Instance.GetService<IUserInfo>().GetId(), signPos);
            ServiceLocator.Instance.GetService<IPullMessage>().updatePushMessages(newMessage);
        }

        #endregion Instantiate Sign

        #region Cancel Sign

        if (Input.GetKeyDown(KeyCode.Escape) && isWriting)
        {
            WritingUI(false);
            canWrite = true;
        }

        #endregion Cancel Sign
    }

    void Orientation(GameObject sign)
    {
        int layerMask = 1 << 7;
        RaycastHit hit;

        if (Physics.Raycast(transformCamera.position, transformCamera.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            sign.transform.localRotation = Quaternion.LookRotation(hit.normal);
            sign.transform.position = hit.point;
        }
    }

    void WritingUI(bool on)
    {
        isWriting = on;
        writingWindow.SetActive(on);
        writingWindow.GetComponentInChildren<InputField>().ActivateInputField();
    }

    #endregion Create Messages

    #region Read Messages

    void Read(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isReading = !isReading;
            currentMessage = 0;
            ePrompt.SetActive(false);
            readingWindow.SetActive(isReading);
        }

        if (isReading) DisplayMessages(hit.transform.gameObject);
    }
    void DisplayMessages(GameObject sign)
    {
        Message msg = sign.GetComponent<Message>();
        readingText.text = msg.messages[currentMessage];

        if (msg.messages.Count > 1)
        {
            if (currentMessage == msg.messages.Count - 1)
            {
                leftButton.interactable = true;
                rightButton.interactable = false;
            }
            else if (currentMessage == 0)
            {
                leftButton.interactable = false;
                rightButton.interactable = true;
            }
            else
            {
                leftButton.interactable = true;
                rightButton.interactable = true;
            }

            ShowCursor();
            ArrowsUI(true);
        }
        else ArrowsUI(false);
    }

    void ArrowsUI(bool on)
    {
        leftButton.gameObject.SetActive(on);
        rightButton.gameObject.SetActive(on);
    }

    void PreviousMessage()
    {
        currentMessage--;
    }

    void NextMessage()
    {
        currentMessage++;
    }

    #endregion Read Messages

    void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rejilla")
        {
            StartCoroutine(ServiceLocator.Instance.GetService<GameManager>().UpdateGame(false));
        }else if (other.gameObject.tag == "Finish")
        {
            StartCoroutine(ServiceLocator.Instance.GetService<GameManager>().UpdateGame(true));
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
}