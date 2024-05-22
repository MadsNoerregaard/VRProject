using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experiment : MonoBehaviour
{
    private enum States {
        BlackOutState,
        RedOutState,
        GreenOutState,
        InputState,
        WaitState,
        LogState,
        EndState
    }
    private List<(string, int, string)> fullList = new List<(string, int, string)>();
    public BlackoutToggle blackout;
    public GameObject lineUp;
    public GameObject lineWide;
    public DataLogger datalogger;
    public WeightManager weightmanager;
    public TeleportDumbbell teleportDumbbell;
    public Button lighterButton;
    public Button heavierButton;
    public TextMeshProUGUI weightval;
    public int UId = 0;
    private int TrialId = 0;
    private string answer = "yes";
    private States currentState = States.BlackOutState;
    private (string, int, string) previousInput;

    public float buttonCooldown = 2.0f; 

    private string weight;
    private string firstGrip;
    private string secondGrip;

    void Start()
    {
        GenerateRandomizedList();
        datalogger.StartLogging(';', "TestSession", new string[] { "Uid", "TrialID", "Dumbell", "Dumbell Weight", "Grip Type", "Answer" } );
    }

    void Update()
    {
        if (currentState == States.WaitState){
            if (Input.GetKeyDown(KeyCode.Q)) {  
                currentState = States.InputState;
            }
        }
        if (Input.GetKeyDown(KeyCode.O)) {   //Use this to reset current test
            resetScene();
            currentState = States.BlackOutState;
        }
        if (Input.GetKeyDown(KeyCode.P)) {  //Use this to rollback one test (in case of missclick)
            resetScene();
            fullList.Insert(0, previousInput);
            TrialId --;
            currentState = States.BlackOutState;
        }  
        if (fullList.Count <= 0){
            datalogger.StopLogging();
            currentState = States.EndState;
        }
        ExperimentLoop();
    }

    void ExperimentLoop() {
        switch (currentState){
            case States.BlackOutState:
                Debug.Log($"(Real Weight = {fullList[0].Item2}, Grip Type = {fullList[0].Item3})");
                weightval.text = fullList[0].Item2.ToString();
                blackout.StartBlackOut();
                currentState = States.WaitState;
                break;
            case States.InputState:
                weightmanager.ChooseWeight(fullList[0].Item1);
                if(fullList[0].Item3 == "wide"){
                    lineWide.SetActive(true);
                } else {
                    lineUp.SetActive(true);
                }
                blackout.StopBlackOut();
                currentState = States.WaitState;
                break;
            case States.LogState:
                datalogger.Log(UId, TrialId, fullList[0].Item1, fullList[0].Item2, fullList[0].Item3, answer);
                Debug.Log($"(Uid = {UId}, TrialId = {TrialId}, Virtual Weight = {fullList[0].Item1}, Real Weight = {fullList[0].Item2}, Grip Type = {fullList[0].Item3}, Answer = {answer})");
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
        lineWide.SetActive(false);
        lineUp.SetActive(false);
        weightmanager.DestroyExistingWeights();
        teleportDumbbell.Detach();
    }

    void GenerateRandomizedList() {
        System.Random rand = new System.Random();
        int randval = rand.Next(0, 2);
        switch (randval){
            case 0:
                firstGrip = "wide";
                break;
            case 1:
                firstGrip = "up";
                break;

        }
        List<(string, int, string)> list1 = new List<(string, int, string)>();
        List<(string, int, string)> list2 = new List<(string, int, string)>();

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                switch (i) {
                    case 1:
                        weight = "light";
                        break;
                    case 2:
                        weight = "medium";
                        break;
                    case 3:
                        weight = "heavy";
                        break;
                }
                list1.Add((weight, j*65-65, firstGrip));
            }
        }

        if (firstGrip == "wide") {
            secondGrip = "up";
        } else { secondGrip = "wide";}

        for (int i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 7; j++)
            {
                switch (i) {
                    case 1:
                        weight = "light";
                        break;
                    case 2:
                        weight = "medium";
                        break;
                    case 3:
                        weight = "heavy";
                        break;
                }
                list2.Add((weight, j*65-65, secondGrip));
            }
        }

        list1 = ShuffleList(list1);
        list2 = ShuffleList(list2);
        fullList.AddRange(list1);
        fullList.AddRange(list2);
    }

    List<(string, int, string)> ShuffleList(List<(string, int, string)> list)
    {
        List<(string, int, string)> shuffledList = new List<(string, int, string)>(list);
        System.Random rng = new System.Random();
        int n = shuffledList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (string, int, string) value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }
        return shuffledList;
    }
}
