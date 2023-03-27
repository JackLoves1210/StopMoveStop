using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNameIndicator : GameUnit
{
    [SerializeField] RectTransform rect;
   
    [SerializeField] TextMeshProUGUI nameTxt;


    [SerializeField] CanvasGroup canvasGroup;

    public Transform target;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;

    Vector3 viewPoint;

    Vector2 viewPointX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointY = new Vector2(0.05f, 0.85f);

    Vector2 viewPointInCameraX = new Vector2(0.075f, 0.925f);
    Vector2 viewPointInCameraY = new Vector2(0.05f, 0.95f);



    private bool IsInCamera => viewPoint.x > viewPointInCameraX.x && viewPoint.x < viewPointInCameraX.y && viewPoint.y > viewPointInCameraY.x && viewPoint.y < viewPointInCameraY.y;

    private void LateUpdate()
    {
        viewPoint = Camera.main.WorldToViewportPoint(target.position);

        nameTxt.gameObject.SetActive(IsInCamera);

        viewPoint.x = Mathf.Clamp(viewPoint.x, viewPointX.x, viewPointX.y);
        viewPoint.y = Mathf.Clamp(viewPoint.y, viewPointY.x, viewPointY.y);

        Vector3 targetSPoint = Camera.main.ViewportToScreenPoint(viewPoint) - screenHalf;
        rect.anchoredPosition = targetSPoint;

    }

    private void OnInit()
    {
        SetScore(0);
        SetColor(new Color(Random.value, Random.value, Random.value, 1));
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        OnInit();
    }

    public void SetScore(int score)
    {
       // scoreTxt.SetText(score.ToString());
    }

    public void SetName(string name)
    {
        nameTxt.SetText(name);
    }

    private void SetColor(Color color)
    {
       // iconImg.color = color;
        nameTxt.color = color;
    }

    public void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
