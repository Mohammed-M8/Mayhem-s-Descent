using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;

    private int currentPoints = 0;

    public static PointsManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int amount)
    {
        currentPoints += amount;
        UpdateUI();
    }

  

    void UpdateUI()
    {
        if (pointsText != null)
            pointsText.text = currentPoints.ToString();
    }


    public bool SpendPoints(int amount)
    {
        if (currentPoints >= amount)
        {
            currentPoints -= amount;
            UpdateUI(); // optional: update a TMP text
            return true;
        }

        return false; // not enough points
    }
}
