using TMPro;
using UnityEngine;
using Decker;
using System.Collections.Generic;

public class CardUI_sc : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardText;
    [SerializeField]
    private TextMeshProUGUI cardName;

    public void SetCardName(string name)
    {
        cardName.text = name;
    }

    public void SetCardText(List<EffectLogic_SO> effects, List<Keyword> keywords)
    {
        cardText.text = "";
        int i = 0;
        if (!keywords.Contains(Keyword.Miss))
        {
            foreach (var e in effects)
            {
                if (i == effects.Count - 1)
                    cardText.text += e.Text;
                else
                    cardText.text += e.Text + "\n";
                i++;
            }
            if (keywords.Count > 0)
                cardText.text += "\n";
        }

        i = 0;
        foreach (var k in keywords)
        {
            if (i == keywords.Count - 1)
                cardText.text += "<b>" + k.ToString() + "</b>";
            else
                cardText.text += "<b>" + k.ToString() + "</b>" + "\n";
            i++;
        }
    }
}
