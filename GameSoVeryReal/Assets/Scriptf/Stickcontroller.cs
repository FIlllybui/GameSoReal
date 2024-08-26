using TMPro;
using UnityEngine;
using UnityEngine.UI; // Import สำหรับการใช้ UI

public class StickController : MonoBehaviour
{
    public Transform cue;
    public Ball ball;
    public Camera mainCamera;
    public  Slider powerSlider; // Slider สำหรับปรับค่าแรง
    public TextMeshProUGUI powerText;     // Text สำหรับแสดงค่าความแรง (ไม่บังคับ)
    public float followDistance ; // ระยะห่างระหว่างไม้คิวกับลูกบอล
    public LineRenderer aimLineRenderer;   // LineRenderer สำหรับเส้นทิศทาง
    public float maxAimDistance = 20f;      // ระยะสูงสุดของเส้นทิศทาง
    public float ballStopThreshold = 0.5f; // ค่าความเร็วที่ใช้ตรวจสอบว่าลูกบอลหยุดหรือไม่
    
    private Vector3 hitDirection;
    private bool isBallMoving = false;
    private void Update()
    {
        
        if (ball != null)
        {
            FollowBall();
            AimCue();
            DrawAimLine(); // วาดเส้นทิศทาง
            
            if (Input.GetMouseButtonDown(0))
            {
                HitBall();
            }

            // อัปเดตค่าความแรงที่แสดงใน UI Text (ถ้ามี)
            if (powerText != null)
            {
                powerText.text = "Power: " + powerSlider.value.ToString("0.0");
            }
            // ตรวจสอบการหยุดของลูกบอล
           // if ( ball.GetComponent<Rigidbody>().velocity.magnitude < ballStopThreshold)
        //   {
           //     isBallMoving = false;
           // }

         //   if (isBallMoving == true)
          //  {
            //    ShowCue();
           // }
           //Debug.Log(ball.GetComponent<Rigidbody>().velocity.magnitude);
            //Debug.Log(isBallMoving);
        }
    }

    private void FollowBall()
    {
        Vector3 direction = (cue.position - ball.transform.position).normalized;
        cue.position = ball.transform.position - direction * followDistance;
    }

    private void AimCue()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hitDirection = (ball.transform.position - hit.point).normalized;
            cue.position = ball.transform.position - hitDirection * followDistance;
            cue.LookAt(ball.transform);
        }
    }
    private void DrawAimLine()
    {
        // กำหนดจุดเริ่มต้นของเส้น (ตำแหน่งลูกบอล)
        aimLineRenderer.SetPosition(0, ball.transform.position);

        // กำหนดจุดสิ้นสุดของเส้น (ทิศทางการเล็ง) โดยจำกัดระยะสูงสุด
        Vector3 aimEndPosition = ball.transform.position + hitDirection * 20;
        aimLineRenderer.SetPosition(1, aimEndPosition);

        // เปิดใช้งาน LineRenderer
        aimLineRenderer.enabled = true;
    }
    private void HitBall()
    {
        // ตีลูกบอลด้วยค่าแรงจาก Slider
        ball.Hit(hitDirection * powerSlider.value);
        aimLineRenderer.enabled = false;
        //if (Input.GetMouseButtonDown(0) && !isBallMoving)//ทำไมกด2ครั้งเเล้วหาย
        //{
            //HideCue();
          
          //  Debug.Log("Hit");
       // }
    }
    private void HideCue()
    {
        cue.gameObject.SetActive(false); // ซ่อนไม้คิว
    }

    private void ShowCue()
    {
        cue.gameObject.SetActive(true); // แสดงไม้คิว
        aimLineRenderer.enabled = true; // เปิดใช้งานเส้นทิศทางอีกครั้ง
    }
}