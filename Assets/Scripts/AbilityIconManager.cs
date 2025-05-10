using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconManager : MonoBehaviour
{
    public Image abilityIconImage; 

    // Enum-Sprite map
    private Dictionary<ElementalEffect.StatusEffect, string> spriteNames = new Dictionary<ElementalEffect.StatusEffect, string>()
    {
        { ElementalEffect.StatusEffect.Burn, "Fire_Element" },
        { ElementalEffect.StatusEffect.Knock, "Earth_Element" },
        { ElementalEffect.StatusEffect.Shock, "Lightning_Element" },
        { ElementalEffect.StatusEffect.Slow, "Water_Element" },
        { ElementalEffect.StatusEffect.Joker, "Light_Element" }
    };

    public void SetAbilityIcon(ElementalEffect.StatusEffect effect)
    {
        string spriteName = spriteNames.ContainsKey(effect) ? spriteNames[effect] : "Light_Element";
        Sprite s = Resources.Load<Sprite>("UI/" + spriteName); 

        if (s != null)
            abilityIconImage.sprite = s;
        else
            Debug.LogWarning("Sprite not found for " + effect.ToString());
    }
}
