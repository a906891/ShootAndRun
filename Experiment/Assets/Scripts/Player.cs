using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Joystick moveJoystick;
    public Joystick rotateJoystick;

    public GameObject bullet;
    public Transform gunPoint;
    private float bulletSpeed = 1000f;
    private float TimeDelayShoot = .5f;

    float hAxis;
    float vAxis;
    float zAxis;
    bool moved;

    float tempTime;

    void Start()
    {
        moved = false;
    }

    void Update()
    {
        tempTime += Time.deltaTime;

        //rotate
        if (rotateJoystick.Horizontal > 0.2f || rotateJoystick.Vertical > 0.2f || rotateJoystick.Vertical < -0.2 || rotateJoystick.Horizontal < -0.2f)
        {
            moved = true;
            hAxis = rotateJoystick.Horizontal;//x axis
            vAxis = rotateJoystick.Vertical;//y axis
            zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, zAxis - 90, 0);

        }

        // if Leftjoystick moved move player
        if (moveJoystick.Horizontal > 0.2f || moveJoystick.Vertical > 0.2f || moveJoystick.Vertical < -0.2 || moveJoystick.Horizontal <-0.2f)
        {
                rigidbody.velocity = new Vector3(moveJoystick.Horizontal * 5f,
                rigidbody.velocity.y,
                moveJoystick.Vertical * 5f);
        }  
        
      
        /// waiting for time and then shooting
        if (tempTime > TimeDelayShoot && moved == true)
        {
            tempTime = 0;
            Shoot();
        }

    }

    public void Shoot()
    {
        
        print("X"+gunPoint.position.x);
        print("Y"+gunPoint.position.y);
        print("Z"+gunPoint.position.z);
        GameObject bulletClone =  Instantiate(bullet, gunPoint.position, gunPoint.rotation);
        Rigidbody bulletRigidBody = bulletClone.GetComponent<Rigidbody>();
        bulletRigidBody.AddForce( new Vector3(hAxis,0,vAxis) * bulletSpeed * Time.deltaTime , ForceMode.Impulse);
        
    }

}
// transform.rotation = new Vector3(transform.rotation.x,, transform.position.z);

//if(Input.touchCount>0)
//{

//    Touch touch = Input.GetTouch(0);

//    if (touch.phase == TouchPhase.Moved)
//    {
//        print(touch.deltaPosition);

//        direction = Input.touches[0].deltaPosition.normalized;
//        print("direction " + direction);


//        if ((touch.deltaPosition.x < 40 && touch.deltaPosition.x > 4 || touch.deltaPosition.y<40 && touch.deltaPosition.y>4)
//            || (touch.deltaPosition.x > -40 && touch.deltaPosition.x< -4 || touch.deltaPosition.y > -40 && touch.deltaPosition.y <-4))
//        {


//            rigidbody.AddForce(new Vector3(direction.x,0,direction.y), ForceMode.VelocityChange);
//         //  print("Auto Right Switched off");
//           //transform.position = new Vector3(
//           //transform.position.x + direction.x*movefactor,
//           //transform.position.y,
//           //transform.position.z + direction.y*movefactor);
//        }

//    }

//    if(touch.phase == TouchPhase.Stationary)
//    {
//        AutoMoveOn();
//    }


//void AutoMoveOn()
//{

//    rigidbody.AddForce(new Vector3(direction.x, 0, direction.y), ForceMode.VelocityChange);
//    //transform.position = new Vector3(
//    //   transform.position.x + direction.x * movefactor,
//    //   transform.position.y,
//    //   transform.position.z + direction.y * movefactor);
//}



//2nd Method Works but hmmmmmm
//Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//Plane plane = new Plane(Vector3.up, transform.position);
//float distance = 0;
//if (plane.Raycast(ray, out distance))
//{
//    Vector3 pos = ray.GetPoint(distance);
//    print(pos);
//    transform.position = new Vector3(pos.x, transform.position.y, pos.z);
//}

//1st Method
// to convert 
//position of object is in world cordinate and the touch is in pixel then to conver pixel into World cordinate we use ScreenToWorldPoint(Camera helps0)

//NOT WORKING DON'T KNOW WHY GETTING THE TOUCH POINT BUT NOT ABLE TO CONVERT THEM TO THE WORLD POINTS
//Vector3 touchposition = Camera.main.ScreenToWorldPoint(touch.position);
//print(touchposition);
//print(" position " + touch.position);
//print("touch x " + touchposition.x + " touch y" + touchposition.y + "touch z" + touchposition);

//// change the position of the object to the touch position

//transform.position = new Vector3(touchposition.x,transform.position.y,touchposition.z) ; //


