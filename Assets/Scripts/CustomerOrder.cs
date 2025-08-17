using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderItem
{
    public Crop crop;
    public int quantity = 1;
    public int receivedQuantity = 0;
}

public class CustomerOrderList
{
    public float totalTime;
    [HideInInspector] public float remainingTime;
    public List<CustomerOrderItem> items = new List<CustomerOrderItem>();
}

public class CustomerOrder : MonoBehaviour
{
    [SerializeField] private Image m_timeDisplay;
    public CustomerOrderList order { get; private set; } = new CustomerOrderList();

    public void Setup(CustomerOrderList order)
    {
        this.order = order;
    }

    public void StartTimer(OrderQueue queue)
    {
        LeanTween.value(gameObject, order.totalTime, 0.0f, order.totalTime).setOnUpdate((float val) =>
        {
            m_timeDisplay.fillAmount = val / order.totalTime;
            order.remainingTime = val;
        }).setOnComplete(queue.NotifyTimerEnded);
    }
}
