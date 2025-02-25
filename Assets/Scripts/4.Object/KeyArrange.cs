using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 동아리방 키 배치 스크립트
/// </summary>
public class KeyArrange : MonoBehaviour
{
    public Transform[] keySpaces; // 키가 배치될 transform
    public LockerMonsterTrigger lockerMonster;

    [SerializeField] private int num;

    void Start()
    {
        Arrangement();
    }

    // 블루키 배치 로직
    private void Arrangement()
    {
        num = Random.Range(0, keySpaces.Length - 1);

        if (GameManager.instance.anomalyNum == 0)
        {
            num = 15;
        }
        else if (GameManager.instance.anomalyNum == 14)
        {
            num = 29;
        }
        else if (GameManager.instance.anomalyNum == 17)
        {
            num = 28;
            StartCoroutine(KeySFX());
        }

        Vector3 index = keySpaces[num].position;
        Quaternion indexRotation = keySpaces[num].rotation;

        // 사물함 안에 열쇠를 배치할 때
        if (num < 15)
        {
            index.x += 0.15f;
            index.z -= 0.1f;
        }
        else if(num < 29){
            index.y -= 0.05f;
            transform.rotation = indexRotation;
        }

        index.y += 0.05f;
        transform.position = index;

    }

    IEnumerator KeySFX()
    {
        while (true)
        {
            if(lockerMonster.check)
                GetComponent<AudioSource>().Play();

            yield return new WaitForSeconds(3f);
        }

    }
}
