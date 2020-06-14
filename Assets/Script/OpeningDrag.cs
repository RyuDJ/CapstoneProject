using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region [스크립트 설명]

/*
 * 
 * 오프닝에서 화면에 나와있는 물건들을 옮기기 위해선
 * 다음과 같은 스크립트로 손으로 드래그한 것을 인식해야합니다.
 * 
*/

#endregion

public class OpeningDrag : MonoBehaviour
{
    int c = 0;
    Vector3 input, output;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FullGame.OpeningSelection && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    input = touch.position;
                    break;
                case TouchPhase.Ended:
                    output = touch.position;

                    if (input.x - output.x > 150)
                    {
                        FullGame.DragLeft = true;
                        FullGame.OpeningSelection = false;
                    }

                    else if (input.x - output.x < -150)
                    {
                        FullGame.DragRight = true;
                        FullGame.OpeningSelection = false;
                    }

                    break;
                default:
                    break;
            }
        }

        else if (FullGame.OpeningSelection)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                FullGame.DragLeft = true;
                FullGame.OpeningSelection = false;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                FullGame.DragRight = true;
                FullGame.OpeningSelection = false;
            }
        }
    }
}
