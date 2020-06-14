using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullGame : MonoBehaviour
{
    public static int phase = 0, c = 0, menu_num = 0;
    public Animator[] anim = new Animator[7];
    public Sprite[] menu = new Sprite[7];
    public Image[] menu_image = new Image[3];
    public Text[] menutext = new Text[2];
    public string[] menu_name = new string[3] { "시작하기", "환경설정", "나가기" };
    public static bool DragLeft = false, DragRight = false, OpeningSelection = false;

    public void MenuSelect()
    {
        if(OpeningSelection)
        {
            anim[1].SetInteger("Select_num", menu_num+1);
            OpeningSelection = false;

            switch (menu_num)
            {
                case 0:
                    phase = 1;
                    c = 0;
                    break;
                case 1:
                    break;
                case 2:
                    /*끝내기*/
                    phase = 0;
                    c = 6;
                    break;
                default:
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menu_num = 0;

        menu_image[1].sprite = menu[menu_num % 3];
        menu_image[2].sprite = menu[(menu_num + 1) % 3];
        menu_image[0].sprite = menu[(menu_num + 2) % 3];
    }

    // Update is called once per frame
    void Update()
    {
        #region 시작
        /*
         * 오프닝에 로고(Unity, 개인로고)가 띄워진다. (Canvas Animator-OpeningScene)
         * 로고가 꺼지면 TV가 켜지고, TV에 각 내용이 들어온다.
         */

        if (phase == 0)
        {
            #region 시작
            if (c == 0 && anim[0].GetCurrentAnimatorStateInfo(0).IsName("FinishLogo"))
            {
                anim[3].SetBool("Start", true);
                c = 1;
            }

            else if (c == 1 && anim[3].GetCurrentAnimatorStateInfo(0).IsName("LightOff"))
            {
                anim[1].SetBool("Open", true);
                c = 2;
            }

            else if (c == 2 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("TV_Opening_Start"))
            {
                anim[5].SetInteger("Color", 1);
            }

            else if (c == 2 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("TV_Opening_button"))
            {
                menutext[0].text = menu_name[menu_num];
                anim[3].SetBool("LightOn", true);
                anim[5].SetInteger("Color", 0);
                OpeningSelection = true;
                c = 3;
            }
            #endregion

            #region 메뉴 이동
            else if (c == 3 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("TV_Opening_button_stay"))
            {
                menutext[0].text = menu_name[menu_num];

                menu_image[0].sprite = menu[(menu_num + 2) % 3];
                menu_image[1].sprite = menu[(menu_num + 0) % 3];
                menu_image[2].sprite = menu[(menu_num + 1) % 3];

                if (DragLeft)
                {
                    anim[1].SetBool("PointLeft", true);
                    c = 4;
                }

                else if (DragRight)
                {
                    anim[1].SetBool("PointRight", true);
                    c = 5;
                }
            }

            else if (c == 4 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("TV_Opening_button_Toleft"))
            {
                menu_num++;

                if (menu_num >= 3)
                    menu_num = 0;

                menutext[0].text = menu_name[(menu_num + 2) % 3];
                menutext[1].text = menu_name[menu_num];

                anim[1].SetBool("PointLeft", false);
                DragLeft = false;
                OpeningSelection = true;
                c = 3;
            }

            else if (c == 5 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("TV_Opening_button_Toright"))
            {
                menu_num--;

                if (menu_num < 0)
                    menu_num = 2;

                menutext[0].text = menu_name[(menu_num + 1) % 3];
                menutext[1].text = menu_name[menu_num];

                anim[1].SetBool("PointRight", false);
                DragRight = false;
                OpeningSelection = true;
                c = 3;
            }
            #endregion

            #region 어플리케이션 끝내기
            else if (c == 6 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("ByeBye"))
            {
                menu_image[1].sprite = menu[3];
                anim[3].SetBool("LightOn", false);
                anim[5].SetInteger("Color", 1);
            }

            else if (c == 6 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("Ending"))
            {
                Debug.Log("THE END");
                Application.Quit();
            }
            #endregion
        }

        #endregion

        #region 게임 시작하기 전 프롤로그
        else if (phase == 1)
        {
            if (c == 0 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("Loading"))
            {
                anim[1].SetInteger("Select_num", 0);
                menu_image[1].sprite = menu[4];
                menu_image[0].sprite = menu[5];
                menu_image[2].sprite = menu[6];
                anim[5].SetInteger("Color", 1);
            }

            else if (c == 0 && anim[1].GetCurrentAnimatorStateInfo(0).IsName("Story"))
            {
                anim[5].SetInteger("Color", 0);
                anim[6].SetInteger("State", 1);
                c = 1;
            }

            else if (c == 1 && anim[6].GetCurrentAnimatorStateInfo(0).IsName("Before_Start"))
            {
                anim[5].SetInteger("Color", 2);
                anim[4].SetBool("Show", true);
            }

            else if (c == 1 && anim[6].GetCurrentAnimatorStateInfo(0).IsName("MovingTv"))
            {
                anim[5].SetInteger("Color", 0);
                Texting.textStart = true;
                c = 0;
                phase = 999;
            }
        }
        #endregion

        else if (phase == 2)
        {
            if (c == 0)
            {
                Story.health = 50;
                Story.life = 50;
                Story.psycho = 0;
                Story.health = 0;
                Story.mental = 0;
                Story.hungry = 0;

                for (int i = 0; i < Story.import.Length; i++)
                    Story.import[i] = false;

                for (int i = 0; i < Story.have.Length; i++)
                    Story.have[i] = false;

                anim[5].SetInteger("Color", 1);
                Texting.textStart = false;
                Story.chapter = 1;
                Story.line = 0;
                anim[6].SetInteger("State", 0);
                c = 1;
            }

            else if (c == 1 && anim[6].GetCurrentAnimatorStateInfo(0).IsName("OpenStageByTv"))
            {
                anim[5].SetInteger("Color", 2);
                anim[7].SetInteger("Scene_num", 0);
            }

            else if (c == 1 && anim[7].GetCurrentAnimatorStateInfo(0).IsName("#0"))
            {
                anim[5].SetInteger("Color", 0);
                Texting.textStart = true;
                c = 0;
                phase = 999;
            }
        }

        else if (phase == 3)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 0;
                c = 1;
            }

            else if (c == 1)
            {
                Texting.textStart = true;
                c = 0;
                Debug.Log("ENDING 0");
                phase = 999;
            }
        }

        else if (phase == 4)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 1;
                c = 1;
            }

            else if (c == 1)
            {
                Texting.textStart = true;
                c = 0;
                Debug.Log("ENDING 1");
                phase = 999;
            }
        }

        else if (phase == 5)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 2;
                c = 1;
            }

            else if (c == 1)
            {
                Texting.textStart = true;
                c = 0;
                Debug.Log("ENDING 2");
                phase = 999;
            }
        }

        else if (phase == 6)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 3;
                c = 1;
            }

            else if (c == 1)
            {
                Texting.textStart = true;
                c = 0;
                Debug.Log("ENDING 3");
                phase = 999;
            }
        }

        else if (phase == 7)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 4;
                c = 1;
            }

            else if (c == 1)
            {
                Texting.textStart = true;
                c = 0;
                Debug.Log("ENDING 4");
                phase = 999;
            }
        }

        else if (phase == 8)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 2;
                c = 1;
            }

            else if (c == 1)
            {
                Story.health = 50;
                Story.life = 50;
                Story.psycho = 0;
                Story.health = 0;
                Story.mental = 0;
                Story.hungry = 0;

                for (int i = 0; i < Story.import.Length; i++)
                    Story.import[i] = false;

                for (int i = 0; i < Story.have.Length; i++)
                    Story.have[i] = false;
                Debug.Log("CREDIT");
            }
        }

        else if(phase == 9)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 1;
                Story.line = 0;
                c = 1;
            }

            else if (c == 1)
            {
                Story.health = 50;
                Story.life = 50;
                Story.psycho = 0;
                Story.health = 0;
                Story.mental = 0;
                Story.hungry = 0;

                for (int i = 0; i < Story.import.Length; i++)
                    Story.import[i] = false;

                for (int i = 0; i < Story.have.Length; i++)
                    Story.have[i] = false;
                Texting.textStart = true;
                c = 0;
                phase = 999;
            }
        }

        else if (phase == 10)
        {
            if (c == 0)
            {
                Texting.textStart = false;
                Story.chapter = 2;
                Story.line = 2;
                c = 1;
            }

            else if (c == 1)
            {
                Story.health = 50;
                Story.life = 50;
                Story.psycho = 0;
                Story.health = 0;
                Story.mental = 0;
                Story.hungry = 0;

                for (int i = 0; i < Story.import.Length; i++)
                    Story.import[i] = false;

                for (int i = 0; i < Story.have.Length; i++)
                    Story.have[i] = false;
                Debug.Log("STOPIT");
            }
        }

        else if(phase == 999)
        {
            string health = (Story.health == 0 ? "정상" : (Story.health == 1 ? "물림" : (Story.health == 2 ? "머리 부상" : (Story.health == 3 ? "상체 부상" : (Story.health == 4 ? "다리 부상" : "확인 안 됨"))))
                + (Story.hungry > 30 ? ", 배고픔" : ""));
            string mental = (Story.mental == 0 ? "정상" : (Story.mental == 1 ? "우울함" : (Story.mental == 2 ? "정신분열" : (Story.mental == 3 ? "패닉" : "확인 안 됨"))));

            string import = "", have = "";

            for(int i = 0; i < Story.import.Length; i++)
            {
                if(Story.import[i])
                {
                    switch(i)
                    {
                        case 0:
                            import += "205호에 접근함. ";
                            break;
                        case 1:
                            import += "할머니를 만남. ";
                            break;
                        case 2:
                            import += "3층의 경비원좀비를 만남. ";
                            break;
                        case 3:
                            import += "방망이를 얻음";
                            break;
                        case 4:
                            import += "205호에 들어가려다 맒";
                            break;
                        case 5:
                            import += "좀비와 조우하고 곧바로 여자에게 돌아감";
                            break;
                        case 6:
                            import += "1층의 문이 잠긴 것을 확인함";
                            break;
                        case 7:
                            import += "1층 열쇠를 얻음";
                            break;
                        default:
                            break;
                    }
                }
            }

            for (int i = 0; i < Story.have.Length; i++)
            {
                if (Story.have[i])
                {
                    switch (i)
                    {
                        case 0:
                            have += "칼. ";
                            break;
                        case 1:
                            have += "방망이. ";
                            break;
                        case 2:
                            have += "열쇠. ";
                            break;
                        default:
                            break;
                    }
                }
            }

            Debug.Log("확인하기 : 현재 챕터 = " + Story.chapter + "\n현재 글 = " + Story.line + "\n현재 음식 양 = " + Story.food + "\n현재 체력게이지 = " + Story.life + "\n몸상태 = "
                + health + "\n정신상태 = " + mental + "(" + Story.psycho + ")" + "\n중요사항들 = " + import + "\n물품들 = " + have);
        }
    }
}
