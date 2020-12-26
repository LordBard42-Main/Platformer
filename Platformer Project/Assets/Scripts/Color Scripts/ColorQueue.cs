using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ColorQueue : MonoBehaviour
{
    [SerializeField] [HideInInspector] private ColorSlot[] colorSlots;
    public ColorSlot[] ColorSlots { get => colorSlots; set => colorSlots = value; }

    [SerializeField] private Transform colorQueueParent;
    [SerializeField] private GameObject colorSlotPrefab;
    [SerializeField] private bool updateColors;

    // Start is called before the first frame update
    void Start()
    {
        if(colorQueueParent.childCount < colorSlots.Length)
        {
            for(int i = 0; i < colorSlots.Length; i++)
            {
                GameObject colorSlotObject = Instantiate(colorSlotPrefab, new Vector3(0,0), Quaternion.identity);
                colorSlotObject.transform.GetChild(0).GetComponent<Image>().color = ColorDictionaries.primaryColors[colorSlots[i].currentColor];
                colorSlotObject.transform.parent = colorQueueParent;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isEditor)
        {
            if (updateColors)
            {
                Image[] list = colorQueueParent.GetComponentsInChildren<Image>();
                foreach (Image image in list)
                {
                    Debug.Log(image);
                    if (image != null)
                        DestroyImmediate(image.gameObject);
                    Debug.Log("Destroyed!" + ColorSlots.Length);
                }
           

                for (int i = 0; i < colorSlots.Length; i++)
                {
                    GameObject colorSlotObject = Instantiate(colorSlotPrefab, new Vector3(0, 0), Quaternion.identity);
                    colorSlotObject.transform.GetChild(0).GetComponent<Image>().color = ColorDictionaries.primaryColors[colorSlots[i].currentColor];
                    colorSlotObject.transform.parent = colorQueueParent;
                }
            }
            updateColors = false;
        }
    }

    public void UseColor()
    {
        ColorSlot[] tempSlots = new ColorSlot[colorSlots.Length - 1];

        for(int i = 0; i < tempSlots.Length; i++)
        {
            tempSlots[i] = colorSlots[i + 1];
        }

        colorSlots = tempSlots;

        Destroy(colorQueueParent.GetChild(0).gameObject);

    }

}

