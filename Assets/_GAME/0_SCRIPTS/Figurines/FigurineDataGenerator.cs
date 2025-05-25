using System.Collections.Generic;
using UnityEngine;

public class FigurineDataGenerator
{
    /// <summary>
    /// Генерирует список уникальных фигурок, по 3 штуки каждой.
    /// </summary>
    /// <param name="uniqueCount"></param>
    public List<FigurineData> Generate(int uniqueCount)
    {
        // Получаем все возможные комбинации
        var allCombos = new List<FigurineData>();
        foreach (ShapeType s in System.Enum.GetValues(typeof(ShapeType)))
            foreach (BackColor c in System.Enum.GetValues(typeof(BackColor)))
                foreach (AnimalType a in System.Enum.GetValues(typeof(AnimalType)))
                    allCombos.Add(new FigurineData(s, c, a));

        // Перетасовка
        int maxUnique = allCombos.Count;
        if (uniqueCount > maxUnique) uniqueCount = maxUnique;
        var rnd = new System.Random();
        // Фишка Фишера-Йейтса
        for (int i = maxUnique - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            var temp = allCombos[i]; allCombos[i] = allCombos[j]; allCombos[j] = temp;
        }

        // берем нужное количество уникальных и утраиваем
        var result = new List<FigurineData>();
        for (int i = 0; i < uniqueCount; i++)
        {
            for (int k = 0; k < 3; k++) result.Add(allCombos[i]);
        }

        // Перетасовка повторно
        int n = result.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            var tmp = result[i]; result[i] = result[j]; result[j] = tmp;
        }

        return result;
    }
}
