using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidBody;

    // RAYMARCH STUFF
    FractalMaster fractalMasterScript;
    public float minDist;


    // MOVEMENT STUFF
    public float maxForVelConst;
    public float maxForVel;

    public float maxHorVelIncConst;
    public float maxHorVelInc;
    public float maxForVelIncConst;
    public float maxForVelInc;
    public float maxVerVelIncConst;
    public float maxVerVelInc;

    private Vector3 currVelCalc;
    private Vector3 targetVelocity;


    // ROTARY STUFF
    public float maxMouseRateRot;

    public float maxMouseRateRotInc;
    public float maxHorRateRotInc;

    //private Quaternion currRateRotCalc;
    //private Quaternion targetRateRotation;

    private Quaternion prevFrameRotation;
    private Quaternion currFrameRotation;
    private Quaternion deltaRotation;

    private Vector3 currRateRotCalc;
    private Vector3 targetRateRotation;

    //private Vector3 prevFrameRotation;
    //private Vector3 currFrameRotation;
    //private Vector3 deltaRotation;


    // LERP STUFF
    private bool lerping = true;
    private float currentLerpTime;
    public float lerpTime;
    private float percentage;
    private Vector3 startVal;
    private Vector3 endVal;

    // SLERP STUFF
    private float currentSlerpTime;
    public float slerpTime;
    private float percentageSlerp;

    //private Quaternion startValSlerp;
    //private Quaternion endValSlerp;

    private Vector3 startValSlerp;
    private Vector3 endValSlerp;

    private bool haltHeld;

    public bool SpeedChangeWithDist;


    void Start()
    {
        fractalMasterScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FractalMaster>();
    }


    void FixedUpdate()
    {
        if (SpeedChangeWithDist)
        {
            minDist = fractalMasterScript.minDist;
        }

        else
        {
            minDist = 1.0f;
        }

        maxForVel = maxForVelConst * minDist;
        maxHorVelInc = maxHorVelIncConst * minDist;
        maxForVelInc = maxForVelIncConst * minDist;
        maxVerVelInc = maxVerVelIncConst * minDist;


        // ROTATIONS
        currRateRotCalc = transform.InverseTransformDirection(rigidBody.angularVelocity);

        //currFrameRotation = new Vector3(rigidBody.rotation.x, rigidBody.rotation.y, rigidBody.rotation.z);
        currFrameRotation = rigidBody.rotation;


        deltaRotation = currFrameRotation * Quaternion.Inverse(prevFrameRotation);

        //currRateRotCalc += deltaRotation.eulerAngles;
        //currRateRotCalc += -1 * deltaRotation.eulerAngles;

        ////////////////Debug.Log("deltaRotation: " + deltaRotation);


        //currRateRotCalc = rigidBody.rotation.eulerAngles;
        //currRateRotCalc += (deltaRotation.eulerAngles)/Time.deltaTime;


        //currRateRotCalc = rigidBody.angularVelocity;
        ////////////////Debug.Log("AV: " + rigidBody.angularVelocity);

        ////////////////Debug.Log(currRateRotCalc);

        //currRateRotCalc *= Quaternion.Inverse(deltaRotation);
        //currRateRotCalc *= deltaRotation;

        //currRateRotCalc += Quaternion.Angle(currFrameRotation, prevFrameRotation);

        //currRateRotCalc += deltaRotation.eulerAngles;
        //currRateRotCalc += -1 * deltaRotation.eulerAngles;
        //targetRateRotation += deltaRotation.eulerAngles;
        //targetRateRotation += -1 * deltaRotation.eulerAngles;


        // MOUSE X
        if (Mathf.Abs(currRateRotCalc.x - (Input.GetAxis("Mouse Y") * maxMouseRateRotInc)) >= maxMouseRateRot)
        {
            currRateRotCalc += new Vector3((Mathf.Sign(currRateRotCalc.x) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.x))), 0F, 0F);
            targetRateRotation += new Vector3((Mathf.Sign(currRateRotCalc.x) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.x))), 0F, 0F);

            //currRateRotCalc *= Quaternion.Euler(new Vector3((Mathf.Sign(currRateRotCalc.x) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.x))), 0F, 0F));
            //targetRateRotation *= Quaternion.Euler(new Vector3((Mathf.Sign(currRateRotCalc.x) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.x))), 0F, 0F));
        }

        else
        {
            currRateRotCalc += -1 * new Vector3((Input.GetAxis("Mouse Y") * maxMouseRateRotInc), 0F, 0F);
            targetRateRotation += -1 * new Vector3((Input.GetAxis("Mouse Y") * maxMouseRateRotInc), 0F, 0F);

            //currRateRotCalc *= Quaternion.Euler(-1 * new Vector3((Input.GetAxis("Mouse Y") * maxMouseRateRotInc), 0F, 0F));
            //targetRateRotation *= Quaternion.Euler(-1 * new Vector3((Input.GetAxis("Mouse Y") * maxMouseRateRotInc), 0F, 0F));
        }

        if (Mathf.Abs(targetRateRotation.x) > maxMouseRateRot)
        {
            targetRateRotation = new Vector3((Mathf.Sign(targetRateRotation.x) * maxMouseRateRot), targetRateRotation.y, targetRateRotation.z);
            //targetRateRotation = Quaternion.Euler(new Vector3((Mathf.Sign(targetRateRotation.x) * maxMouseRateRot), targetRateRotation.y, targetRateRotation.z));
        }


        // MOUSE Z
        if (Mathf.Abs(currRateRotCalc.z - (Input.GetAxis("Mouse X") * maxMouseRateRotInc)) >= maxMouseRateRot)
        {
            currRateRotCalc += new Vector3(0F, 0F, Mathf.Sign(currRateRotCalc.z) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.z)));
            targetRateRotation += new Vector3(0F, 0F, Mathf.Sign(currRateRotCalc.z) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.z)));

            //currRateRotCalc *= Quaternion.Euler(new Vector3(0F, 0F, Mathf.Sign(currRateRotCalc.z) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.z))));
            //targetRateRotation *= Quaternion.Euler(new Vector3(0F, 0F, Mathf.Sign(currRateRotCalc.z) * (maxMouseRateRot - Mathf.Abs(currRateRotCalc.z))));
        }

        else
        {
            currRateRotCalc += -1 * new Vector3(0F, 0F, (Input.GetAxis("Mouse X") * maxMouseRateRotInc));
            targetRateRotation += -1 * new Vector3(0F, 0F, (Input.GetAxis("Mouse X") * maxMouseRateRotInc));

            //currRateRotCalc *= Quaternion.Euler(-1 * new Vector3(0F, 0F, (Input.GetAxis("Mouse X") * maxMouseRateRotInc)));
            //targetRateRotation *= Quaternion.Euler(-1 * new Vector3(0F, 0F, (Input.GetAxis("Mouse X") * maxMouseRateRotInc)));
        }

        if (Mathf.Abs(targetRateRotation.z) > maxMouseRateRot)
        {
            targetRateRotation = new Vector3(targetRateRotation.x, targetRateRotation.y, (Mathf.Sign(targetRateRotation.z) * maxMouseRateRot));

            //targetRateRotation = Quaternion.Euler(new Vector3(targetRateRotation.x, targetRateRotation.y, (Mathf.Sign(targetRateRotation.z) * maxMouseRateRot)));
        }




        // MOVEMENT
        currVelCalc = transform.InverseTransformDirection(rigidBody.velocity);


        // FORWARD
        if ((currVelCalc.z + (Input.GetAxis("Forward") * maxForVelInc)) >= maxForVel)
        {
            currVelCalc += new Vector3(0F, 0F, (maxForVel - currVelCalc.z));
            targetVelocity += new Vector3(0F, 0F, (maxForVel - targetVelocity.z));
        }

        else if ((currVelCalc.z + (Input.GetAxis("Forward") * maxForVelInc)) <= -1 * maxForVel)
        {
            currVelCalc -= new Vector3(0F, 0F, (currVelCalc.z + maxForVel));
            targetVelocity -= new Vector3(0F, 0F, (targetVelocity.z + maxForVel));
        }

        else
        {
            currVelCalc += new Vector3(0F, 0F, (Input.GetAxis("Forward") * maxForVelInc));
            targetVelocity += new Vector3(0F, 0F, (Input.GetAxis("Forward") * maxForVelInc));
        }

        if (Mathf.Abs(targetVelocity.z) > maxForVel)
        {
            targetVelocity = new Vector3(targetVelocity.x, targetVelocity.y, (Mathf.Sign(targetVelocity.z) * maxForVel));
        }


        // DAMPENER CONTROLLER
        if (Input.GetAxis("LerpButton") > 0)
        {
            lerping = false;
            currentLerpTime = 0F;
        }

        else
        {
            lerping = true;
        }

        // HALT BUTTON
        if (Input.GetAxis("HaltButton") != 0)
        {
            if (haltHeld == false)
            {
                currentLerpTime = 0F;
                targetVelocity = new Vector3(0F, 0F, 0F);

                haltHeld = true;
            }
        }

        else
        {
            haltHeld = false;
        }


        // LERPING
        if (lerping == true)
        {
            if ((currentLerpTime + Time.deltaTime) >= lerpTime)
            {
                currentLerpTime = 0F;
            }

            startVal = currVelCalc;
            endVal = new Vector3((Input.GetAxis("Horizontal") * maxHorVelInc), (Input.GetAxis("Vertical") * maxVerVelInc), targetVelocity.z);

            percentage = currentLerpTime / lerpTime;
            //percentage = Mathf.Sin(percentage * Mathf.PI * 0.5F);
            //percentage = percentage * percentage * (3f - (2f * percentage));
            //percentage = percentage * percentage * percentage * ((percentage * ((6f * percentage)) - 15f) + 10f);

            currVelCalc += (Vector3.Lerp(startVal, endVal, percentage) - currVelCalc);

            currentLerpTime += Time.deltaTime;
        }

        // SLERPING
        if ((currentSlerpTime + Time.deltaTime) >= slerpTime)
        {
            currentSlerpTime = 0F;
        }

        startValSlerp = currRateRotCalc;
        endValSlerp = new Vector3(targetRateRotation.x, (Input.GetAxis("HorizontalRotate") * maxHorRateRotInc), targetRateRotation.z);

        percentageSlerp = currentSlerpTime / slerpTime;

        currRateRotCalc += (Vector3.Lerp(startValSlerp, endValSlerp, percentageSlerp) - currRateRotCalc);
        //currRateRotCalc *= (Quaternion.Slerp(startValSlerp, endValSlerp, percentageSlerp) * Quaternion.Inverse(currRateRotCalc));
        //currRateRotCalc = (Quaternion.Slerp(startValSlerp, endValSlerp, percentageSlerp));

        currentSlerpTime += Time.deltaTime;

        //rigidBody.rotation *= Quaternion.Euler(currRateRotCalc);


        rigidBody.angularVelocity = transform.TransformDirection(currRateRotCalc);



        //transform.rotation *= Quaternion.Euler(currRateRotCalc);

        currVelCalc = transform.TransformDirection(currVelCalc);
        rigidBody.velocity = currVelCalc;

        ////////////Debug.Log(currRateRotCalc);
        ////////////Debug.Log(targetRateRotation);
        ////////////Debug.Log(currentSlerpTime);

        //Debug.Log(rigidBody.velocity);
        //Debug.Log(targetVelocity);
        //Debug.Log(currentLerpTime);


        prevFrameRotation = rigidBody.rotation;
        //prevFrameRotation = new Vector3(rigidBody.rotation.x, rigidBody.rotation.y, rigidBody.rotation.z);
    }


    void OnCollisionEnter(Collision collision)
    {
        //isCollision = true;
        //Debug.Log("Collision Entered");
        //rigidBody.velocity = new Vector3(0F, 0F, 0F);

        currentLerpTime = 0F;
        currentSlerpTime = 0F;

        //lerping = true;
    }

    void OnCollisionStay(Collision collision)
    {
        //isCollision = false;
    }

    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Collision Exited");
        //isCollision = false;
    }



    //void OnTriggerEnter(Collider other)
    //{
    //    //isCollision = true;
    //    rigidBody.velocity = transform.TransformVector(0F, 0F, 0F);
    //}

    //void OnTriggerExit(Collider other)
    //{
    //   //isCollision = false;
    //}
}


