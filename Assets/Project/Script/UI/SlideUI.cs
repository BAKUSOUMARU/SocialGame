using System;
using UnityEngine;
using UnityEngine.UI;

public class SlideUI : MonoBehaviour
{
    public GameObject[] slideElements;  // スライドUIの要素配列
    private int currentIndex = 0;       // 現在のインデックス

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;


    private void FixedUpdate()
    {
        Flick();
    }


    void Flick(){
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            touchStartPos = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)){
            touchEndPos = new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                Input.mousePosition.z);
            GetDirection();
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction = null;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        else
        {
            //タッチを検出
            Direction = "touch";
        }
        switch (Direction)
        {
            case "right":
                //右フリックされた時の処理
                SlideRight();
                break;

            case "left":
                //左フリックされた時の処理
                SlideLeft();
                break;

            case "touch":
                //タッチされた時の処理
                break;
        }
    }   

    
    private void OnEnable()
    {
        ShowSlideElement(currentIndex);
    }

    public void SlideRight()
    {
        currentIndex++;
        if (currentIndex >= slideElements.Length)
        {
            currentIndex = 0;
        }
        ShowSlideElement(currentIndex);
    }

    public void SlideLeft()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = slideElements.Length - 1;
        }
        ShowSlideElement(currentIndex);
    }

    private void ShowSlideElement(int index)
    {
        // すべてのスライド要素を非表示にする
        foreach (GameObject element in slideElements)
        {
            element.SetActive(false);
        }

        // 指定されたインデックスのスライド要素を表示する
        slideElements[index].SetActive(true);
    }
}