using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelixController : MonoBehaviour
{
    


    //know last screen tap
    private Vector2 lastTapPosition;
    private Vector3 startRotation;

    //stage related variables
    public Transform topTransform;  //top platform
    public Transform goalTransform; //bottom platform
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    //for the helix distance (diff between the top and goal transform
    private float helixDistance;
    //know how many levels are created
    private List<GameObject> spawnedLevels = new List<GameObject>();


    // Add a reference to the background image ui
    public Image backgroundImage;

    //add deathplay sfx
    public AudioClip witchScreamClip;  // Assign this in the Unity Inspector


    // Start is called before the first frame update
    void Awake()
    {
        
        startRotation = transform.localEulerAngles;
        //calculate helix distance
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);

        //test code
        LoadStage(0);
    }

    // Update is called once per frame
    void Update()
    {
        //for touchscreen
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;

            if(lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float delta = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }



    //Stage Code
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if(stage == null)
        {
            Debug.LogError("No stage" + stageNumber + " found in allStages List. Are all stages assigned in the list?");
            return;
        }

        //change background colour of main camera
        //Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;


        // Change background image
        if (stage.stageBackgroundImage != null)
        {
            backgroundImage.sprite = stage.stageBackgroundImage;
        }

        //change color of ball in stage
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

       


        //reset helix rotation
        transform.localEulerAngles = startRotation;

        //destroy old levels if there are any
        foreach(GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        //create new levels / platforms
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPositionY = topTransform.localPosition.y;

        for(int i = 0; i < stage.levels.Count; i++)
        {
            spawnPositionY -= levelDistance;
            //creates level within scene
            GameObject level = Instantiate(helixLevelPrefab, transform);
            //spawn level
            Debug.Log("Level spawned");
            level.transform.localPosition = new Vector3(0, spawnPositionY, 0);
            spawnedLevels.Add(level);

            //Creating the gaps
            int partsToDisbale = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while(disabledParts.Count < partsToDisbale)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;

                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }

            }

            
            List<GameObject> leftParts = new List<GameObject>();

            foreach(Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;

                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            //Creating the death parts
            List<GameObject> deathParts = new List<GameObject>();

           while(deathParts.Count < stage.levels[i].deathPartCount)
           {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathParts.Contains(randomPart))
                {
                    //randomPart.gameObject.AddComponent<DeathPart>();
                    //deathParts.Add(randomPart);


                    // Add the DeathPart script
                    DeathPart deathPart = randomPart.gameObject.AddComponent<DeathPart>();

                    // Add AudioSource and assign the witch scream sound
                    AudioSource audioSource = randomPart.gameObject.AddComponent<AudioSource>();
                    audioSource.clip = witchScreamClip;  // Assign the witch scream clip from HelixController
                    audioSource.playOnAwake = false;     // Ensure it doesn't play immediately

                    // Add the created part to the list of death parts
                    deathParts.Add(randomPart);


                    Debug.Log("Play sound 1");


                }

           }

        }






    }







}
