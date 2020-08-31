using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private Action OnBuildAreaHandler;
    private Action OnCancleActionHandler;


    public Button buildResidentialAreaBtn;
    public Button cancleActionBtn;
    public GameObject cancleActionPanel;

    // Start is called before the first frame update
    void Start()
    {
        cancleActionPanel.SetActive(false);
        buildResidentialAreaBtn.onClick.AddListener(OnBuildAreaCallback);
        cancleActionBtn.onClick.AddListener(OnCancleActionCallback);;


    }

    private void OnBuildAreaCallback()
    {
        cancleActionPanel.SetActive(true);
        OnBuildAreaHandler?.Invoke();
    }

    private void OnCancleActionCallback()
    {
        cancleActionPanel.SetActive(false);
        OnCancleActionHandler?.Invoke();
    }

    public void AddListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler += listener;
    }

    public void RemoveListenerOnBuildAreaEvent(Action listener)
    {
        OnBuildAreaHandler -= listener;
    }
    public void AddListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler += listener;
    }

    public void RemoveListenerOnCancleActionEvent(Action listener)
    {
        OnCancleActionHandler -= listener;
    }

}
