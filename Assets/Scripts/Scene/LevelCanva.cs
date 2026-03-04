using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelCanva : MonoBehaviour
{
    [SerializeField] List<GameObject> levelButtons; //순서가 일치해야 함
    private void Start()
    {
        initializeButton();
        this.gameObject.SetActive(false);
    }

    public void initializeButton()
    {
        Debug.Log("curData maxReachedStage : " + SceneLoader.Instance.Data.maxReachedStage);
        for(int i = 0; i < SceneLoader.Instance.Data.maxReachedStage; i++)
        {
            levelButtons[i].GetComponent<Image>().color = Color.white;
            levelButtons[i].GetComponent<Button>().interactable = true;
        }
        for(int i = SceneLoader.Instance.Data.maxReachedStage; i < levelButtons.Count; i++)
        {
            levelButtons[i].GetComponent<Image>().color = Color.gray2;
            levelButtons[i].GetComponent<Button>().interactable = false;
        }
    }
}
