using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerController : MonoBehaviour
{
    private int destroyCount = 0; // 用于跟踪破坏的数量
    public int upgradeThreshold = 3; // 指定升级的破坏阈值
    public GameObject upgradeCanvas; // 在 Inspector 中分配 PlayerFollowCamera 下的 Canvas

    private void Start()
    {
        // 订阅 CubeDestroyer 的 Cube 被销毁事件
        CubeDestroyer cubeDestroyer = FindObjectOfType<CubeDestroyer>();
        if (cubeDestroyer != null)
        {
            cubeDestroyer.onCubeDestroyed.AddListener(HandleCubeDestroyed);
            Debug.Log("Event subscribed successfully.");
        }
        else
        {
            Debug.LogError("CubeDestroyer not found!"); // 添加这一行以检查是否找到了 CubeDestroyer
        }
    }

    private void OnDestroy()
    {
        // 在 OnDestroy 方法中取消订阅事件，以防止内存泄漏
        CubeDestroyer cubeDestroyer = FindObjectOfType<CubeDestroyer>();
        if (cubeDestroyer != null)
        {
            cubeDestroyer.onCubeDestroyed.RemoveListener(HandleCubeDestroyed);
        }
    }

    public void HandleCubeDestroyed()
    {
        destroyCount++;
        Debug.Log("Good");

        if (destroyCount >= upgradeThreshold)
        {
            LevelUp();

            destroyCount = 0; // 重置计数器
        }
    }

    public void LevelUp()
    {
        switch (this.gameObject.tag)
        {
            case "Level1":
                this.gameObject.tag = "Level2";
                break;
            case "Level2":
                this.gameObject.tag = "Level3";
                break;
            case "Level3":
                // 你可以决定在这里做什么，比如不做任何事情或输出信息等。
                break;
        }

        // 打开 Canvas
        if (upgradeCanvas != null)
        {
            upgradeCanvas.SetActive(true);

            // 延迟两秒后关闭 Canvas
            StartCoroutine(CloseUpgradeCanvas());
        }
    }

    private IEnumerator CloseUpgradeCanvas()
    {
        yield return new WaitForSeconds(2f);

        // 关闭 Canvas
        if (upgradeCanvas != null)
        {
            upgradeCanvas.SetActive(false);
        }
    }
}
