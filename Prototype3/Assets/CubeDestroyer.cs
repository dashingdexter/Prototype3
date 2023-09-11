using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeDestroyer : MonoBehaviour
{
    public int cubeLevel = 1; // 在Inspector中设置Cube的级别
    public GameObject smallCubePrefab; // 这是你的小方块预制体
    public int numberOfSmallCubes = 30; // 生成的小方块数量

    // 添加一个事件来表示Cube被销毁
    public UnityEvent onCubeDestroyed = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        int otherObjectLevel = 0;
        if (Input.GetMouseButton(0))
        {
            if (other.gameObject.CompareTag("Level1"))
            {
                otherObjectLevel = 1;
                
            }
            else if (other.gameObject.CompareTag("Level2"))
            {
                otherObjectLevel = 2;
                
            }
            else if (other.gameObject.CompareTag("Level3"))
            {
                otherObjectLevel = 3;
               
            }

            if (otherObjectLevel >= cubeLevel)
            {
                DestroyCube();
            }
        }
    }

    public void DestroyCube()
    {
        // 触发Cube被销毁事件
        if (onCubeDestroyed != null)
        {
            onCubeDestroyed.Invoke();
        }

        // 获取原始Cube的尺寸
        Vector3 originalSize = transform.localScale;

        // 计算小方块的尺寸
        Vector3 smallCubeSize = originalSize / 3f;

        for (int i = 0; i < numberOfSmallCubes; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-smallCubeSize.x, smallCubeSize.x), Random.Range(-smallCubeSize.x, smallCubeSize.x), Random.Range(-smallCubeSize.x, smallCubeSize.x));
            GameObject smallCube = Instantiate(smallCubePrefab, transform.position + randomOffset, Quaternion.identity);

            // 设置小方块的尺寸
            smallCube.transform.localScale = smallCubeSize;

            Rigidbody rb = smallCube.GetComponent<Rigidbody>();
            if (rb)
            {
                //rb.AddExplosionForce(500, transform.position, 5);
            }
        }

        // 销毁原始Cube
        Destroy(gameObject);
        AudioManager.PlayironDestroyAudio();
    }
}


