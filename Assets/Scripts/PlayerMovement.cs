using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public bool notDead = true;

    bool place = true;
    float timeKeep;

    

    public float speed = 10f;
    //public float timeHeld;

    Camera mainCamera;

    float rotation;
    float delta;

    int c = 0;

    public bool canAdAgain = true;

   int max = 40;
   int aroundMax = 320;

    public GameObject deathSound;
    bool soundTracker = true;
    // Update is called once per frame
    void FixedUpdate()
    {

        Movement();
        CheckIfFall();
        MoveLeftAndRight();

        c = (int) gameObject.transform.position.z;
        UpdateMovement();

        if (Time.time > timeKeep)
        {
            place = true;
        }

        if (!notDead && soundTracker)
        {
            soundTracker = false; 
            GameObject deathWav = Instantiate(deathSound, gameObject.transform.position, Quaternion.identity);
            Destroy(deathWav, 2f);
        }

        //.Log(delta);

    }

    private void Start()
    {
        canAdAgain = true;
        mainCamera = Camera.main;

        this.transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            notDead = false;
            PlayerPrefs.Save();
        }
    }

    void Movement()
    {
        try
        {
            if (notDead)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);





                float ADMovement = Input.GetAxis("Horizontal") * speed;
                //ADMovement *= Time.deltaTime;
                ADMovement *= -1;

                transform.Rotate(0, 0, ADMovement / 3);

            }
        }
        catch
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

    }

   
    void CheckIfFall()
    {



        if (rotation > max && rotation < aroundMax && notDead && place)
        {
            notDead = false;
            //.Log("help ive fallen");
            PlayerPrefs.Save();

        }



    }


    void MoveLeftAndRight()
    {

        //Calculate disance from touch
        if (Input.touchCount > 0 && notDead)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 cos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            float vector = (gameObject.transform.position.x - cos.x) / 4;
            this.transform.Rotate(0, 0, vector);
        }
        else if (!notDead)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        

    
       

        

        Vector3 currentRotation = transform.rotation.eulerAngles;
        rotation = currentRotation.z;
        /*
        if (timeHeld != 0)
        {
            if (rotation > max)
            {
                float interimRot = Mathf.Abs(rotation - 360);
                delta = timeHeld * Time.deltaTime * interimRot;
            }
            else
            {
                delta = timeHeld * Time.deltaTime * rotation;
            }

        }
        else
        {*/
            

            if (rotation > max)
            {
                float interimRot = Mathf.Abs(rotation - 360);
                delta = Time.deltaTime * interimRot * 1;
            }
            else
            {
                delta = Time.deltaTime * rotation * 1;
            }
        //}
        
        if (rotation > 355f || rotation < 5f)
        {
            
        }
        else if(rotation > 200f)
        {
            float interimRot = Mathf.Abs(rotation - 360);
            transform.Rotate(new Vector3(0, 0, -1), interimRot / 50);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1), rotation / 50);
        }
        
        if (notDead && currentRotation.z < aroundMax)
        {
            Vector3 curPos = transform.position;


            transform.position = Vector3.MoveTowards(curPos, new Vector3(curPos.x - delta, curPos.y, curPos.z), delta/3);

        }
        if (notDead && currentRotation.z > max)
        {
            Vector3 curPos = transform.position;


            transform.position = Vector3.MoveTowards(curPos, new Vector3(curPos.x + delta, curPos.y, curPos.z), delta/3);

        }




    }

    public void RunAgain()
    {
        if (place)
        {
            timeKeep = Time.time + 5f;
            place = false;
        }
        
        ////.Log("I ran Again"); 
        
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, 0f, transform.position.z + 3);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + 3);
        notDead = true;
        transform.rotation = Quaternion.identity;
        canAdAgain = false;




        
    }

    void UpdateMovement()
    {
        if (speed < 80)
        {
            speed = Mathf.Sqrt(Time.timeSinceLevelLoad) + 10;
        }
        else
        {
            speed = 80;
        }

    }



}

