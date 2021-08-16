using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;

    [SerializeField] TextMeshProUGUI tmp;
    void Awake()
    {
        currentBalance = startingBalance;
    }
    void Start()
    {
        tmp.text = "gold : " + currentBalance;
    }
    public int CurrentBalance{get{return currentBalance;}}

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        OnChange();
    }
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        OnChange();
        if(currentBalance < 0)
        {
            // Lost the game
            ReloadScene();
        }
    }

    void OnChange()
    {
        tmp.text = "gold : " + currentBalance;
    }
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
