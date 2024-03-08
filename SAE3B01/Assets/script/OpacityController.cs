using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    public float opacity = 0.5f; 
    public float opacityToReach = 0.1f;
    public Image image;
    public Color color;

    void Start()
    {
        Color color = image.color;
    }

    void FixedUpdate(){
        while (true){
            if (opacityToReach == color.a)
            {
                if (opacityToReach == 0.1f)
                {
                    opacityToReach = 1f;
                }else opacityToReach = 0.1f;
            }
            if (opacityToReach < color.a)
            {
                color.a -= 0.1f;
            }else color.a += 0.1f;
        }
    }
}