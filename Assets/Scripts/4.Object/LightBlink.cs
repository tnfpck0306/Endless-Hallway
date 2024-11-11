using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������ �� ���θ޴� �̺�Ʈ�� ���� ��ũ��Ʈ
/// </summary>
public class LightBlink : MonoBehaviour
{
    public GameObject spotLight;
    public GameObject footprint;
    public GameObject door;
    public GameObject eyes;
    public GameObject monster;

    private bool isWork = false; // �۵� ����
    private float timer = 0; // ������ Ÿ�̸�
    private int count; // �̻����� ȿ�� Ÿ�̹�

    private void Start()
    {
        count = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3 && !isWork)
        {
            isWork = true;
            StartCoroutine(blink());
        }
    }

    IEnumerator blink()
    {
        spotLight.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(false);

        count++;
        footprintEffect();

        yield return new WaitForSeconds(0.1f);

        spotLight.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        eyes.SetActive(true);
        spotLight.SetActive(false);

        yield return new WaitForSeconds(2f);

        eyes.SetActive(false);
        spotLight.SetActive(true);

        timer = 0;
        isWork = false;
    }

    private void footprintEffect()
    {
        if (footprint.activeSelf)
        {
            door.SetActive(true);
            monster.SetActive(false);
            footprint.SetActive(false);
        }
        else
        {
            door.SetActive(false);
            monster.SetActive(true);
            footprint.SetActive(true);
        }
    }
}
