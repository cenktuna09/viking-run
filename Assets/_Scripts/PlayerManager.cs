using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ElephantSDK;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{

    private PathCreation.Examples.PathFollower pathFollower;

    public Animator animator;

    public int touristCounter;
    public int collectedTouristNO;
    public TextMeshProUGUI nameTag;
    public float totalAmount = 50f;
    public Image filledBar;
    public GameObject barCanvas;

    [SerializeField] GameObject itemCollectRef;
    [Header("Movement Settings")]
    public float xSpeed;
    private Touch touch;

    [Header("Conditions")]
    public bool isFinished = false;

    [Header("Particles")]
    public GameObject[] changeParticle;
    public GameObject rainParticle;
    public GameObject confParticle;
    private bool canRotate;
    [Header("Chars")]
    public GameObject activeModel;
    public GameObject villager3D;
    public GameObject viking3D;
    public GameObject ragnar3D;
    public GameObject thor3D;
    public GameObject thorParticle;
    public GameObject odin3D;
    public GameObject odinParticle;
    private bool isClick;
    private float mouseX;
    private Vector3 move;


    void Start()
    {
        pathFollower = transform.parent.GetComponent<PathCreation.Examples.PathFollower>();
        animator = transform.GetChild(1).GetComponent<Animator>();
        activeModel = villager3D.gameObject;
        canRotate = true;
    }
    void Update()
    {
        Movement();
        MouseInputAndClickCheck();
        MovementAndSpeedControl();
        //Debug.Log(canRotate);       
    }
    public void Movement()
    {
        if (isFinished)
            return;

        if (Input.touchCount > 0)
        {
            
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (CanvasManager.instance.guide.activeInHierarchy)
                {
                    MoveForward();

                    CanvasManager.instance.guide.SetActive(false);

                    Elephant.LevelStarted((LevelManager.GetLevelID() + 1));

                    animator.SetBool("Walk", true);
                }

                var pos = transform.localPosition;

                transform.localPosition = new Vector3(Mathf.Clamp(pos.x + touch.deltaPosition.x * xSpeed * Time.deltaTime, -1, 1), 0, pos.z);

            }

            if (touch.phase==TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if(canRotate)
                {
                    activeModel.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear);
                    activeModel.transform.DOLocalMoveX(0, 0.1f).SetEase(Ease.Linear);
                }

            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collect"))
        {
            other.GetComponent<Collectable>().Collect();

            FillBar(other.GetComponent<Collectable>().touristValue);

            MMVibrationManager.Haptic(HapticTypes.LightImpact);

            other.transform.DOScale(0, 0.20f).SetEase(Ease.Linear);
            other.transform.DOMove(itemCollectRef.transform.position, 0.20f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(other.gameObject);

            });
        }

        if (other.CompareTag("BadCollect"))
        {
            other.GetComponent<Obstacle>().Collect();
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            //CanvasManager.instance.CreateCollectTxt(transform.position, Color.red, "-" + 5);
            if(canRotate)
            {
            FillBar(-5);
            }
            
            other.transform.DOScale(0, 0.20f).SetEase(Ease.Linear);
            other.transform.DOMove(itemCollectRef.transform.position, 0.20f).SetEase(Ease.Linear).OnComplete(() =>
            {
                Destroy(other.gameObject);

            });
        }

        if (other.CompareTag("Loki"))
        {
            CanvasManager.instance.CreateCollectTxt(LevelManager.instance.GetActiveLevel().player.transform.position, Color.red, "-25");
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            other.GetComponent<LokiScript>().LokiAttack();
            if (canRotate)
            {
                FillBar(-25);
            }


        }

        if (other.CompareTag("LokiBig"))
        {
            CanvasManager.instance.CreateCollectTxt(LevelManager.instance.GetActiveLevel().player.transform.position, Color.red, "-50");
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            other.GetComponent<LokiScript>().LokiAttack();
            if (canRotate)
            {
                FillBar(-50);
            }


        }

        if (other.CompareTag("Finish"))
        {
            
            if(villager3D.activeInHierarchy)
            {
                FailPanel.instance.OpenPanel();
                activeModel.GetComponent<Animator>().SetBool("Finish", true);
                isFinished = true;
                pathFollower.enabled = false;
                other.enabled = false;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                isFinished = true;
                
                activeModel.GetComponent<Animator>().SetBool("Finish", true);
                pathFollower.enabled = false;
                other.enabled = false;
                CompletePanel.instance.OpenPanel(LevelManager.GetLevelID());
                MMVibrationManager.Haptic(HapticTypes.LightImpact);
                
            }

            if (viking3D.activeInHierarchy)
            {
                other.transform.GetChild(3).gameObject.SetActive(true);

            }
            if (ragnar3D.activeInHierarchy)
            {

                other.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (thor3D.activeInHierarchy)
            {

                other.transform.GetChild(1).gameObject.SetActive(true);
            }
            if (odin3D.activeInHierarchy)
            {

                other.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void FillBar(int value)
    {
        touristCounter += value;
        collectedTouristNO += value;
        if (touristCounter <= 0)
        {
            touristCounter = 0;

            if (viking3D.activeInHierarchy)
            {
                
                viking3D.SetActive(false);
                viking3D.transform.DORotate((Vector3.zero), 0);
                villager3D.SetActive(true);
                activeModel = villager3D.gameObject;
                var transformParticle = Instantiate(changeParticle[Random.Range(0, 1)], itemCollectRef.transform.position, Quaternion.identity);
                transformParticle.transform.parent = itemCollectRef.transform;
                Destroy(transformParticle, 1f);
                //
                pathFollower.speed = 7f;
                nameTag.SetText("VILLAGER");
                villager3D.GetComponent<Animator>().SetBool("Walk", true);

                touristCounter = (int)totalAmount - 5;
            }

            if (ragnar3D.activeInHierarchy)
            {
                /*
                 
                ragnar3D.SetActive(false);
                ragnar3D.transform.DORotate((Vector3.zero), 0);
                viking3D.SetActive(true);
                */
                StartCoroutine(ChangeActiveModel(viking3D,ragnar3D));
                var transformParticle = Instantiate(changeParticle[Random.Range(0, 1)], itemCollectRef.transform.position, Quaternion.identity);
                transformParticle.transform.parent = itemCollectRef.transform;
                Destroy(transformParticle, 1f);
                //
                pathFollower.speed = 7.5f;
                nameTag.SetText("VIKING");

                touristCounter = (int)totalAmount - 5;
            }

            if (thor3D.activeInHierarchy)
            {
                /*
                
                thor3D.SetActive(false);
                thor3D.transform.DORotate((Vector3.zero), 0);
                ragnar3D.SetActive(true);
                 */
                StartCoroutine(ChangeActiveModel(ragnar3D,thor3D));
                //
                nameTag.SetText("RAGNAR");
                pathFollower.speed = 8f;
                touristCounter = (int)totalAmount - 5;
            }

            if (odin3D.activeInHierarchy)
            {
                /*
                 
                odin3D.SetActive(false);
                odin3D.transform.DORotate((Vector3.zero), 0);
                thor3D.SetActive(true);
                */
                StartCoroutine(ChangeActiveModel(thor3D,odin3D));
                //
                transform.GetChild(0).GetComponent<canvasMover>().GoDown();
                nameTag.SetText("THOR");
                nameTag.SetText("THOR");
                pathFollower.speed = 9f;
                touristCounter = (int)totalAmount - 5;
            }
        }

        if (touristCounter >= totalAmount && villager3D.activeInHierarchy)
        {
           // villager3D.SetActive(false);
           // villager3D.transform.DORotate((Vector3.zero), 0);
           // viking3D.SetActive(true);
            StartCoroutine(ChangeActiveModel(viking3D,villager3D));
            var transformParticle = Instantiate(changeParticle[0], itemCollectRef.transform.position,Quaternion.identity);
            transformParticle.transform.parent = itemCollectRef.transform;
            Destroy(transformParticle, 1f);
            pathFollower.speed = 7f;
            //
            nameTag.SetText("VIKING");
            touristCounter = 0;
        }
        if (touristCounter >= totalAmount && viking3D.activeInHierarchy)
        {
            //viking3D.SetActive(false);
            //viking3D.transform.DORotate((Vector3.zero), 0);
            //ragnar3D.SetActive(true);
            StartCoroutine(ChangeActiveModel(ragnar3D,viking3D));
            var transformParticle = Instantiate(changeParticle[Random.Range(0, 1)], itemCollectRef.transform.position, Quaternion.identity);
            transformParticle.transform.parent = itemCollectRef.transform;
            Destroy(transformParticle, 1f);
            //
            pathFollower.speed = 7.5f;
            nameTag.SetText("RAGNAR");
            touristCounter = 0;
        }

        if (touristCounter >= totalAmount && ragnar3D.activeInHierarchy)
        {
            //ragnar3D.SetActive(false);
            //ragnar3D.transform.DORotate((Vector3.zero), 0);
            //thor3D.SetActive(true);
            StartCoroutine(ChangeActiveModel(thor3D,ragnar3D));
            var transformParticle = Instantiate(thorParticle, itemCollectRef.transform.position, Quaternion.identity);
            transformParticle.transform.parent = itemCollectRef.transform;
            Destroy(transformParticle, 1f);
            //
            
            nameTag.SetText("THOR");
            pathFollower.speed = 8f;
            touristCounter = 0;
        }

        if (touristCounter >= totalAmount && thor3D.activeInHierarchy)
        {
            //thor3D.SetActive(false);
            //thor3D.transform.DORotate((Vector3.zero), 0);
            //odin3D.SetActive(true);
            StartCoroutine(ChangeActiveModel(odin3D,thor3D));
            var transformParticle = Instantiate(odinParticle, itemCollectRef.transform.position, Quaternion.identity);
            transformParticle.transform.parent = itemCollectRef.transform;
            Destroy(transformParticle, 1f);
            //
            transform.GetChild(0).GetComponent<canvasMover>().GoUp();
            nameTag.SetText("THOR");
            pathFollower.speed = 9f;
            nameTag.SetText("ODIN");
            touristCounter = 0;
        }

        filledBar.fillAmount = touristCounter / totalAmount;
    }
    


    public void MoveForward()
    {
        Camera.main.transform.SetParent(transform.parent);
        //Camera.main.transform.SetAsLastSibling();
        CameraFollow.instance.target = transform;
        CameraFollow.instance.enabled = true;
        pathFollower.enabled = true;
    }

    IEnumerator ChangeActiveModel(GameObject currentModel,GameObject lastModel)
    {
        lastModel.gameObject.SetActive(false);
        canRotate = false;
        activeModel = currentModel;
        activeModel.SetActive(true);
        lastModel.transform.DOLocalRotate(Vector3.zero, 0.1f);
        lastModel.transform.DOLocalMove(Vector3.zero, 0.1f);
        Debug.Log(lastModel + "CHANGED and ROTATION IS " + lastModel.transform.rotation);
        yield return new WaitForSeconds(1f);
        canRotate = true;
        activeModel.transform.DOLocalMove(new Vector3(0, 0, 0), 0.1f).SetEase(Ease.Linear);
        animator = currentModel.GetComponent<Animator>();
        animator.SetBool("Walk", true);
        //animator.SetTrigger("Spin");
    }

    private void MovementAndSpeedControl()
    {

        if (isClick) // If Player Clicking Set Position X
        {

            if (move.x != 0)
            {
                if(canRotate)
                {
                    activeModel.transform.localRotation = Quaternion.Slerp(activeModel.transform.localRotation, Quaternion.LookRotation(new Vector3(move.x / 2, 0, pathFollower.speed)), 5 * Time.deltaTime);

                }
            }
            else
            {
                if(canRotate)
                {
                    activeModel.transform.localRotation = Quaternion.Slerp(activeModel.transform.localRotation, Quaternion.LookRotation(new Vector3(0, 0, pathFollower.speed)), 5 * Time.deltaTime);

                }
            }
        }


        else
        {
            if(canRotate)
            {
                activeModel.transform.localRotation = Quaternion.Slerp(activeModel.transform.localRotation, Quaternion.LookRotation(new Vector3(0, 0, pathFollower.speed)), 5 * Time.deltaTime);

            }
        }
    }
    private void MouseInputAndClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClick = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isClick = false;
        }

        if (isClick) // If Player Clicking Set Player's Rotation
        {
            mouseX += Input.GetAxis("Mouse X");
            float pointer_x = Input.GetAxis("Mouse X");
            //float pointer_y = Input.GetAxis("Mouse Y");
            if (Input.touchCount > 0)
            {
                pointer_x = Input.touches[0].deltaPosition.x;
                //pointer_y = Input.touches[0].deltaPosition.y;
            }
            move = new Vector3(pointer_x, 0, 0);

        }
        else // If Player Isn't Clicking Set Rotation to 0
        {
            
        }
    }

    private void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -4f, 4f);
        transform.position = clampedPosition;
    }

   
}
