using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject inputField;
    [SerializeField] Button addButton;
    [SerializeField] Button continueButton;

    [SerializeField] TMP_Dropdown typeVolume;

    [SerializeField] GameObject[] listShape;

    [SerializeField] TMP_InputField rectrangle_length;
    [SerializeField] TMP_InputField rectrangle_width;

    [SerializeField] TMP_InputField triangle_edge1;
    [SerializeField] TMP_InputField triangle_edge2;
    [SerializeField] TMP_InputField triangle_edge3;

    [SerializeField] TMP_InputField trapezoid_base1;
    [SerializeField] TMP_InputField trapezoid_base2;
    [SerializeField] TMP_InputField trapezoid_height;

    [SerializeField] TMP_InputField circle_radius;

    [SerializeField] TMP_InputField sector_radius;
    [SerializeField] TMP_InputField sector_A;

    [SerializeField] TMP_InputField ellipse_a;
    [SerializeField] TMP_InputField ellipse_b;

    [SerializeField] TMP_InputField parallelogram_b;
    [SerializeField] TMP_InputField parallelogram_height;



    [SerializeField] TextMeshProUGUI resultCal;


    [SerializeField] GameObject listFieldParent;

    [SerializeField] Button calButton;

    private List<GameObject> list = new List<GameObject>();
    private List<double> listInt = new List<double>();
    private int amount = 0;
    private double result = 0;

    public string CURRENCY_FORMAT = "#,##0.00";
    public NumberFormatInfo NFI = new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." };

    private int type = 0;

    [SerializeField] Color[] listColor;

    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void Start()
    {
        Clear();
        typeVolume.options.Clear();
        List<string> items = new List<string>();

        items.Add("Rectrangle");
        items.Add("Triangle");
        items.Add("Trapezoid");
        items.Add("Circle");
        items.Add("Sector");
        items.Add("Ellipse");
        items.Add("Parralelogram");

        foreach(var item in items)
        {
            typeVolume.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }

        typeVolume.onValueChanged.AddListener(delegate { DropdownitemSelected(); });
        typeVolume.value = 0;
        type = 0;
        listShape[0].SetActive(true);
    }

    private void DropdownitemSelected()
    {
        switch(typeVolume.options[typeVolume.value].text)
        {
            case "Rectrangle": type = 0;
                break; 
            case "Triangle": type = 1;
                break;
            case "Trapezoid": type = 2;
                break;
            case "Circle": type = 3;
                break;
            case "Sector":
                type = 4;
                break;
            case "Ellipse":
                type = 5;
                break;
            case "Parralelogram":
                type = 6;
                break;
        }

        SwitchToVolume();
    }



    public void OnValueChanged()
    {
        if(CheckValidate())
        {
            calButton.interactable = true;
        }
        else
        {
            calButton.interactable= false;
        }
    }

    private bool CheckValidate()
    {
        if(type == 0)
        {
            if (rectrangle_length.text == "" || rectrangle_width.text == "")
            {
                return false;
            }
        }
        else if (type == 1)
        {
            if (triangle_edge1.text == "" || triangle_edge2.text == "" || triangle_edge3.text == "")
            {
                return false;
            }
        }
        else if (type == 2)
        {
            if (trapezoid_base1.text == "" || trapezoid_base2.text == "" || trapezoid_height.text == "")
            {
                return false;
            }
        }
        else if (type == 3)
        {
            if (circle_radius.text == "")
            {
                return false;
            }
        }
        else if (type == 4)
        {
            if (sector_A.text == "" || sector_radius.text == "")
            {
                return false;
            }
        }
        else if (type == 5)
        {
            if (ellipse_a.text == "" || ellipse_b.text == "")
            {
                return false;
            }
        }
        else if (type == 4)
        {
            if (parallelogram_b.text == "" || parallelogram_height.text == "")
            {
                return false;
            }
        }

        //return text.All(char.IsDigit);
        return true;
    }


    public void Sum()
    {
        CalWithAdult();
        listFieldParent.SetActive(true);
    }

    private void CalWithAdult()
    {
        if(type==0)
        {
            float l = float.Parse(rectrangle_length.text);
            float w = float.Parse(rectrangle_width.text);

            resultCal.text = (l * w).ToString("0.00");
        }
        else if(type == 1)
        {
            float edge1 = float.Parse(triangle_edge1.text);
            float edge2 = float.Parse(triangle_edge2.text);
            float edge3 = float.Parse(triangle_edge3.text);

            var s = (edge1 + edge2 + edge3) / 2;
            var result = Mathf.Sqrt(s*(s-edge1)*(s - edge2)*(s-edge3));

            resultCal.text = result.ToString("0.00");
        }
        else if (type == 2)
        {
            float b1 = float.Parse(trapezoid_base1.text);
            float b2 = float.Parse(trapezoid_base2.text);
            float height = float.Parse(trapezoid_height.text);

            resultCal.text = ((b1 + b2) * height / 2).ToString("0.00");
        }
        else if (type == 3)
        {
            float radius = float.Parse(circle_radius.text);

            resultCal.text = (Mathf.PI * radius * radius).ToString("0.00");
        }
        else if (type == 4)
        {
            float r = float.Parse(sector_radius.text);
            float A = float.Parse(sector_A.text);

            resultCal.text = (A/360 * Mathf.PI * r * r).ToString("0.00");
        }
        else if (type == 5)
        {
            float a = float.Parse(ellipse_a.text);
            float b = float.Parse(ellipse_b.text);

            resultCal.text = (Mathf.PI * a * b).ToString("0.00");
        }
        else if (type == 6)
        {
            float h = float.Parse(parallelogram_height.text);
            float b = float.Parse(parallelogram_b.text);

            resultCal.text = (h * b).ToString("0.00");
        }
    }

    void SwitchToVolume()
    {
        for(int i = 0; i < listShape.Length; i++)
        {
            listShape[i].SetActive(i==type);
        }
    }

    double m2toft2 = 10.7639104;
    double m2toin2 = 1550.0031;


    double M2ToFt2(double m2)
    {
        return m2 * m2toft2;
    }
    
    double M2ToIn2(double m2)
    {
        return m2 * m2toin2;
    }

    public void Continue()
    {
        if(amount==0) return;
        double currentResult = result;
        Clear();
        list[0].GetComponent<TMP_InputField>().text = currentResult.ToString();
        listInt[0] = currentResult;
    }

    public void Clear()
    {
        listFieldParent.SetActive(false);

        typeVolume.value = 0;
        rectrangle_length.text = "";
        rectrangle_width.text = "";
        triangle_edge1.text = "";
        triangle_edge2.text = "";
        triangle_edge3.text = "";
        trapezoid_base1.text = "";
        trapezoid_base2.text = "";
        trapezoid_height.text = "";
        circle_radius.text = "";
        sector_radius.text = "";
        sector_A.text = "";
        ellipse_a.text = "";
        ellipse_b.text = "";
        parallelogram_b.text = "";
        parallelogram_height.text = "";

        calButton.interactable = false;
    }

    public void Quit()
    {
        Clear();
        Application.Quit();
    }
}
