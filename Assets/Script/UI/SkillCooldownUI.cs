using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("Cooldown Visuals")]
    public Image cooldownOverlay;
    public TextMeshProUGUI cooldownText;

    [Header("Stack Visuals")]
    public TextMeshProUGUI stackText;
    public int maxStack = 1;
    private int currentStack = 1;

    [Header("Settings")]
    public bool showDecimal = false;

    private float cooldownTime = 0f;
    private float remainingTime = 0f;
    private bool isCooling = false;

    public bool IsColling => isCooling;
    public int CurrentStack => CurrentStack;

    public void StartCooldown(float time)
    {
        cooldownTime = time;
        remainingTime = time;
        isCooling = true;

        cooldownOverlay.fillAmount = 1f;
        cooldownOverlay.gameObject.SetActive(true);
        cooldownText.gameObject.SetActive(true);
        UpdateText();
    }

    private void Awake()
    {
        if (cooldownOverlay) cooldownOverlay.gameObject.SetActive(false);
        if (cooldownText) cooldownText.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (stackText != null && stackText.transform.parent != null)
        {
            stackText.transform.parent.gameObject.SetActive(maxStack >= 2);
        }
    }

    private void Update()
    {
        if (!isCooling) return;

        remainingTime -= Time.deltaTime;
        float ratio = Mathf.Clamp01(remainingTime / cooldownTime);

        cooldownOverlay.fillAmount = ratio;
        UpdateText();

        if (remainingTime <= 0f)
        {
            EndCooldown();
        }
    }

    private void UpdateText()
    {
        if (!cooldownText) return;

        if (remainingTime <= 0)
        {
            cooldownText.text = "";
        }
        else
        {
            cooldownText.text = showDecimal
                ? remainingTime.ToString("F1")
                : Mathf.CeilToInt(remainingTime).ToString();
        }

        if (stackText)
        {
            stackText.text = $"{currentStack}";
        }
    }

    private void EndCooldown()
    {
        isCooling = false;
        cooldownOverlay.gameObject.SetActive(false);
        cooldownText.gameObject.SetActive(false);

        if(currentStack < maxStack)
        {
            currentStack++;
            UpdateText();
        }
    }

    public bool TryUseSkill(float cooldownTime)
    {
        if (currentStack <= 0 || isCooling) return false;

        currentStack--;
        UpdateText();
        StartCooldown(cooldownTime);
        return true;
    }

    public void RefillStack() => SetStack(maxStack);

    public void SetStack(int value)
    {
        currentStack = Mathf.Clamp(value, 0, maxStack);
        UpdateText();

        if(stackText != null && stackText.transform.parent != null)
        {
            bool showStack = maxStack >= 2;
            stackText.transform.parent.gameObject.SetActive(showStack);
        }
    }

}
