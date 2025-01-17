using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
public Button startButton;
public Button endButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && LogicScript.sceneRolling){
            startButton.onClick.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !LogicScript.sceneRolling){
            endButton.onClick.Invoke();
        }
    }
}
