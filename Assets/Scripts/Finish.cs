using UnityEngine;
public class Finish : MonoBehaviour
{
    [SerializeField] private Canvas endLevel;
    [SerializeField] private Canvas failLevel;
    [SerializeField] private Canvas winGame;

    public void EndLevelUI()
    {
        endLevel.gameObject.SetActive(!endLevel.gameObject.activeSelf);
    }
    public void FailUI()
    {
        failLevel.gameObject.SetActive(!failLevel.gameObject.activeSelf);
    }
    public void WinUI()
    {
        winGame.gameObject.SetActive(!winGame.gameObject.activeSelf);
    }
}