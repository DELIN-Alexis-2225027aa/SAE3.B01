/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    ValluesConvertor valluesConvertor;
    DBManager dbManager;
    [SerializeField] itemArea1 inventorySlot1;
    [SerializeField] itemArea2 inventorySlot2;
    [SerializeField] itemArea3 inventorySlot3;
    [SerializeField] itemArea4 inventorySlot4;
    [SerializeField] itemArea5 inventorySlot5;
    [SerializeField] itemArea6 inventorySlot6;

    // Start is called before the first frame update
    void Start()
    {
        List<List<object>> resultat = dbManager.Select("Proof", "*", "isCollected = T");
        if (resultat.Count > 0)
        {
            foreach (List<object> row in resultat)
            {
                classroomRow = valluesConvertor.convertRowToString(row);
                    GameObject enfant = transform.Find("img").gameObject;
                    Image imageComponent = enfant.GetComponent<image>();

            }
        }
        return classroomRow;
    }

}
*/