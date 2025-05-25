using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboChecker 
{    
    private Dictionary<Figurine, List<int>> countedMatching;
    private List<Figurine> checkedFigurinees;
    private bool isMatchFound = false;

    public bool CheckForCombo(List<Figurine> unsortedFigurinesList, int comboLength, out List<int> matchedIndexes)
    {
        isMatchFound = false;
        matchedIndexes = new List<int>();
        countedMatching = new Dictionary<Figurine, List<int>>();

        for (int i = 0; i < unsortedFigurinesList.Count; i++)  // Пробегаем по всем фигуркам в Экшенбаре
        {
            if(countedMatching.Count == 0) // если это первая проверяемая 
            {
                countedMatching[unsortedFigurinesList[i]] = new List<int>() { 0 }; // создаем первую запись в словаре с этой фигуркой и с её индексом                 
            }
            else // если уже не первая в списке
            {                
                bool isFounded=false;
                foreach (var kvp in countedMatching) // пробегаемся по списку уже обработанных фигурок
                {
                    if (unsortedFigurinesList[i].IsEqual(kvp.Key)) // если текущая фигурка равна какому либо ключу из списка обработанных
                    {
                        kvp.Value.Add(i); // добавляем ее индекс в эту же запись в словаре
                        isFounded = true;                        
                        break;
                    }
                }
                if (!isFounded)
                {
                    countedMatching[unsortedFigurinesList[i]] = new List<int>() { i }; // иначе добавляем новую запись с новой фигуркой и ее индексом                    
                }
            }
        }

        foreach (var entry in countedMatching) // проверяем , есть ли повторения по 3 раза.
        {
            if (entry.Value.Count == comboLength)
            {
                isMatchFound = true;
                matchedIndexes = new List<int>();
                matchedIndexes = entry.Value; // если есть записываем их индексы в out переменную
                break;
            }
        }
            return isMatchFound;
    }
}
