using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public Vehicle[] vehicles;          

    public Car car;                         // ������Ʈ ����
    public Bicycle bicycle;                 // ������Ʈ ����

    float Timer;                            // Ÿ�̸� ����


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i< vehicles.Length; i++)
        {
            vehicles[i].Move();
        }
    //    car.Move();
    //    bicycle.Move();
        Timer -= Time.deltaTime;            // Ÿ�̸� ī��Ʈ�� �Ѵ�.
        if(Timer <= 0)
        {

            //car.Horn();
            //bicycle.Horn();

            Timer = 1.0f;                // 1�ʷ� ������ش�.
        }
    }
}
