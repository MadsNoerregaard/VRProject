using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Experiment : MonoBehaviour
{
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


    void Start()
    {
        GenerateRandomizedList();
        lighterButton.onClick.AddListener(OnLighterButtonClick);
        heavierButton.onClick.AddListener(OnHeavierButtonClick);
    }

    void Update()
    {
        
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

        foreach (var tuple in fullList)
        {
            Debug.Log($"(Visual Weight = {tuple.Item1}, Real Weight = {tuple.Item2}, Grip Type = {tuple.Item3})");
        }
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

    void ExperimentLoop() {
        blackout.StartBlackOut();
        //wait for keypress
        weightmanager.ChooseWeight(fullList[0].Item1);
        if(fullList[0].Item3 == 0){
            orbWide.SetActive(true);
            lineWide.SetActive(true);
        } else {
            orbUp.SetActive(true);
            lineUp.SetActive(true);
        }
        blackout.StopBlackOut();
        //wait for user answer
        datalogger.Log(UId, TrialId, fullList[0].Item1, fullList[0].Item2, fullList[0].Item3, answer);
        fullList.RemoveAt(0);
        TrialId ++;
        resetScene();
    }
    void OnLighterButtonClick(){
        answer = "lighter";
    }
    void OnHeavierButtonClick(){
        answer = "heavier";
    }
    void resetScene(){
        orbWide.SetActive(false);
        lineWide.SetActive(false);
        orbUp.SetActive(false);
        lineUp.SetActive(false);
        weightmanager.DestroyExistingWeights();
    }
}