using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;

    [SerializeField] private GameObject playerShip;
    [SerializeField] private TextMeshProUGUI infoText;

    [Header("Movement")]
    public float maxSpeed;

    private Rigidbody rb;
    private float rollSpeed;
    private float speed;
    private Vector3 movementDirection;
    private Vector2 rotateSpeed;

    [Header("QT")]
    public float QTSpeed;
    [SerializeField] public Slider QTSlider;
    [SerializeField] public TextMeshProUGUI QTText;
    private Coroutine QTCoroutine;
    private bool warping;
    private bool loadingQT;


    private void Start()
    {
        _input.MoveEvent += HandleMove;
        _input.RotateEvent += HandleRotate;
        _input.RollEvent += HandleRoll;

        _input.PauseEvent += Pause;
        _input.UICloseEvent += Resume;

        _input.CancelQTEvent += CancelQT;

        rb = playerShip.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        QTSlider.gameObject.SetActive(false);
        QTText.text = "null";

        infoText.text = "null";
        infoText.gameObject.SetActive(false);
    }

    private void HandleMove(Vector3 value)
    {
        movementDirection = value;
    }

    private void HandleRotate(Vector2 value)
    {
        rotateSpeed = (value * 50);
    }

    private void HandleRoll(float value)
    {
        rollSpeed = value * -100;
    }

    private void Update()
    {
        // Calculate roll rotation independently
        playerShip.transform.Rotate(Vector3.forward, rollSpeed * Time.deltaTime, Space.Self);

        // Store current rotation
        Quaternion currentRotation = playerShip.transform.rotation;

        // Calculate yaw (Y-axis rotation) and pitch (X-axis rotation) based on input
        Quaternion yaw = Quaternion.AngleAxis(rotateSpeed.x * Time.deltaTime, Vector3.up);   // Yaw rotation on world Y-axis
        Quaternion pitch = Quaternion.AngleAxis(-rotateSpeed.y * Time.deltaTime, Vector3.right); // Pitch rotation on local X-axis

        // Apply yaw and pitch to current rotation
        playerShip.transform.rotation = currentRotation * yaw * pitch;


        speed = maxSpeed;
        // Calculate the direction relative to the ship's rotation
        Vector3 movement = transform.TransformDirection(movementDirection) * speed;
        // Apply this as the ship's velocity
        rb.linearVelocity = movement;
    }


    private void Pause()
    {
        _ui.Pause();
        _input.SetUI();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        if (!loadingQT)
        {
            _ui.Resume();
            _input.SetShipControls();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #region QT
    
    public void InitiateQT(GameObject button)
    {
        QTButton QTVar = button.GetComponent<QTButton>();

        GameObject travelLocation;
        bool starTravel;
        Vector3 starLocation;
        string starScene;

        StartQT(travelLocation, starTravel, starLocation, starScene);
    }

    public void StartQT(GameObject travelLocation, bool starTravel, Vector3 starLocation, string starScene)
    {
        StartCoroutine(QTStart(travelLocation, starTravel, starLocation, starScene));
    }

    public void CancelQT()
    {
        if (QTCoroutine != null && !warping)
        {
            StopCoroutine(QTCoroutine);
            QTCoroutine = null; // Clear the reference

            _input.SetShipControls();
            QTSlider.value = 0;
            QTSlider.gameObject.SetActive(false);
        }
    }

    public IEnumerator QTStart(GameObject travelLocation, bool starTravel, Vector3 starLocation, string starScene)
    {
        _ui.CloseQT();
        _input.SetUI();
        _ui.currentMenu = null;

        if (Vector3.Distance(transform.position, travelLocation.transform.position) < 500)
        {
            _input.SetShipControls();

            infoText.gameObject.SetActive(true);
            infoText.text = "Cannot QT while closer than 500m";

            yield return new WaitForSeconds(5f);

            infoText.text = "null";
            infoText.gameObject.SetActive(false);

            yield break;
        }

        if (starTravel)
        {
            QTCoroutine = StartCoroutine(StarQT(starLocation, starScene));
        }
        else
        {
            QTCoroutine = StartCoroutine(QT(travelLocation));
        }
    }

    private IEnumerator QT(GameObject travelLocation)
    {
        loadingQT = true;

        // Define how long the rotation should take
        float rotationDuration = 1f; // 1 second to rotate
        float elapsedTime = 0f;

        // Cache the initial rotation and the target rotation
        Quaternion initialRotation = playerShip.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(travelLocation.transform.position - playerShip.transform.position);

        // Smoothly rotate the playerShip over time
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate between the initial and target rotation
            playerShip.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);

            yield return null; // Wait for the next frame
        }

        // Ensure final rotation is set (in case of minor discrepancies)
        playerShip.transform.rotation = targetRotation;


        yield return new WaitForSeconds(0.5f); 
        QTSlider.gameObject.SetActive(true);
        QTSlider.value = 0;
        QTText.text = "Loading QT - Press Z to Cancel";

        while (QTSlider.value < 1f)
        {
            QTSlider.value += 0.3f * Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTText.text = "Warping to Location";
        warping = true;

        while (QTSlider.value < 1f)
        {
            QTSlider.value += 0.8f * Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTSlider.gameObject.SetActive(false);

        while (Vector3.Distance(playerShip.transform.position, travelLocation.transform.position) > 2f)
        { 
            playerShip.transform.position = Vector3.Lerp(playerShip.transform.position, travelLocation.transform.position, QTSpeed * Time.deltaTime);
            yield return null;
        }

        warping = false;
        loadingQT = false;

        _input.SetShipControls();
    }

    private IEnumerator StarQT(Vector3 starLocation, string starScene)
    {
        loadingQT = true;

        // Define how long the rotation should take
        float rotationDuration = 1f; // 1 second to rotate
        float elapsedTime = 0f;

        // Cache the initial rotation and the target rotation
        Quaternion initialRotation = playerShip.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(starLocation - playerShip.transform.position);

        // Smoothly rotate the playerShip over time
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolate between the initial and target rotation
            playerShip.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / rotationDuration);

            yield return null; // Wait for the next frame
        }

        // Ensure final rotation is set (in case of minor discrepancies)
        playerShip.transform.rotation = targetRotation;


        yield return new WaitForSeconds(0.5f);
        QTSlider.gameObject.SetActive(true);
        QTSlider.value = 0;
        QTText.text = "Loading Cross-Sytem QT - Press Z to Cancel";

        while (QTSlider.value < 1f)
        {
            QTSlider.value += 0.1f * Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTText.text = "Warping to Star";
        warping = true;

        while (QTSlider.value < 1f)
        {
            QTSlider.value += 0.5f * Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTSlider.gameObject.SetActive(false);

        while (Vector3.Distance(playerShip.transform.position, starLocation) > 2f)
        {
            playerShip.transform.position = Vector3.Lerp(playerShip.transform.position, starLocation, QTSpeed * Time.deltaTime);
            yield return null;
        }


        warping = false;
        loadingQT = false;

        _input.SetShipControls();
        SceneManager.LoadScene(starScene);
    } 

    #endregion
}
