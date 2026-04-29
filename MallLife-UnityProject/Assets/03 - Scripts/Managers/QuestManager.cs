using System;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Inputs.InputMenus _inputMenus;
    [SerializeField] private UIViewQuestResolve _viewQuestResolve;

    [Header("Events")]
    [SerializeField] private EventChannelVoidSO _onQuestResolveStarted;
    
    private void OnEnable()
    {
        _viewQuestResolve.gameObject.SetActive(true);
        _viewQuestResolve.OnExit += Cancel;
        _viewQuestResolve.OnConfirm += Confirm;
        _onQuestResolveStarted.OnEventRaised += () => _viewQuestResolve.Show();
        _inputMenus.Cancel.OnUp += Cancel;
        _inputMenus.Confirm.OnUp += Confirm;
    }
    private void OnDisable()
    {
        _viewQuestResolve.gameObject.SetActive(false);
        _viewQuestResolve.OnExit -= Cancel;
        _viewQuestResolve.OnConfirm -= Confirm;
        _onQuestResolveStarted.OnEventRaised -= () => _viewQuestResolve.Show();
        _inputMenus.Cancel.OnUp -= Cancel;
        _inputMenus.Confirm.OnUp -= Confirm;
    }
    private void Start()
    {
        _viewQuestResolve.Hide();
    }
    
    private void Confirm()
    {
        _viewQuestResolve.Hide();
    }
    private void Cancel()
    {
        _viewQuestResolve.Hide();
    }

}
