using BackEndRefactored;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using BackEndRefactored;

public class CountryObject : MonoBehaviour
{
    [SerializeField]
    public Country country;

    private GameObject _textObject;
    private TextMeshProUGUI _tmp;
    private Image _image;
    private Utils _utils = new Utils();

    void Start()
    {
        MakeButtonClickAble();
        _textObject = transform.GetChild(0).GameObject();
        _tmp = _textObject.GetComponent<TextMeshProUGUI>();
        _image = this.GetComponent<Image>();
        
        country = _utils.GetCountry[gameObject.name];
        Debug.Log(country.GetName());
    }
    
    void Update()
    {
        _tmp.text = country.Troops.ToString();
       _image.color = country.GetPlayer().playerColor;
       ShowTroops(_tmp);
       ShowPlayerColor(_image);
    }
    
    void MakeButtonClickAble() => this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    

    void ShowTroops(TextMeshProUGUI tmp)
    {
        tmp.text = country.Troops.ToString();
        
    }

    void ShowPlayerColor(Image img)
    {
        img.color = country.GetPlayer().playerColor;
    }
}
