using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;

public class GazeSelect : MonoBehaviour
{
    public event Action OnGazeSelection;

    public static GazeSelect Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] private GameObject gazeBarCanvas;
    [SerializeField] Image fillIndicator;
    [Tooltip("Time in seg")]
    [SerializeField] private float timeForSelection = 2.5f;

    private float timeCounter;
    private float timeProggres;
    private bool runTimer;
    private Color active = Color.yellow;// new Color(1, 1, 1);
    private Color resting = Color.red;// new Color(1, 1, 1);
    void Start()
    {
        gazeBarCanvas.SetActive(false);
        fillIndicator.fillAmount = Normalise();
        fillIndicator.color = active;
    }


    public void Update()
    {
        if (runTimer)
        {
            timeProggres += Time.deltaTime;
            AddValue(timeProggres);
        }
    }
    public void SetUpGaze(float timeForSelection)
    {
        this.timeForSelection = timeForSelection;
    }
    public void StartGazeSelection()
    {
        gazeBarCanvas.SetActive(true);
        runTimer = true;
        timeProggres = 0;
    }

    public void CancelGazeSelection()
    {
        gazeBarCanvas.SetActive(false);
        runTimer = false;
        timeProggres = 0;
        timeCounter = 0;
        fillIndicator.color = active;
    }

    private void AddValue(float val)
    {
        timeCounter = val;
        if (timeCounter >= timeForSelection)
        {
            timeCounter = 0;
            runTimer = false;
            OnGazeSelection?.Invoke();
            timeProggres = 0;
        }

        fillIndicator.fillAmount = Normalise();
    }
    private float Normalise()
    {
        return (float)timeCounter / timeForSelection;
    }
}
