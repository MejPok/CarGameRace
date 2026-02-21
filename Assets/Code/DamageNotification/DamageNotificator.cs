using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNotificator : MonoBehaviour
{
    public static DamageNotificator instance;
    public GameObject notification;

    void Start()
    {
        instance = this;
    }

    public void CreateDamageNotification(Vector2 pos, int damage)
    {
        System.Random random = new System.Random();
        GameObject notificationInstance = Instantiate(notification, pos + new Vector2(random.Next(0,2) / 10 , random.Next(0,2) / 10), Quaternion.identity);
        notificationInstance.transform.GetChild(0).GetComponent<TextMeshPro>().text = $"- {damage}";
        Destroy(notificationInstance, 2f);
    }


}
