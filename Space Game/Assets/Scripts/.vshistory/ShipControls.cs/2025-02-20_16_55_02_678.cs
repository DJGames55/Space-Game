using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIManager _ui;
    [SerializeField] private StarLocationsData _starLocData;

    [SerializeField] private GameObject playerShip;
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private Camera MainCam;

    public static ShipControls instance;

    [Header("Movement")]
    public float maxSpeed;

    private Rigidbody rb;
    private float rollSpeed;
    private float speed;
    private Vector3 movementDirection;
    private Vector2 rotateSpeed;

    [Header("QT")]
    public float QTSpeed;
    public Slider QTSlider;
    public TextMeshProUGUI QTText;
    private Coroutine QTCoroutine;
    private bool warping;
    private bool loadingQT;
    [SerializeField] private ParticleSystem QTParticles;

    [System.Serializable]
    public class LastSceneLoc
    {
        public SceneField Scene;
        public Vector3 Location;
    }

    public List<LastSceneLoc> SceneLocations = new List<LastSceneLoc>();

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

        MainCam.gameObject.SetActive(true);

        _starLocData.LoadDistances();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
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

        GameObject travelLocation = QTVar.warpPos;
        string warpName = QTVar.warpName;

        bool starTravel = QTVar.starTravel;
        SceneField starScene = QTVar.starScene;
        Vector3 starLocation = QTVar.starLocation;

        StartQT(travelLocation, warpName, starTravel, starLocation, starScene);
    }

    public void StartQT(GameObject travelLocation, string warpName, bool starTravel, Vector3 starLocation, SceneField starScene)
    {
        StartCoroutine(QTStart(travelLocation, warpName, starTravel, starLocation, starScene));
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

    public IEnumerator QTStart(GameObject travelLocation, string warpName, bool starTravel, Vector3 starLocation, SceneField starScene)
    {
        _ui.CloseQT();
        _input.SetUI();
        _ui.currentMenu = null;
        infoText.text = "null";
        infoText.gameObject.SetActive(false);

        if (travelLocation != null) 
        { 
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
        }

        if (starTravel)
        {
            QTCoroutine = StartCoroutine(StarQT(starLocation, starScene, warpName));
        }
        else
        {
            QTCoroutine = StartCoroutine(QT(travelLocation, warpName));
        }
    }

    private IEnumerator QT(GameObject travelLocation, string warpName)
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

        QTSlider.maxValue = 3f;
        while (QTSlider.value < QTSlider.maxValue)
        {
            QTSlider.value += Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTText.text = $"Warping to {warpName}";
        warping = true;

        QTSlider.maxValue = 1.5f;
        while (QTSlider.value < QTSlider.maxValue)
        {
            QTSlider.value += Time.deltaTime; // Moves at a constant rate
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

    private IEnumerator StarQT(Vector3 starLocation, SceneField starScene, string warpName)
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

        QTSlider.maxValue = 7f;
        while (QTSlider.value < QTSlider.maxValue)
        {
            QTSlider.value += Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTText.text = $"Warping to {warpName}";
        warping = true;

        QTSlider.maxValue = 4f;
        while (QTSlider.value < QTSlider.maxValue)
        {
            QTSlider.value += Time.deltaTime; // Moves at a constant rate
            yield return null;
        }
        QTSlider.value = 0;
        QTSlider.gameObject.SetActive(false);

        SetSceneLastLoc();
        QTParticles.gameObject.SetActive(true);

        float warpTime = 0f;
        float totalWarpTime = 10f;
        float startFOV = 60f;
        float endFOV = 167f;
        while (warpTime < 3f)
        {
            playerShip.transform.position = Vector3.Lerp(playerShip.transform.position, (starLocation / 1_000_000_000_000), QTSpeed * Time.deltaTime);
            MainCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, warpTime / totalWarpTime);

            warpTime += Time.deltaTime;
            yield return null;
        }

        while (warpTime < totalWarpTime)
        {
            MainCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, warpTime / totalWarpTime);
            warpTime += Time.deltaTime; 
            yield return null;
        }

        StartCoroutine(QTToCurrentLoc(FindSceneLastLoc(), _ui.currentScene));
        _ui.currentScene = starScene;
        SceneManager.LoadScene(starScene);
    }

    private void SetSceneLastLoc()
    {
        bool found = false;

        foreach (var item in SceneLocations)
        {
            if (item.Scene.SceneName == _ui.currentScene)
            {
                item.Location = transform.position;
                found = true;
                break;
            }
        }

        // If no match was found, add a new entry
        if (!found)
        {
            var data = new LastSceneLoc
            {
                Scene = _ui.currentScene,
                Location = transform.position,
            };

            SceneLocations.Add(data);
        }
    }

    private Vector3 FindSceneLastLoc()
    {
        foreach (var item in SceneLocations)
        {
            if (item.Scene.SceneName == _ui.currentScene)
            {
                return item.Location;
            }
        }

        return Vector3.zero;
    }

    public IEnumerator QTToCurrentLoc(Vector3 prevSceneLoc, SceneField prevScene)
    {
        foreach (StarLocations.DistanceData var in _starLocData.savedDistances)
        {
            if (var.toStar.SceneName == prevScene.SceneName && var.fromStar == _ui.currentScene.SceneName)
            {
                Debug.Log(var.distance);
                playerShip.transform.position = -var.distance * 1_000;
            }
        }

        if (prevSceneLoc != Vector3.zero)
            playerShip.transform.rotation = Quaternion.LookRotation(prevSceneLoc - playerShip.transform.position); ;

        float warpTime = 0f;
        float startFOV = 167f;  
        float endFOV = 60f;
        float totalWarpTime = 5f;
        var emission = QTParticles.emission;
        float startParticles = emission.rateOverTime.constant;
        while (warpTime < totalWarpTime)
        {
            MainCam.fieldOfView = Mathf.Lerp(startFOV, endFOV, warpTime / totalWarpTime);
            emission.rateOverTime = Mathf.Lerp(startParticles, 0, warpTime / totalWarpTime);
            playerShip.transform.position = Vector3.Lerp(playerShip.transform.position, prevSceneLoc, QTSpeed * Time.deltaTime);

            Debug.Log(MainCam.fieldOfView);

            warpTime += Time.deltaTime;
            yield return null;
        }

        warping = false;
        loadingQT = false;
        QTParticles.gameObject.SetActive(false);
        _input.SetShipControls();
    }



    #endregion
}
