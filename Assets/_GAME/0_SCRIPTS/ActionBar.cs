using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;
using Zenject;

public class ActionBar : MonoBehaviour
{
    private int slotIndex = 0;
    private int figurinsCountCombined = 0;
    private ComboChecker comboChecker;
    private List<int> matchedIndexes;
    private int maxFigurinesCount;
    private int comboLength = 3;
    private GameSettings _gameSettings;
    private List<Figurine> figurinesInBar;
    private int maxSlotIndex;

    [SerializeField] private float attractAnimationDuration = 1;
    [SerializeField] Transform[] snapPoints;

    public static event Action OnSlotsEndedEvent;
    public static event Action OnFigurinesEndedEvent;

    [Inject]
    public void Construct(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }

    private void Awake()
    {
        comboChecker = new ComboChecker();
        maxSlotIndex = snapPoints.Length-1;
        figurinesInBar = new List<Figurine>();
    }

    public void AttractFigurine(Figurine figurine)
    {       
        if (slotIndex<=maxSlotIndex)
        {
            var tweener = figurine.transform.DOMove(snapPoints[slotIndex].position, attractAnimationDuration) ;
            figurine.transform.DOScale(0.5f, attractAnimationDuration);
            figurine.transform.DORotateQuaternion(Quaternion.identity, attractAnimationDuration);            
            slotIndex++;         
            tweener.OnComplete(() => CheckCombo(figurine));
        }
        else 
        {
            Debug.Log($"Not enought slots - Game over{this}");
        }
    }

    public bool IsEnoughSlots()
    {
        return slotIndex <= maxSlotIndex;
    }

    private void CheckCombo(Figurine figurine)
    {

        figurinesInBar.Add(figurine);
        bool isfound = CheckCombination(figurine);
        if (isfound)
        {
            figurinsCountCombined += comboLength;
            RemoveMatched();
            MoveFigurines();
            if (figurinsCountCombined >= maxFigurinesCount) OnFigurinesEndedEvent?.Invoke();
        }
        if (!isfound && slotIndex > maxSlotIndex) OnSlotsEndedEvent?.Invoke();
    }

    private void MoveFigurines()
    {
        slotIndex = 0;
        List<Figurine> activeInBar = new List<Figurine>();
        foreach(var figurine in figurinesInBar)
        {
            if(figurine.isActiveAndEnabled)
            {
                activeInBar.Add(figurine);
            }
        }
        figurinesInBar = new List<Figurine>();
        foreach (var figurine in activeInBar)
        { 
            AttractFigurine(figurine);
        }
    }

    private void RemoveMatched()
    {
        if (matchedIndexes.Count>0)
        {
            for (int i = 0; i < matchedIndexes.Count; i++)
            {
                figurinesInBar[matchedIndexes[i]].gameObject.SetActive(false);
            } 
        }
    }

    /// <summary>
    /// проверка на комбинацию
    /// </summary>
    /// <returns></returns>
    private bool CheckCombination(Figurine figurine)
    {
        var match = comboChecker.CheckForCombo(figurinesInBar, comboLength,out matchedIndexes);
        return match;
    }

    public void Reset()
    {
        figurinesInBar.Clear();
        slotIndex = 0;
        maxFigurinesCount = ReturnRemainingUnique() * comboLength;
        figurinsCountCombined = 0;
    }

    public int ReturnRemainingUnique()
    {        
        var remain = (maxFigurinesCount - figurinsCountCombined) / comboLength;        
        return remain;
    }

    internal void Activate()
    {
        maxFigurinesCount = _gameSettings.uniqueFirurinesCount * comboLength;
    }
}
