using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public FloatValue heartContainers;

    // Start is called before the first frame update
    void Start()
    {
        SetHeart((int)heartContainers.runtimeValue);
    }

    public void SetHeart(int heartCount)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(i < heartCount);
            if (i < heartCount)
            {
                if (heartCount % 2 == 0 || i < heartCount - 1)
                {
                    hearts[i].sprite = fullHeart; // Full heart for even counts or all but the last heart
                    i++;
                }
                else
                {
                    hearts[i].sprite = halfHeart; // Half heart for the last heart if count is odd
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Empty heart for hearts beyond the count
            }
        }
    }

    public void UpdateHeart()
    {
        SetHeart((int)heartContainers.runtimeValue);
        Debug.Log("Heart count updated to: " + heartContainers.runtimeValue);
    }
}
