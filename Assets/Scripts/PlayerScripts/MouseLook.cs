using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] 
    private Transform playerRoot, lookRoot;
    
    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 5f;

    [SerializeField]
    private int smooth_Steps = 10;

    [SerializeField]
    private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_Angle = 10f;

    [SerializeField]
    private float roll_Speed = 3f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-10f, 80f);

    private Vector2 look_Angles;
    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;

    private float current_Roll_Angle;
    private int last_Look_Frame;

    void Start()
    {
        // 커서 숨김 & 비활성상태로...
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        LockAndUnlockCursor();
        if(Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
    }

    void LockAndUnlockCursor() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround() {
        // Look Root의 transform rotation을 조정해보면 X를 조작할때 위아래로 움직이고, Y를 조작할 때 좌우로 움직인다.
        current_Mouse_Look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X)
        );
        // invert를 옵션으로 제공?
        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensivity;
        
        // 시야(x와 y 값) 제한
        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);
        
        // 이와 동일한 표현이라고 생각하면 돼!
        // if (look_Angles.x > -70) {
        //     look_Angles.x = -70
        // }
        // if (look_Angles.y > 80) {
        //     look_Angles = 80
        // }


        // 굳이 이것도 없어도 된다니 뭐...
        // lerp https://artiper.tistory.com/110
        // current_Roll_Angle =
        //     Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * roll_Angle,
        //                 Time.deltaTime * roll_Speed);

        // print("Value is " + Input.GetAxis("Horizontal"));
        // print("Value is " + Input.GetAxisRaw("Horizontal"));

        // 대충 뭐 적용하는거 같은데 정확히 알아둘 필요가 있음!
        // lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }
}
