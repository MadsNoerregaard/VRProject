using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experiment : MonoBehaviour
{
    private enum States {
        BlackOutState,
        InputState,
        WaitState,
        LogState,
        EndState
    }
    private List<(int, int, int)> fullList = new List<(int, int, int)>();
    public BlackoutToggle blackout;
    public GameObject orbWide;
    public GameObject lineWide;
    public GameObject orbUp;
    public GameObject lineUp;
    public DataLogger datalogger;
    public WeightManager weightmanager;
    public Button lighterButton;
    public Button heavierButton;
    public int UId = 0;
    private int TrialId = 0;
    private string answer = "yes";
    private States currentState = States.BlackOutState;
    private (int, int, int) previousInput;

    public float buttonCooldown = 2.0f; 

    void Start()
    {
        GenerateRandomizedList();
        lighterButton.onClick.AddListener(OnLighterButtonClick);
        heavierButton.onClick.AddListener(OnHeavierButtonClick);
    }

    void Update()
    {
        if (currentState == States.WaitState){
            if (Input.GetKeyDown(KeyCode.Q)) {  
                currentState = States.InputState;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {   //Use this to reset current test
                currentState = States.BlackOutState;
            }
        if (Input.GetKeyDown(KeyCode.Y)) {  //Use this to rollback one test (in case of missclick)
            fullList.Insert(0, previousInput);
            TrialId --;
            currentState = States.BlackOutState;
        }  
        if (fullList.Count <= 0){
            currentState = States.EndState;
        }
        ExperimentLoop();
    }

    void ExperimentLoop() {
        switch (currentState){
            case States.BlackOutState:
                Debug.Log($"(Real Weight = {fullList[0].Item2})");
                blackout.StartBlackOut();
                currentState = States.WaitState;
                break;
            case States.InputState:
                weightmanager.ChooseWeight(fullList[0].Item1);
                if(fullList[0].Item3 == 0){
                    orbWide.SetActive(true);
                    lineWide.SetActive(true);
                } else {
                    orbUp.SetActive(true);
                    lineUp.SetActive(true);
                }
                blackout.StopBlackOut();
                currentState = States.WaitState;
                break;
            case States.LogState:
                datalogger.Log(UId, TrialId, fullList[0].Item1, fullList[0].Item2, fullList[0].Item3, answer);
                Debug.Log($"(Uid = {UId}, TrialId = {TrialId}, Virtual Weight = {fullList[0].Item1}, Real Weight = {fullList[0].Item2}, Grip Type = {fullList[0].Item3}, Answer = {answer})");
                Debug.Log($"(List Count = {fullList.Count})");
                previousInput = fullList[0];
                fullList.RemoveAt(0);
                TrialId++;
                resetScene();
                currentState = States.BlackOutState;
                break; 
            case States.EndState:
                blackout.EndExperiment();
                blackout.StartBlackOut();
                break;  
        } 
    }

    public void OnLighterButtonClick(){
        if (lighterButton.interactable) {
            answer = "lighter";
            currentState = States.LogState;
            StartCoroutine(ButtonCooldown());
        }
    }

    public void OnHeavierButtonClick(){
        if (heavierButton.interactable) {
            answer = "heavier";
            currentState = States.LogState;
            StartCoroutine(ButtonCooldown());
        }
    }

    IEnumerator ButtonCooldown(){
        lighterButton.interactable = false;
        heavierButton.interactable = false;
        yield return new WaitForSeconds(buttonCooldown);
        lighterButton.interactable = true;
        heavierButton.interactable = true;
    }

    void resetScene(){
        orbWide.SetActive(false);
        lineWide.SetActive(false);
        orbUp.SetActive(false);
        lineUp.SetActive(false);
        weightmanager.DestroyExistingWeights();
    }

    void GenerateRandomizedList() {
        System.Random rand = new System.Random();
        int firstGrip = rand.Next(0, 2); 
        List<(int, int, int)> list1 = new List<(int, int, int)>();
        List<(int, int, int)> list2 = new List<(int, int, int)>();

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                list1.Add((i, j, firstGrip));
            }
        }

        int secondGrip = firstGrip == 0 ? 1 : 0;

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                list2.Add((i, j, secondGrip));
            }
        }

        list1 = ShuffleList(list1);
        list2 = ShuffleList(list2);
        fullList.AddRange(list1);
        fullList.AddRange(list2);
    }

    List<(int, int, int)> ShuffleList(List<(int, int, int)> list)
    {
        List<(int, int, int)> shuffledList = new List<(int, int, int)>(list);
        System.Random rng = new System.Random();
        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (int, int, int) value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }
        return shuffledList;
    }
}
